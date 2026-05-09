using TwitchySharp.Api.Helix.Moderation;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

[Collection("twitch")]
public class Test_BlockedTerms(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_BlockedTermRequests_ReturnSuccessResponses()
    {
        const string TEST_BLOCKED_TERM = "test-term";
        UserId broadcasterId = _fixture.UserIdentity.UserId;
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        var addResponse = await AddBlockedTerm(client, broadcasterId, TEST_BLOCKED_TERM, ct);
        BlockedTerm blockedTerm = addResponse.Content.Data.Single();
        await Task.Delay(100, ct);
        await GetBlockedTerms(client, broadcasterId, ct);
        await RemoveBlockedTerm(client, broadcasterId, blockedTerm.Id, ct);
    }

    private static ValueTask<TwitchResponse<AddBlockedTermResponse>> AddBlockedTerm(ITwitchClient client, UserId broadcasterId, string term, CancellationToken ct)
        => client.SendAsync(new AddBlockedTermRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Term = new() { Text = term }
        }, ct);

    private static ValueTask<TwitchResponse<GetBlockedTermsResponse>> GetBlockedTerms(ITwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetBlockedTermsRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, ct);

    private static ValueTask<TwitchResponse<RemoveBlockedTermResponse>> RemoveBlockedTerm(ITwitchClient client, UserId broadcasterId, AutomodBlockedTermId termId, CancellationToken ct)
        => client.SendAsync(new RemoveBlockedTermRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            BlockedTermId = termId,
        }, ct);
}
