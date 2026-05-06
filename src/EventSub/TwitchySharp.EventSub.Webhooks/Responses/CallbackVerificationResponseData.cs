using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.EventSub.Webhooks.Responses;

public record CallbackVerificationResponseData : WebhookResponseData
{
    public required string Challenge { get; init; }
    public int ChallengeLength => Challenge.Length;
}
