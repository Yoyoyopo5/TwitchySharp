using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Whispers;

namespace TwitchySharp.Api.Tests.Integration.Helix.Whispers;
[Collection("helix")]
public class Test_SendWhisper(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_SendWhisperRequest_ReturnSuccessResponse()
    {
        const string toUserId = "52137752";
        string fromUserId = await _fixture.GetUserIdFromAccessTokenAsync();

        await _fixture.Api.SendRequestAsync(new SendWhisperRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            fromUserId,
            toUserId,
            new SendWhisperRequestData() 
            { 
                Message = "test whisper pls ignore"
            }
            ));
    }
}
