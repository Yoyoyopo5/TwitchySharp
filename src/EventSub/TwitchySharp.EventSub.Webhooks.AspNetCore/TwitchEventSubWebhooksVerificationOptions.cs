using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

public class TwitchEventSubWebhooksVerificationOptions
{
    public string Secret { get; set; } = string.Empty;
}
