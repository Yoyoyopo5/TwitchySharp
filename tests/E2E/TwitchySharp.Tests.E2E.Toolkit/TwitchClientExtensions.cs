using TwitchySharp.Api;
using Xunit;

namespace TwitchySharp.Tests.E2E;

public static class TwitchClientExtensions
{
    public static TwitchClient WithExceptionsAsTestAttachments(this TwitchClient client)
        => client.With(next => async (request, ct) =>
        {
            try
            {
                return await next(request, ct);
            }
            catch (TwitchApiException apiEx)
            {
                TestContext.Current.AddAttachment("twitch-api-exception", apiEx.ToReportString());
                throw;
            }
        });
}
