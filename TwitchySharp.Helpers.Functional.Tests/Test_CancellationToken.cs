using System.Threading;
using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_CancellationToken
{
    [Fact]
    public async Task Then_StepChain_PropagatesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByFirst = default;
        CancellationToken receivedBySecond = default;
        Step<int, int> first = (input, ct) =>
        {
            receivedByFirst = ct;
            return (input + 1).AsValueTask();
        };
        Step<int, string> second = (input, ct) =>
        {
            receivedBySecond = ct;
            return input.ToString().AsValueTask();
        };
        Step<int, string> pipeline = first.Then(second);
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        string result = await pipeline(42, token);

        // Assert
        Assert.Equal("43", result);
        Assert.Equal(token, receivedByFirst);
        Assert.Equal(token, receivedBySecond);
    }

    [Fact]
    public async Task Then_HomomorphicStepChain_PropagatesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByFirst = default;
        CancellationToken receivedBySecond = default;
        Step<int> first = (input, ct) =>
        {
            receivedByFirst = ct;
            return (input + 1).AsValueTask();
        };
        Step<int> second = (input, ct) =>
        {
            receivedBySecond = ct;
            return (input * 2).AsValueTask();
        };
        Step<int> pipeline = first.Then(second);
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        int result = await pipeline(5, token);

        // Assert
        Assert.Equal(12, result); // (5 + 1) * 2
        Assert.Equal(token, receivedByFirst);
        Assert.Equal(token, receivedBySecond);
    }

    [Fact]
    public async Task Tap_EffectOnOutput_ReceivesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByStep = default;
        CancellationToken receivedByEffect = default;
        Step<int, int> step = (input, ct) =>
        {
            receivedByStep = ct;
            return (input * 2).AsValueTask();
        };
        Effect<int> effect = (input, ct) =>
        {
            receivedByEffect = ct;
            return ValueTask.CompletedTask;
        };
        Step<int, int> tapped = step.Tap(effect);
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        int result = await tapped(5, token);

        // Assert
        Assert.Equal(10, result);
        Assert.Equal(token, receivedByStep);
        Assert.Equal(token, receivedByEffect);
    }

    [Fact]
    public async Task TapInput_EffectOnInput_ReceivesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByStep = default;
        CancellationToken receivedByEffect = default;
        Step<int, int> step = (input, ct) =>
        {
            receivedByStep = ct;
            return (input * 3).AsValueTask();
        };
        Effect<int> effect = (input, ct) =>
        {
            receivedByEffect = ct;
            return ValueTask.CompletedTask;
        };
        Step<int, int> tapped = step.TapInput(effect);
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        int result = await tapped(4, token);

        // Assert
        Assert.Equal(12, result);
        Assert.Equal(token, receivedByStep);
        Assert.Equal(token, receivedByEffect);
    }

    [Fact]
    public async Task Layer_Middleware_PropagatesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByCore = default;
        CancellationToken receivedByLayer = default;
        Step<int, int> core = (input, ct) =>
        {
            receivedByCore = ct;
            return (input * 2).AsValueTask();
        };
        Layer<int, int> layer = next => (input, ct) =>
        {
            receivedByLayer = ct;
            return next(input + 10, ct);
        };
        Step<int, int> wrapped = layer(core);
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        int result = await wrapped(5, token);

        // Assert
        Assert.Equal(30, result); // (5 + 10) * 2
        Assert.Equal(token, receivedByCore);
        Assert.Equal(token, receivedByLayer);
    }

    [Fact]
    public async Task With_Extension_PropagatesCancellationToken()
    {
        // Arrange
        CancellationToken receivedByStep = default;
        Step<int, string> step = (input, ct) =>
        {
            receivedByStep = ct;
            return input.ToString().AsValueTask();
        };
        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        string result = await 42.With(step, token);

        // Assert
        Assert.Equal("42", result);
        Assert.Equal(token, receivedByStep);
    }

    [Fact]
    public async Task Step_CancelledToken_ThrowsOperationCanceledException()
    {
        // Arrange
        Step<int, int> step = (input, ct) =>
        {
            ct.ThrowIfCancellationRequested();
            return (input * 2).AsValueTask();
        };
        CancellationTokenSource cts = new();
        cts.Cancel();
        CancellationToken cancelledToken = cts.Token;

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(
            () => step(5, cancelledToken).AsTask());
    }

    [Fact]
    public async Task Pipeline_WithLayersEffectsAndSteps_PropagatesCancellationTokenThroughout()
    {
        // Arrange
        CancellationToken receivedByTapInputEffect = default;
        CancellationToken receivedByLayer = default;
        CancellationToken receivedByCore = default;
        CancellationToken receivedByThenStep = default;
        CancellationToken receivedByTapEffect = default;

        Step<int, int> core = (input, ct) =>
        {
            receivedByCore = ct;
            return (input * 2).AsValueTask();
        };

        Layer<int, int> layer = next => async (input, ct) =>
        {
            receivedByLayer = ct;
            int result = await next(input, ct);
            return result + 100;
        };

        Step<int, string> toString = (input, ct) =>
        {
            receivedByThenStep = ct;
            return input.ToString().AsValueTask();
        };

        Effect<int> tapInputEffect = (input, ct) =>
        {
            receivedByTapInputEffect = ct;
            return ValueTask.CompletedTask;
        };

        Effect<string> tapEffect = (input, ct) =>
        {
            receivedByTapEffect = ct;
            return ValueTask.CompletedTask;
        };

        Step<int, string> pipeline = core
            .TapInput(tapInputEffect)
            .Then(layer)
            .Then(toString)
            .Tap(tapEffect);

        CancellationTokenSource cts = new();
        CancellationToken token = cts.Token;

        // Act
        string result = await pipeline(5, token);

        // Assert
        Assert.Equal("110", result); // (5 * 2) + 100 = 110, then ToString
        Assert.Equal(token, receivedByTapInputEffect);
        Assert.Equal(token, receivedByLayer);
        Assert.Equal(token, receivedByCore);
        Assert.Equal(token, receivedByThenStep);
        Assert.Equal(token, receivedByTapEffect);
    }
}
