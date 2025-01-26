using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Message;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;
/// <summary>
/// Handles one-time setup of subscription types.
/// </summary>
public class EventSubFixture : HelixFixture, IAsyncLifetime
{
    private IReadOnlyDictionary<string, IEventSubSubscriptionType> _subscriptionTypes = ImmutableDictionary<string, IEventSubSubscriptionType>.Empty;

    public async Task InitializeAsync()
        => _subscriptionTypes = await GenerateTypeMapAsync();

    public Task DisposeAsync()
        => Task.CompletedTask;

    private async ValueTask<IReadOnlyDictionary<string, IEventSubSubscriptionType>> GenerateTypeMapAsync()
    {
        string broadcasterId = await GetUserIdFromAccessTokenAsync();

        return new Dictionary<string, IEventSubSubscriptionType>
        {
            { nameof(AutomodMessageHoldV2), new AutomodMessageHoldV2(broadcasterId, broadcasterId) }
        };
    }

    public IEventSubSubscriptionType GetSubscriptionType(string subscriptionTypeName)
        => _subscriptionTypes[subscriptionTypeName];
}
