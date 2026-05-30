namespace TwitchySharp.Infrastructure.Functional;

public readonly record struct Unit
{
    public static Unit Instance { get; } = new Unit();
}
