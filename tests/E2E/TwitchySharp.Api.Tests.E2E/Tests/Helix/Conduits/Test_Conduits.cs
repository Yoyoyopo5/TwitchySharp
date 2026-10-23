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

        TwitchResponse<CreateConduitsResponseContent> createResponse = await CreateConduit(client, ct);
        Assert.NotNull(createResponse.Content);
        ConduitId conduitId = createResponse.Content.Data.First().Id;
        try
        {
            await Task.Delay(250, ct);

            TwitchResponse<UpdateConduitShardsResponseContent> updateShardsResponse = await UpdateConduitShards(client, conduitId, new("0"), ct);
            Assert.Null(updateShardsResponse.Error);

            TwitchResponse<GetConduitsResponseContent> getConduitsResponse = await GetConduits(client, ct);
            Assert.Null(getConduitsResponse.Error);

            TwitchResponse<GetConduitShardsResponseContent> getShardsResponse = await GetConduitShards(client, conduitId, ct);
            Assert.Null(getShardsResponse.Error);

            TwitchResponse<UpdateConduitResponseContent> updateConduitResponse = await UpdateConduit(client, conduitId, ct);
            Assert.Null(updateConduitResponse.Error);
        }
        finally
        {
            TwitchResponse<DeleteConduitResponseContent> deleteResponse = await DeleteConduit(client, conduitId, ct);
            Assert.Null(deleteResponse.Error);
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
