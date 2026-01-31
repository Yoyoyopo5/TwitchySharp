using System.Web;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Authorization.ClientUrls;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_ImplicitGrantUrl
{
    private const string TestClientId = "hof5gwx0su6owfnys0yan9c87zr6t";
    private const string TestRedirectUri = "http://localhost:3000";
    private const string TestState = "c3ab8aa609ea11e793ae92361f002671";
    private const string TestNonce = "c3ab8aa609ea11e793ae92361f002671";

    [Fact]
    public void Uri_ResponseTypeToken_WhenTokenOnly()
    {
        var url = new ImplicitGrantUrl
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new Uri(TestRedirectUri),
            Scopes = [Scope.ChannelManagePolls],
            IncludeResponseTypes = [TwitchAuthorizationResponseType.Token]
        };

        var query = HttpUtility.ParseQueryString(url.Uri.Query);

        Assert.Equal("token", query["response_type"]);
    }

    [Fact]
    public void Uri_ResponseTypeIdToken_WhenIdTokenOnly()
    {
        var url = new ImplicitGrantUrl
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new Uri(TestRedirectUri),
            Scopes = [Scope.OpenId],
            IncludeResponseTypes = [TwitchAuthorizationResponseType.IdToken]
        };

        var query = HttpUtility.ParseQueryString(url.Uri.Query);

        Assert.Equal("id_token", query["response_type"]);
    }

    [Fact]
    public void Uri_ResponseTypeTokenPlusIdToken_WhenBoth()
    {
        var url = new ImplicitGrantUrl
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new Uri(TestRedirectUri),
            Scopes = [Scope.OpenId],
            IncludeResponseTypes = [TwitchAuthorizationResponseType.Token, TwitchAuthorizationResponseType.IdToken]
        };

        var query = HttpUtility.ParseQueryString(url.Uri.Query);
        var responseType = query["response_type"];

        // ImmutableHashSet order may vary, so check both possibilities
        Assert.True(
            responseType == "token+id_token" || responseType == "id_token+token",
            $"Expected 'token+id_token' or 'id_token+token', got '{responseType}'");
    }

    [Fact]
    public void Uri_MatchesTwitchImplicitExample()
    {
        // Example from Twitch docs:
        // https://id.twitch.tv/oauth2/authorize
        //     ?response_type=token
        //     &client_id=hof5gwx0su6owfnys0yan9c87zr6t
        //     &redirect_uri=http://localhost:3000
        //     &scope=channel%3Amanage%3Apolls+channel%3Aread%3Apolls
        //     &state=c3ab8aa609ea11e793ae92361f002671

        var url = new ImplicitGrantUrl
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new Uri(TestRedirectUri),
            Scopes = [Scope.ChannelManagePolls, Scope.ChannelReadPolls],
            State = TestState,
            IncludeResponseTypes = [TwitchAuthorizationResponseType.Token]
        };

        var uri = url.Uri;
        var query = HttpUtility.ParseQueryString(uri.Query);

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("id.twitch.tv", uri.Host);
        Assert.Equal("/oauth2/authorize", uri.AbsolutePath);
        Assert.Equal("token", query["response_type"]);
        Assert.Equal(TestClientId, query["client_id"]);
        Assert.StartsWith(TestRedirectUri, query["redirect_uri"]);
        Assert.Equal("channel:manage:polls+channel:read:polls", query["scope"]);
        Assert.Equal(TestState, query["state"]);
    }

    [Fact]
    public void Uri_MatchesTwitchOidcExample()
    {
        // OIDC Implicit Grant Flow example:
        // https://id.twitch.tv/oauth2/authorize
        //     ?response_type=token+id_token
        //     &client_id=hof5gwx0su6owfnys0nyan9c87zr6t
        //     &redirect_uri=http://localhost:3000
        //     &scope=channel%3Amanage%3Apolls+channel%3Aread%3Apolls+openid
        //     &state=c3ab8aa609ea11e793ae92361f002671
        //     &nonce=c3ab8aa609ea11e793ae92361f002671

        var url = new ImplicitGrantUrl
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new Uri(TestRedirectUri),
            Scopes = [Scope.ChannelManagePolls, Scope.ChannelReadPolls, Scope.OpenId],
            State = TestState,
            Nonce = TestNonce,
            IncludeResponseTypes = [TwitchAuthorizationResponseType.Token, TwitchAuthorizationResponseType.IdToken]
        };

        var uri = url.Uri;
        var query = HttpUtility.ParseQueryString(uri.Query);

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("id.twitch.tv", uri.Host);
        Assert.Equal(TestClientId, query["client_id"]);
        Assert.StartsWith(TestRedirectUri, query["redirect_uri"]);
        Assert.Equal("channel:manage:polls+channel:read:polls+openid", query["scope"]);
        Assert.Equal(TestState, query["state"]);
        Assert.Equal(TestNonce, query["nonce"]);

        // Check response type contains both (order may vary)
        var responseType = query["response_type"];
        Assert.Contains("token", responseType);
        Assert.Contains("id_token", responseType);
    }
}
