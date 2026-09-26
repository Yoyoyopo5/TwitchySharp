using System.Text;
using TwitchySharp.Api;

namespace TwitchySharp.Tests.E2E;

public static class TwitchApiExceptionExtensions
{
    public static string ToReportString(this TwitchApiException ex)
        => $"""
        Request: {ex.Request}
        Status Code: {ex.StatusCode}
        Headers: {string.Join(", ", ex.Headers)}
        Content Headers: {string.Join(", ", ex.ContentHeaders)}
        Content: {ex.Content}
        """;
}
