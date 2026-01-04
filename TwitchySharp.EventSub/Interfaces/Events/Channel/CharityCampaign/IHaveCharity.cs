using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.CharityCampaign;

/// <summary>
/// A charity in a Twitch charity campaign.
/// </summary>
public interface IHaveCharity
{
    /// <summary>
    /// The name of the charity.
    /// </summary>
    string CharityName { get; }
    /// <summary>
    /// The description of the charity.
    /// </summary>
    string CharityDescription { get; }
    /// <summary>
    /// A URL pointing to a 100x100 PNG image of the charity's logo.
    /// </summary>
    string CharityLogo { get; }
    /// <summary>
    /// The URL of the charity's website.
    /// </summary>
    string CharityWebsite { get; }
}
