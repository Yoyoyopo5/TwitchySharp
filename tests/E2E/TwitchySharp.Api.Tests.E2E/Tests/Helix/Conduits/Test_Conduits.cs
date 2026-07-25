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
        ClientConfiguration clientConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ClientConfiguration>(TestName);

        ITwitchClient client = _fixture.GetTwitchApiClient();
        CancellationToken ct = TestContext.Current.CancellationToken;

        ConduitId conduitId = (await CreateConduit(client, clientConfig, ct)).Content.Data.First().Id;
        await Task.Delay(250, ct);

        await GetConduits(client, clientConfig, ct);
        ConduitShardId shardId = (await GetConduitShards(client, clientConfig, conduitId, ct)).Content.Data.First().Id;

        await UpdateConduitShards(client, clientConfig, conduitId, shardId, ct);
        await UpdateConduit(client, clientConfig, conduitId, ct);
        await DeleteConduit(client, clientConfig, conduitId, ct);
    }

    private static Task<TwitchResponse<CreateConduitsResponse>> CreateConduit(ITwitchClient client, ClientConfiguration clientConfig, CancellationToken ct)
        => client.SendAsync(new CreateConduitRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            ConduitData = new()
            {
                ShardCount = 1
            }
        }, ct);

    private static Task<TwitchResponse<GetConduitsResponse>> GetConduits(ITwitchClient client, ClientConfiguration clientConfig, CancellationToken ct)
        => client.SendAsync(new GetConduitsRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() }
        }, ct);

    private static Task<TwitchResponse<GetConduitShardsResponse>> GetConduitShards(ITwitchClient client, ClientConfiguration clientConfig, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new GetConduitShardsRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            ConduitId = conduitId
        }, ct);

    private static Task<TwitchResponse<UpdateConduitShardsResponse>> UpdateConduitShards(ITwitchClient client, ClientConfiguration clientConfig, ConduitId conduitId, ConduitShardId shardId, CancellationToken ct)
        => client.SendAsync(new UpdateConduitShardsRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
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
        }, ct);

    private static Task<TwitchResponse<UpdateConduitResponse>> UpdateConduit(ITwitchClient client, ClientConfiguration clientConfig, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new UpdateConduitRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            ConduitData = new()
            {
                Id = conduitId,
                ShardCount = 2
            }
        }, ct);

    private static Task<TwitchResponse<DeleteConduitResponse>> DeleteConduit(ITwitchClient client, ClientConfiguration clientConfig, ConduitId conduitId, CancellationToken ct)
        => client.SendAsync(new DeleteConduitRequest()
        {
            AuthorizationContext = new() { Identity = clientConfig.ToIdentity() },
            ConduitId = conduitId
        }, ct);
}
