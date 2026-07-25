using System.Globalization;
using TwitchySharp.Api.Helix.Streams;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Streams;

public class Test_GetStreams(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-streams");

    [Fact]
    public async Task Send_GetStreamsRequest_ReturnSuccessResponse()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        GetStreamsRequest request = new()
        {
            UserIds = [userConfig.UserId],
            GameIds = [new GameId("33214")],
            UserLogins = [new UserLogin("dreadbreadcrumb")],
            Languages = [new LanguageCode(CultureInfo.CurrentCulture)],
            Type = StreamType.All
        };

        await _fixture.GetTwitchApiClient().SendAsync(request, TestContext.Current.CancellationToken);
    }
}
