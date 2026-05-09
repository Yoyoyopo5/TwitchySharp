using TwitchySharp.Api.Helix.Conduits;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Conduits;

[Collection("twitch")]
public class Test_Conduits(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;

    [Fact]
    public async Task Send_ConduitsRequests_ReturnSuccessResponses()
    {
        ITwitchClient client = TwitchClientFixture.Client;
        CancellationToken ct = TestContext.Current.CancellationToken;

        ConduitId conduitId = (await CreateConduit(client, ct)).Content.Data.First().Id;
        await Task.Delay(250, ct);

        await GetConduits(client, ct);
        ConduitShardId shardId = (await GetConduitShards(client, conduitId, ct)).Content.Data.First().Id;

        await UpdateConduitShards(client, conduitId, shardId, ct);
        await UpdateConduit(client, conduitId, ct);
        await DeleteConduit(client, conduitId, ct);
    }

    private static ValueTask<TwitchResponse<CreateConduitsResponse>> CreateConduit(ITwitchClient client, CancellationToken ct)
        => client.SendAsync(new CreateConduitRequest()
        {
            ConduitData = new()
            {
                ShardCount = 1
            }
        }, ct);

    private static ValueTask<TwitchResponse<GetConduitsResponse>> GetConduits(ITwitchClient client, CancellationToken ct)
        => client.SendAsync(new GetConduitsRequest(), ct);

    private static ValueTask<TwitchResponse<GetConduitShardsResponse>> GetConduitShards(ITwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new GetConduitShardsRequest()
        {
            ConduitId = conduitId
        }, ct);

    private static ValueTask<TwitchResponse<UpdateConduitShardsResponse>> UpdateConduitShards(ITwitchClient client, ConduitId conduitId, ConduitShardId shardId, CancellationToken ct)
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
                        Secret = "super_secure_secret"
                    }
                } ]
            }
        }, ct);

    private static ValueTask<TwitchResponse<UpdateConduitResponse>> UpdateConduit(ITwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new UpdateConduitRequest()
        {
            ConduitData = new()
            {
                Id = conduitId,
                ShardCount = 2
            }
        }, ct);

    private static ValueTask<TwitchResponse<DeleteConduitResponse>> DeleteConduit(ITwitchClient client, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new DeleteConduitRequest()
        {
            ConduitId = conduitId
        }, ct);
}
