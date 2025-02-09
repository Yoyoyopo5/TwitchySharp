using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.Moderation;
using TwitchySharp.Api.Tests.Integration.Helix.Channels;

namespace TwitchySharp.Api.Tests.Integration.Helix.Moderation;
[Collection("helix")]
public class Test_BlockedTerms(HelixFixture fixture)
{
    private readonly HelixFixture _fixture = fixture;
    private const string TEST_TERM = "test_term";

    [Fact]
    public async void Send_BlockedTermRequests_ReturnSuccessResponses()
    {
        string broadcasterId = await _fixture.GetUserIdFromAccessTokenAsync();

        string termId = (await AddBlockedTerm(broadcasterId)).Data.First().Id;
        await GetBlockedTerms(broadcasterId);
        await RemoveBlockedTerm(broadcasterId, termId);
    }

    private ValueTask<AddBlockedTermResponse> AddBlockedTerm(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new AddBlockedTermRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            new AddBlockedTermRequestData()
            {
                Text = TEST_TERM
            }
            ));

    private ValueTask<GetBlockedTermsResponse> GetBlockedTerms(string broadcasterId)
        => _fixture.Api.SendRequestAsync(new GetBlockedTermsRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId
            ));

    private ValueTask<RemoveBlockedTermResponse> RemoveBlockedTerm(string broadcasterId, string termId)
        => _fixture.Api.SendRequestAsync(new RemoveBlockedTermRequest(
            _fixture.Secrets.Client.Id,
            _fixture.Secrets.User.AccessToken,
            broadcasterId,
            broadcasterId,
            termId
            ));
}
