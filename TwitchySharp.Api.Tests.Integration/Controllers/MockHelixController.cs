using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TwitchySharp.Api.Tests.Integration.Fixtures;
using TwitchySharp.Api.Tests.Integration.Models;

namespace TwitchySharp.Api.Tests.Integration.Controllers;

/// <summary>
/// Mock controller for Twitch Helix API endpoints (/helix/*).
/// </summary>
[ApiController]
[Route("helix")]
public class MockHelixController(MockResponseConfigurator config) : ControllerBase
{
    private readonly MockResponseConfigurator _config = config;

    /// <summary>
    /// Validates that required Twitch authorization headers are present.
    /// </summary>
    /// <returns>An error result if validation fails, null if validation passes.</returns>
    private UnauthorizedObjectResult? ValidateAuthHeaders()
    {
        if (!Request.Headers.TryGetValue("Client-Id", out var clientId) ||
            string.IsNullOrEmpty(clientId))
        {
            return Unauthorized(new TwitchErrorResponse
            {
                Error = "Unauthorized",
                Status = 401,
                Message = "Missing Client-Id header"
            });
        }

        if (!Request.Headers.TryGetValue("Authorization", out var auth) ||
            !auth.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new TwitchErrorResponse
            {
                Error = "Unauthorized",
                Status = 401,
                Message = "Missing or invalid Authorization header"
            });
        }

        return null; // Validation passed
    }

    /// <summary>
    /// Adds Twitch rate limit headers to the response.
    /// </summary>
    private void AddRateLimitHeaders()
    {
        Response.Headers["Ratelimit-Limit"] = _config.RateLimitLimit.ToString();
        Response.Headers["Ratelimit-Remaining"] = _config.RateLimitRemaining.ToString();
        Response.Headers["Ratelimit-Reset"] = _config.RateLimitReset.ToUnixTimeSeconds().ToString();
    }

    /// <summary>
    /// POST /helix/moderation/warnings - Warn Chat User
    /// </summary>
    [HttpPost("moderation/warnings")]
    public IActionResult WarnChatUser(
        [FromQuery(Name = "broadcaster_id")] string? broadcasterId,
        [FromQuery(Name = "moderator_id")] string? moderatorId,
        [FromBody] WarnChatUserRequestBody? body)
    {
        // Check forced error first
        if (_config.ForceStatusCode.HasValue)
        {
            return StatusCode((int)_config.ForceStatusCode.Value, new TwitchErrorResponse
            {
                Error = "Forced Error",
                Status = (int)_config.ForceStatusCode.Value,
                Message = _config.ForceErrorMessage ?? "Forced error for testing"
            });
        }

        // Validate auth headers
        if (ValidateAuthHeaders() is IActionResult authError)
            return authError;

        AddRateLimitHeaders();

        // Validate query parameters
        if (string.IsNullOrEmpty(broadcasterId))
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required parameter: broadcaster_id"
            });
        }

        if (string.IsNullOrEmpty(moderatorId))
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required parameter: moderator_id"
            });
        }

        // Validate body
        if (body?.Data?.UserId == null)
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required body parameter: data.user_id"
            });
        }

        if (body?.Data?.Reason == null)
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required body parameter: data.reason"
            });
        }

        // Success response
        return Ok(new
        {
            data = new[]
            {
                new
                {
                    broadcaster_id = broadcasterId,
                    moderator_id = moderatorId,
                    user_id = body.Data.UserId,
                    reason = body.Data.Reason
                }
            }
        });
    }

    /// <summary>
    /// POST /helix/channels/vips - Add Channel VIP
    /// </summary>
    [HttpPost("channels/vips")]
    public IActionResult AddChannelVip(
        [FromQuery(Name = "broadcaster_id")] string? broadcasterId,
        [FromQuery(Name = "user_id")] string? userId)
    {
        // Check forced error first
        if (_config.ForceStatusCode.HasValue)
        {
            return StatusCode((int)_config.ForceStatusCode.Value, new TwitchErrorResponse
            {
                Error = "Forced Error",
                Status = (int)_config.ForceStatusCode.Value,
                Message = _config.ForceErrorMessage ?? "Forced error for testing"
            });
        }

        // Validate auth headers
        if (ValidateAuthHeaders() is IActionResult authError)
            return authError;

        AddRateLimitHeaders();

        // Validate query parameters
        if (string.IsNullOrEmpty(broadcasterId))
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required parameter: broadcaster_id"
            });
        }

        if (string.IsNullOrEmpty(userId))
        {
            return BadRequest(new TwitchErrorResponse
            {
                Error = "Bad Request",
                Status = 400,
                Message = "Missing required parameter: user_id"
            });
        }

        // 204 No Content (empty response for EmptyResponseConverter)
        return NoContent();
    }
}

/// <summary>
/// Standard Twitch API error response format.
/// </summary>
public record TwitchErrorResponse
{
    [JsonPropertyName("error")]
    public required string Error { get; init; }

    [JsonPropertyName("status")]
    public required int Status { get; init; }

    [JsonPropertyName("message")]
    public required string Message { get; init; }
}

/// <summary>
/// Request body for WarnChatUser endpoint.
/// </summary>
public record WarnChatUserRequestBody
{
    [JsonPropertyName("data")]
    public WarnChatUserData? Data { get; init; }
}

/// <summary>
/// Warning data within WarnChatUser request body.
/// </summary>
public record WarnChatUserData
{
    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("reason")]
    public string? Reason { get; init; }
}
