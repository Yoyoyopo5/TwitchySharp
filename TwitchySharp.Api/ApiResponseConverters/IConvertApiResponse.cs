﻿using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace TwitchySharp.Api.ApiResponseConverters;

public interface IConvertApiResponse
{
    ValueTask<TResponse> Convert<TResponse>(HttpResponseMessage httpResponse, CancellationToken ct = default);
}
