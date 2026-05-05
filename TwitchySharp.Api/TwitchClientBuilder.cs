using System;
using System.Net.Http;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api;

public record TwitchClientBuilder : ITwitchClientBuilder
{
    public HttpClient HttpClient { get; init; } = new HttpClient();

    private static TwitchRequestHandler CreateTerminalHandler(HttpClient httpClient)
        => async (context, ct) =>
        {
            using HttpRequestMessage request = context.Request.ToHttpRequestMessage();
            request.AddTwitchAuthorizationHeaders(context.AuthorizationHeaders); // Somewhat uncomposable, but this is probably fine.
            using HttpResponseMessage response = await httpClient.SendAsync(request, ct).ConfigureAwait(false);

            return response switch
            {
                { IsSuccessStatusCode: true } => await context.Request.CreateResponse(response, ct),
                _ => throw await TwitchApiException.FromRequestResponseAsync(context.Request, response, ct).ConfigureAwait(false)
            };
        };

    private readonly MiddlewarePipelineBuilder<TwitchRequestHandler> _handlerBuilder = new();
    public ITwitchClientBuilder Use(Func<TwitchRequestHandler, TwitchRequestHandler> func)
    {
        _handlerBuilder.Use(func);
        return this;
    }
    public ITwitchClient Build()
        => new TwitchClient { RequestHandler = _handlerBuilder.Finally(CreateTerminalHandler(HttpClient)) };
}
