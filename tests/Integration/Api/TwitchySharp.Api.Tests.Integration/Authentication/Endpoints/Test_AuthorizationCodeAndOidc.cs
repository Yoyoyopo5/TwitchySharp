using System.Runtime.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TwitchySharp.Api.Authentication;

namespace TwitchySharp.Api.Tests.Integration.Authentication;

public class Test_AuthorizationCodeAndOidc(TwitchApiIntegrationTestFixture fixture)
{
    private class AuthorizationCodeFormData
    {
        [DataMember(Name = "client_id")]
        public ClientId ClientId { get; set; } = new(string.Empty);
        [DataMember(Name = "client_secret")]
        public ClientSecret ClientSecret { get; set; } = new(string.Empty);
        [DataMember(Name = "code")]
        public string Code { get; set; } = string.Empty;
        [DataMember(Name = "grant_type")]
        public string GrantType { get; set; } = string.Empty;
        [DataMember(Name = "redirect_uri")]
        public RedirectUri RedirectUri { get; set; } = new(string.Empty);
    }

    private static Func<AuthorizationCodeFormData, IResult> CreateAuthorizationCodeEndpointDelegate(AuthorizationCodeResponseContent responseContent)
        => ([FromForm] AuthorizationCodeFormData data) =>
        {
            return data.GrantType != "authorization_code"
                ? Results.BadRequest("grant_type must be 'authorization_code'.")
                : Results.Ok(responseContent);
        };

    [Fact]
    public async Task SendAsync_AuthorizationCodeRequestWithTwitchOidc_ContainsExpectedData()
    {
        const string OIDC_JWT = """
            eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyMzQ3NCIsImF1ZCI6IjEyMzQ1IiwiYXpwIjoiMTIzNDUiLCJleHAiOjE3ODg4MzEwOTIsImlhdCI6MTc4ODgzMTA5MiwiaXNzIjoidHdpdGNoLnR2IiwiZW1haWwiOiJtZUB0d2l0Y2gudHYiLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwibm9uY2UiOm51bGwsInBpY3R1cmUiOiJodHRwczovL2ltYWdlcy5jb20vbXlfcHJvZmlsZS5qcGciLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJZb3lveW9wbzUiLCJ1cGRhdGVkX2F0IjoxNzg4ODMxMDkyfQ.dPGnHIf7c1-YJjRFxxo1Fhj0a6YXiOlv-EBNqFfPw-k
            """;
        TwitchOidc expectedOidc = TwitchOidc.FromJsonWebToken(new JsonWebToken(OIDC_JWT));

        AuthorizationCodeResponseContent expectedResponseContent = new()
        {
            AccessToken = new("12345"),
            ExpiresIn = TimeSpan.FromSeconds(250),
            RefreshToken = new("678910"),
            TokenType = "bearer",
            Scope = [Scope.UserReadEmail, Scope.UserReadFollows],
            IdToken = OIDC_JWT
        };

        using IDisposable endpoint = fixture.TestServer.Map(HttpMethod.Post, "/oauth2/token", CreateAuthorizationCodeEndpointDelegate(expectedResponseContent));

        AuthorizationCodeRequest request = new()
        {
            Host = "localhost",
            ClientId = new("fake_client_id"),
            ClientSecret = new("very_real_secret"),
            Code = "my_auth_code",
            RedirectUri = new("https://redirecthere.zip")
        };

        TwitchResponse<AuthorizationCodeResponseContent> response = await fixture.TestServer.GetDefaultTwitchClient().SendAsync(request, TestContext.Current.CancellationToken);

        TwitchOidc? oidc = response.Content.GetOidc();

        Assert.Equal(expectedOidc, oidc);
        Assert.Equal(expectedResponseContent.AccessToken, response.Content.AccessToken);
        Assert.Equal(expectedResponseContent.ExpiresIn, response.Content.ExpiresIn);
        Assert.Equal(expectedResponseContent.RefreshToken, response.Content.RefreshToken);
        Assert.Equal(expectedResponseContent.TokenType, response.Content.TokenType);
        Assert.Equal(expectedResponseContent.Scope.AsEnumerable(), response.Content.Scope);
    }
}
