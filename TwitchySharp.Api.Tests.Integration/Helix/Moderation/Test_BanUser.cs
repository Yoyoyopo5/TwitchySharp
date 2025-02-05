using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_BanUser(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_BanUnbanUserRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();
        const string bannedUserId = "52137750";

        await BanUser(broadcasterId, bannedUserId);
        await UnbanUser(broadcasterId, bannedUserId);
    }

    private ValueTask<BanUserResponse> BanUser(string broadcasterId, string userId)
        => _fixture.Api.SendRequestAsync(new BanUserRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            new BanUserRequestData()
            { 
                Data = new()
                {
                    UserId = userId,
                    Duration = TimeSpan.FromSeconds(60),
                    Reason = "testing"
                }
            }
            ));

    private ValueTask<UnbanUserResponse> UnbanUser(string broadcasterId, string userId)
        => _fixture.Api.SendRequestAsync(new UnbanUserRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            userId
            ));
}
