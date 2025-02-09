using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Users;

namespace TwitchySharp.Api.Tests.Integration.Helix.Users;
[Collection("helix")]
public class Test_BlockUnblockUser(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_BlockUnblockUserRequests_ReturnSuccessResponses()
    {
        const string userId = "12345";

        await BlockUser(userId);
        await UnblockUser(userId);
    }

    private ValueTask<BlockUserResponse> BlockUser(string userId)
        => _fixture.Api.SendRequestAsync(new BlockUserRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            userId,
            BlockUserContext.Chat,
            BlockUserReason.Other
            ));

    private ValueTask<UnblockUserResponse> UnblockUser(string userId)
        => _fixture.Api.SendRequestAsync(new UnblockUserRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            userId
            ));
}
