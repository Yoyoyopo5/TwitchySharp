using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Tests.Integration.Controllers;

public record HttpError(string Message, HttpStatusCode Status) : Error(Message);

public class AuthorizationControllerOptions
{
    public required ClientId ValidClientId { get; set; }
    public required ClientSecret ValidClientSecret { get; set; }
    public required string ValidAuthorizationCode { get; set; }
    public required RefreshToken ValidRefreshToken { get; set; }
    public required RedirectUri ValidRedirectUri { get; set; }
}

/// <summary>
/// Mock controller for Twitch Authorization endpoints (/oauth2/*).
/// </summary>
[ApiController]
[Route("oauth2")]
public class MockAuthorizationController(AuthorizationControllerOptions options) : ControllerBase
{
    private readonly AuthorizationControllerOptions _options = options;

    [HttpPost("token")]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult Token([FromForm] TokenRequest request)
        => request.GrantType switch
        {
            "authorization_code" => HandleAuthorizationCodeGrant(request),
            "client_credentials" => HandleClientCredentialsGrant(request),
            "refresh_token" => HandleRefreshTokenGrant(request),
            _ => BadRequest(new { error = "unsupported_grant_type", message = $"Grant type '{request.GrantType}' is not supported" })
        };

    public static TwitchOidc TestOidc { get; } = new()
    {
        Aud = new("12345"),
        Azp = new("12345"),
        Exp = DateTimeOffset.FromUnixTimeSeconds(1782847858),
        Iat = DateTimeOffset.FromUnixTimeSeconds(1782847858),
        Iss = new("twitch.tv"),
        Sub = new("1234567890"),
        Nonce = "example",
        Email = new("user@example.com"),
        EmailVerified = true,
        PreferredUsername = new("test_user"),
        UpdatedAt = DateTimeOffset.FromUnixTimeSeconds(1782847858)
    };

    private ObjectResult HandleAuthorizationCodeGrant(TokenRequest request)
        => new Validation<TokenRequest>(request)
            .HasExpectedValue(_options.ValidClientId)
            .HasExpectedValue(_options.ValidClientSecret)
            .HasExpectedCode(_options.ValidAuthorizationCode)
            .HasExpectedValue(_options.ValidRedirectUri)
            .Match<ObjectResult>(
            e => BadRequest((e as HttpError)!.Message),
            request => Ok(new
            {
                access_token = TwitchApiTestFixture.TEST_ACCESS_TOKEN,
                expires_in = 14124,
                refresh_token = TwitchApiTestFixture.TEST_REFRESH_TOKEN,
                scope = new[] { "channel:moderate", "chat:edit", "chat:read" },
                token_type = "bearer",
                id_token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiIxMjM0NSIsImF6cCI6IjEyMzQ1IiwiZXhwIjoxNzgyODQ3ODU4LCJpYXQiOjE3ODI4NDc4NTgsImlzcyI6InR3aXRjaC50diIsInN1YiI6IjEyMzQ1Njc4OTAiLCJub25jZSI6ImV4YW1wbGUiLCJlbWFpbCI6InVzZXJAZXhhbXBsZS5jb20iLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwicHJlZmVycmVkX3VzZXJuYW1lIjoidGVzdF91c2VyIiwidXBkYXRlZF9hdCI6MTc4Mjg0Nzg1OH0.akfmVzFVQD8y3vowkV6qV6u8bFDndarcGRahF7pnQu8ofpTX4Sf8ZtfZ2V_N04pEEugNziHcAZvzFvCoYhGkML0MzB3hgctfeHv1vaMr5e9sgxXY6p-TG6PY3t_hoXuTnPKiksPsXQ1czzYBBTb3pG8D2CYa8ozODOQSKnbVnwchpWbukKI4-wd4wnhB6VQnFMRYGHu4y0DPMg7rzUIEqQ8NOSX5h-q9qw2EEKDyETjbPAquejjMel1f_7hdw1k1hiabsGliGLi8faIhY8ucyySNav-7twiObjBpF-oxE6xE2KmbrKmu-lP7DHhBSgvbyN_cy8uQPelC7LNCt1IvlA"
            }));

    private ObjectResult HandleClientCredentialsGrant(TokenRequest request)
        => new Validation<TokenRequest>(request)
            .HasExpectedValue(_options.ValidClientId)
            .HasExpectedValue(_options.ValidClientSecret)
            .Match<ObjectResult>(
            e => BadRequest((e as HttpError)!.Message),
            r => Ok(new
            {
                access_token = TwitchApiTestFixture.TEST_ACCESS_TOKEN,
                expires_in = 5011271,
                token_type = "bearer"
            })
            );

    private ObjectResult HandleRefreshTokenGrant(TokenRequest request)
        => new Validation<TokenRequest>(request)
            .HasExpectedValue(_options.ValidClientId)
            .HasExpectedValue(_options.ValidClientSecret)
            .HasExpectedValue(_options.ValidRefreshToken)
            .Match<ObjectResult>(
            e => BadRequest((e as HttpError)!.Message),
            r => Ok(new
            {
                access_token = "new_" + TwitchApiTestFixture.TEST_ACCESS_TOKEN,
                expires_in = 14124,
                refresh_token = "new_" + TwitchApiTestFixture.TEST_REFRESH_TOKEN,
                scope = new[] { "channel:moderate", "chat:edit", "chat:read" },
                token_type = "bearer"
            })
            );
}

public record TokenRequest
{
    [FromForm(Name = "client_id")]
    public string? ClientId { get; init; }

    [FromForm(Name = "client_secret")]
    public string? ClientSecret { get; init; }

    [FromForm(Name = "code")]
    public string? Code { get; init; }

    [FromForm(Name = "grant_type")]
    public string? GrantType { get; init; }

    [FromForm(Name = "redirect_uri")]
    public string? RedirectUri { get; init; }

    [FromForm(Name = "refresh_token")]
    public string? RefreshToken { get; init; }
}

public static class TokenRequestExtensions
{
    public static Validation<T> True<T>(this Validation<T> input, string errorMessage, Func<T, bool> isValid)
        => input.Bind(i => isValid(i)
        ? input
        : new HttpError(errorMessage, HttpStatusCode.BadRequest)
        );

    public static Validation<TokenRequest> Required(this Validation<TokenRequest> request, string propertyName, Func<TokenRequest, string?> accessor)
        => request.True($"{propertyName} is required.", r => !string.IsNullOrWhiteSpace(accessor(r)));

    public static Validation<TokenRequest> HasExpectedValue(this Validation<TokenRequest> request, ClientId expected)
        => request
            .Required("client_id", r => r.ClientId)
            .True("Invalid client_id", r => r.ClientId == expected);

    public static Validation<TokenRequest> HasExpectedValue(this Validation<TokenRequest> request, ClientSecret expected)
        => request
            .Required("client_secret", r => r.ClientSecret)
            .True("Invalid client_secret", r => r.ClientSecret == expected);

    public static Validation<TokenRequest> HasExpectedCode(this Validation<TokenRequest> request, string code)
        => request
            .Required("code", r => r.Code)
            .True("Invalid code", r => r.Code == code);

    public static Validation<TokenRequest> HasExpectedValue(this Validation<TokenRequest> request, RedirectUri expected)
        => request
            .Required("redirect_uri", r => r.RedirectUri)
            .True("Invalid redirect_uri", r => r.RedirectUri == expected);

    public static Validation<TokenRequest> HasExpectedValue(this Validation<TokenRequest> request, RefreshToken expected)
        => request
            .Required("refresh_token", r => r.RefreshToken)
            .True("Invalid refresh_token", r => r.RefreshToken == expected);
}
