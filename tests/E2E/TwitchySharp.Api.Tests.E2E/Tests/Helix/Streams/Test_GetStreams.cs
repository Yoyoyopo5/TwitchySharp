using System.Globalization;
using TwitchySharp.Api.Helix.Streams;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

[Collection("twitch")]
public class Test_GetStreams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_GetStreamsRequest_ReturnSuccessResponse()
    {
        GetStreamsRequest request = new()
        {
            UserIds = [_fixture.UserIdentity.UserId],
            GameIds = [new GameId("33214")],
            UserLogins = [new UserLogin("dreadbreadcrumb")],
            Languages = [new LanguageCode(CultureInfo.CurrentCulture)],
            Type = StreamType.All
        };

        await TwitchClientFixture.Client.SendAsync(request, TestContext.Current.CancellationToken);
    }
}
