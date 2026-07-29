using TwitchySharp.Api;
using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Api.Helix.Teams;
using Xunit;
using static TwitchySharp.Api.TwitchIdentity;

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

    public static async Task<TwitchStream?> GetStream(
        this ITwitchClient client,
        UserId broadcasterId,
        CancellationToken ct
        )
        => (await client.SendAsync(new GetStreamsRequest() { UserIds = [broadcasterId] }, ct))
            .Content.Data.SingleOrDefault();

    public static async Task SkipIfBroadcasterIsNotStreaming(
        this ITwitchClient client,
        UserId broadcasterId,
        CancellationToken ct
        )
        => Assert.SkipWhen(
            (await client.GetStream(broadcasterId, ct)) is null,
            "The broadcaster is not live."
            );
}
