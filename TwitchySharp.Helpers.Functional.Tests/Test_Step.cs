using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Step
{
    [Fact]
    public async Task Step_WithTwoTypeParams_TransformsInput()
    {
        // Arrange
        Step<int, string> step = input => input.ToString().AsValueTask();

        // Act
        string result = await step(42);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public async Task Step_WithTwoTypeParams_HandlesNullableOutput()
    {
        // Arrange
        Step<string, string?> step = input => ((string?)null).AsValueTask();

        // Act
        string? result = await step("hello");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Step_WithTwoTypeParams_CanBeAsync()
    {
        // Arrange
        Step<int, int> step = async input =>
        {
            await Task.Delay(1);
            return input * 2;
        };

        // Act
        int result = await step(5);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public async Task Step_HomomorphicSingleTypeParam_ReturnsSameType()
    {
        // Arrange
        Step<int> step = input => (input + 1).AsValueTask();

        // Act
        int result = await step(10);

        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public async Task Step_HomomorphicSingleTypeParam_IdentityFunction()
    {
        // Arrange
        Step<string> identity = input => input.AsValueTask();

        // Act
        string result = await identity("unchanged");

        // Assert
        Assert.Equal("unchanged", result);
    }

    [Fact]
    public async Task Step_WithTwoTypeParams_TransformsBetweenDifferentTypes()
    {
        // Arrange
        Step<double, int> step = input => ((int)Math.Floor(input)).AsValueTask();

        // Act
        int result = await step(3.7);

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public async Task Step_HomomorphicSingleTypeParam_CanBeAsync()
    {
        // Arrange
        Step<string> step = async input =>
        {
            await Task.Delay(1);
            return input.ToUpper();
        };

        // Act
        string result = await step("hello");

        // Assert
        Assert.Equal("HELLO", result);
    }
}
