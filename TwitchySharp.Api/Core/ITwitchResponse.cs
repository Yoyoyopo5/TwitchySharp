using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Core;

public interface ITwitchResponse
{
    ITwitchRequest Request { get; }
    HttpStatusCode StatusCode { get; }
    TwitchRateLimitDetails? RateLimitDetails { get; }
}

public interface ITwitchResponse<TResponseContent> : ITwitchResponse
{
    TResponseContent Content { get; }
}
