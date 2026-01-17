using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Api.Extensions;

namespace TwitchySharp.Api.Handlers;
// May want to get rid of this.
public class TwitchAuthorizationHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken ct = default)
    {
        request.AddTwitchAuthorizationHeaders();
        return base.SendAsync(request, ct);
    }
}
