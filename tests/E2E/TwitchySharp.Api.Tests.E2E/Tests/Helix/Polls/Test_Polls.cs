using TwitchySharp.Api.Helix.Polls;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Polls;

[Collection("twitch")]
public class Test_Polls(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_PollRequests_ReturnSuccessResponses()
    {
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = _fixture.CreateClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        var createResponse = await CreatePoll(client, broadcasterId, ct);
        ChatPoll poll = createResponse.Content.Data.Single();
        await Task.Delay(250, ct);

        await GetPolls(client, broadcasterId, ct);
        await EndPoll(client, broadcasterId, poll.Id, ct);
    }

    private static ValueTask<TwitchResponse<CreatePollResponse>> CreatePoll(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new CreatePollRequest()
        {
            Poll = new CreatePollRequestData()
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
        }, ct);

    private static ValueTask<TwitchResponse<GetPollsResponse>> GetPolls(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetPollsRequest()
        {
            BroadcasterId = broadcasterId
        }, ct);

    private static ValueTask<TwitchResponse<EndPollResponse>> EndPoll(ITwitchClient client, UserId broadcasterId, PollId pollId, CancellationToken ct)
        => client.SendAsync(new EndPollRequest()
        {
            Poll = new EndPollRequestData()
            {
                BroadcasterId = broadcasterId,
                Id = pollId,
                Status = EndPollStatus.Terminated
            }
        }, ct);
}
