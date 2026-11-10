using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Infrastructure.Functional;

/// <summary>
/// A general base error.
/// </summary>
public record Error()
{
    /// <summary>
    /// Create an error with a message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public Error(string message) : this() => Message = message;
    /// <summary>
    /// The error message.
    /// </summary>
    public string Message { get; init; } = string.Empty;
    /// <summary>
    /// The nested error, if one exists.
    /// </summary>
    public Error? InnerError { get; init; }
};

/// <summary>
/// An error that carries an <see cref="System.Exception"/>.
/// </summary>
/// <param name="Exception">The wrapped <see cref="System.Exception"/>.</param>
public record ExceptionError(Exception Exception) : Error;

/// <summary>
/// The TwitchySharp untyped <i>result</i> monad.
/// </summary>
/// <remarks>
/// <para>
/// Eliminate the result using the <see cref="Match"/> function.
/// Also provides <see cref="Map"/> and <see cref="Bind"/> functions.
/// </para>
/// <para>
/// <i>Result</i> is a fail-fast error propogating context.
/// The first error remains in the context and passes through <see cref="Map"/> and <see cref="Bind"/>.
/// </para>
/// </remarks>
public readonly record struct Result
{
    private readonly Error? _error;
    /// <summary>
    /// Create an error result.
    /// </summary>
    /// <param name="error">The error.</param>
    public Result(Error error) => _error = error;
    /// <summary>
    /// Create a success result.
    /// </summary>
    public Result() => _error = null;
    /// <summary>
    /// Implictly convert an error to <see cref="Result"/>
    /// </summary>
    /// <param name="error">The error.</param>
    public static implicit operator Result(Error error) => new(error);
    /// <summary>
    /// Evaluate a function that returns a <see cref="Result{T}"/>.
    /// </summary>
    /// <remarks>
    /// The function is only evaluated if this <see cref="Result"/> does not contain an <see cref="Error"/>.
    /// </remarks>
    /// <typeparam name="TNextR">The success type of the next function.</typeparam>
    /// <param name="func">The function to evaluate on success.</param>
    /// <returns>
    /// The return value of <paramref name="func"/> or,
    /// if this <see cref="Result"/> contains an <see cref="Error"/>,
    /// a <see cref="Result{T}"/> containing that <see cref="Error"/>.
    /// </returns>
    public Result<TNextR> Bind<TNextR>(Func<Result<TNextR>> func)
        => _error is not null ? _error : func();
    /// <summary>
    /// Evaluate a function that returns a <see cref="Result"/>.
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="Bind{TNextR}(Func{Result{TNextR}})"/>
    /// </remarks>
    /// <param name="func"><inheritdoc cref="Bind{TNextR}(Func{Result{TNextR}})" path="/param[@name = 'func']"/></param>
    /// <returns>
    /// The return value of <paramref name="func"/> or,
    /// if this <see cref="Result"/> contains an <see cref="Error"/>,
    /// a <see cref="Result"/> containing that <see cref="Error"/>.
    /// </returns>
    public Result Bind(Func<Result> func)
        => _error is not null ? _error : func();
    /// <summary>
    /// Evaluate a function returning <typeparamref name="TNextR"/>
    /// if this <see cref="Result"/> does not contain an <see cref="Error"/>.
    /// </summary>
    /// <typeparam name="TNextR">The success type.</typeparam>
    /// <param name="func">A function returning <typeparamref name="TNextR"/>.</param>
    /// <returns>
    /// A new <see cref="Result{T}"/> containing either the return value of <paramref name="func"/> or
    /// the <see cref="Error"/> contained in this <see cref="Result"/>, if it exists.
    /// </returns>
    public Result<TNextR> Map<TNextR>(Func<TNextR> func)
        => _error is not null ? _error : func();
    /// <summary>
    /// Eliminate the <see cref="Result"/> by providing a function that is evaluated
    /// on success and a function that is evaluated on <see cref="Error"/>.
    /// </summary>
    /// <typeparam name="TOut">The return type of both functions.</typeparam>
    /// <param name="onError">The function to be evaluated if the <see cref="Result"/> contains an <see cref="Error"/>.</param>
    /// <param name="onValid">The function to be evaluated if the <see cref="Result"/> does not contain an <see cref="Error"/>.</param>
    /// <returns>The return value of <paramref name="onError"/> or <paramref name="onValid"/>.</returns>
    public TOut Match<TOut>(Func<Error, TOut> onError, Func<TOut> onValid)
        => _error is not null ? onError(_error) : onValid();
}

