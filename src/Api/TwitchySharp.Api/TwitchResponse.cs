using System.Net;
using System.Text.Json;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api;

public record UnexpectedHttpStatusCodeError(
    HttpStatusCode StatusCode,
    string Content) : Error($"Twitch API returned unexpected status code {StatusCode}.");

internal static class UnexpectedHttpStatusCodeErrorExtensions
{
    extension(UnexpectedHttpStatusCodeError _)
    {
        public static async ValueTask<TwitchResponse<T>> CreateResponse<T>(HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                return new TwitchResponse<T>(new UnexpectedHttpStatusCodeError(response.StatusCode, await response.Content.ReadAsStringAsync(ct)));
            }
            catch (Exception ex)
            {
                return new TwitchResponse<T>(new ExceptionError(ex));
            }
        }
    }
}

internal static class EmptyResponse
{
    public static async ValueTask<TwitchResponse<T>> Create<T>(
        HttpResponseMessage httpResponse,
        CancellationToken ct)
        where T : new()
    {
        using HttpResponseMessage r = httpResponse;
        return r.IsSuccessStatusCode
            ? new TwitchResponse<T>(new T())
            : await UnexpectedHttpStatusCodeError.CreateResponse<T>(httpResponse, ct);
    }
}

internal static class JsonResponse
{
    public record JsonExceptionError(JsonException JsonException) : ExceptionError(JsonException);

    public static async ValueTask<TwitchResponse<T>> Create<T>(
        HttpResponseMessage httpResponse,
        CancellationToken ct
        )
    {
        using HttpResponseMessage r = httpResponse;
        try
        {
            // We assume success status is required for JSON endpoints.
            if (!r.IsSuccessStatusCode)
                return await UnexpectedHttpStatusCodeError.CreateResponse<T>(r, ct);

            // Null forgiving operator is used here.
            // Null can return if the JSON is "null" literal value.
            // Since we want to allow for nullable TResponseContent types,
            // and we can't ascertain nullability of a reference type at runtime,
            // we forgive the null possibility
            return new((await JsonSerializer.DeserializeAsync<T>(r.Content.ReadAsStream(ct), JsonConfig.ApiOptions, ct))!);
        }
        catch (JsonException ex)
        {
            return new TwitchResponse<T>(new JsonExceptionError(ex));
        }
        catch (Exception ex)
        {
            return new TwitchResponse<T>(new ExceptionError(ex));
        }
        finally
        {
            r.Dispose();
        }
    }
}

/// <summary>
/// A general Twitch API response with strongly-typed content.
/// </summary>
/// <remarks>
/// A <i>result</i> type. Either <see cref="Error"/> or <see cref="Content"/>
/// will be not <see langword="null"/>. A <see cref="Match"/> function is provided.
/// </remarks>
/// <typeparam name="TResponseContent">The response content type.</typeparam>
public record TwitchResponse<TResponseContent>
{
    /// <summary>
    /// Create a response from an error.
    /// </summary>
    /// <param name="error">The response error.</param>
    public TwitchResponse(Error error) => Error = error;
    /// <summary>
    /// Create a response from its content.
    /// </summary>
    /// <param name="content">The response content.</param>
    public TwitchResponse(TResponseContent content) => Content = content;

    public static TwitchResponse<TResponseContent> FromResult(Result<TResponseContent> result)
        => result.Match(
            e => new TwitchResponse<TResponseContent>(e),
            v => new TwitchResponse<TResponseContent>(v)
            );

    /// <summary>
    /// The error that occurred during the request, if any.
    /// </summary>
    public Error? Error { get; }
    /// <summary>
    /// The response content, if any.
    /// </summary>
    /// <remarks>
    /// Access is null-unsafe if <see cref="Error"/> is not <see langword="null"/>
    /// (the value will be <see langword="default"/>).
    /// Use <see cref="Match"/> or null-check <see cref="Error"/> for safe access.
    /// </remarks>
    public TResponseContent? Content { get; }

    public T Match<T>(Func<TResponseContent, T> content, Func<Error, T> error)
        => Error is not null
            ? error(Error)
            : content(Content!);
}
