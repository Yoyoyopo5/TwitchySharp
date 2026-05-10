using System;

namespace TwitchySharp.Infrastructure.Functional;

public record Error()
{
    public Error(string message) : this() => Message = message;
    public string Message { get; init; } = string.Empty;
    public Error? InnerError { get; init; }
};

public readonly record struct Validation<T>
{
    private readonly Error? _error;
    private readonly T? _valid;
    public Validation(Error error) => _error = error;
    public Validation(T right) => _valid = right;
    public static implicit operator Validation<T>(Error error) => new(error);
    public static implicit operator Validation<T>(T right) => new(right);
    public Validation<TNextR> Bind<TNextR>(Func<T, Validation<TNextR>> func)
        => _error is not null ?_error : func(_valid!);
    public Validation<TNextR> Map<TNextR>(Func<T, TNextR> func)
        => _error is not null ?_error : func(_valid!);
    public TOut Match<TOut>(Func<Error, TOut> onError, Func<T, TOut> onValid)
        => _error is not null ? onError(_error) : onValid(_valid!);
}
