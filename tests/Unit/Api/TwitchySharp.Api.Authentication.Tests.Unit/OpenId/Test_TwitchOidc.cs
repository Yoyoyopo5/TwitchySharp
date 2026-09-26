using Microsoft.IdentityModel.JsonWebTokens;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Api.Tests.Unit.Authorization;

public class Test_TwitchOidc
{
    [Fact]
    public void FromJsonWebToken_JsonWebToken_ReturnsTwitchOidc()
    {
        const string STUB_JWT_STRING = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTYiLCJpYXQiOjE1MTYyMzkwMjIsImF1ZCI6IjEyMzQ1IiwiYXpwIjoiMTIzNDUiLCJleHAiOjE1MTYyMzkwMjIsImlzcyI6Imh0dHBzOi8vdHdpdGNoLnR2Iiwibm9uY2UiOiJub25jZV9zdHJpbmciLCJlbWFpbCI6Im1lQHRlc3RtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJwaWN0dXJlIjoiaHR0cHM6Ly90ZXN0LmNvbS9waWN0dXJlLmpwZyIsInByZWZlcnJlZF91c2VybmFtZSI6InVzZXIiLCJ1cGRhdGVkX2F0IjoxNjQzOTA0OTc2fQ.ZXf5mJrz6cmwL0VEkndN-jMHaiOcKxGu_8wl9Oe8Nd4";
        JsonWebToken stubWebToken = new(STUB_JWT_STRING);
        TwitchOidc mockOidc = new()
        {
            Sub = new("123456"),
            Iat = DateTimeOffset.FromUnixTimeSeconds(1516239022),
            Aud = new("12345"),
            Azp = new("12345"),
            Exp = DateTimeOffset.FromUnixTimeSeconds(1516239022),
            Iss = new("https://twitch.tv"),
            Nonce = "nonce_string",
            Email = new("me@testmail.com"),
            EmailVerified = true,
            Picture = new("https://test.com/picture.jpg"),
            PreferredUsername = new("user"),
            UpdatedAt = DateTimeOffset.Parse("2022-02-03T16:16:16Z")
        };

        TwitchOidc actual = TwitchOidc.FromJsonWebToken(stubWebToken);

        Assert.Equal(mockOidc, actual);
    }
}
