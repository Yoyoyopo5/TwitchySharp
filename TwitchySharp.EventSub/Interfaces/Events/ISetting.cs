namespace TwitchySharp.EventSub.Interfaces.Events;

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
