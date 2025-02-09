using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ApiResponseConverters;
internal class EmptyResponseConverter : IConvertApiResponse
{
    public ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => ValueTask.FromResult(Activator.CreateInstance<TResponse>());
}
