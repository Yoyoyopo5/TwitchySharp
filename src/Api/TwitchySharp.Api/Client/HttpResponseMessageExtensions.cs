namespace TwitchySharp.Api;

internal static class HttpResponseMessageExtensions
{
    public static TwitchResponse<TResponseContent> ToTwitchResponse<TResponseContent>(
        this HttpResponseMessage httpResponse,
        TwitchRequest request,
        TResponseContent content
        )
        => new()
        {
            Request = request,
            StatusCode = httpResponse.StatusCode,
            RateLimitDetails = httpResponse.Headers.ToTwitchRateLimitDetails(),
            Content = content
        };

    public static Task<TwitchApiException> ToTwitchApiException(
        this HttpResponseMessage httpResponse,
        TwitchRequest request,
        CancellationToken ct
        )
        => TwitchApiException.FromRequestResponse(request, httpResponse, ct);
}
