using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Helpers.Functional;

/// <summary>An async side effect that observes a value without changing the data flow.</summary>
/// <remarks>Use with <c>.Tap()</c> to observe a step's output or <c>.TapInput()</c> to observe its input. Create from a synchronous <see cref="System.Action{T}"/> with <c>.AsEffect()</c>.</remarks>
/// <param name="in">The value to observe.</param>
/// <param name="ct">Cancellation token for cooperative cancellation.</param>
public delegate ValueTask Effect<TIn>(TIn @in, CancellationToken ct = default);
