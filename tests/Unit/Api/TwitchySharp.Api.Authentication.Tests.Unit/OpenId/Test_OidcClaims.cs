using System.Text.Json;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_OidcClaims
{
    [Fact]
    public void JsonEncode_IdTokenSingleClaim_ReturnsJsonString()
    {
        OidcClaims stubClaims = new() { IdToken = new HashSet<OidcClaim>() { OidcClaim.Email } };
        const string MOCK_JSON_STRING = """
            {
                "id_token": {
                    "email": null
                },
                "userinfo": {
                }
            }
            """;
        JsonElement mockJson = JsonDocument.Parse(MOCK_JSON_STRING).RootElement;
        string mockJsonString = JsonSerializer.Serialize(mockJson);

        JsonElement resultJson = JsonDocument.Parse(stubClaims.JsonEncode()).RootElement;
        string actual = JsonSerializer.Serialize(resultJson);

        Assert.Equal(mockJsonString, actual);
    }

    [Fact]
    public void JsonEncode_IdTokenUserInfoClaims_ReturnsJsonString()
    {
        OidcClaims stubClaims = new()
        {
            IdToken = new HashSet<OidcClaim>() { OidcClaim.Email, OidcClaim.EmailVerified, OidcClaim.Picture, OidcClaim.PreferredUsername, OidcClaim.UpdatedAt },
            Userinfo = new HashSet<OidcClaim>() { OidcClaim.Email, OidcClaim.EmailVerified, OidcClaim.Picture, OidcClaim.PreferredUsername, OidcClaim.UpdatedAt }
        };
        const string MOCK_JSON_STRING = """
            {
                "id_token": {
                    "email": null,
                    "email_verified": null,
                    "picture": null,
                    "preferred_username": null,
                    "updated_at": null
                },
                "userinfo": {
                    "email": null,
                    "email_verified": null,
                    "picture": null,
                    "preferred_username": null,
                    "updated_at": null
                }
            }
            """;
        JsonElement mockJson = JsonDocument.Parse(MOCK_JSON_STRING).RootElement;
        string mockJsonString = JsonSerializer.Serialize(mockJson);

        JsonElement resultJson = JsonDocument.Parse(stubClaims.JsonEncode()).RootElement;
        string actual = JsonSerializer.Serialize(resultJson);

        Assert.Equal(mockJsonString, actual);
    }
}