/// <summary>
/// The TwitchySharp typed <i>result</i> monad.
/// </summary>
/// <remarks>
/// <inheritdoc cref="Result"/>
/// </remarks>
public readonly record struct Result<T>
{
    private readonly Error? _error;
    private readonly T? _valid;
    /// <inheritdoc cref="Result(Error)"/>
    public Result(Error error) => _error = error;
    /// <summary>
    /// Create a success result from its value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    public Result(T value) => _valid = value;
    /// <summary>
    /// Implicit conversion from <see cref="Error"/> to <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="error">The error to use in the <see cref="Result{T}"/>.</param>
    public static implicit operator Result<T>(Error error) => new(error);
    /// <summary>
    /// Implicit conversion from <typeparamref name="T"/> to <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="value">The value to use for the <see cref="Result{T}"/>.</param>
    public static implicit operator Result<T>(T value) => new(value);

    /// <summary>
    /// Evaluate a function that returns a <see cref="Result{TNextR}"/>.
    /// </summary>
    /// <remarks>
    /// The function is only evaluated if this <see cref="Result{T}"/> does not contain an <see cref="Error"/>.
    /// </remarks>
    /// <typeparam name="TNextR">The success type of the next function.</typeparam>
    /// <param name="func">The function to evaluate on success.</param>
    /// <returns>
    /// The return value of <paramref name="func"/> or,
    /// if this <see cref="Result{T}"/> contains an <see cref="Error"/>,
    /// a <see cref="Result{TNextR}"/> containing that <see cref="Error"/>.
    /// </returns>
    public Result<TNextR> Bind<TNextR>(Func<T, Result<TNextR>> func)
        => _error is not null ? _error : func(_valid!);
    /// <summary>
    /// Evaluate a function that returns a <see cref="Result"/>.
    /// </summary>
    /// <remarks>
    /// <inheritdoc cref="Bind{TNextR}(Func{T, Result{TNextR}})"/>
    /// </remarks>
    /// <param name="func"><inheritdoc cref="Bind{TNextR}(Func{T, Result{TNextR}})" path="/param[@name = 'func']"/></param>
    /// <returns>
    /// <inheritdoc cref="Bind{TNextR}(Func{T, Result{TNextR}})"/>
    /// </returns>
    public Result Bind(Func<T, Result> func)
        => _error is not null ? _error : func(_valid!);
    /// <summary>
    /// Evaluate a function returning <typeparamref name="TNextR"/>
    /// if this <see cref="Result{T}"/> does not contain an <see cref="Error"/>.
    /// </summary>
    /// <typeparam name="TNextR"><inheritdoc cref="Result.Map"/></typeparam>
    /// <param name="func"><inheritdoc cref="Result.Map" path="/param[@name = 'func']"/></param>
    /// <returns>
    /// A new <see cref="Result{T}"/> containing either the return value of <paramref name="func"/> or
    /// the <see cref="Error"/> contained in this <see cref="Result{T}"/>, if it exists.
    /// </returns>
    public Result<TNextR> Map<TNextR>(Func<T, TNextR> func)
        => _error is not null ? _error : func(_valid!);
    /// <summary>
    /// Eliminate the <see cref="Result"/> by providing a function that is evaluated
    /// on success and a function that is evaluated on <see cref="Error"/>.
    /// </summary>
    /// <typeparam name="TOut">The return type of both functions.</typeparam>
    /// <param name="onError">The function to be evaluated if the <see cref="Result"/> contains an <see cref="Error"/>.</param>
    /// <param name="onValid">The function to be evaluated if the <see cref="Result"/> does not contain an <see cref="Error"/>.</param>
    /// <returns>The return value of <paramref name="onError"/> or <paramref name="onValid"/>.</returns>
    public TOut Match<TOut>(Func<Error, TOut> onError, Func<T, TOut> onValid)
        => _error is not null ? onError(_error) : onValid(_valid!);
}

/// <summary>
/// Extensions on <see cref="Task"/> and <see cref="ValueTask"/> containing <see cref="Result"/> types.
/// </summary>
public static class AsyncResultExtensions
{
    /// <summary>
    /// Perform a <see cref="Result{T}.Bind{TNextR}(Func{T, Result{TNextR}})"/> operation in a <see cref="ValueTask"/> context.
    /// </summary>
    /// <typeparam name="T">The initial <see cref="Result{T}"/> type.</typeparam>
    /// <typeparam name="TNext">The <see cref="Result{T}"/> return type of the bind function.</typeparam>
    /// <param name="val">The <see cref="ValueTask"/> of <see cref="Result{T}"/> to bind.</param>
    /// <param name="func">The bind function returning a <see cref="ValueTask"/> of <see cref="Result{TNext}"/>.</param>
    /// <returns>A <see cref="ValueTask"/> wrapping the result of the bind operation.</returns>
    public static async ValueTask<Result<TNext>> BindAsync<T, TNext>(this ValueTask<Result<T>> val, Func<T, ValueTask<Result<TNext>>> func)
        => await (await val).Match(
            onError: e => ValueTask.FromResult<Result<TNext>>(e),
            onValid: valid => func(valid)
            );

