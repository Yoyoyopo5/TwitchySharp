using TwitchySharp.Helpers.Functional;
using Xunit;

namespace TwitchySharp.Helpers.Functional.Tests;

public class Test_AsStep
{
    // ========== Func<TIn, TOut>.AsStep() (heteromorphic, TIn != TOut) ==========

    [Fact]
    public async Task AsStep_Heteromorphic_ConvertsFuncToStep()
    {
        // Arrange
        Func<int, string> func = x => x.ToString();

        // Act
        Step<int, string> step = func.AsStep();
        string result = await step(42);

        // Assert
        Assert.Equal("42", result);
    }

    [Fact]
    public async Task AsStep_Heteromorphic_CompletedSynchronously()
    {
        // Arrange
        Func<int, string> func = x => x.ToString();

        // Act
        Step<int, string> step = func.AsStep();
        ValueTask<string> task = step(5);

        // Assert
        Assert.True(task.IsCompleted);
        Assert.Equal("5", await task);
    }

    [Fact]
    public async Task AsStep_Heteromorphic_WorksWithComplexTypes()
    {
        // Arrange
        Func<string, int> func = s => s.Length;

        // Act
        Step<string, int> step = func.AsStep();
        int result = await step("hello");

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task AsStep_Heteromorphic_CanBeComposedWithThen()
    {
        // Arrange
        Func<int, string> intToStr = x => x.ToString();
        Step<string, int> strLen = (input, _) => input.Length.AsValueTask();

        // Act
        Step<int, int> pipeline = intToStr.AsStep().Then(strLen);
        int result = await pipeline(12345);

        // Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public async Task AsStep_Heteromorphic_CanBeUsedWithWith()
    {
        // Arrange
        Func<int, string> func = x => $"value={x}";

        // Act
        string result = await 42.With(func.AsStep());

        // Assert
        Assert.Equal("value=42", result);
    }

    // ========== Func<T, T>.AsStep() (homomorphic) ==========
    // Note: When TIn == TOut, we must use explicit type argument to disambiguate
    // between AsStep<TIn, TOut> and AsStep<T>.

    [Fact]
    public async Task AsStep_Homomorphic_ConvertsFuncToStep()
    {
        // Arrange
        Func<int, int> func = x => x + 1;

        // Act
        Step<int> step = FunctionalExtensions.AsStep<int>(func);
        int result = await step(10);

        // Assert
        Assert.Equal(11, result);
    }

    [Fact]
    public async Task AsStep_Homomorphic_CompletedSynchronously()
    {
        // Arrange
        Func<string, string> func = s => s.ToUpper();

        // Act
        Step<string> step = FunctionalExtensions.AsStep<string>(func);
        ValueTask<string> task = step("hello");

        // Assert
        Assert.True(task.IsCompleted);
        Assert.Equal("HELLO", await task);
    }

    [Fact]
    public async Task AsStep_Homomorphic_CanBeComposedWithThen()
    {
        // Arrange
        Func<int, int> addOne = x => x + 1;
        Func<int, int> timesTwo = x => x * 2;

        // Act
        Step<int> pipeline = FunctionalExtensions.AsStep<int>(addOne)
            .Then(FunctionalExtensions.AsStep<int>(timesTwo));
        int result = await pipeline(5);

        // Assert
        Assert.Equal(12, result); // (5 + 1) * 2
    }

    [Fact]
    public async Task AsStep_Homomorphic_CanBeUsedWithLayers()
    {
        // Arrange
        Func<int, int> func = x => x * 3;
        Layer<int> addTenLayer = next => async (input, ct) => await next(input, ct) + 10;

        // Act
        Step<int> pipeline = FunctionalExtensions.AsStep<int>(func).Then(addTenLayer);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(25, result); // (5 * 3) + 10
    }

    [Fact]
    public async Task AsStep_Homomorphic_CanBeUsedWithTap()
    {
        // Arrange
        int captured = 0;
        Func<int, int> func = x => x + 1;
        Effect<int> effect = (output, _) =>
        {
            captured = output;
            return ValueTask.CompletedTask;
        };

        // Act
        Step<int> pipeline = FunctionalExtensions.AsStep<int>(func).Tap(effect);
        int result = await pipeline(5);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(6, captured);
    }

    // ========== Ambiguity is itself valuable to test ==========

    [Fact]
    public async Task AsStep_ExplicitHeteromorphicCall_SameTypesDisambiguated()
    {
        // Arrange - when TIn == TOut, we can force heteromorphic via explicit type args
        Func<int, int> func = x => x * 2;

        // Act
        Step<int, int> step = FunctionalExtensions.AsStep<int, int>(func);
        int result = await step(5);

        // Assert
        Assert.Equal(10, result);
    }
}
