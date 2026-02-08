using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Identity
{
    [Fact]
    public async Task Identity_Int_ReturnsInputUnchanged()
    {
        // Arrange
        Step<int> identity = FunctionalExtensions.Identity<int>();

        // Act
        int result = await identity(42);

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public async Task Identity_String_ReturnsInputUnchanged()
    {
        // Arrange
        Step<string> identity = FunctionalExtensions.Identity<string>();

        // Act
        string result = await identity("hello");

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public async Task Identity_Null_ReturnsNull()
    {
        // Arrange
        Step<string?> identity = FunctionalExtensions.Identity<string?>();

        // Act
        string? result = await identity(null);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Identity_ComplexType_ReturnsSameReference()
    {
        // Arrange
        Step<List<int>> identity = FunctionalExtensions.Identity<List<int>>();
        List<int> input = new() { 1, 2, 3 };

        // Act
        List<int> result = await identity(input);

        // Assert
        Assert.Same(input, result);
    }

    [Fact]
    public void Identity_CompletedSynchronously()
    {
        // Arrange
        Step<int> identity = FunctionalExtensions.Identity<int>();

        // Act
        ValueTask<int> task = identity(99);

        // Assert
        Assert.True(task.IsCompleted);
        Assert.True(task.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task Identity_CanBeComposedWithThen()
    {
        // Arrange
        Step<int> identity = FunctionalExtensions.Identity<int>();
        Step<int> addOne = (input, _) => (input + 1).AsValueTask();

        // Act
        Step<int> pipeline = identity.Then(addOne);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public async Task Identity_AsStartOfPipeline_PassesInputThrough()
    {
        // Arrange
        Step<int> identity = FunctionalExtensions.Identity<int>();
        Step<int, string> toStr = (input, _) => input.ToString().AsValueTask();

        // Act
        Step<int, string> pipeline = identity.Expand().Then(toStr);
        string result = await pipeline(42);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public async Task Identity_WithLayer_LayerSeesOriginalValue()
    {
        // Arrange
        int layerSaw = 0;
        Step<int> identity = FunctionalExtensions.Identity<int>();
        Layer<int> captureLayer = next => async (input, ct) =>
        {
            layerSaw = input;
            return await next(input, ct);
        };

        // Act
        Step<int> pipeline = identity.Then(captureLayer);
        int result = await pipeline(42);

        // Assert
        Assert.Equal(42, result);
        Assert.Equal(42, layerSaw);
    }
}
