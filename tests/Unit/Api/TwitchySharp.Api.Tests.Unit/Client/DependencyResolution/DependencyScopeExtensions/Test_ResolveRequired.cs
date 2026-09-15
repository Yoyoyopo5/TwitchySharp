using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.DependencyResolution.DependencyScopeExtensions;

public class Test_ResolveRequired
{
    [Fact]
    public async Task ResolveRequired_OnCollectionWithNullValue_ReturnsMissingRequiredDependencyError()
    {
        ImmutableRequestDependencyCollection dc = new();

        StubDependencyScope scope = new(dc);

        await scope.ResolveRequired<string>(TestContext.Current.CancellationToken).MatchAsync(
            e =>
            {
                MissingRequiredDependencyError mrde = Assert.IsType<MissingRequiredDependencyError>(e);
                Assert.Equal(typeof(string), mrde.DependencyType);
                return string.Empty;
            },
            v => throw new Exception("No error was returned.")
            );
    }

    [Fact]
    public async Task ResolveRequired_OnCollectionWithNonNullValue_ReturnsValue()
    {
        const string EXPECTED = "test_string";

        ImmutableRequestDependencyCollection dc = new ImmutableRequestDependencyCollection()
            .SetFixed(EXPECTED);

        StubDependencyScope scope = new(dc);

        await scope.ResolveRequired<string>(TestContext.Current.CancellationToken).MatchAsync(
            e => throw new Exception(e.Message),
            v =>
            {
                Assert.Equal(EXPECTED, v);
                return v;
            }
            );
    }
}
