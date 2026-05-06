using Microsoft.AspNetCore.Http;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal interface ITwitchWebhooksHeaderConverter
{
    TwitchWebhooksRequestHeaderConversionResult Convert(IHeaderDictionary headers);
}
