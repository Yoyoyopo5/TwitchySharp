using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Unit.Authorization;
public class Test_OidcClaims
{
    [Fact]
    public void JsonEncode_IdTokenSingleClaim_ReturnsJsonString()
    {
        OidcClaims stubClaims = new() { IdToken = [OidcClaim.Email] };
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
            IdToken = [OidcClaim.Email, OidcClaim.EmailVerified, OidcClaim.Picture, OidcClaim.PreferredUsername, OidcClaim.UpdatedAt],
            Userinfo = [OidcClaim.Email, OidcClaim.EmailVerified, OidcClaim.Picture, OidcClaim.PreferredUsername, OidcClaim.UpdatedAt]
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
