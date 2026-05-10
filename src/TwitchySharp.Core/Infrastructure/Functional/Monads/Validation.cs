using System;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Infrastructure.Functional;

public readonly record struct Unit;

public record Error()
{
    public Error(string message) : this() => Message = message;
    public string Message { get; init; } = string.Empty;
    public Error? InnerError { get; init; }
};

public readonly record struct Validation
{
    private readonly Error? _error;
    public Validation(Error error) => _error = error;
    public Validation() => _error = null;
    public static implicit operator Validation(Error error) => new(error);
    public Validation<TNextR> Bind<TNextR>(Func<Validation<TNextR>> func)
        => _error is not null ? _error : func();
    public Validation Bind(Func<Validation> func)
        => _error is not null ? _error : func();
    public Validation<TNextR> Map<TNextR>(Func<TNextR> func)
        => _error is not null ? _error : func();
    public TOut Match<TOut>(Func<Error, TOut> onError, Func<TOut> onValid)
        => _error is not null ? onError(_error) : onValid();
}

public readonly record struct Validation<T>
{
    private readonly Error? _error;
    private readonly T? _valid;
    public Validation(Error error) => _error = error;
    public Validation(T right) => _valid = right;
    public static implicit operator Validation<T>(Error error) => new(error);
    public static implicit operator Validation<T>(T right) => new(right);
    public Validation<TNextR> Bind<TNextR>(Func<T, Validation<TNextR>> func)
        => _error is not null ? _error : func(_valid!);
    public Validation Bind(Func<T, Validation> func)
        => _error is not null ? _error : func(_valid!);
    public Validation<TNextR> Map<TNextR>(Func<T, TNextR> func)
        => _error is not null ? _error : func(_valid!);
    public TOut Match<TOut>(Func<Error, TOut> onError, Func<T, TOut> onValid)
        => _error is not null ? onError(_error) : onValid(_valid!);
}

public static class AsyncValidationExtensions
{
    public static async ValueTask<Validation<TNext>> BindAsync<T, TNext>(this ValueTask<Validation<T>> val, Func<T, CancellationToken, ValueTask<Validation<TNext>>> func, CancellationToken ct)
        => await (await val).Match(
            onError: e => ValueTask.FromResult<Validation<TNext>>(e),
            onValid: valid => func(valid, ct)
            );

    public static async ValueTask<Validation<TNext>> MapAsync<T, TNext>(this ValueTask<Validation<T>> val, Func<T, TNext> func)
        => (await val).Match<Validation<TNext>>(
            onError: e => e,
            onValid: valid => func(valid)
            );

    public static async ValueTask<TNext> MatchAsync<T, TNext>(this ValueTask<Validation<T>> val, Func<Error, CancellationToken, ValueTask<TNext>> onError, Func<T, CancellationToken, ValueTask<TNext>> onValid, CancellationToken ct)
        => await (await val).Match(
            onError: e => onError(e, ct),
            onValid: valid => onValid(valid, ct)
            );
}
