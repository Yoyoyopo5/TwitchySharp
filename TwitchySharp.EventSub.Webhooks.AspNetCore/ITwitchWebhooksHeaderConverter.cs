using Microsoft.AspNetCore.Http;

namespace TwitchySharp.EventSub.Webhooks.AspNetCore;

internal interface ITwitchWebhooksHeaderConverter
{
    public TwitchWebhooksRequestHeaderConversionResult Convert(IHeaderDictionary headers);
}
