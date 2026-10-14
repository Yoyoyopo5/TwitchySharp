using TwitchySharp.Api.Tests.Unit.Client.DependencyResolution;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Unit.Client.Authentication;

public class Test_AlwaysUsePriorAuthorization
{
    private class TokenTypeCapturingMiddleware(Action<BearerTokenType?> tokenTypeUsed) : TwitchClient.Middleware
    {
        protected override async ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send, ITwitchRequestDependencyScope scope, CancellationToken ct)
        {
            await scope.ResolveOrDefault<BearerTokenType?>(ct).MapAsync(t =>
            {
                tokenTypeUsed(t);
                return t;
            });

            return await send(scope, ct);
        }
    }

    private record StubAuthenticatedRequest(ITwitchRequestAuthenticationContext<TwitchIdentity> context) : TwitchRequest<object>,
        IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
    {
        public override HttpMethod Method => HttpMethod.Get;
        public override Uri RequestUri => new("http://localhost/");

        public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; } = context;

        public override ValueTask<TwitchResponse<object>> CreateResponse(HttpResponseMessage response, CancellationToken ct)
            => ValueTask.FromResult(new TwitchResponse<object>(new object()));
    }

    [Fact]
    public async Task ResolveTokenType_RequestSupportingPriorAuthorization_ReturnsAppAccessToken()
    {
        UserSupportingPriorAuthorizationAuthenticationContext context = new()
        {
            Identity = new(new("12345"))
        };

        TwitchClient stubClient = new TwitchClient()
            .AlwaysUsePriorAuthorization();

        StubDependencyScope scope = new(stubClient.Resolvers) { Request = new StubAuthenticatedRequest(context) };

        Result<BearerTokenType?> result = await scope.ResolveOrDefault<BearerTokenType?>(TestContext.Current.CancellationToken);

        Assert.Equal(BearerTokenType.AppAccessToken, result);
    }

    [Fact]
    public async Task ResolveTokenType_RequestNotSupportingPriorAuthorization_DoesNotReturnAppAccessToken()
    {
        UserWithScopesAuthenticationContext context = new()
        {
            Identity = new(new("12345"))
        };
        TwitchClient stubClient = new TwitchClient()
            .AlwaysUsePriorAuthorization();

        StubDependencyScope scope = new(stubClient.Resolvers) { Request = new StubAuthenticatedRequest(context) };

        Result<BearerTokenType?> result = await scope.ResolveOrDefault<BearerTokenType?>(TestContext.Current.CancellationToken);

        Assert.NotEqual(BearerTokenType.AppAccessToken, result);
    }
}