    /// <summary>
    /// <inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})"/> 
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/param[@name = 'val']"/></param>
    /// <param name="func">The bind function returning a <see cref="Result{TNext}"/>.</param>
    /// <returns><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})"/></returns>
    public static async ValueTask<Result<TNext>> BindAsync<T, TNext>(this ValueTask<Result<T>> val, Func<T, Result<TNext>> func)
        => (await val).Bind(func);

    /// <summary>
    /// Perform a <see cref="Result{T}.Bind{TNextR}(Func{T, Result{TNextR}})"/> operation in a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val">The <see cref="Task"/> of <see cref="Result{T}"/> to bind.</param>
    /// <param name="func">The bind function returning a <see cref="Task"/> of <see cref="Result{TNext}"/>.</param>
    /// <returns>A <see cref="Task"/> wrapping the result of the bind operation.</returns>
    public static async Task<Result<TNext>> BindAsync<T, TNext>(this Task<Result<T>> val, Func<T, Task<Result<TNext>>> func)
        => await (await val).Match(
            onError: e => Task.FromResult<Result<TNext>>(e),
            onValid: valid => func(valid)
            );

    /// <summary>
    /// Perform a <see cref="Result.Bind(Func{Result})"/> operation in a <see cref="ValueTask"/> context.
    /// </summary>
    /// <param name="val">The <see cref="ValueTask"/> of <see cref="Result"/> to bind.</param>
    /// <param name="func">The bind function returning a <see cref="ValueTask"/> of <see cref="Result"/>.</param>
    /// <returns>A <see cref="ValueTask"/> wrapping the result of the bind operation.</returns>
    public static async ValueTask<Result> BindAsync(this ValueTask<Result> val, Func<ValueTask<Result>> func)
        => await (await val).Match(
            onError: e => ValueTask.FromResult<Result>(e),
            onValid: () => func()
            );

    /// <summary>
    /// <inheritdoc cref="BindAsync(ValueTask{Result}, Func{ValueTask{Result}})"/>
    /// </summary>
    /// <typeparam name="TNext"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val"><inheritdoc cref="BindAsync(ValueTask{Result}, Func{ValueTask{Result}})" path="/param[@name = 'val']"/></param>
    /// <param name="func">The bind function returning a <see cref="ValueTask"/> of <see cref="Result{TNext}"/>.</param>
    /// <returns><inheritdoc cref="BindAsync(ValueTask{Result}, Func{ValueTask{Result}})"/></returns>
    public static async ValueTask<Result<TNext>> BindAsync<TNext>(this ValueTask<Result> val, Func<ValueTask<Result<TNext>>> func)
        => await (await val).Match(
            onError: e => ValueTask.FromResult<Result<TNext>>(e),
            onValid: () => func()
            );

    /// <summary>
    /// Perform a <see cref="Result.Bind(Func{Result})"/> operation in a <see cref="Task"/> context.
    /// </summary>
    /// <param name="val">The <see cref="Task"/> of <see cref="Result"/> to bind.</param>
    /// <param name="func">The bind function returning a <see cref="Task"/> of <see cref="Result"/>.</param>
    /// <returns><inheritdoc cref="BindAsync{T, TNext}(Task{Result{T}}, Func{T, Task{Result{TNext}}})"/></returns>
    public static async Task<Result> BindAsync(this Task<Result> val, Func<Task<Result>> func)
        => await (await val).Match(
            onError: e => Task.FromResult<Result>(e),
            onValid: () => func()
            );

    /// <summary>
    /// Perform a <see cref="Result{T}.Map{TNextR}(Func{T, TNextR})"/> operation in a <see cref="ValueTask"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(Task{Result{T}}, Func{T, Task{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext">The return type of the map function.</typeparam>
    /// <param name="val">The <see cref="ValueTask"/> of <see cref="Result{T}"/> to map.</param>
    /// <param name="func">The map function returning a <typeparamref name="TNext"/>.</param>
    /// <returns>
    /// A <see cref="ValueTask"/> of a <see cref="Result{TNext}"/>,
    /// where the <see cref="Result{TNext}"/> contains either the error from <paramref name="val"/> if it exists or,
    /// the return value of <paramref name="func"/>.
    /// </returns>
    public static async ValueTask<Result<TNext>> MapAsync<T, TNext>(this ValueTask<Result<T>> val, Func<T, TNext> func)
        => (await val).Match<Result<TNext>>(
            onError: e => e,
            onValid: valid => func(valid)
            );

