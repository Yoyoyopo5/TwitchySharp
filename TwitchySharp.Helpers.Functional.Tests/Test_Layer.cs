using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_Layer
{
    [Fact]
    public async Task Layer_WithTwoTypeParams_WrapsStep()
    {
        // Arrange
        Step<int, string> core = input => input.ToString().AsValueTask();
        Layer<int, string> layer = next => async input => $"[{await next(input)}]";

        // Act
        Step<int, string> wrapped = layer(core);
        string result = await wrapped(42);

        // Assert
        Assert.Equal("[42]", result);
    }

    [Fact]
    public async Task Layer_WithTwoTypeParams_CanModifyInput()
    {
        // Arrange
        Step<int, int> core = input => (input * 2).AsValueTask();
        Layer<int, int> layer = next => input => next(input + 10);

        // Act
        Step<int, int> wrapped = layer(core);
        int result = await wrapped(5);

        // Assert
        Assert.Equal(30, result); // (5 + 10) * 2
    }

    [Fact]
    public async Task Layer_WithTwoTypeParams_CanModifyOutput()
    {
        // Arrange
        Step<int, int> core = input => (input * 2).AsValueTask();
        Layer<int, int> layer = next => async input =>
        {
            int output = await next(input);
            return output + 100;
        };

        // Act
        Step<int, int> wrapped = layer(core);
        int result = await wrapped(5);

        // Assert
        Assert.Equal(110, result); // (5 * 2) + 100
    }

    [Fact]
    public async Task Layer_HomomorphicSingleTypeParam_WrapsStep()
    {
        // Arrange
        Step<string> core = input => input.ToUpper().AsValueTask();
        Layer<string> layer = next => async input => $"({await next(input)})";

        // Act
        Step<string> wrapped = layer(core);
        string result = await wrapped("hello");

        // Assert
        Assert.Equal("(HELLO)", result);
    }

    [Fact]
    public async Task Layer_MultipleLayers_ApplyInOrder()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addOne = next => async input => await next(input) + 1;
        Layer<int, int> timesTwo = next => async input => await next(input) * 2;

        // Act - apply addOne first, then timesTwo wraps around it
        Step<int, int> wrapped = timesTwo(addOne(core));
        int result = await wrapped(5);

        // Assert
        Assert.Equal(12, result); // (5 + 1) * 2
    }

    [Fact]
    public async Task Layer_CanShortCircuit_ByNotCallingNext()
    {
        // Arrange
        bool coreCalled = false;
        Step<int, int> core = input =>
        {
            coreCalled = true;
            return input.AsValueTask();
        };
        Layer<int, int> shortCircuit = next => input => (-1).AsValueTask();

        // Act
        Step<int, int> wrapped = shortCircuit(core);
        int result = await wrapped(5);

        // Assert
        Assert.Equal(-1, result);
        Assert.False(coreCalled);
    }

    [Fact]
    public async Task Layer_HomomorphicSingleTypeParam_CanModifyInputAndOutput()
    {
        // Arrange
        Step<int> core = input => (input * 3).AsValueTask();
        Layer<int> layer = next => async input => await next(input + 1) + 10;

        // Act
        Step<int> wrapped = layer(core);
        int result = await wrapped(2);

        // Assert
        Assert.Equal(19, result); // (2 + 1) * 3 + 10
    }
}
