using Microsoft.AspNetCore.Mvc;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Api.Tests.Integration.Models;

namespace TwitchySharp.Api.Tests.Integration.Controllers;

/// <summary>
/// Mock controller for Twitch Authorization endpoints (/oauth2/*).
/// </summary>
[ApiController]
[Route("oauth2")]
public class MockAuthorizationController(MockResponseConfigurator config) : ControllerBase
{
    private readonly MockResponseConfigurator _config = config;

    /// <summary>
    /// Mock token endpoint supporting multiple grant types.
    /// </summary>
    [HttpPost("token")]
    [Consumes("application/x-www-form-urlencoded")]
    public IActionResult Token([FromForm] TokenRequest request)
    {
        // Check for forced error responses
        if (_config.ForceStatusCode.HasValue)
        {
            return StatusCode((int)_config.ForceStatusCode.Value, new
            {
                error = "forced_error",
                message = _config.ForceErrorMessage ?? "Forced error for testing"
            });
        }

        return request.GrantType switch
        {
            "authorization_code" => HandleAuthorizationCodeGrant(request),
            "client_credentials" => HandleClientCredentialsGrant(request),
            "refresh_token" => HandleRefreshTokenGrant(request),
            _ => BadRequest(new { error = "unsupported_grant_type", message = $"Grant type '{request.GrantType}' is not supported" })
        };
    }

    private IActionResult HandleAuthorizationCodeGrant(TokenRequest request)
    {
        // Validate required fields
        if (string.IsNullOrEmpty(request.ClientId))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter: client_id"
            });

        if (string.IsNullOrEmpty(request.ClientSecret))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter: client_secret"
            });

        if (string.IsNullOrEmpty(request.Code))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter: code"
            });

        if (string.IsNullOrEmpty(request.RedirectUri))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter: redirect_uri"
            });

        // Simulate invalid code
        if (request.Code == "invalid_code")
            return BadRequest(new
            {
                error = "invalid_grant",
                message = "Invalid authorization code"
            });

        // Success response matching Twitch format
        return Ok(new
        {
            access_token = TwitchApiTestFixture.TEST_ACCESS_TOKEN,
            expires_in = 14124,
            refresh_token = TwitchApiTestFixture.TEST_REFRESH_TOKEN,
            scope = new[] { "channel:moderate", "chat:edit", "chat:read" },
            token_type = "bearer"
        });
    }

    private IActionResult HandleClientCredentialsGrant(TokenRequest request)
    {
        if (string.IsNullOrEmpty(request.ClientId) || string.IsNullOrEmpty(request.ClientSecret))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter"
            });

        return Ok(new
        {
            access_token = TwitchApiTestFixture.TEST_ACCESS_TOKEN,
            expires_in = 5011271,
            token_type = "bearer"
        });
    }

    private IActionResult HandleRefreshTokenGrant(TokenRequest request)
    {
        if (string.IsNullOrEmpty(request.ClientId) ||
            string.IsNullOrEmpty(request.ClientSecret) ||
            string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest(new
            {
                error = "invalid_request",
                message = "Missing required parameter"
            });

        if (request.RefreshToken == "invalid_refresh_token")
            return BadRequest(new
            {
                error = "invalid_grant",
                message = "Invalid refresh token"
            });

        return Ok(new
        {
            access_token = "new_" + TwitchApiTestFixture.TEST_ACCESS_TOKEN,
            expires_in = 14124,
            refresh_token = "new_" + TwitchApiTestFixture.TEST_REFRESH_TOKEN,
            scope = new[] { "channel:moderate", "chat:edit", "chat:read" },
            token_type = "bearer"
        });
    }
}

/// <summary>
/// Model for token request form data.
/// </summary>
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
