using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Interfaces.Events.Channel.ChannelPoints;

public interface ISetting<T>
{
    /// <summary>
    /// Indicates whether the setting is enabled.
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// The setting value.
    /// </summary>
    T Value { get; }
}
