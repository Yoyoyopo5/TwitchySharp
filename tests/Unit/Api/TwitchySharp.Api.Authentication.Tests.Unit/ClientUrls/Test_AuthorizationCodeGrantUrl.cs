using System.Collections.Specialized;
using System.Web;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_AuthorizationCodeGrantUrl
{
    private const string TestClientId = "hof5gwx0su6owfnys0nyan9c87zr6t";
    private const string TestRedirectUri = "http://localhost:3000/"; // System.Uri does not like to 
    private const string TestState = "c3ab8aa609ea11e793ae92361f002671";
    private const string TestNonce = "c3ab8aa609ea11e793ae92361f002671";

    [Fact]
    public void Uri_MatchesTwitchExample()
    {
        // Example from Twitch docs:
        // https://id.twitch.tv/oauth2/authorize
        //     ?response_type=code
        //     &client_id=hof5gwx0su6owfnys0nyan9c87zr6t
        //     &redirect_uri=http://localhost:3000
        //     &scope=channel%3Amanage%3Apolls+channel%3Aread%3Apolls <-- we use space instead of "+" due to Uri.EscapeDataString behavior.
        //     &state=c3ab8aa609ea11e793ae92361f002671

        AuthorizationCodeGrantUrl url = new()
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new RedirectUri(TestRedirectUri),
            Scopes = [Scope.ChannelManagePolls, Scope.ChannelReadPolls],
            State = TestState
        };

        Uri uri = url.Uri;
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        Assert.Equal("https", uri.Scheme);
        Assert.Equal("id.twitch.tv", uri.Host);
        Assert.Equal("/oauth2/authorize", uri.AbsolutePath);
        Assert.Equal("code", query["response_type"]);
        Assert.Equal(TestClientId, query["client_id"]);
        Assert.Equal(TestRedirectUri, query["redirect_uri"]);
        Assert.Equal("channel:manage:polls channel:read:polls", query["scope"]);
        Assert.Equal(TestState, query["state"]);
        Assert.Null(query["force_verify"]);
        Assert.Null(query["nonce"]);
    }

    [Fact]
    public void Uri_MatchesTwitchOidcExample()
    {
        // OIDC Authorization Code Flow example:
        // https://id.twitch.tv/oauth2/authorize
        //     ?response_type=code
        //     &client_id=hof5gwx0su6owfnys0nyan9c87zr6t
        //     &redirect_uri=http://localhost:3000
        //     &scope=channel%3Amanage%3Apolls+channel%3Aread%3Apolls+openid
        //     &state=c3ab8aa609ea11e793ae92361f002671
        //     &nonce=c3ab8aa609ea11e793ae92361f002671

        AuthorizationCodeGrantUrl url = new()
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new RedirectUri(TestRedirectUri),
            Scopes = [Scope.ChannelManagePolls, Scope.ChannelReadPolls, Scope.OpenId],
            State = TestState,
            Nonce = TestNonce
        };

        Uri uri = url.Uri;
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        Assert.Equal("code", query["response_type"]);
        Assert.Equal(TestClientId, query["client_id"]);
        Assert.Equal(TestRedirectUri, query["redirect_uri"]);
        Assert.Equal("channel:manage:polls channel:read:polls openid", query["scope"]);
        Assert.Equal(TestState, query["state"]);
        Assert.Equal(TestNonce, query["nonce"]);
    }

    [Fact]
    public void Uri_EmptyScopes_IncludesEmptyScope()
    {
        AuthorizationCodeGrantUrl url = new()
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new RedirectUri(TestRedirectUri),
            Scopes = []
        };

        Uri uri = url.Uri;
        NameValueCollection query = HttpUtility.ParseQueryString(uri.Query);

        Assert.Equal(string.Empty, query["scope"]);
    }

    [Fact]
    public void Uri_CustomHost_UsesProvidedHost()
    {
        AuthorizationCodeGrantUrl url = new()
        {
            ClientId = new ClientId(TestClientId),
            RedirectUri = new RedirectUri(TestRedirectUri),
            Scopes = [Scope.OpenId],
            Host = "test.example.com"
        };

        Assert.Equal("test.example.com", url.Uri.Host);
    }
}
