using System.Net;

namespace TwitchySharp.Api.Tests.Integration.Models;

/// <summary>
/// Allows tests to configure mock server responses.
/// </summary>
public class MockResponseConfigurator
{
    // Rate limit stub values (configurable per test)
    public int RateLimitLimit { get; set; } = 800;
    public int RateLimitRemaining { get; set; } = 799;
    public DateTimeOffset RateLimitReset { get; set; } = DateTimeOffset.UtcNow.AddMinutes(1);

    // Error simulation
    public HttpStatusCode? ForceStatusCode { get; set; }
    public string? ForceErrorMessage { get; set; }

    /// <summary>
    /// Resets all configuration to default values.
    /// </summary>
    public void Reset()
    {
        RateLimitLimit = 800;
        RateLimitRemaining = 799;
        RateLimitReset = DateTimeOffset.UtcNow.AddMinutes(1);
        ForceStatusCode = null;
        ForceErrorMessage = null;
    }
}
