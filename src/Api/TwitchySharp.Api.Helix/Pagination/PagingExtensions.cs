using System.Runtime.CompilerServices;

namespace TwitchySharp.Api;

public static class PagingExtensions
{
    /// <summary>
    /// Create a request for the page of results after the specified cursor.
    /// </summary>
    /// <typeparam name="T">The request type to page.</typeparam>
    /// <param name="request">The request to get the next page of results for.</param>
    /// <param name="after"><inheritdoc cref="IForwardPageableRequest.After" path="/summary"/></param>
    /// <returns>A new <typeparamref name="T"/> with the <see cref="IForwardPageableRequest.After"/> property set to <paramref name="after"/>.</returns>
    public static T NextPage<T>(this T request, PaginationCursor after)
        where T : TwitchRequest, IForwardPageableRequest
        => request with { After = after };

    /// <summary>
    /// Create a request for the page of results before the specified cursor.
    /// </summary>
    /// <typeparam name="T">The request type to page.</typeparam>
    /// <param name="request">The request to get the previous page of results for.</param>
    /// <param name="before"><inheritdoc cref="IBackwardPageableRequest.Before" path="/summary"/></param>
    /// <returns>A new <typeparamref name="T"/> with the <see cref="IBackwardPageableRequest.Before"/> property set to <paramref name="before"/>.</returns>
    public static T PreviousPage<T>(this T request, PaginationCursor before)
        where T : TwitchRequest, IBackwardPageableRequest
        => request with { Before = before };

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="after"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='after']"/></param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IForwardPageableRequest.After"/> property set to <paramref name="after"/>
    /// or <see langword="null"/> if <paramref name="after"/> is <see langword="null"/>.
    /// </returns>
    public static T? NextPage<T>(this T request, PaginationCursor? after)
        where T : TwitchRequest, IForwardPageableRequest
        => after.HasValue ? request.NextPage(after.Value) : default;

    /// <summary>
    /// <inheritdoc cref="PreviousPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="before"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)" path="/param[@name='before']"/></param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IBackwardPageableRequest.Before"/> property set to <paramref name="before"/>
    /// or <see langword="null"/> if <paramref name="before"/> is <see langword="null"/>.
    /// </returns>
    public static T? PreviousPage<T>(this T request, PaginationCursor? before)
        where T : TwitchRequest, IBackwardPageableRequest
        => before.HasValue ? request.PreviousPage(before.Value) : default;

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="after">
    /// The <see cref="Pagination"/> to get results after.
    /// </param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IForwardPageableRequest.After"/> property set to <see cref="Pagination.Cursor"/>
    /// or <see langword="null"/> if the <see cref="Pagination.Cursor"/> is <see langword="null"/>.
    /// </returns>
    public static T? NextPage<T>(this T request, Pagination after)
        where T : TwitchRequest, IForwardPageableRequest
        => request.NextPage(after.Cursor);

    /// <summary>
    /// <inheritdoc cref="PreviousPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)"/></typeparam>
    /// <param name="request"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="before">The <see cref="Pagination"/> to get results before.</param>
    /// <returns>
    /// A new <typeparamref name="T"/> with the <see cref="IBackwardPageableRequest.Before"/> property set to <see cref="Pagination.Cursor"/>
    /// or <see langword="null"/> if the <see cref="Pagination.Cursor"/> is <see langword="null"/>.
    /// </returns>
    public static T? PreviousPage<T>(this T request, Pagination before)
        where T : TwitchRequest, IBackwardPageableRequest
        => request.PreviousPage(before.Cursor);

    /// <summary>
    /// <inheritdoc cref="NextPage{T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="TRequest"><inheritdoc cref="NextPage{T}(T, PaginationCursor)"/></typeparam>
    /// <typeparam name="TResponse">The response type to get <see cref="Pagination"/> from.</typeparam>
    /// <param name="request"><inheritdoc cref="NextPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="response">The <see cref="IPageableResponse"/> to get <see cref="Pagination"/> from.</param>
    /// <returns>
    /// A new <typeparamref name="TRequest"/> with the <see cref="IForwardPageableRequest.After"/> property set to <see cref="IPageableResponse.Pagination"/>'s cursor
    /// or <see langword="null"/> if the <see cref="IPageableResponse.Pagination"/>'s cursor is <see langword="null"/>.
    /// </returns>
    public static TRequest? NextPage<TRequest, TResponse>(this TRequest request, TResponse response)
        where TRequest : TwitchRequest, IForwardPageableRequest
        where TResponse : IPageableResponse
        => request.NextPage(response.Pagination);

    /// <summary>
    /// <inheritdoc cref="PreviousPage{T}(T, Pagination){T}(T, PaginationCursor)"/>
    /// </summary>
    /// <typeparam name="TRequest"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)"/></typeparam>
    /// <typeparam name="TResponse">The response type to get <see cref="Pagination"/> from.</typeparam>
    /// <param name="request"><inheritdoc cref="PreviousPage{T}(T, PaginationCursor)" path="/param[@name='request']"/></param>
    /// <param name="response">The <see cref="IPageableResponse"/> to get <see cref="Pagination"/> from.</param>
    /// <returns>
    /// A new <typeparamref name="TRequest"/> with the <see cref="IBackwardPageableRequest.Before"/> property set to <see cref="IPageableResponse.Pagination"/>'s cursor
    /// or <see langword="null"/> if the <see cref="IPageableResponse.Pagination"/>'s cursor is <see langword="null"/>.
    /// </returns>
    public static TRequest? PreviousPage<TRequest, TResponse>(this TRequest request, TResponse response)
        where TRequest : TwitchRequest, IBackwardPageableRequest
        where TResponse : IPageableResponse
        => request.PreviousPage(response.Pagination);

    /// <inheritdoc cref="NextPage{TRequest, TResponse}(TRequest, TResponse)"/>
    public static TRequest? NextPage<TRequest, TResponse>(this TResponse response, TRequest request)
        where TRequest : TwitchRequest, IForwardPageableRequest
        where TResponse : IPageableResponse
        => request.NextPage(response);

    /// <inheritdoc cref="PreviousPage{TRequest, TResponse}(TRequest, TResponse)"/>
    public static TRequest? PreviousPage<TRequest, TResponse>(this TResponse response, TRequest request)
        where TRequest : TwitchRequest, IBackwardPageableRequest
        where TResponse : IPageableResponse
        => request.PreviousPage(response);

    /// <summary>
    /// Get all pages of a <see cref="IForwardPageableRequest"/> with a specified <paramref name="client"/>.
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
        where TRequest : TwitchRequest<TResponseContent>, IForwardPageableRequest
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
