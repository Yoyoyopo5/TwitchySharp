using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;

/// <summary>
/// A Twitch charity campaign.
/// </summary>
public interface IHaveCharityCampaign
{
    /// <summary>
    /// The id of the charity campaign.
    /// </summary>
    string Id { get; }
    /// <summary>
    /// The current amount of donations for the campaign.
    /// </summary>
    CharityAmount CurrentAmount { get; }
    /// <summary>
    /// The target amount of donations for the campaign.
    /// </summary>
    CharityAmount TargetAmount { get; }
}
