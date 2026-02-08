using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_AsValueTask
{
    // ========== AsValueTask ==========

    [Fact]
    public async Task AsValueTask_Int_WrapsInValueTask()
    {
        // Arrange
        int value = 42;

        // Act
        ValueTask<int> task = value.AsValueTask();
        int result = await task;

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task AsValueTask_String_WrapsInValueTask()
    {
        // Arrange
        string value = "hello";

        // Act
        ValueTask<string> task = value.AsValueTask();
        string result = await task;

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public async Task AsValueTask_Null_WrapsNullInValueTask()
    {
        // Arrange
        string? value = null;

        // Act
        ValueTask<string?> task = value.AsValueTask();
        string? result = await task;

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void AsValueTask_IsCompletedSynchronously()
    {
        // Arrange
        int value = 10;

        // Act
        ValueTask<int> task = value.AsValueTask();

        // Assert
        Assert.True(task.IsCompleted);
        Assert.True(task.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task AsValueTask_ComplexType_WrapsInValueTask()
    {
        // Arrange
        List<int> value = new() { 1, 2, 3 };

        // Act
        ValueTask<List<int>> task = value.AsValueTask();
        List<int> result = await task;

        // Assert
        Assert.Same(value, result);
    }

    // ========== AsEffect ==========

    [Fact]
    public async Task AsEffect_ConvertsActionToEffect()
    {
        // Arrange
        int captured = 0;
        Action<int> action = x => captured = x;

        // Act
        Effect<int> effect = action.AsEffect();
        await effect(42);

        // Assert
        Assert.Equal(42, captured);
    }

    [Fact]
    public async Task AsEffect_ReturnedEffectCompleteSynchronously()
    {
        // Arrange
        Action<string> action = x => { };

        // Act
        Effect<string> effect = action.AsEffect();
        ValueTask task = effect("test");

        // Assert
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async Task AsEffect_ActionExecutesOnInvocation()
    {
        // Arrange
        List<string> log = new();
        Action<string> action = x => log.Add(x);

        // Act
        Effect<string> effect = action.AsEffect();
        await effect("first");
        await effect("second");

        // Assert
        Assert.Equal(2, log.Count);
        Assert.Equal("first", log[0]);
        Assert.Equal("second", log[1]);
    }

    [Fact]
    public async Task AsEffect_CanBeUsedWithThen()
    {
        // Arrange
        int effectCapture = 0;
        Action<int> action = x => effectCapture = x;
        Step<int> step = input => (input + 1).AsValueTask();

        // Act
        Step<int> composed = step.TapInput(action.AsEffect());
        int result = await composed(5);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(5, effectCapture); // Effect receives original input via TapInput
    }
}
