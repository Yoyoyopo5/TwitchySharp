using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Core;

public record TwitchResponse : ITwitchResponse
{
    public TwitchRateLimitDetails? RateLimitDetails { get; init; }
    public required ITwitchRequest Request { get; init; }
    public required HttpStatusCode StatusCode { get; init; }
}

public record TwitchResponse<TResponseContent> : TwitchResponse, ITwitchResponse<TResponseContent>
{
    public required TResponseContent Content { get; init; }
}
