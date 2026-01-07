using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.Requests;
internal record CallbackVerificationRequestData : WebhookRequestData
{
    public required string Challenge { get; init; }
}
