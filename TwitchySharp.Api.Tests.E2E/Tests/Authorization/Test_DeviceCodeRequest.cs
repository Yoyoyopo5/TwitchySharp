using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Tests.E2E.Tests.Authorization;

[Collection("twitch")]
public class Test_DeviceCodeRequest(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_DeviceCodeRequest_ReturnSuccessResponse()
    {
        HashSet<Scope> scopes = [];
        DeviceCodeRequest request = new()
        {
            ClientId = new(_fixture.Client.Id),
            Scopes = scopes
        };
        ITwitchClient client = _fixture.CreateClient();
        
        DeviceCodeResponse response = (await client.SendAsync(request, TestContext.Current.CancellationToken)).Content;

        Assert.False(string.IsNullOrEmpty(response.DeviceCode.Value));
        Assert.True(response.ExpiresIn > TimeSpan.Zero);
        Assert.True(response.Interval > TimeSpan.Zero);
        Assert.False(string.IsNullOrEmpty(response.UserCode));
        Assert.False(string.IsNullOrEmpty(response.VerificationUri.AbsoluteUri));
    }
}
