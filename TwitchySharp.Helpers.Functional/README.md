# TwitchySharp.Helpers.Functional

A minimal functional pipeline library for C# .NET 8.0. Build composable async pipelines from three primitives: **Steps** (transforms), **Layers** (middleware), and **Effects** (side effects).

## Core Types

```csharp
// A transform: takes TIn, produces TOut (async)
delegate ValueTask<TOut> Step<TIn, TOut>(TIn @in);

// A same-type refinement: takes T, returns T (async)
delegate ValueTask<T> Step<T>(T @in);

// Middleware: wraps a step, returning a decorated step
delegate Step<TIn, TOut> Layer<TIn, TOut>(Step<TIn, TOut> next);
delegate Step<T> Layer<T>(Step<T> next);

// A side effect: takes TIn, returns nothing (async)
delegate ValueTask Effect<TIn>(TIn @in);
```

Everything is a delegate. No interfaces, no base classes. Lambdas work everywhere.

---

## Creating Steps

### From async lambdas (native)

```csharp
Step<string, int> parseLength = async input =>
{
    await Task.Delay(1); // simulate async work
    return input.Length;
};
```

### From sync lambdas (wrap with AsValueTask)

```csharp
Step<string, int> parseLength = input => input.Length.AsValueTask();
Step<string, string> trim = input => input.Trim().AsValueTask();
```

### From existing functions (lift with AsStep)

```csharp
Func<string, string> toUpper = s => s.ToUpper();
Step<string, string> upperStep = toUpper.AsStep();

// Same-type variant
Func<int, int> doubleIt = x => x * 2;
Step<int> doubleStep = FunctionalExtensions.AsStep<int>(doubleIt);
```

### Identity (no-op passthrough)

```csharp
Step<int> noOp = FunctionalExtensions.Identity<int>();
// noOp(42) returns 42
```

---

## Simple Step Chains

Chain steps with `.Then()`. Each step's output feeds into the next step's input.

```csharp
Step<string, string> trim    = input => input.Trim().AsValueTask();
Step<string, string> toUpper = input => input.ToUpper().AsValueTask();
Step<string, int>    length  = input => input.Length.AsValueTask();

// Compose a pipeline: string -> trim -> upper -> length -> int
Step<string, int> pipeline = trim.Then(toUpper).Then(length);

// Execute with .With()
int result = await "  hello world  ".With(pipeline);
// result: 11
```

### Mixing Step<T> and Step<TIn, TOut>

`Step<T>` (same-type) chains naturally with `Step<TIn, TOut>`:

```csharp
Step<string> normalize = input => input.Trim().ToLower().AsValueTask();
Step<string, int> count = input => input.Split(' ').Length.AsValueTask();

// Step<string> -> Step<string, int> = Step<string, int>
Step<string, int> pipeline = normalize.Then(count);
```

And the reverse:

```csharp
Step<string, int> parse   = input => int.Parse(input).AsValueTask();
Step<int>         doubler = input => (input * 2).AsValueTask();

// Step<string, int> -> Step<int> = Step<string, int>
Step<string, int> pipeline = parse.Then(doubler);
```

---

## Effects (Side Effects / Taps)

An `Effect<T>` runs a side effect without changing the data flow.

```csharp
Effect<string> logInput = input =>
{
    Console.WriteLine($"Received: {input}");
    return ValueTask.CompletedTask;
};

// From a sync Action
Effect<int> logNumber = ((Action<int>)(x => Console.WriteLine(x))).AsEffect();
```

### Tap (effect on output, runs AFTER the step)

```csharp
Step<string, int> pipeline = parse
    .Tap(logNumber);  // logs the parsed int AFTER parse runs
```

### TapInput (effect on input, runs BEFORE the step)

```csharp
Step<string, int> pipeline = parse
    .TapInput(logInput);  // logs the raw string BEFORE parse runs
```

### Combining taps in a chain

```csharp
Step<string, int> pipeline = trim
    .TapInput(logInput)       // log raw input
    .Then(toUpper)
    .Then(length)
    .Tap(logNumber);          // log final result
```

### Ignore (discard output, convert Step to Effect)

```csharp
Step<string, int> parse = input => int.Parse(input).AsValueTask();
Effect<string> fireAndForget = parse.Ignore();  // runs parse, discards the int
```

---

## Layers (Middleware)

A `Layer` wraps a step, adding behavior before and/or after it executes. The layer receives a `next` step and returns a new step.

### Defining a layer

```csharp
Layer<string, int> timing = next => async input =>
{
    Stopwatch sw = Stopwatch.StartNew();
    int result = await next(input);
    sw.Stop();
    Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    return result;
};
```

