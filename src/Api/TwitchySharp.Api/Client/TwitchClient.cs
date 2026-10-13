using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api;

/// <summary>
/// A Twitch API client.
/// </summary>
/// <remarks>
/// Provides a default implementation for sending typed <see cref="TwitchRequest"/> commands using an <see cref="System.Net.Http.HttpClient"/>.
/// Can be further configured using
/// <see cref="Set{T}(ResolveRequestDependency{T})"/>
/// and <see cref="Configure{T}(Func{ResolveRequestDependency{T}, ResolveRequestDependency{T}})"/> methods,
/// as well as various built-in extension methods.
/// </remarks>
public record TwitchClient : ITwitchClient, ITwitchRequestDependencyCollection<TwitchClient>
{
    public ITwitchRequestDependencyCollection Resolvers { get; init; }
            = new ImmutableRequestDependencyCollection() // Default Pipeline
                .UseRequestContent()
                .WithSystemTextJsonRequestContentObjectConverter(JsonConfig.ApiOptions)
                .SetResolver<HttpRequestMessage>((scope, ct) => scope.ResolveOrDefault<HttpContent>(ct)
                    .MapAsync(content => new HttpRequestMessage(scope.Request.Method, scope.Request.RequestUri) { Content = content })
                )
                .ConfigureDispose<ITwitchRequestDependencyCollection, HttpRequestMessage>(request => request.Dispose())
                .UseAuthenticatedRequests()
                .WithHttpClient(new())
                .UseHttpClientToSendRequests();

    public TwitchClient SetResolver<T>(ResolveRequestDependency<T> resolver)
        => this with { Resolvers = Resolvers.SetResolver(resolver) };

    public ResolveRequestDependency<T>? GetResolver<T>()
        => Resolvers.GetResolver<T>();

    /// <summary>
    /// Configured send middleware.
    /// </summary>
    public ImmutableList<Middleware> ConfiguredMiddleware { get; init; } = [];

    /// <summary>
    /// Represents generic behavior around the <see cref="TwitchClient"/> send function.
    /// </summary>
    /// <remarks>
    /// Allows for configuration against the entire send pipeline via <see cref="ConfiguredMiddleware"/> while maintaining the generic context.
    /// </remarks>
    public abstract class Middleware
    {
        public Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> ConfigureSend<TResponseContent>(
            Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send
            )
            => (scope, ct) => SendAsync(send, scope, ct);

        /// <param name="send">The inner send function. Use this to send the request.</param>
        protected abstract ValueTask<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<TwitchResponse<TResponseContent>>> send, ITwitchRequestDependencyScope scope, CancellationToken ct);
    }

    /// <summary>
    /// Send a <see cref="TwitchRequest"/> using the configured client.
    /// </summary>
    /// <typeparam name="TResponseContent">The response content type.</typeparam>
    /// <exception cref="TwitchApiException">When the HTTP response is a non-success status code.</exception>
    /// <exception cref="InvalidOperationException">When another error occurred.</exception>
    /// <inheritdoc/>
    public async Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
    {
        // This method MUST remain async otherwise the scope will be disposed before the response is resolved!
        await using MemoizingRequestDependencyScope requestScope = new(request, Resolvers.WithTwitchClient(this));

        return await ConfiguredMiddleware.Aggregate(
            (ITwitchRequestDependencyScope scope, CancellationToken ct) => scope.CreateResponse(request, ct),
            (send, next) => next.ConfigureSend(send))(requestScope, ct);
    }
}

public static class TwitchClientExtensions
{
    public static TwitchClient WithHttpClient(
        this TwitchClient client,
        HttpClient httpClient
        )
        => client.SetFixed(httpClient);

    public static TwitchClient UseMiddleware(
        this TwitchClient client,
        TwitchClient.Middleware middleware
        )
        => client with { ConfiguredMiddleware = client.ConfiguredMiddleware.Add(middleware) };
}

