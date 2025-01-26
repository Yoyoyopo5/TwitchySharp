using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Helix.EventSub;
using TwitchySharp.Api.Helix.EventSub.Models.Types.Automod.Message;

namespace TwitchySharp.Api.Tests.Integration.Helix.EventSub;
public class EventSubTestTypes : TheoryData<string>
{
    /// <summary>
    /// Types available to the testing framework. 
    /// Be sure to include the corresponding type instance in <see cref="EventSubFixture.GenerateTypeMapAsync"/>.
    /// </summary>
    private IEnumerable<string> _subscriptionTypeNames = new string[]
    {
        nameof(AutomodMessageHoldV2)
    };

    public EventSubTestTypes()
    {
        foreach (string subscriptionType in _subscriptionTypeNames)
            Add(subscriptionType);
    }
}
