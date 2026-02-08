using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Tap
{
    // ========== Tap (effect on output) - heteromorphic ==========

    [Fact]
    public async Task Tap_Heteromorphic_EffectReceivesOutput()
    {
        // Arrange
        string captured = "";
        Step<int, string> step = (input, _) => input.ToString().AsValueTask();
        Effect<string> effect = (output, _) =>
        {
            captured = output;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, string> tapped = step.Tap(effect);
        string result = await tapped(42);

        // Assert
        Assert.Equal("42", result);
        Assert.Equal("42", captured);
    }

    [Fact]
    public async Task Tap_Heteromorphic_StepOutputIsUnchanged()
    {
        // Arrange
        Step<int, int> step = (input, _) => (input * 2).AsValueTask();
        Effect<int> effect = (output, _) => ValueTask.CompletedTask;

        // Act
        Step<int, int> tapped = step.Tap(effect);
        int result = await tapped(5);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public async Task Tap_Heteromorphic_EffectRunsAfterStep()
    {
        // Arrange
        List<string> order = new();
        Step<int, string> step = (input, _) =>
        {
            order.Add("step");
            return input.ToString().AsValueTask();
        };
        Effect<string> effect = (output, _) =>
        {
            order.Add("effect");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, string> tapped = step.Tap(effect);
        await tapped(1);

        // Assert
        Assert.Equal(2, order.Count);
        Assert.Equal("step", order[0]);
        Assert.Equal("effect", order[1]);
    }

    [Fact]
    public async Task Tap_Heteromorphic_EffectReceivesTransformedValue()
    {
        // Arrange
        int capturedOutput = 0;
        Step<int, int> step = (input, _) => (input * 10).AsValueTask();
        Effect<int> effect = (output, _) =>
        {
            capturedOutput = output;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> tapped = step.Tap(effect);
        int result = await tapped(5);

        // Assert
        Assert.Equal(50, result);
        Assert.Equal(50, capturedOutput); // Effect sees the output, not the input
    }

    // ========== Tap (effect on output) - homomorphic ==========

    [Fact]
    public async Task Tap_Homomorphic_EffectReceivesOutput()
    {
        // Arrange
        string captured = "";
        Step<string> step = (input, _) => input.ToUpper().AsValueTask();
        Effect<string> effect = (output, _) =>
        {
            captured = output;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<string> tapped = step.Tap(effect);
        string result = await tapped("hello");

        // Assert
        Assert.Equal("HELLO", result);
        Assert.Equal("HELLO", captured);
    }

    [Fact]
    public async Task Tap_Homomorphic_EffectRunsAfterStep()
    {
        // Arrange
        List<string> order = new();
        Step<int> step = (input, _) =>
        {
            order.Add("step");
            return (input + 1).AsValueTask();
        };
        Effect<int> effect = (output, _) =>
        {
            order.Add("effect");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int> tapped = step.Tap(effect);
        await tapped(1);

        // Assert
        Assert.Equal(2, order.Count);
        Assert.Equal("step", order[0]);
        Assert.Equal("effect", order[1]);
    }

    [Fact]
    public async Task Tap_Homomorphic_ResultIsUnchanged()
    {
        // Arrange
        Step<int> step = (input, _) => (input * 3).AsValueTask();
        Effect<int> effect = (output, _) => ValueTask.CompletedTask;

        // Act
        Step<int> tapped = step.Tap(effect);
        int result = await tapped(7);

        // Assert
        Assert.Equal(21, result);
    }

    // ========== TapInput (effect on input) - heteromorphic ==========

    [Fact]
    public async Task TapInput_Heteromorphic_EffectReceivesInput()
    {
        // Arrange
        int capturedInput = 0;
        Step<int, string> step = (input, _) => input.ToString().AsValueTask();
        Effect<int> effect = (input, _) =>
        {
            capturedInput = input;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, string> tapped = step.TapInput(effect);
        string result = await tapped(42);

        // Assert
        Assert.Equal("42", result);
        Assert.Equal(42, capturedInput);
    }

    [Fact]
    public async Task TapInput_Heteromorphic_EffectRunsBeforeStep()
    {
        // Arrange
        List<string> order = new();
        Step<int, string> step = (input, _) =>
        {
            order.Add("step");
            return input.ToString().AsValueTask();
        };
        Effect<int> effect = (input, _) =>
        {
            order.Add("effect");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, string> tapped = step.TapInput(effect);
        await tapped(1);

        // Assert
        Assert.Equal(2, order.Count);
        Assert.Equal("effect", order[0]);
        Assert.Equal("step", order[1]);
    }

    [Fact]
    public async Task TapInput_Heteromorphic_StepOutputIsUnchanged()
    {
        // Arrange
        Step<int, int> step = (input, _) => (input * 5).AsValueTask();
        Effect<int> effect = (input, _) => ValueTask.CompletedTask;

        // Act
        Step<int, int> tapped = step.TapInput(effect);
        int result = await tapped(4);

        // Assert
        Assert.Equal(20, result);
    }

    // ========== TapInput (effect on input) - homomorphic ==========

    [Fact]
    public async Task TapInput_Homomorphic_EffectReceivesInput()
    {
        // Arrange
        int capturedInput = 0;
        Step<int> step = (input, _) => (input + 100).AsValueTask();
        Effect<int> effect = (input, _) =>
        {
            capturedInput = input;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int> tapped = step.TapInput(effect);
        int result = await tapped(7);

        // Assert
        Assert.Equal(107, result);
        Assert.Equal(7, capturedInput);
    }

    [Fact]
    public async Task TapInput_Homomorphic_EffectRunsBeforeStep()
    {
        // Arrange
        List<string> order = new();
        Step<int> step = (input, _) =>
        {
            order.Add("step");
            return (input + 1).AsValueTask();
        };
        Effect<int> effect = (input, _) =>
        {
            order.Add("effect");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int> tapped = step.TapInput(effect);
        await tapped(1);

        // Assert
        Assert.Equal(2, order.Count);
        Assert.Equal("effect", order[0]);
        Assert.Equal("step", order[1]);
    }

    // ========== Tap vs TapInput difference ==========

    [Fact]
    public async Task Tap_VsTapInput_EffectSeesOutputVsInput()
    {
        // Arrange
        int tapCapture = 0;
        int tapInputCapture = 0;
        Step<int, int> step = (input, _) => (input * 10).AsValueTask();
        Effect<int> tapEffect = (output, _) =>
        {
            tapCapture = output;
            return ValueTask.CompletedTask;
        };
        Effect<int> tapInputEffect = (input, _) =>
        {
            tapInputCapture = input;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> withTap = step.Tap(tapEffect);
        Step<int, int> withTapInput = step.TapInput(tapInputEffect);
        int tapResult = await withTap(3);
        int tapInputResult = await withTapInput(3);

        // Assert
        Assert.Equal(30, tapResult);
        Assert.Equal(30, tapInputResult);
        Assert.Equal(30, tapCapture);     // Tap sees the OUTPUT (3 * 10 = 30)
        Assert.Equal(3, tapInputCapture); // TapInput sees the INPUT (3)
    }

    // ========== Chaining Tap and TapInput ==========

    [Fact]
    public async Task Tap_ChainedMultiple_AllEffectsRun()
    {
        // Arrange
        List<int> captures = new();
        Step<int> step = (input, _) => (input + 1).AsValueTask();
        Effect<int> effectA = (output, _) => { captures.Add(output); return ValueTask.CompletedTask; };
        Effect<int> effectB = (output, _) => { captures.Add(output * 10); return ValueTask.CompletedTask; };

        // Act
        Step<int> tapped = step.Tap(effectA).Tap(effectB);
        int result = await tapped(5);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(2, captures.Count);
        Assert.Equal(6, captures[0]);   // effectA sees output of step
        Assert.Equal(60, captures[1]);  // effectB sees output of step.Tap(effectA) which is still 6
    }

    [Fact]
    public async Task TapInput_ThenTap_BothExecuteInOrder()
    {
        // Arrange
        List<string> log = new();
        Step<int, int> step = (input, _) => (input * 2).AsValueTask();
        Effect<int> inputEffect = (input, _) =>
        {
            log.Add($"input:{input}");
            return ValueTask.CompletedTask;
        };
        Effect<int> outputEffect = (output, _) =>
        {
            log.Add($"output:{output}");
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int, int> pipeline = step.TapInput(inputEffect).Tap(outputEffect);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(10, result);
        Assert.Equal(2, log.Count);
        Assert.Equal("input:5", log[0]);
        Assert.Equal("output:10", log[1]);
    }
}
