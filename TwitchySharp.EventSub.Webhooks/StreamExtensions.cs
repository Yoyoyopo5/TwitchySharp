using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks;

internal static class StreamExtensions
{
    public static Stream Reset(this Stream stream)
    {
        stream.Position = 0;
        return stream;
    }
}
