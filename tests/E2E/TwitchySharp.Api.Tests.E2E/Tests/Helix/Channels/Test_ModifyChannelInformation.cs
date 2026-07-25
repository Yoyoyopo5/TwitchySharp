using TwitchySharp.Api.Helix.Channels;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Channels;

public class Test_ModifyChannelInformation(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("modify-channel-information");

    [Fact]
    public async Task Send_ModifyChannelInformationRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        GetChannelInformationRequest getInfoRequest = new()
        {
            BroadcasterIds = [userConfig.UserId]
        };

        // cache original
        ChannelInformation channelInfo = (await client.SendAsync(getInfoRequest, ct)).Content.Data.First();

        Assert.True(LanguageCode.TryParse("en", out LanguageCode language));
        ModifyChannelInformationRequest modifyRequest = new()
        {
            BroadcasterId = userConfig.UserId,
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
