using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Moderation;

public class Test_BlockedTerms(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("blocked-terms");

    [Fact]
    public async Task Send_BlockedTermRequests_ReturnSuccessResponses()
    {
        UserConfiguration userConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<UserConfiguration>(TestName);

        const string TEST_BLOCKED_TERM = "test-term";
        UserId broadcasterId = userConfig.UserId;

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        TwitchResponse<AddBlockedTermResponseContent> addResponse = await AddBlockedTerm(client, broadcasterId, TEST_BLOCKED_TERM, ct);
        BlockedTerm blockedTerm = addResponse.Content.Data.Single();
        await Task.Delay(100, ct);
        await GetBlockedTerms(client, broadcasterId, ct);
        await RemoveBlockedTerm(client, broadcasterId, blockedTerm.Id, ct);
    }

    private static Task<TwitchResponse<AddBlockedTermResponseContent>> AddBlockedTerm(TestingTwitchClient client, UserId broadcasterId, string term, CancellationToken ct)
        => client.SendAsync(new AddBlockedTermRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            Term = new() { Text = term }
        }, TestName, ct);

    private static Task<TwitchResponse<GetBlockedTermsResponseContent>> GetBlockedTerms(TestingTwitchClient client, UserId broadcasterId, CancellationToken ct)
        => client.SendAsync(new GetBlockedTermsRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId
        }, TestName, ct);

    private static Task<TwitchResponse<RemoveBlockedTermResponseContent>> RemoveBlockedTerm(TestingTwitchClient client, UserId broadcasterId, AutomodBlockedTermId termId, CancellationToken ct)
        => client.SendAsync(new RemoveBlockedTermRequest()
        {
            BroadcasterId = broadcasterId,
            ModeratorId = broadcasterId,
            BlockedTermId = termId,
        }, TestName, ct);
}
