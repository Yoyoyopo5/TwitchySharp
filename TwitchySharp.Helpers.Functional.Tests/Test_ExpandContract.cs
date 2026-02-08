using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_ExpandContract
{
    [Fact]
    public async Task Expand_HomomorphicStep_BecomesStepTT()
    {
        // Arrange
        Step<int> addOne = (input, _) => (input + 1).AsValueTask();

        // Act
        Step<int, int> expanded = addOne.Expand();
        int result = await expanded(5);

        // Assert
        Assert.Equal(6, result);
    }

    [Fact]
    public async Task Contract_StepTT_BecomesHomomorphicStep()
    {
        // Arrange
        Step<int, int> doubleIt = (input, _) => (input * 2).AsValueTask();

        // Act
        Step<int> contracted = doubleIt.Contract();
        int result = await contracted(5);

        // Assert
        Assert.Equal(10, result);
    }

    [Fact]
    public async Task ExpandThenContract_RoundTrip_PreservesBehavior()
    {
        // Arrange
        Step<int> original = (input, _) => (input + 10).AsValueTask();

        // Act
        Step<int> roundTripped = original.Expand().Contract();
        int originalResult = await original(5);
        int roundTrippedResult = await roundTripped(5);

        // Assert
        Assert.Equal(15, originalResult);
        Assert.Equal(15, roundTrippedResult);
        Assert.Equal(originalResult, roundTrippedResult);
    }

    [Fact]
    public async Task ContractThenExpand_RoundTrip_PreservesBehavior()
    {
        // Arrange
        Step<string, string> original = (input, _) => input.ToUpper().AsValueTask();

        // Act
        Step<string, string> roundTripped = original.Contract().Expand();
        string originalResult = await original("hello");
        string roundTrippedResult = await roundTripped("hello");

        // Assert
        Assert.Equal("HELLO", originalResult);
        Assert.Equal("HELLO", roundTrippedResult);
        Assert.Equal(originalResult, roundTrippedResult);
    }

    [Fact]
    public async Task Expand_AllowsChainingWithTwoTypeParamThen()
    {
        // Arrange
        Step<int> addOne = (input, _) => (input + 1).AsValueTask();
        Step<int, string> toStr = (input, _) => input.ToString().AsValueTask();

        // Act
        Step<int, string> composed = addOne.Expand().Then(toStr);
        string result = await composed(5);

        // Assert
        Assert.Equal("6", result);
    }

    [Fact]
    public async Task Contract_AllowsChainingWithHomomorphicThen()
    {
        // Arrange
        Step<int, int> doubleIt = (input, _) => (input * 2).AsValueTask();
        Step<int> addOne = (input, _) => (input + 1).AsValueTask();

        // Act
        Step<int> composed = doubleIt.Contract().Then(addOne);
        int result = await composed(5);

        // Assert
        Assert.Equal(11, result); // (5 * 2) + 1
    }

    [Fact]
    public async Task Expand_WithStringType_Works()
    {
        // Arrange
        Step<string> trimStep = (input, _) => input.Trim().AsValueTask();

        // Act
        Step<string, string> expanded = trimStep.Expand();
        string result = await expanded("  hello  ");

        // Assert
        Assert.Equal("hello", result);
    }

    [Fact]
    public async Task Contract_WithStringType_Works()
    {
        // Arrange
        Step<string, string> toLower = (input, _) => input.ToLower().AsValueTask();

        // Act
        Step<string> contracted = toLower.Contract();
        string result = await contracted("HELLO");

        // Assert
        Assert.Equal("hello", result);
    }
}
