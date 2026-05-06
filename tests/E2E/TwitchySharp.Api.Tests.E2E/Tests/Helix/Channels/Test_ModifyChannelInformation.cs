using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

[Collection("twitch")]
public class Test_ModifyChannelInformation(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ModifyChannelInformationRequest_ReturnSuccessResponse()
    {
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChannelInformationRequest getInfoRequest = new()
        {
            BroadcasterIds = [_fixture.UserIdentity.UserId]
        };

        // cache original
        ChannelInformation channelInfo = (await client.SendAsync(getInfoRequest, ct)).Content.Data.First();

        Assert.True(LanguageCode.TryParse("en", out LanguageCode language));
        ModifyChannelInformationRequest modifyRequest = new()
        {
            BroadcasterId = _fixture.UserIdentity.UserId,
            ChannelInformation = new()
            {
                BroadcasterLanguage = language,
                ContentClassificationLabels = [new(ContentClassificationLabelId.ProfanityVulgarity, true)],
                GameId = GameId.None,
                Tags = ["TestStream"],
                Title = "Test Stream PLS Ignore"
            }
        };

        await client.SendAsync(modifyRequest, ct);
        await Task.Delay(250, ct);
        // restore original
        await client.SendAsync(modifyRequest with
        {
            ChannelInformation = modifyRequest.ChannelInformation with
            {
                BroadcasterLanguage = channelInfo.BroadcasterLanguage,
                ContentClassificationLabels = [.. channelInfo.ContentClassificationLabels.Select(id => new ContentClassificationLabel(id, true))],
                GameId = channelInfo.GameId,
                Tags = channelInfo.Tags,
                Title = channelInfo.Title
            }
        }, ct);
    }
}
