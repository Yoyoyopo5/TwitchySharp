using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_With
{
    [Fact]
    public async Task With_InputDotWithStep_InvokesStepWithInput()
    {
        // Arrange
        Step<int, string> step = (input, _) => input.ToString().AsValueTask();

        // Act
        string result = await 42.With(step);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public async Task With_InputDotWithStep_WorksWithComplexTypes()
    {
        // Arrange
        Step<List<int>, int> sumStep = (input, _) => input.Sum().AsValueTask();
        List<int> numbers = new() { 1, 2, 3, 4, 5 };

        // Act
        int result = await numbers.With(sumStep);

        // Assert
        Assert.Equal(15, result);
    }

    [Fact]
    public async Task With_InputDotWithStep_WorksWithComposedStep()
    {
        // Arrange
        Step<int, int> addOne = (input, _) => (input + 1).AsValueTask();
        Step<int, string> toStr = (input, _) => input.ToString().AsValueTask();
        Step<int, string> composed = addOne.Then(toStr);

        // Act
        string result = await 5.With(composed);

        // Assert
        Assert.Equal("6", result);
    }

    [Fact]
    public async Task With_InputDotWithStep_WorksWithNullInput()
    {
        // Arrange
        Step<string?, string> step = (input, _) => (input ?? "default").AsValueTask();

        // Act
        string result = await ((string?)null).With(step);

        // Assert
        Assert.Equal("default", result);
    }
}
