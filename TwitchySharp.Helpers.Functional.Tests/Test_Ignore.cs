using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Ignore
{
    [Fact]
    public async Task Ignore_ConvertsStepToEffect_DiscardsOutput()
    {
        // Arrange
        Step<int, string> step = (input, _) => input.ToString().AsValueTask();

        // Act
        Effect<int> effect = step.Ignore();
        await effect(42);

        // Assert - no exception, effect completes (output is discarded)
    }

    [Fact]
    public async Task Ignore_StepStillExecutes()
    {
        // Arrange
        bool stepExecuted = false;
        Step<int, string> step = (input, _) =>
        {
            stepExecuted = true;
            return input.ToString().AsValueTask();
        };

        // Act
        Effect<int> effect = step.Ignore();
        await effect(42);

        // Assert
        Assert.True(stepExecuted);
    }

    [Fact]
    public async Task Ignore_StepReceivesCorrectInput()
    {
        // Arrange
        int capturedInput = 0;
        Step<int, string> step = (input, _) =>
        {
            capturedInput = input;
            return input.ToString().AsValueTask();
        };

        // Act
        Effect<int> effect = step.Ignore();
        await effect(99);

        // Assert
        Assert.Equal(99, capturedInput);
    }

    [Fact]
    public async Task Ignore_ReturnsCompletedValueTask()
    {
        // Arrange
        Step<int, int> step = (input, _) => (input * 2).AsValueTask();

        // Act
        Effect<int> effect = step.Ignore();
        ValueTask task = effect(5);
        await task;

        // Assert
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async Task Ignore_CanBeUsedWithTapInput()
    {
        // Arrange
        int capturedInput = 0;
        Step<int, string> innerStep = (input, _) =>
        {
            capturedInput = input;
            return input.ToString().AsValueTask();
        };
        Step<int> mainStep = (input, _) => (input + 1).AsValueTask();

        // Act
        Step<int> pipeline = mainStep.TapInput(innerStep.Ignore());
        int result = await pipeline(5);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(5, capturedInput);
    }

    [Fact]
    public async Task Ignore_CanBeUsedWithTap()
    {
        // Arrange
        int capturedOutput = 0;
        Step<int, int> innerStep = (input, _) =>
        {
            capturedOutput = input;
            return (input * 100).AsValueTask();
        };
        Step<int> mainStep = (input, _) => (input + 1).AsValueTask();

        // Act
        Step<int> pipeline = mainStep.Tap(innerStep.Ignore());
        int result = await pipeline(5);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(6, capturedOutput); // Tap effect sees the output (6), Ignore discards innerStep's return
    }

    [Fact]
    public async Task Ignore_WithAsyncStep_AwaitsStepToCompletion()
    {
        // Arrange
        bool completed = false;
        Step<int, string> step = async (input, _) =>
        {
            await Task.Delay(1);
            completed = true;
            return input.ToString();
        };

        // Act
        Effect<int> effect = step.Ignore();
        await effect(42);

        // Assert
        Assert.True(completed);
    }

    [Fact]
    public async Task Ignore_MultipleInvocations_EachCallsStep()
    {
        // Arrange
        int callCount = 0;
        Step<int, int> step = (input, _) =>
        {
            callCount++;
            return (input * 2).AsValueTask();
        };

        // Act
        Effect<int> effect = step.Ignore();
        await effect(1);
        await effect(2);
        await effect(3);

        // Assert
        Assert.Equal(3, callCount);
    }
}
