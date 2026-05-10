using System;

namespace TwitchySharp.Infrastructure.Functional;

public readonly record struct Either<TL, TR>
{
    private readonly TL? _left;
    private readonly TR? _right;
    public Either(TL left) => _left = left;
    public Either(TR right) => _right = right;
    public static implicit operator Either<TL, TR>(TL left) => new(left);
    public static implicit operator Either<TL, TR>(TR right) => new(right);
    public Either<TL, TNextR> Bind<TNextR>(Func<TR, Either<TL, TNextR>> func)
        => _left is not null ? _left : func(_right!);
    public Either<TL, TNextR> Map<TNextR>(Func<TR, TNextR> func)
        => _left is not null ? _left : func(_right!);
    public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc)
        => _left is not null ? leftFunc(_left) : rightFunc(_right!);
}
