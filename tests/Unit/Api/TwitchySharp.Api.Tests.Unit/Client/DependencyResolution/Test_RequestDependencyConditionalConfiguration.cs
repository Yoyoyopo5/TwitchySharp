using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;

public class Test_RequestDependencyConditionalConfiguration
{
    [Fact]
    public async Task ResolveOrDefault_WithConditionalConfiguration_TruePredicate_ConditionalResolverCalled()
    {
        const string EXPECTED = "hello";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .When(scope => true)
            .SetFixed(EXPECTED)
            .EndWhen();

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
    public async Task ResolveOrDefault_WithConditionalConfiguration_FalsePredicate_ConditionalResolverNotCalled()
    {
        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .When(scope => false)
            .SetFixed("not expected")
            .EndWhen();

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Null(v);
                return v;
            });
    }

    [Fact]
    public async Task ResolveOrDefault_WithConditionalConfigurationAndPreviousResolver_FalsePredicate_PreviousResolverCalled()
    {
        const string EXPECTED = "expected";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED)
            .When(scope => false)
            .SetFixed("not expected")
            .EndWhen();

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
    public async Task ResolveOrDefault_WithConditionalConfigurationUsingPreviousResolver_TruePredicate_PreviousResolverCalled()
    {
        const string EXPECTED = "expected";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED)
            .When(scope => true)
            .Configure<RequestDependencyConditionalConfiguration<ImmutableRequestDependencyCollection>, string?>(next => (scope, ct) => next(scope, ct))
            .EndWhen();

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string?>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            });
    }

    [Fact]
    public async Task ResolveOrDefault_WithMultipleConditionalConfigurations_RootResolverCalledOnce()
    {
        int rootEvaluationCount = 0;

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetResolver(scope =>
            {
                rootEvaluationCount++;
                return "hello";
            })
            .When(scope => true)
            .Configure<RequestDependencyConditionalConfiguration<ImmutableRequestDependencyCollection>, string?>(next => (scope, ct) => next(scope, ct))
            .EndWhen()
            .When(scope => true)
            .Configure<RequestDependencyConditionalConfiguration<ImmutableRequestDependencyCollection>, string?>(next => (scope, ct) => next(scope, ct))
            .EndWhen()
            .When(scope => false)
            .Configure<RequestDependencyConditionalConfiguration<ImmutableRequestDependencyCollection>, string?>(next => (scope, ct) => next(scope, ct))
            .EndWhen();

        StubDependencyScope scope = new(dc);

        await scope.ResolveOrDefault<string?>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(1, rootEvaluationCount);
                return v;
            });
    }
}
