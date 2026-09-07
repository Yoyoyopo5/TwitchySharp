using TwitchySharp.Api.Helix.Conduits;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Conduits;

public class Test_Conduits(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("conduits");

    [Fact]
    public async Task Send_ConduitsRequests_ReturnSuccessResponses()
    {
        _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        TestingTwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        ConduitId conduitId = (await CreateConduit(client, ct)).Content.Data.First().Id;
        try
        {
            await Task.Delay(250, ct);

            await UpdateConduitShards(client, conduitId, new("0"), ct);

            await GetConduits(client, ct);
            await GetConduitShards(client, conduitId, ct);
            
            await UpdateConduit(client, conduitId, ct);
        }
        finally
        {
            await DeleteConduit(client, conduitId, ct);
        }
    }

    private static Task<TwitchResponse<CreateConduitsResponseContent>> CreateConduit(TestingTwitchClient client, CancellationToken ct)
        => client.SendAsync(new CreateConduitRequest()
        {
            ConduitData = new()
            {
                ShardCount = 1
            }
        }, TestName, ct);

    private static Task<TwitchResponse<GetConduitsResponseContent>> GetConduits(TestingTwitchClient client, CancellationToken ct)
        => client.SendAsync(new GetConduitsRequest(), TestName, ct);

    private static Task<TwitchResponse<GetConduitShardsResponseContent>> GetConduitShards(TestingTwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new GetConduitShardsRequest()
        {
            ConduitId = conduitId
        }, TestName, ct);

    private static Task<TwitchResponse<UpdateConduitShardsResponseContent>> UpdateConduitShards(TestingTwitchClient client, ConduitId conduitId, ConduitShardId shardId, CancellationToken ct)
        => client.SendAsync(new UpdateConduitShardsRequest()
        {
            ShardUpdates = new()
            {
                ConduitId = conduitId,
                Shards = [ new() {
                    Id = shardId,
                    Transport = new ConduitWebhookTransportUpdate()
                    {
                        Callback = new("https://test.com"),
                        Secret = new("super_secure_secret")
                    }
                } ]
            }
        }, TestName, ct);

    private static Task<TwitchResponse<UpdateConduitResponseContent>> UpdateConduit(TestingTwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new UpdateConduitRequest()
        {
            ConduitData = new()
            {
                Id = conduitId,
                ShardCount = 3
            }
        }, TestName, ct);

    private static Task<TwitchResponse<DeleteConduitResponseContent>> DeleteConduit(TestingTwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new DeleteConduitRequest()
        {
            ConduitId = conduitId
        }, TestName, ct);
}