    /// <summary>
    /// Perform a map operation in a <see cref="ValueTask"/> context to an untyped <see cref="Result"/>.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <param name="val"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/param[@name = 'val']"/></param>
    /// <param name="action">The action to perform with the success result.</param>
    /// <returns>
    /// A <see cref="ValueTask"/> wrapping an untyped <see cref="Result"/>
    /// containing either the error in <paramref name="val"/> if it exists or the
    /// return of <paramref name="action"/>.
    /// </returns>
    public static async ValueTask<Result> MapAsync<T>(this ValueTask<Result<T>> val, Action<T> action)
        => (await val).Match(
            e => e,
            value =>
            {
                action(value);
                return new Result();
            }
            );

    /// <summary>
    /// <inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/param[@name = 'val']"/></param>
    /// <param name="func">The async map function, returning a <see cref="ValueTask{TNext}"/></param>
    /// <returns><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})"/></returns>
    public static async ValueTask<Result<TNext>> MapAsync<T, TNext>(this ValueTask<Result<T>> val, Func<T, ValueTask<TNext>> func)
        => await (await val).Match<ValueTask<Result<TNext>>>(
            e => ValueTask.FromResult<Result<TNext>>(e),
            async value => await func(value)
            );

    /// <summary>
    /// Perform a <see cref="Result{T}.Map{TNextR}(Func{T, TNextR})"/> operation in a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val">The <see cref="Task"/> of <see cref="Result{T}"/> to map.</param>
    /// <param name="func"><inheritdoc cref="MapAsync{T, TNext}(ValueTask{Result{T}}, Func{T, TNext})" path="/param[@name = 'func']"/></param>
    /// <returns>
    /// A <see cref="Task"/> of a <see cref="Result{TNext}"/>,
    /// where the <see cref="Result{TNext}"/> contains either the error from <paramref name="val"/> if it exists or,
    /// the return value of <paramref name="func"/>.
    /// </returns>
    public static async Task<Result<TNext>> MapAsync<T, TNext>(this Task<Result<T>> val, Func<T, TNext> func)
        => (await val).Match<Result<TNext>>(
            onError: e => e,
            onValid: valid => func(valid)
            );

    /// <summary>
    /// Map an untyped <see cref="Result"/> to a typed <see cref="Result{TNext}"/> in a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="TNext"><inheritdoc cref="MapAsync{T, TNext}(Task{Result{T}}, Func{T, TNext})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val">The <see cref="Task"/> of <see cref="Result"/> to map.</param>
    /// <param name="func"><inheritdoc cref="MapAsync{T, TNext}(Task{Result{T}}, Func{T, TNext})" path="/param[@name = 'func']"/></param>
    /// <returns><inheritdoc cref="MapAsync{T, TNext}(Task{Result{T}}, Func{T, TNext})"/></returns>
    public static async Task<Result<TNext>> MapAsync<TNext>(this Task<Result> val, Func<TNext> func)
        => (await val).Match<Result<TNext>>(
            onError: e => e,
            onValid: () => func()
            );

    /// <inheritdoc cref="MatchAsync{T, TNext}(ValueTask{Result{T}}, Func{Error, TNext}, Func{T, TNext})"/>
    public static async ValueTask<TNext> MatchAsync<T, TNext>(this ValueTask<Result<T>> val, Func<Error, ValueTask<TNext>> onError, Func<T, ValueTask<TNext>> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: valid => onValid(valid)
            );

    /// <summary>
    /// Perform a <see cref="Result{T}.Match"/> operation within a <see cref="ValueTask"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext">The return type of both match functions.</typeparam>
    /// <param name="val">The <see cref="ValueTask"/> of <see cref="Result{T}"/> to match.</param>
    /// <param name="onError">The error handling function.</param>
    /// <param name="onValid">The success handling function.</param>
    /// <returns>A <see cref="ValueTask"/> containing the return value of either the <paramref name="onError"/> or <paramref name="onValid"/> function.</returns>
    public static async ValueTask<TNext> MatchAsync<T, TNext>(this ValueTask<Result<T>> val, Func<Error, TNext> onError, Func<T, TNext> onValid)
        => (await val).Match(onError, onValid);