### Applying a layer to a step

```csharp
Step<string, int> pipeline = trim
    .Then(toUpper)
    .Then(length)
    .Then(timing);  // wraps the entire chain in timing middleware

int result = await "  hello  ".With(pipeline);
// prints: Elapsed: 0ms
// result: 5
```

### Same-type layers

```csharp
Layer<string> ensureNotEmpty = next => async input =>
{
    string result = await next(input);
    return string.IsNullOrEmpty(result) ? "(empty)" : result;
};

Step<string> safeTrim = trim.Then(ensureNotEmpty);
```

### Composing layers together

Layers compose with `.Then()` to build reusable middleware stacks:

```csharp
Layer<string, int> logging = next => async input =>
{
    Console.WriteLine($"Input: {input}");
    int result = await next(input);
    Console.WriteLine($"Output: {result}");
    return result;
};

Layer<string, int> timing = next => async input =>
{
    Stopwatch sw = Stopwatch.StartNew();
    int result = await next(input);
    sw.Stop();
    Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds}ms");
    return result;
};

// Compose layers into a reusable middleware stack
Layer<string, int> observability = logging.Then(timing);

// Apply the composed layer to any step
Step<string, int> pipeline = length.Then(observability);
```

Layer composition order: `a.Then(b)` means `a` wraps first (innermost), `b` wraps second (outermost). When the pipeline executes, `b`'s before-logic runs first, then `a`'s, then the core step, then `a`'s after-logic, then `b`'s.

---

## Hybrid Chains (Steps + Layers + Effects)

The real power is blending all three seamlessly:

```csharp
Step<string, string> trim    = input => input.Trim().AsValueTask();
Step<string, string> toUpper = input => input.ToUpper().AsValueTask();
Step<string, int>    length  = input => input.Length.AsValueTask();

Effect<string> logRaw    = input => { Console.WriteLine($"Raw: {input}"); return ValueTask.CompletedTask; };
Effect<int>    logResult = input => { Console.WriteLine($"Result: {input}"); return ValueTask.CompletedTask; };

Layer<string, int> retry = next => async input =>
{
    try { return await next(input); }
    catch { return await next(input); }  // retry once
};

// Build a full pipeline: tap input, transform, wrap in middleware, tap output
Step<string, int> pipeline = trim
    .TapInput(logRaw)         // side effect: log raw input
    .Then(toUpper)            // step: transform
    .Then(length)             // step: transform
    .Then(retry)              // layer: wrap in retry middleware
    .Tap(logResult);          // side effect: log final output

int result = await "  hello  ".With(pipeline);
// prints: Raw:   hello
// prints: Result: 5
// result: 5
```

---

## Including Dependencies and Services

Steps are delegates, so dependencies are captured via closures. This works naturally with dependency injection.

### Direct closure capture

```csharp
HttpClient httpClient = new();
ILogger logger = loggerFactory.CreateLogger("Pipeline");

Step<string, UserProfile> fetchUser = async userId =>
{
    logger.LogInformation("Fetching user {UserId}", userId);
    HttpResponseMessage response = await httpClient.GetAsync($"/api/users/{userId}");
    return await response.Content.ReadFromJsonAsync<UserProfile>();
};
```

### Factory methods for injectable steps

Define steps as static factory methods that accept dependencies:

```csharp
public static class UserSteps
{
    public static Step<string, UserProfile> FetchUser(HttpClient http) =>
        async userId => await http.GetFromJsonAsync<UserProfile>($"/api/users/{userId}");

    public static Step<UserProfile, UserProfile> ValidateAge(int minimumAge) =>
        input => (input.Age >= minimumAge
            ? input
            : throw new ValidationException($"User must be at least {minimumAge}"))
            .AsValueTask();

    public static Layer<string, UserProfile> WithCache(IMemoryCache cache, TimeSpan ttl) =>
        next => async userId =>
        {
            string cacheKey = $"user:{userId}";
            if (cache.TryGetValue(cacheKey, out UserProfile cached))
                return cached;

            UserProfile result = await next(userId);
            cache.Set(cacheKey, result, ttl);
            return result;
        };
}
```

Compose with DI-provided services:

```csharp
public class UserPipelineService(HttpClient http, IMemoryCache cache, ILogger<UserPipelineService> logger)
{
    private readonly Step<string, UserProfile> _pipeline =
        UserSteps.FetchUser(http)
            .Then(UserSteps.ValidateAge(18))
            .Then(UserSteps.WithCache(cache, TimeSpan.FromMinutes(5)))
            .TapInput(((Action<string>)(id => logger.LogInformation("Looking up {Id}", id))).AsEffect());

    public ValueTask<UserProfile> GetUserAsync(string userId) =>
        userId.With(_pipeline);
}
```

