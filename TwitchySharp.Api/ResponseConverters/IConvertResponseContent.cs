using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;

public interface IConvertResponseContent
{
    ValueTask<TResponseContent> Convert<TResponseContent>(HttpResponseMessage httpResponse, CancellationToken ct = default);
}
