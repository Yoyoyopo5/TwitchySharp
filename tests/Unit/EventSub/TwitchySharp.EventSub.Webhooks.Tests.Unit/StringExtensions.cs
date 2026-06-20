using System.Text;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

internal static class StringExtensions
{
    public static MemoryStream ToMemoryStream(this string body)
        => new(Encoding.UTF8.GetBytes(body));
}
