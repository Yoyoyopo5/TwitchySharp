using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

public static class TwitchClientBuilderExtensions
{
    public static ITwitchClientBuilder WithAuthorizationResolution(this ITwitchClientBuilder builder,
        TwitchAuthorizationResolutionOptions? options = null
        )
    {
        // build resolver dependencies
        options ??= new();
        (var resolveClientId, var resolveBearerToken) = (options.ClientIdResolver, options.BearerTokenResolver);
        return builder.Use(next => async (context, ct)
            => await next(context.Request switch
            {
                IAuthorizedTwitchRequest request => await resolveClientId(request.AuthorizationContext, ct) switch
                {
                    ClientId clientId => context with
                    {
                        AuthorizationHeaders = new()
                        {
                            ClientId = clientId,
                            // Use real resolved client id in bearer token resolver if it was null on the context.
                            // This will be the case if TwitchIdentity.Client.Default is used (most app access token endpoints).
                            BearerToken = await resolveBearerToken(request.AuthorizationContext.Identity.ClientId switch
                            {
                                ClientId => request.AuthorizationContext,
                                _ => request.AuthorizationContext with
                                {
                                    Identity = request.AuthorizationContext.Identity.WithClientId(clientId)
                                }
                            }, ct)
                        }
                    },
                    _ => context with 
                    { 
                        AuthorizationHeaders = context.AuthorizationHeaders with
                        {
                            BearerToken = await resolveBearerToken(request.AuthorizationContext, ct)
                        }
                    }
                },
                _ => context
            }, ct));
    }
}