internal static class DefaultRequestPipelineExtensions
{
    public static ITwitchRequestDependencyCollection UseTwitchIdentity(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.From<ITwitchRequestDependencyCollection, TwitchIdentity?, ITwitchRequestAuthenticationContext<TwitchIdentity>>(
                context => context?.Identity
            )
            .From<ITwitchRequestDependencyCollection, ClientId?, ITwitchRequestAuthenticationContext<TwitchIdentity>>(ctx => ctx?.Identity?.ClientId)
            .MapNullableStruct<ITwitchRequestDependencyCollection, ClientId>()
            .As<ITwitchRequestDependencyCollection, TwitchIdentity.Client, TwitchIdentity>()
            .As<ITwitchRequestDependencyCollection, TwitchIdentity.User, TwitchIdentity>()
            .From<ITwitchRequestDependencyCollection, UserId?, TwitchIdentity.User>(identity => identity?.UserId)
            .MapNullableStruct<ITwitchRequestDependencyCollection, UserId>()
            .As<ITwitchRequestDependencyCollection, TwitchIdentity.Extension, TwitchIdentity>()
            .From<ITwitchRequestDependencyCollection, ExtensionId?, TwitchIdentity.Extension>(identity => identity?.ExtensionId)
            .MapNullableStruct<ITwitchRequestDependencyCollection, ExtensionId>()
            .ConfigureConditional<ITwitchRequestDependencyCollection, TwitchIdentity>(
                (identity, scope, ct) => ValueTask.FromResult<Result<bool>>(identity is { ClientId: null }),
                (identity, scope, ct) => scope.ResolveOrDefault<ClientId?>(ct).MapAsync(clientId => identity! with { ClientId = clientId })
                );

    public static ITwitchRequestDependencyCollection UseAuthenticatedRequests(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.RequestAs<ITwitchRequestDependencyCollection, IAuthenticatedTwitchRequest>()
            .FromRequest(
                request => (request as IAuthenticatedTwitchRequest)?.AuthenticationContext
            )
            .From<ITwitchRequestDependencyCollection, BearerToken?, ITwitchRequestAuthenticationContext<TwitchIdentity>>(
                context => context?.BearerToken
            )
            .MapNullableStruct<ITwitchRequestDependencyCollection, BearerToken>()
            .From<ITwitchRequestDependencyCollection, BearerTokenType?, ITwitchRequestAuthenticationContext<TwitchIdentity>>(
                context => context?.TokenType
            )
            .MapNullableStruct<ITwitchRequestDependencyCollection, BearerTokenType>()
            .UseTwitchIdentity()
            .When(scope => scope.Request is IAuthenticatedTwitchRequest)
            .Configure<RequestDependencyConditionalConfiguration<ITwitchRequestDependencyCollection>, HttpRequestMessage?>(
                next => next.WithAuthenticationHeaders()
            )
            .EndWhen();

    public static ITwitchRequestDependencyCollection WithHttpClient(
        this ITwitchRequestDependencyCollection resolvers,
        HttpClient client
        )
        => resolvers.SetFixed(client);

    public static ITwitchRequestDependencyCollection UseHttpClientToSendRequests(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.SetResolver<HttpResponseMessage>((scope, ct) =>
            scope.ResolveRequired<HttpClient>(ct)
                .BindAsync(httpClient => scope.ResolveRequired<HttpRequestMessage>(ct)
                .BindAsync<HttpRequestMessage, HttpResponseMessage>(async httpRequestMessage => await httpClient.SendAsync(httpRequestMessage, ct))));

    public static ITwitchRequestDependencyCollection UseRequestContent(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.Configure<ITwitchRequestDependencyCollection, HttpContent?>(next => (scope, ct) =>
            scope.Request.Content is not null
                ? ValueTask.FromResult<Result<HttpContent?>>(scope.Request.Content)
                : next(scope, ct));

    public static ITwitchRequestDependencyCollection WithSystemTextJsonRequestContentObjectConverter(
        this ITwitchRequestDependencyCollection resolvers,
        JsonSerializerOptions options
        )
        => resolvers.Configure<ITwitchRequestDependencyCollection, HttpContent?>(next => (scope, ct) =>
            scope.Request.ContentObject is null
            ? next(scope, ct)
            : ValueTask.FromResult<Result<HttpContent?>>(JsonContent.Create(scope.Request.ContentObject, options: options)));

    public static ITwitchRequestDependencyCollection WithAuthenticationHeaders(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.Configure<ITwitchRequestDependencyCollection, HttpRequestMessage?>(next => next.WithAuthenticationHeaders());

    public static ResolveRequestDependency<HttpRequestMessage?> ConfigureWith<T>(
        this ResolveRequestDependency<HttpRequestMessage?> resolveRequestMessage,
        Func<HttpRequestMessage, T, HttpRequestMessage> configure
        )
        => (scope, ct) => resolveRequestMessage(scope, ct).BindAsync(request => request is null
            ? ValueTask.FromResult<Result<HttpRequestMessage?>>(request)
            : scope.ResolveOrDefault<T>(ct).MapAsync<T?, HttpRequestMessage?>(t => t is null
            ? request
            : configure(request, t)));

    public static ResolveRequestDependency<HttpRequestMessage?> WithAuthenticationHeaders(
        this ResolveRequestDependency<HttpRequestMessage?> resolveHttpRequestMessage
        )
        => resolveHttpRequestMessage.WithClientIdHeader().WithAuthorizationBearerHeader();

    public static ResolveRequestDependency<HttpRequestMessage?> WithClientIdHeader(
        this ResolveRequestDependency<HttpRequestMessage?> resolveHttpRequest
        )
        => resolveHttpRequest.ConfigureWith<ClientId?>((request, clientId) => clientId.HasValue
            ? request.AddOrUpdateHeader("Client-Id", clientId.Value)
            : request);

    public static ResolveRequestDependency<HttpRequestMessage?> WithAuthorizationBearerHeader(
        this ResolveRequestDependency<HttpRequestMessage?> resolveHttpRequest
        )
        => resolveHttpRequest.ConfigureWith<BearerToken?>((request, token) => token.HasValue
            ? request.SetAuthorizationBearer(token.Value)
            : request);

    public static ValueTask<TwitchResponse<T>> CreateResponse<T>(
        this ITwitchRequestDependencyScope scope,
        TwitchRequest<T> request,
        CancellationToken ct)
        => scope.ResolveRequired<HttpResponseMessage>(ct)
            .MatchAsync(
                e => ValueTask.FromResult(new TwitchResponse<T>(e)),
                r => request.CreateResponse(r, ct)
            );

    public static ITwitchRequestDependencyCollection WithTwitchClient(
        this ITwitchRequestDependencyCollection resolvers,
        ITwitchClient client
        )
        => resolvers.TrySetResolver((_, _) => ValueTask.FromResult(new Result<ITwitchClient>(client)));
}