### Layer factories for cross-cutting concerns

```csharp
public static class MiddlewareLayers
{
    public static Layer<TIn, TOut> WithLogging<TIn, TOut>(ILogger logger) =>
        next => async input =>
        {
            logger.LogDebug("Input: {@Input}", input);
            TOut result = await next(input);
            logger.LogDebug("Output: {@Output}", result);
            return result;
        };

    public static Layer<TIn, TOut> WithRetry<TIn, TOut>(int maxAttempts) =>
        next => async input =>
        {
            int attempt = 0;
            while (true)
            {
                try { return await next(input); }
                catch when (++attempt < maxAttempts) { await Task.Delay(attempt * 100); }
            }
        };

    public static Layer<TIn, TOut> WithTimeout<TIn, TOut>(TimeSpan timeout) =>
        next => async input =>
        {
            Task<TOut> task = next(input).AsTask();
            Task completed = await Task.WhenAny(task, Task.Delay(timeout));
            return completed == task
                ? await task
                : throw new TimeoutException();
        };
}
```

Stack them into a reusable middleware bundle:

```csharp
Layer<string, UserProfile> reliability =
    MiddlewareLayers.WithLogging<string, UserProfile>(logger)
        .Then(MiddlewareLayers.WithRetry<string, UserProfile>(3))
        .Then(MiddlewareLayers.WithTimeout<string, UserProfile>(TimeSpan.FromSeconds(10)));

Step<string, UserProfile> robustPipeline =
    UserSteps.FetchUser(http).Then(reliability);
```

---

## Writing Custom Extensions

The library is built on a `partial class FunctionalExtensions` pattern. Add new combinators by extending the same class in your own project.

### Adding a new combinator

```csharp
namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalExtensions
{
    // Branch: run two steps on the same input, combine results
    public static Step<TIn, (TA, TB)> Branch<TIn, TA, TB>(
        this Step<TIn, TA> left,
        Step<TIn, TB> right) =>
        async input => (await left(input), await right(input));

    // Conditional: pick a step based on a predicate
    public static Step<T> When<T>(Func<T, bool> predicate, Step<T> step) =>
        async input => predicate(input) ? await step(input) : input;
}
```

### Adding a new operation

```csharp
namespace TwitchySharp.Helpers.Functional;

public static partial class FunctionalOperations
{
    // Validate: throws if predicate fails, passes through if it succeeds
    public static Step<T> Validate<T>(Func<T, bool> predicate, string message) =>
        input => predicate(input)
            ? ValueTask.FromResult(input)
            : throw new ValidationException(message);
}
```

### Design guidelines for extensions

1. **Return `Step`, `Layer`, or `Effect`** -- every combinator should produce one of the three core types so it composes with `.Then()`, `.Tap()`, and `.With()`.
2. **Use `ValueTask<T>`** -- keep the async-first convention. Use `ValueTask.FromResult()` for sync paths.
3. **Capture dependencies via closures** -- don't add constructor parameters or state. A step is a function, not an object.
4. **Prefer expression-bodied members** when the method is a single expression.
5. **Use `Func<>.AsStep()`** to lift sync functions instead of manually wrapping with `AsValueTask()`.

---

## Quick Reference

| Combinator | Signature | What it does |
|---|---|---|
| `a.Then(b)` | Step + Step | Chain transforms: output of `a` feeds into `b` |
| `step.Then(layer)` | Step + Layer | Wrap step in middleware |
| `a.Then(b)` | Layer + Layer | Compose middleware into a stack |
| `step.Tap(effect)` | Step + Effect | Run effect on output, after step |
| `step.TapInput(effect)` | Step + Effect | Run effect on input, before step |
| `input.With(step)` | T + Step | Execute a pipeline with input |
| `func.AsStep()` | Func -> Step | Lift sync function to async step |
| `action.AsEffect()` | Action -> Effect | Lift sync action to async effect |
| `value.AsValueTask()` | T -> ValueTask | Wrap value in completed ValueTask |
| `Identity<T>()` | -> Step | No-op passthrough step |
| `step.Ignore()` | Step -> Effect | Discard output, keep side effect |
| `step.Expand()` | Step&lt;T&gt; -> Step&lt;T,T&gt; | Widen type signature |
| `step.Contract()` | Step&lt;T,T&gt; -> Step&lt;T&gt; | Narrow type signature |
