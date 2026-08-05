using System.Runtime.CompilerServices;

namespace TwitchySharp.Api;

public static class PagingExtensions
{
    /// <summary>
    /// Create a request for the page of results after the specified cursor.
    /// </summary>
    /// <typeparam name="T">The request type to page.</typeparam>
    /// <param name="request">The request to get the next page of results for.</param>
    /// <param name="cursor"><inheritdoc cref="IPageableRequest.After" path="/summary"/></param>
    /// <returns>A new <typeparamref name="T"/> with the <see cref="IPageableRequest.After"/> property set to <paramref name="cursor"/>.</returns>
    public static T NextPage<T>(this T request, PaginationCursor cursor)
        where T : TwitchRequest, IPageableRequest
        => request with { After = cursor };

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="cursor"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='cursor']"/></param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IPageableRequest.After"/> property set to <paramref name="cursor"/>
    /// or <see langword="null"/> if <paramref name="cursor"/> is <see langword="null"/>.
    /// </returns>
    public static T? NextPage<T>(this T request, PaginationCursor? cursor)
        where T : TwitchRequest, IPageableRequest
        => cursor.HasValue ? request.NextPage(cursor.Value) : default;

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="pagination">
    /// The <see cref="Pagination"/> to get results after.
    /// </param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IPageableRequest.After"/> property set to <see cref="Pagination.Cursor"/>
    /// or <see langword="null"/> if the <see cref="Pagination.Cursor"/> is <see langword="null"/>.
    /// </returns>
    public static T? NextPage<T>(this T request, Pagination pagination)
        where T : TwitchRequest, IPageableRequest
        => request.NextPage(pagination.Cursor);

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="TRequest"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <typeparam name="TResponse">The response type to get <see cref="Pagination"/> from.</typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="response">The <see cref="IPageableResponse"/> to get <see cref="Pagination"/> from.</param>
    /// <returns>
    /// A new <typeparamref name="TRequest"/> with the <see cref="IPageableRequest.After"/> property set to <see cref="IPageableResponse.Pagination"/>'s cursor
    /// or <see langword="null"/> if the <see cref="IPageableResponse.Pagination"/>'s cursor is <see langword="null"/>.
    /// </returns>
    public static TRequest? NextPage<TRequest, TResponse>(this TRequest request, TResponse response)
        where TRequest : TwitchRequest, IPageableRequest
        where TResponse : IPageableResponse
        => request.NextPage(response.Pagination);

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="TRequest"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <typeparam name="TResponse"><inheritdoc cref="NextPage{TRequest, TResponse}(TRequest, TResponse)" path="/typeparam[@name='TResponse']"/></typeparam>
    /// <param name="response"><inheritdoc cref="NextPage{TRequest, TResponse}(TRequest, TResponse)" path="/param[@name='response']"/></param>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <returns><inheritdoc cref="NextPage{TRequest, TResponse}(TRequest, TResponse)"/></returns>
    public static TRequest? NextPage<TRequest, TResponse>(this TResponse response, TRequest request)
        where TRequest : TwitchRequest, IPageableRequest
        where TResponse : IPageableResponse
        => request.NextPage(response);

    /// <summary>
    /// Get all pages of a <see cref="IPageableRequest"/> with a specified <paramref name="client"/>.
    /// </summary>
    /// <remarks>
    /// The amount of results per page is determined by the <paramref name="request"/>
    /// (typically by the <c>First</c> property).
    /// </remarks>
    /// <typeparam name="TRequest">The request type to get all pages for.</typeparam>
    /// <typeparam name="TResponseContent">
    /// The type of response content of the <paramref name="request"/>.
    /// /This must implement <see cref="IPageableResponse"/>.
    /// </typeparam>
    /// <param name="request">The first paged request to send, sending new paged requests based on it for each received response until no more pages remain.</param>
    /// <param name="client">The <see cref="ITwitchClient"/> to send each request with.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>An <see cref="IAsyncEnumerable{T}"/> containing each page as a <see cref="TwitchResponse{TResponseContent}"/>.</returns>
    public static async IAsyncEnumerable<TwitchResponse<TResponseContent>> GetPages<TRequest, TResponseContent>(
        this TRequest request,
        ITwitchClient client,
        [EnumeratorCancellation] CancellationToken ct = default
        )
        where TRequest : TwitchRequest<TResponseContent>, IPageableRequest
        where TResponseContent : IPageableResponse
    {
        TRequest? nextRequest = request;

        while (nextRequest is not null)
        {
            TwitchResponse<TResponseContent> response = await client.SendAsync(nextRequest, ct);
            yield return response;

            nextRequest = nextRequest.NextPage(response.Content.Pagination);
        }
    }
}
