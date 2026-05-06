using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.Responses;

public abstract record WebhookResponseData
{
    public int StatusCode { get; init; } = 200;
}
