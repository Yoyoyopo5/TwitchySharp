using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Polls;

namespace TwitchySharp.Api.Tests.Integration.Helix.Polls;
[Collection("helix")]
public class Test_Polls(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_PollRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string pollId = (await CreatePoll(broadcasterId)).Data.First().Id;
        await GetPolls(broadcasterId);
        await EndPoll(broadcasterId, pollId);
    }

    private ValueTask<CreatePollResponse> CreatePoll(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new CreatePollRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new CreatePollRequestData()
            {
                BroadcasterId = broadcasterId,
                Title = "Test Poll",
                Duration = TimeSpan.FromMinutes(2),
                Choices =
                [
                    new()
                    {
                        Title = "Test Option 1"
                    },
                    new()
                    {
                        Title = "Test Option 2"
                    }
                ]
            }
            ));

    private ValueTask<GetPollsResponse> GetPolls(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new GetPollsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId
            ));

    private ValueTask<EndPollResponse> EndPoll(string broadcasterId, string pollId)
        => _fixture.Api.SendRequestAsync(new EndPollRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            new EndPollRequestData()
            {
                BroadcasterId = broadcasterId,
                Id = pollId,
                Status = EndPollStatus.Terminated
            }
            ));
}
