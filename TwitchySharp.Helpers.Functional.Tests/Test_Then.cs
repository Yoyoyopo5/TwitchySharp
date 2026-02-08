using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Then
{
    // ========== Step + Step (TIn -> TMid -> TOut) ==========

    [Fact]
    public async Task Then_StepTInTMid_StepTMidTOut_ComposesSequentially()
    {
        // Arrange
        Step<int, string> first = input => input.ToString().AsValueTask();
        Step<string, int> second = input => input.Length.AsValueTask();

        // Act
        Step<int, int> composed = first.Then(second);
        int result = await composed(12345);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task Then_StepTInTMid_StepTMidTOut_PassesOutputOfFirstToSecond()
    {
        // Arrange
        Step<int, double> first = input => (input * 1.5).AsValueTask();
        Step<double, string> second = input => input.ToString("F1").AsValueTask();

        // Act
        Step<int, string> composed = first.Then(second);
        string result = await composed(10);

        // Assert
        Assert.Equal("15.0", result);
    }

    // ========== Homomorphic Step<T> + Step<T> ==========

    [Fact]
    public async Task Then_HomomorphicStep_HomomorphicStep_ComposesSequentially()
    {
        // Arrange
        Step<int> addOne = input => (input + 1).AsValueTask();
        Step<int> timesTwo = input => (input * 2).AsValueTask();

        // Act
        Step<int> composed = addOne.Then(timesTwo);
        int result = await composed(5);

        // Assert
        Assert.Equal(12, result); // (5 + 1) * 2
    }

    [Fact]
    public async Task Then_HomomorphicStep_ChainMultiple()
    {
        // Arrange
        Step<int> addOne = input => (input + 1).AsValueTask();
        Step<int> timesTwo = input => (input * 2).AsValueTask();
        Step<int> subtractThree = input => (input - 3).AsValueTask();

        // Act
        Step<int> composed = addOne.Then(timesTwo).Then(subtractThree);
        int result = await composed(5);

        // Assert
        Assert.Equal(9, result); // ((5 + 1) * 2) - 3
    }

    // ========== Step<TIn> (homomorphic) + Step<TIn, TOut> ==========

    [Fact]
    public async Task Then_HomomorphicStepTIn_StepTInTOut_ComposesSequentially()
    {
        // Arrange
        Step<int> addOne = input => (input + 1).AsValueTask();
        Step<int, string> toStr = input => input.ToString().AsValueTask();

        // Act
        Step<int, string> composed = addOne.Then(toStr);
        string result = await composed(5);

        // Assert
        Assert.Equal("6", result);
    }

    // ========== Step<TIn, TOut> + Step<TOut> (homomorphic) ==========

    [Fact]
    public async Task Then_StepTInTOut_HomomorphicStepTOut_ComposesSequentially()
    {
        // Arrange
        Step<string, int> parse = input => int.Parse(input).AsValueTask();
        Step<int> timesTwo = input => (input * 2).AsValueTask();

        // Act
        Step<string, int> composed = parse.Then(timesTwo);
        int result = await composed("7");

        // Assert
        Assert.Equal(14, result);
    }

    // ========== Step + Effect (via TapInput) ==========

    [Fact]
    public async Task TapInput_StepTInTOut_EffectTIn_ExecutesEffectThenStep()
    {
        // Arrange
        int effectCapture = 0;
        Step<int, string> step = input => input.ToString().AsValueTask();
        Effect<int> effect = input =>
        {
            effectCapture = input;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, string> composed = step.TapInput(effect);
        string result = await composed(42);

        // Assert
        Assert.Equal("42", result);
        Assert.Equal(42, effectCapture);
    }

    [Fact]
    public async Task TapInput_StepTInTOut_EffectTIn_EffectReceivesOriginalInput()
    {
        // Arrange
        List<int> effectLog = new();
        Step<int, int> step = input => (input * 10).AsValueTask();
        Effect<int> effect = input =>
        {
            effectLog.Add(input);
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> composed = step.TapInput(effect);
        int result = await composed(5);

        // Assert
        Assert.Equal(50, result);
        Assert.Single(effectLog);
        Assert.Equal(5, effectLog[0]); // Effect receives original input, not transformed
    }

    [Fact]
    public async Task TapInput_HomomorphicStep_Effect_ExecutesEffectThenStep()
    {
        // Arrange
        string effectCapture = "";
        Step<string> step = input => input.ToUpper().AsValueTask();
        Effect<string> effect = input =>
        {
            effectCapture = input;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<string> composed = step.TapInput(effect);
        string result = await composed("hello");

        // Assert
        Assert.Equal("HELLO", result);
        Assert.Equal("hello", effectCapture);
    }

    // ========== Step + Layer ==========

    [Fact]
    public async Task Then_StepTInTOut_LayerTInTOut_AppliesLayerToStep()
    {
        // Arrange
        Step<int, string> core = input => input.ToString().AsValueTask();
        Layer<int, string> layer = next => async input => $"[{await next(input)}]";

        // Act
        Step<int, string> composed = core.Then(layer);
        string result = await composed(42);

        // Assert
        Assert.Equal("[42]", result);
    }

    [Fact]
    public async Task Then_HomomorphicStep_HomomorphicLayer_AppliesLayerToStep()
    {
        // Arrange
        Step<int> core = input => (input + 1).AsValueTask();
        Layer<int> layer = next => async input => await next(input) * 10;

        // Act
        Step<int> composed = core.Then(layer);
        int result = await composed(5);

        // Assert
        Assert.Equal(60, result); // (5 + 1) * 10
    }

    [Fact]
    public async Task Then_StepTInTOut_MultipleLayers_ApplyInChainOrder()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addBracket = next => async input => await next(input) + 100;
        Layer<int, int> multiply = next => async input => await next(input) * 2;

        // Act
        Step<int, int> composed = core.Then(addBracket).Then(multiply);
        int result = await composed(5);

        // Assert
        Assert.Equal(210, result); // (5 + 100) * 2
    }

    // ========== Mixed Then chaining ==========

    [Fact]
    public async Task Then_MixedStepAndLayer_ComposeCorrectly()
    {
        // Arrange
        Step<int> addOne = input => (input + 1).AsValueTask();
        Layer<int> timesThreeLayer = next => async input => await next(input) * 3;

        // Act
        Step<int> composed = addOne.Then(timesThreeLayer);
        int result = await composed(4);

        // Assert
        Assert.Equal(15, result); // (4 + 1) * 3
    }

    [Fact]
    public async Task TapInput_StepThenEffect_EffectRunsFirst()
    {
        // Arrange
        List<string> order = new();
        Step<int, int> step = input =>
        {
            order.Add("step");
            return (input * 2).AsValueTask();
        };
        Effect<int> effect = input =>
        {
            order.Add("effect");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> composed = step.TapInput(effect);
        int result = await composed(5);

        // Assert
        Assert.Equal(10, result);
        Assert.Equal(2, order.Count);
        Assert.Equal("effect", order[0]);
        Assert.Equal("step", order[1]);
    }
}
