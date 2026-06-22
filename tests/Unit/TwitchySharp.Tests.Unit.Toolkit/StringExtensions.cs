using System.Text;

namespace TwitchySharp.Tests.Unit;

public static class StringExtensions
{
    public static MemoryStream ToMemoryStream(this string body)
        => new(Encoding.UTF8.GetBytes(body));
}
