using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Helix.Conduits;

namespace TwitchySharp.Api.Tests.Integration.Helix.Conduits;
[Collection("helix")]
public class Test_Conduits(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;

    [Fact]
    public async void Send_ConduitsRequests_ReturnSuccessResponses()
    {
        string appToken = (await _fixture.Api.SendRequestAsync(new ClientCredentialsRequest(
            _fixture.Secrets.ClientId,
            _fixture.Secrets.ClientSecret
            ))).AccessToken;

        string conduitId = (await CreateConduit(appToken)).Data.First().Id;
        await GetConduits(appToken);
        string shardId = "0";
        await UpdateConduitShards(appToken, conduitId, shardId);
        await GetConduitShards(appToken, conduitId);
        await UpdateConduit(appToken, conduitId);
        await DeleteConduit(appToken, conduitId);
    }

    private ValueTask<CreateConduitsResponse> CreateConduit(string appToken)
        => _fixture.Api.SendRequestAsync(
            new CreateConduitRequest(
                _fixture.Secrets.ClientId,
                appToken,
                new CreateConduitRequestData()
                {
                    ShardCount = 1
                }
                ));

    private ValueTask<GetConduitsResponse> GetConduits(string appToken)
        => _fixture.Api.SendRequestAsync(new GetConduitsRequest(
            _fixture.Secrets.ClientId,
            appToken
            ));

    private ValueTask<GetConduitShardsResponse> GetConduitShards(string appToken, string conduitId)
        => _fixture.Api.SendRequestAsync(new GetConduitShardsRequest(
            _fixture.Secrets.ClientId,
            appToken,
            conduitId
            ));

    private ValueTask<UpdateConduitShardsResponse> UpdateConduitShards(string appToken, string conduitId, string shardId)
        => _fixture.Api.SendRequestAsync(new UpdateConduitShardsRequest(
            _fixture.Secrets.ClientId,
            appToken,
            new UpdateConduitShardsRequestData()
            {
                ConduitId = conduitId,
                Shards = [ new ConduitShardUpdate() { Id = shardId, Transport = new ConduitWebhookTransportUpdate() { Callback = "https://test.com/" } } ]
            }
            ));

    private ValueTask<UpdateConduitResponse> UpdateConduit(string appToken, string conduitId)
        => _fixture.Api.SendRequestAsync(new UpdateConduitRequest(
            _fixture.Secrets.ClientId,
            appToken,
            new UpdateConduitRequestData()
            {
                Id = conduitId,
                ShardCount = 2
            }
            ));

    private ValueTask<DeleteConduitResponse> DeleteConduit(string appToken, string conduitId)
        => _fixture.Api.SendRequestAsync(new DeleteConduitRequest(
            _fixture.Secrets.ClientId,
            appToken,
            conduitId
            ));
}
