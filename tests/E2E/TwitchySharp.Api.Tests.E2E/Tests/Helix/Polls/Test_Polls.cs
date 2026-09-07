using TwitchySharp.Api.Helix.Polls;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Polls;

public class Test_Polls(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("polls");

    [Fact]
    public async Task Send_PollRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        UserId broadcasterId = userConfig.UserId;
        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<CreatePollResponseContent> createResponse = await CreatePoll(client, broadcasterId, ct);
        ChatPoll poll = createResponse.Content.Data.Single();
        await Task.Delay(250, ct);

        await GetPolls(client, broadcasterId, ct);
        await EndPoll(client, broadcasterId, poll.Id, ct);
    }

    private static Task<TwitchResponse<CreatePollResponseContent>> CreatePoll(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
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
        }, TestName, ct);

    private static Task<TwitchResponse<GetPollsResponseContent>> GetPolls(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetPollsRequest()
        {
            BroadcasterId = broadcasterId
        }, TestName, ct);

    private static Task<TwitchResponse<EndPollResponseContent>> EndPoll(TestingTwitchClient client, UserId broadcasterId, PollId pollId, CancellationToken ct)
        => client.SendAsync(new EndPollRequest()
        {
            Poll = new EndPollRequestData()
            {
                BroadcasterId = broadcasterId,
                Id = pollId,
                Status = EndPollStatus.Terminated
            }
        }, TestName, ct);
}
