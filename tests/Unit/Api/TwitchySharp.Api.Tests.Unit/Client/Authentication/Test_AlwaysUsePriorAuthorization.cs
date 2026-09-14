using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.Authentication;

public class Test_AlwaysUsePriorAuthorization
{
    private class TokenTypeCapturingTwitchClient
    {
        private BearerTokenType? _typeUsed;
        private readonly TwitchClient _client;
        public TokenTypeCapturingTwitchClient(ITwitchRequestAuthenticationContext<TwitchIdentity> authenticationContext)
        {
            _client = new TwitchClient() { Resolvers = new ImmutableRequestDependencyCollection() }
            .SetFixed(authenticationContext)
            .AlwaysUsePriorAuthorization()
            .Configure<TwitchClient, BearerTokenType?>(next => (scope, ct) =>
                next(scope, ct).MapAsync(tokenType =>
                {
                    _typeUsed = tokenType;
                    return tokenType;
                }
            ))
            .UseStubResponse<BearerTokenType?>();
        }

        public async Task<BearerTokenType?> SendStub()
        {
            await _client.SendAsync(new StubTwitchRequest(), TestContext.Current.CancellationToken);
            return _typeUsed;
        }
    }

    [Fact]
    public async Task SendAsync_RequestSupportingPriorAuthorization_UsesAppAccessToken()
    {
        UserSupportingPriorAuthorizationAuthenticationContext context = new()
        {
            Identity = new(new("12345"))
        };
        TokenTypeCapturingTwitchClient stubClient = new(context);

        Assert.Equal(BearerTokenType.AppAccessToken, await stubClient.SendStub());
    }

    [Fact]
    public async Task SendAsync_RequestNotSupportingPriorAuthorization_DoesNotUseAppAccessToken()
    {
        UserWithScopesAuthenticationContext context = new()
        {
            Identity = new(new("12345"))
        };
        TokenTypeCapturingTwitchClient stubClient = new(context);

        Assert.NotEqual(BearerTokenType.AppAccessToken, await stubClient.SendStub());
    }
}
