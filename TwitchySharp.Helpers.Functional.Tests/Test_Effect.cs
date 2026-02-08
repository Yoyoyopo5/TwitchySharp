using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Effect
{
    [Fact]
    public async Task Effect_ExecutesSideEffect_WithInput()
    {
        // Arrange
        int captured = 0;
        Effect<int> effect = input =>
        {
            captured = input;
            return ValueTask.CompletedTask;
        };

        // Act
        await effect(42);

        // Assert
        Assert.Equal(42, captured);
    }

    [Fact]
    public async Task Effect_ReturnsCompletedValueTask()
    {
        // Arrange
        Effect<string> effect = input => ValueTask.CompletedTask;

        // Act
        ValueTask task = effect("test");
        await task;

        // Assert
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async Task Effect_CanBeAsync()
    {
        // Arrange
        List<string> log = new();
        Effect<string> effect = async input =>
        {
            await Task.Delay(1);
            log.Add(input);
        };

        // Act
        await effect("entry1");
        await effect("entry2");

        // Assert
        Assert.Equal(2, log.Count);
        Assert.Equal("entry1", log[0]);
        Assert.Equal("entry2", log[1]);
    }

    [Fact]
    public async Task Effect_WithNullInput_DoesNotThrow()
    {
        // Arrange
        string? captured = "not null";
        Effect<string?> effect = input =>
        {
            captured = input;
            return ValueTask.CompletedTask;
        };

        // Act
        await effect(null);

        // Assert
        Assert.Null(captured);
    }

    [Fact]
    public async Task Effect_MultipleInvocations_EachExecutesSideEffect()
    {
        // Arrange
        int callCount = 0;
        Effect<int> effect = input =>
        {
            callCount++;
            return ValueTask.CompletedTask;
        };

        // Act
        await effect(1);
        await effect(2);
        await effect(3);

        // Assert
        Assert.Equal(3, callCount);
    }
}
