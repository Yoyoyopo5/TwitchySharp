using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.EventSub.Models.Automod.Message;

namespace TwitchySharp.EventSub.Interfaces.Automod.Message;

/// <summary>
/// Interface for Automod Message events.
/// </summary>
/// <remarks>
/// <see cref="EventSubSubscriptionType.AutomodMessageHold"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageHoldV2"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdate"/>,
/// <see cref="EventSubSubscriptionType.AutomodMessageUpdateV2"/>.
/// </remarks>
public interface IAutomodMessageEvent : IHaveBroadcaster, IHaveUser, IHaveAutomodHeldMessage;
