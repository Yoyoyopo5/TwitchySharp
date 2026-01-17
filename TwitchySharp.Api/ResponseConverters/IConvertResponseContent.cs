using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;
/// <summary>
/// Implements a method for converting an <see cref="HttpResponseMessage"/> into an instance of an object.
/// </summary>
public interface IConvertResponseContent
{
    /// <summary>
    /// Converts an <see cref="HttpResponseMessage"/> into an instance of <typeparamref name="TResponseContent"/>.
    /// </summary>
    /// <typeparam name="TResponseContent">The type to return.</typeparam>
    /// <param name="httpResponse">The HTTP response.</param>
    /// <returns>A <see cref="ValueTask"/> containing the instance of <typeparamref name="TResponseContent"/>.</returns>
    ValueTask<TResponseContent> Convert<TResponseContent>(HttpResponseMessage httpResponse, CancellationToken ct = default);
}