    /// <summary>
    /// Perform a <see cref="Result{T}.Match"/> operation within a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(Task{Result{T}}, Func{T, Task{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <typeparam name="TNext"><inheritdoc cref="MatchAsync{T, TNext}(ValueTask{Result{T}}, Func{Error, TNext}, Func{T, TNext})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val">The <see cref="Task"/> of <see cref="Result{T}"/> to match.</param>
    /// <param name="onError">The async error handling function.</param>
    /// <param name="onValid">The async success handling function.</param>
    /// <returns>A <see cref="Task{TNext}"/> containing the return value of either the <paramref name="onError"/> or <paramref name="onValid"/> function.</returns>
    public static async Task<TNext> MatchAsync<T, TNext>(this Task<Result<T>> val, Func<Error, Task<TNext>> onError, Func<T, Task<TNext>> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: valid => onValid(valid)
            );

    /// <summary>
    /// Perform a side-effect <see cref="Result{T}.Match"/> operation within a <see cref="ValueTask"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(ValueTask{Result{T}}, Func{T, ValueTask{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <param name="val"><inheritdoc cref="MatchAsync{T, TNext}(ValueTask{Result{T}}, Func{Error, TNext}, Func{T, TNext})" path="/param[@name = 'val']"/></param>
    /// <param name="onError"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onError']"/></param>
    /// <param name="onValid"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onValid']"/></param>
    /// <returns>A <see cref="ValueTask"/> that completes once either the <paramref name="onError"/> or <paramref name="onValid"/> function has completed.</returns>
    public static async ValueTask MatchAsync<T>(this ValueTask<Result<T>> val, Func<Error, ValueTask> onError, Func<T, ValueTask> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: valid => onValid(valid)
            );

    /// <summary>
    /// Perform a side-effect <see cref="Result{T}.Match"/> operation within a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="BindAsync{T, TNext}(Task{Result{T}}, Func{T, Task{Result{TNext}}})" path="/typeparam[@name = 'T']"/></typeparam>
    /// <param name="val"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'val']"/></param>
    /// <param name="onError"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onError']"/></param>
    /// <param name="onValid"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onValid']"/></param>
    /// <returns>A <see cref="Task"/> that completes once either the <paramref name="onError"/> or <paramref name="onValid"/> function has completed.</returns>
    public static async Task MatchAsync(this Task<Result> val, Func<Error, Task> onError, Func<Task> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: () => onValid()
            );

    /// <summary>
    /// Perform a side-effect <see cref="Result.Match"/> operation within a <see cref="Task"/> context.
    /// </summary>
    /// <param name="val"><inheritdoc cref="BindAsync(ValueTask{Result}, Func{ValueTask{Result}})" path="/param[@name = 'val']"/></param>
    /// <param name="onError"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onError']"/></param>
    /// <param name="onValid"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onValid']"/></param>
    /// <returns><inheritdoc cref="MatchAsync{T}(Task{Result{T}}, Func{Error, Task}, Func{T, Task})"/></returns>
    public static async Task MatchAsync<T>(this Task<Result<T>> val, Func<Error, Task> onError, Func<T, Task> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: valid => onValid(valid)
            );

    

    /// <summary>
    /// Perform a <see cref="Result.Match"/> operation within a <see cref="ValueTask"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="MatchAsync{T, TNext}(ValueTask{Result{T}}, Func{Error, TNext}, Func{T, TNext})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val"><inheritdoc cref="BindAsync(ValueTask{Result}, Func{ValueTask{Result}})" path="/param[@name = 'val']"/></param>
    /// <param name="onError"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onError']"/></param>
    /// <param name="onValid"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onValid']"/></param>
    /// <returns><inheritdoc cref="MatchAsync{T, TNext}(ValueTask{Result{T}}, Func{Error, ValueTask{TNext}}, Func{T, ValueTask{TNext}})"/></returns>
    public static async ValueTask<T> MatchAsync<T>(this ValueTask<Result> val, Func<Error, ValueTask<T>> onError, Func<ValueTask<T>> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: () => onValid()
            );

    /// <summary>
    /// Perform a <see cref="Result.Match"/> operation within a <see cref="Task"/> context.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/typeparam[@name = 'TNext']"/></typeparam>
    /// <param name="val"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'val']"/></param>
    /// <param name="onError"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onError']"/></param>
    /// <param name="onValid"><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})" path="/param[@name = 'onValid']"/></param>
    /// <returns><inheritdoc cref="MatchAsync{T, TNext}(Task{Result{T}}, Func{Error, Task{TNext}}, Func{T, Task{TNext}})"/></returns>
    public static async Task<T> MatchAsync<T>(this Task<Result> val, Func<Error, Task<T>> onError, Func<Task<T>> onValid)
        => await (await val).Match(
            onError: e => onError(e),
            onValid: () => onValid()
            );
}
