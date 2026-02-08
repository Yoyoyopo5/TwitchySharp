using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_LayerComposition
{
    // ========== Layer<TIn, TOut>.Then(Layer<TIn, TOut>) ==========

    [Fact]
    public async Task Then_TwoHeteromorphicLayers_ComposesInOrder()
    {
        // Arrange
        Step<int, string> core = input => input.ToString().AsValueTask();
        Layer<int, string> bracketLayer = next => async input => $"[{await next(input)}]";
        Layer<int, string> parenLayer = next => async input => $"({await next(input)})";

        // Act
        Layer<int, string> composed = bracketLayer.Then(parenLayer);
        Step<int, string> pipeline = composed(core);
        string result = await pipeline(42);

        // Assert
        Assert.Equal("([42])", result);
    }

    [Fact]
    public async Task Then_HeteromorphicLayers_FirstAppliesFirst()
    {
        // Arrange
        List<string> order = new();
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> layerA = next => async input =>
        {
            order.Add("A-before");
            int result = await next(input);
            order.Add("A-after");
            return result;
        };
        Layer<int, int> layerB = next => async input =>
        {
            order.Add("B-before");
            int result = await next(input);
            order.Add("B-after");
            return result;
        };

        // Act
        Layer<int, int> composed = layerA.Then(layerB);
        Step<int, int> pipeline = composed(core);
        await pipeline(1);

        // Assert
        Assert.Equal(4, order.Count);
        Assert.Equal("B-before", order[0]);
        Assert.Equal("A-before", order[1]);
        Assert.Equal("A-after", order[2]);
        Assert.Equal("B-after", order[3]);
    }

    [Fact]
    public async Task Then_HeteromorphicLayers_ResultPassesThroughBoth()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addTen = next => async input => await next(input) + 10;
        Layer<int, int> timesTwo = next => async input => await next(input) * 2;

        // Act
        Layer<int, int> composed = addTen.Then(timesTwo);
        Step<int, int> pipeline = composed(core);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(30, result); // (5 + 10) * 2
    }

    [Fact]
    public async Task Then_HeteromorphicLayers_EquivalentToManualApplication()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addTen = next => async input => await next(input) + 10;
        Layer<int, int> timesTwo = next => async input => await next(input) * 2;

        // Act
        Layer<int, int> composed = addTen.Then(timesTwo);
        int composedResult = await composed(core)(5);
        int manualResult = await timesTwo(addTen(core))(5);

        // Assert
        Assert.Equal(manualResult, composedResult);
    }

    [Fact]
    public async Task Then_ThreeHeteromorphicLayers_ChainCorrectly()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addOne = next => async input => await next(input) + 1;
        Layer<int, int> timesTwo = next => async input => await next(input) * 2;
        Layer<int, int> addHundred = next => async input => await next(input) + 100;

        // Act
        Layer<int, int> composed = addOne.Then(timesTwo).Then(addHundred);
        Step<int, int> pipeline = composed(core);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(112, result); // ((5 + 1) * 2) + 100
    }

    // ========== Layer<T>.Then(Layer<T>) ==========

    [Fact]
    public async Task Then_TwoHomomorphicLayers_ComposesInOrder()
    {
        // Arrange
        Step<string> core = input => input.AsValueTask();
        Layer<string> bracketLayer = next => async input => $"[{await next(input)}]";
        Layer<string> parenLayer = next => async input => $"({await next(input)})";

        // Act
        Layer<string> composed = bracketLayer.Then(parenLayer);
        Step<string> pipeline = composed(core);
        string result = await pipeline("x");

        // Assert
        Assert.Equal("([x])", result);
    }

    [Fact]
    public async Task Then_HomomorphicLayers_FirstAppliesFirst()
    {
        // Arrange
        List<string> order = new();
        Step<int> core = input => input.AsValueTask();
        Layer<int> layerA = next => async input =>
        {
            order.Add("A-before");
            int result = await next(input);
            order.Add("A-after");
            return result;
        };
        Layer<int> layerB = next => async input =>
        {
            order.Add("B-before");
            int result = await next(input);
            order.Add("B-after");
            return result;
        };

        // Act
        Layer<int> composed = layerA.Then(layerB);
        Step<int> pipeline = composed(core);
        await pipeline(1);

        // Assert
        Assert.Equal(4, order.Count);
        Assert.Equal("B-before", order[0]);
        Assert.Equal("A-before", order[1]);
        Assert.Equal("A-after", order[2]);
        Assert.Equal("B-after", order[3]);
    }

    [Fact]
    public async Task Then_HomomorphicLayers_EquivalentToManualApplication()
    {
        // Arrange
        Step<int> core = input => input.AsValueTask();
        Layer<int> addTen = next => async input => await next(input) + 10;
        Layer<int> timesTwo = next => async input => await next(input) * 2;

        // Act
        Layer<int> composed = addTen.Then(timesTwo);
        int composedResult = await composed(core)(5);
        int manualResult = await timesTwo(addTen(core))(5);

        // Assert
        Assert.Equal(manualResult, composedResult);
    }

    // ========== Layer.Then(Layer) used with Step.Then(Layer) ==========

    [Fact]
    public async Task Then_ComposedLayerAppliedViaStepThen_Works()
    {
        // Arrange
        Step<int, int> core = input => input.AsValueTask();
        Layer<int, int> addOne = next => async input => await next(input) + 1;
        Layer<int, int> timesThree = next => async input => await next(input) * 3;

        // Act
        Layer<int, int> composed = addOne.Then(timesThree);
        Step<int, int> pipeline = core.Then(composed);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(18, result); // (5 + 1) * 3
    }
}
