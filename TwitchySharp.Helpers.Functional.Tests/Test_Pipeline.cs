using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Pipeline
{
    [Fact]
    public async Task Pipeline_StepChain_ProcessesDataThroughMultipleTransformations()
    {
        // Arrange
        Step<string, int> parse = input => int.Parse(input).AsValueTask();
        Step<int, int> doubleIt = input => (input * 2).AsValueTask();
        Step<int, string> format = input => $"Result: {input}".AsValueTask();

        // Act
        Step<string, string> pipeline = parse.Then(doubleIt).Then(format);
        string result = await pipeline("21");

        // Assert
        Assert.Equal("Result: 42", result);
    }

    [Fact]
    public async Task Pipeline_StepWithLayer_AddsMiddlewareBehavior()
    {
        // Arrange
        List<string> log = new();
        Step<int, int> core = input => (input * 2).AsValueTask();
        Layer<int, int> loggingLayer = next => async input =>
        {
            log.Add($"input: {input}");
            int result = await next(input);
            log.Add($"output: {result}");
            return result;
        };

        // Act
        Step<int, int> pipeline = core.Then(loggingLayer);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(10, result);
        Assert.Equal(2, log.Count);
        Assert.Equal("input: 5", log[0]);
        Assert.Equal("output: 10", log[1]);
    }

    [Fact]
    public async Task Pipeline_StepWithEffect_ExecutesSideEffectDuringPipeline()
    {
        // Arrange
        List<int> auditLog = new();
        Step<int, int> addTen = input => (input + 10).AsValueTask();
        Effect<int> audit = input =>
        {
            auditLog.Add(input);
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> pipeline = addTen.TapInput(audit);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(15, result);
        Assert.Single(auditLog);
        Assert.Equal(5, auditLog[0]);
    }

    [Fact]
    public async Task Pipeline_WithInvocation_UsesWithToFeedInput()
    {
        // Arrange
        Step<int, string> toStr = input => input.ToString().AsValueTask();
        Step<string, string> wrap = input => $"[{input}]".AsValueTask();
        Step<int, string> pipeline = toStr.Then(wrap);

        // Act
        string resultA = await 42.With(pipeline);

        // Assert
        Assert.Equal("[42]", resultA);
    }

    [Fact]
    public async Task Pipeline_HomomorphicChainWithLayerAndEffect_AllComposeTogether()
    {
        // Arrange
        List<string> eventLog = new();
        Step<int> addOne = input => (input + 1).AsValueTask();
        Step<int> timesTwo = input => (input * 2).AsValueTask();
        Layer<int> validationLayer = next => async input =>
        {
            eventLog.Add("validating");
            int result = await next(input);
            eventLog.Add("validated");
            return result;
        };
        Effect<int> logEffect = input =>
        {
            eventLog.Add($"processing: {input}");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int> pipeline = addOne
            .Then(timesTwo)
            .Then(validationLayer)
            .TapInput(logEffect);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(12, result); // (5 + 1) * 2
        Assert.Contains("validating", eventLog);
        Assert.Contains("validated", eventLog);
        Assert.Contains("processing: 5", eventLog);
    }

    [Fact]
    public async Task Pipeline_ExpandContractInPipeline_EnablesTypeConversion()
    {
        // Arrange
        Step<int> addOne = input => (input + 1).AsValueTask();
        Step<int, string> toStr = input => input.ToString().AsValueTask();

        // Act -- use Expand to go from Step<int> to Step<int,int>, then chain with Step<int,string>
        Step<int, string> pipeline = addOne.Expand().Then(toStr);
        string result = await pipeline(5);

        // Assert
        Assert.Equal("6", result);
    }

    [Fact]
    public async Task Pipeline_MultipleLayersNested_ApplyOuterToInner()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addPrefix = next => async input => await next(input) + 100;
        Layer<int, int> multiplyWrapper = next => async input => await next(input) * 2;

        // Act
        Step<int, int> pipeline = core.Then(addPrefix).Then(multiplyWrapper);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(210, result); // (5 + 100) * 2
    }

    [Fact]
    public async Task Pipeline_AsEffectInChain_ConvertsSyncActionToAsyncEffect()
    {
        // Arrange
        List<int> captured = new();
        Action<int> syncAction = x => captured.Add(x);
        Step<int> identity = input => input.AsValueTask();

        // Act
        Step<int> pipeline = identity.TapInput(syncAction.AsEffect());
        int result = await pipeline(99);

        // Assert
        Assert.Equal(99, result);
        Assert.Single(captured);
        Assert.Equal(99, captured[0]);
    }

    [Fact]
    public async Task Pipeline_LongChainOfHomomorphicSteps_AllExecuteInOrder()
    {
        // Arrange
        List<int> trace = new();
        Step<int> step1 = input => { trace.Add(1); return (input + 1).AsValueTask(); };
        Step<int> step2 = input => { trace.Add(2); return (input * 2).AsValueTask(); };
        Step<int> step3 = input => { trace.Add(3); return (input - 3).AsValueTask(); };
        Step<int> step4 = input => { trace.Add(4); return (input * input).AsValueTask(); };

        // Act
        Step<int> pipeline = step1.Then(step2).Then(step3).Then(step4);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(81, result); // ((5+1)*2-3)^2 = 9^2 = 81
        Assert.Equal(new List<int> { 1, 2, 3, 4 }, trace);
    }

    [Fact]
    public async Task Pipeline_LayerShortCircuit_StopsExecution()
    {
        // Arrange
        bool coreExecuted = false;
        Step<int, string> core = input =>
        {
            coreExecuted = true;
            return input.ToString().AsValueTask();
        };
        Layer<int, string> guard = next => input =>
        {
            if (input < 0)
                return "negative".AsValueTask();
            return next(input);
        };

        // Act
        Step<int, string> pipeline = core.Then(guard);
        string negativeResult = await pipeline(-1);
        string positiveResult = await pipeline(5);

        // Assert
        Assert.Equal("negative", negativeResult);
        Assert.True(coreExecuted); // core was called for positive input
        Assert.Equal("5", positiveResult);
    }

    [Fact]
    public async Task Pipeline_FullRealWorldScenario_RequestProcessing()
    {
        // Arrange - simulate a request processing pipeline
        List<string> auditTrail = new();

        Step<string, int> parseRequest = input => int.Parse(input).AsValueTask();
        Step<int, int> processBusinessLogic = input => (input * 100).AsValueTask();
        Step<int, string> formatResponse = input => $"OK:{input}".AsValueTask();

        Layer<string, string> timingLayer = next => async input =>
        {
            auditTrail.Add("start");
            string result = await next(input);
            auditTrail.Add("end");
            return result;
        };

        // Act
        Step<string, string> pipeline = parseRequest
            .Then(processBusinessLogic)
            .Then(formatResponse)
            .Then(timingLayer);

        string result = await "42".With(pipeline);

        // Assert
        Assert.Equal("OK:4200", result);
        Assert.Equal(2, auditTrail.Count);
        Assert.Equal("start", auditTrail[0]);
        Assert.Equal("end", auditTrail[1]);
    }
}
