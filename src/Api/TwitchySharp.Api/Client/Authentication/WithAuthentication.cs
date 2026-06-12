namespace TwitchySharp.Api;

/// <summary>
/// Contains Twitch API pipeline extensions to add authentication headers to requests.
/// </summary>
public static class TwitchApiAuthentication
{
    /// <inheritdoc cref="WithAuthentication(SendTwitchRequest, TwitchAuthorizationResolutionOptions?)"/>
    /// <param name="client">The client to add authentication to.</param>
    public static TwitchClient WithAuthentication(
        this TwitchClient client,
        TwitchAuthorizationResolutionOptions? options = null
        )
        => client.With(send => send.WithAuthentication(options));

    /// <summary>
    /// Add Twitch API request authentication to the request pipeline.
    /// </summary>
    /// <remarks>
    /// This resolves the authentication HTTP headers based on the <see cref="TwitchAuthorizationRequestContext"/> of <see cref="IAuthorizedTwitchRequest"/>s.
    /// </remarks>
    /// <param name="send">The send pipeline to add authentication to.</param>
    /// <param name="options">The authentication resolution options.</param>
    /// <returns>A send pipeline configured to resolve Twitch authorization header values.</returns>
    public static SendTwitchRequest WithAuthentication(
        this SendTwitchRequest send,
        TwitchAuthorizationResolutionOptions? options = null
        )
    {
        options ??= new(); // build resolver dependencies
        return async (context, ct) => await send(context.Request switch
            {
                IAuthorizedTwitchRequest request => await options.ClientIdResolver(request.AuthorizationContext, ct) switch
                {
                    ClientId clientId => TwitchAuthorizationRequestContext.From(context) with
                    {
                        AuthorizationHeaders = new()
                        {
                            ClientId = clientId,
                            BearerToken = await options.BearerTokenResolver(request.AuthorizationContext with
                            {
                                Identity = request.AuthorizationContext.Identity.WithClientId(clientId)
                            }, ct)
                        }
                    },
                    _ => TwitchAuthorizationRequestContext.From(context) with
                    {
                        AuthorizationHeaders = new()
                        {
                            BearerToken = await options.BearerTokenResolver(request.AuthorizationContext, ct)
                        }
                    }
                },
                _ => context
            }, ct);
    }
}
