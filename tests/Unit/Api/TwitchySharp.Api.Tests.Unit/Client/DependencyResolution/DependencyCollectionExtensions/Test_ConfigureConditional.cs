using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyCollectionExtensions;

public class Test_ConfigureConditional
{
    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolver_PredicateCalledWithResolvedValue()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED)
            .ConfigureConditional<ImmutableRequestDependencyCollection, string?>(
                (value, scope, ct) =>
                {
                    Assert.Equal(EXPECTED, value);
                    return ValueTask.FromResult<Validation<bool>>(true);
                },
                (value, scope, ct) => ValueTask.FromResult<Validation<string?>>(value)
            );

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string?>(TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolver_FalsePredicate_ConditionalResolveNotCalledAndOriginalValueReturned()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED)
            .ConfigureConditional<ImmutableRequestDependencyCollection, string?>(
                (value, scope, ct) => ValueTask.FromResult<Validation<bool>>(false),
                (value, scope, ct) => throw new InvalidOperationException("Conditional resolver should not be called.")
            );

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }

    [Fact]
    public async Task ResolveOrDefault_OnCollectionWithResolver_TruePredicate_ConditionalResolveCalledWithResolvedValue()
    {
        const string EXPECTED = "abc";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed("def")
            .ConfigureConditional<ImmutableRequestDependencyCollection, string?>(
                (value, scope, ct) => ValueTask.FromResult<Validation<bool>>(true),
                (value, scope, ct) =>
                {
                    Assert.Equal("def", value);
                    return ValueTask.FromResult<Validation<string?>>(EXPECTED);
                }
            );

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }
}
