
namespace TwitchySharp.Api.AuthorizationResolution.Tests.Unit;

public class Test_WhereTokenMeetsRequirement
{
    private readonly static AccessTokenDetails.App AppTokenDetails
        = new() { AccessToken = new(), Identity = new(new("")), ExpiresAt = DateTimeOffset.MaxValue };
    private readonly static AccessTokenDetails.ExtensionJwt ExtensionJwtDetails
        = new() { AccessToken = new(), Identity = new(new UserId(""), new ExtensionId("")) };
    private static AccessTokenDetails.User CreateUserTokenDetails(string value, IReadOnlySet<Scope> scopes)
        => new() { AccessToken = new(value), Identity = UserIdentity, Scopes = scopes, ExpiresAt = DateTimeOffset.MaxValue };

    private const string USER_ID_VALUE = "test_user_id";
    private const string CLIENT_ID_VALUE = "test_client_id";
    private readonly static TwitchIdentity.User UserIdentity = new(new(USER_ID_VALUE), new(CLIENT_ID_VALUE));

    [Fact]
    public void FilterTokens_NoSameTypeTokens_EmptyEnumerable()
    {
        HashSet<Scope> scopes = [];
        List<AccessTokenDetails> tokens = [
            AppTokenDetails,
            ExtensionJwtDetails
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = UserIdentity,
            ValidScopes = scopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Empty(matched);
    }

    [Fact]
    public void FilterTokens_SameUserIdentityWithNoRequiredScopes_TokenInEnumerable()
    {
        const string FAKE_TOKEN_VALUE = "test_token";
        HashSet<Scope> scopes = [];
        List<AccessTokenDetails> tokens = [
            CreateUserTokenDetails(FAKE_TOKEN_VALUE, scopes)
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = UserIdentity,
            ValidScopes = scopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Equal(FAKE_TOKEN_VALUE, matched.First().AccessToken.Value);
    }

    [Fact]
    public void FilterTokens_NoMatchingTokensWithScope_EmptyEnumerable()
    {
        HashSet<Scope> scopes = [Scope.ChannelManageAds];
        List<AccessTokenDetails> tokens = [
            CreateUserTokenDetails("a", new HashSet<Scope>() { Scope.BitsRead })
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = UserIdentity,
            ValidScopes = scopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Empty(matched);
    }

    [Fact]
    public void FilterTokens_NoMatchingTokensWithIdentity_EmptyEnumerable()
    {
        HashSet<Scope> scopes = [Scope.ChannelManageAds];
        List<AccessTokenDetails> tokens = [
            CreateUserTokenDetails("a", scopes)
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = new TwitchIdentity.User(new("other_user"), new(CLIENT_ID_VALUE)),
            ValidScopes = scopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Empty(matched);
    }

    [Fact]
    public void FilterTokens_ValidTokenWithExactScope_TokenInEnumerable()
    {
        const string FAKE_TOKEN_VALUE = "test_token";
        HashSet<Scope> validScopes = [Scope.BitsRead];
        HashSet<Scope> userScopes = [Scope.BitsRead];
        List<AccessTokenDetails> tokens = [
            CreateUserTokenDetails(FAKE_TOKEN_VALUE, userScopes)
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = UserIdentity,
            ValidScopes = validScopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Equal(FAKE_TOKEN_VALUE, matched.First().AccessToken.Value);
    }

    [Fact]
    public void FilterTokens_ValidTokenWithOneScope_TokenInEnumerable()
    {
        const string FAKE_TOKEN_VALUE = "test_token";
        HashSet<Scope> validScopes = [Scope.BitsRead, Scope.ChannelManageAds];
        HashSet<Scope> userScopes = [Scope.BitsRead];
        List<AccessTokenDetails> tokens = [
            CreateUserTokenDetails(FAKE_TOKEN_VALUE, userScopes)
            ];
        TwitchRequestAuthorizationContext context = new()
        {
            Identity = UserIdentity,
            ValidScopes = validScopes
        };

        IEnumerable<AccessTokenDetails> matched = tokens.WhereTokenMeetsRequirements(context);

        Assert.Equal(FAKE_TOKEN_VALUE, matched.First().AccessToken.Value);
    }
}
