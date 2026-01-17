using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchySharp.Api.ResponseConverters;
/// <summary>
/// Creates a new TResponseContent from a response with no content.
/// </summary>
/// <remarks>
/// Only works with types that have parameterless constructors.
/// </remarks>
public class EmptyResponseConverter : IConvertResponseContent
{
    public ValueTask<TResponseContent> Convert<TResponseContent>(HttpResponseMessage httpResponse, CancellationToken ct = default)
        => ValueTask.FromResult(TypeFactory<TResponseContent>.Create());

    // Inner helper to cache the compiled constructor
    private static class TypeFactory<T>
    {
        public static readonly Func<T> Create =
            System.Linq.Expressions.Expression.Lambda<Func<T>>(
                System.Linq.Expressions.Expression.New(typeof(T))
            ).Compile();
    }
}
