using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.Unit.Authorization;
public class Test_TwitchOidc
{
    [Fact]
    public void FromJsonWebToken_JsonWebToken_ReturnsTwitchOidc()
    {
        const string STUB_JWT_STRING = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTYiLCJpYXQiOjE1MTYyMzkwMjIsImF1ZCI6IjEyMzQ1IiwiYXpwIjoiMTIzNDUiLCJleHAiOjE1MTYyMzkwMjIsImlzcyI6Imh0dHBzOi8vdHdpdGNoLnR2Iiwibm9uY2UiOiJub25jZV9zdHJpbmciLCJlbWFpbCI6Im1lQHRlc3RtYWlsLmNvbSIsImVtYWlsX3ZlcmlmaWVkIjp0cnVlLCJwaWN0dXJlIjoiaHR0cHM6Ly90ZXN0LmNvbS9waWN0dXJlLmpwZyIsInByZWZlcnJlZF91c2VybmFtZSI6InVzZXIiLCJ1cGRhdGVkX2F0IjoiMjAyMi0wMi0wM1QxNjoxNjoxNi45Njg1MDlaIn0.38D1a9YDSYxwUi3k-uLgoJcb2SGuP2PLShThKp6G0uI";
        JsonWebToken stubWebToken = new(STUB_JWT_STRING);
        TwitchOidc mockOidc = new()
        {
            Sub = "123456",
            Iat = DateTimeOffset.FromUnixTimeSeconds(1516239022),
            Aud = "12345",
            Azp = "12345",
            Exp = DateTimeOffset.FromUnixTimeSeconds(1516239022),
            Iss = "https://twitch.tv",
            Nonce = "nonce_string",
            Email = "me@testmail.com",
            EmailVerified = true,
            Picture = "https://test.com/picture.jpg",
            PreferredUsername = "user",
            UpdatedAt = DateTimeOffset.Parse("2022-02-03T16:16:16.968509Z")
        };

        TwitchOidc actual = TwitchOidc.FromJsonWebToken(stubWebToken);

        Assert.Equal(mockOidc, actual);
    }
}
