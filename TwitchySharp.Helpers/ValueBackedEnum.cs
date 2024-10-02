using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Helpers;
/// <summary>
/// Allows for simple creation of hardcoded sets of values that can be discovered via intellisense.
/// </summary>
public record ValueBackedEnum<T>
{
    public T Value { get; private set; }
    protected ValueBackedEnum(T value)
    {
        Value = value;
    }
    public override string ToString() => Value.ToString();
}
