using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using TwitchySharp.Infrastructure.Functional;
using TwitchySharp.Serialization;
using static TwitchySharp.Api.TwitchIdentity;

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
                // HttpContent
                .UseRequestContent()
                .WithSystemTextJsonRequestContentObjectConverter(JsonConfig.ApiOptions)
                // HttpRequestMessage
                .SetResolver<HttpRequestMessage>((scope, ct) => scope.ResolveOrDefault<HttpContent>(ct)
                    .MapAsync(content => new HttpRequestMessage(scope.Request.Method, scope.Request.RequestUri) { Content = content })
                )
                .ConfigureDispose<ITwitchRequestDependencyCollection, HttpRequestMessage>(request => request.Dispose())
                .UseAuthenticatedRequests()
                .WithHttpClient(new())
                .UseHttpClientToSendRequests()
                // HttpResponseMessage
                .WithTwitchApiExceptions();

    public TwitchClient SetResolver<T>(ResolveRequestDependency<T> resolver)
        => this with { Resolvers = Resolvers.SetResolver(resolver) };

    public ResolveRequestDependency<T>? GetResolver<T>()
        => Resolvers.GetResolver<T>();

    private class SystemTextJsonContentConverter : IResponseContentConverter
    {
        public bool CanConvert<TResponseContent>(TwitchRequest<TResponseContent> request)
            => true;
        public async ValueTask<Result<TResponseContent>> Convert<TResponseContent>(TwitchRequest<TResponseContent> request, HttpResponseMessage response, CancellationToken ct)
        {
            try
            {
                return (await JsonSerializer.DeserializeAsync<TResponseContent>(response.Content.ReadAsStream(ct), JsonConfig.ApiOptions, ct))!;
            }
            finally
            {
                response.Dispose();
            }
        }
    }

    private class RequestSpecificContentConverter : IResponseContentConverter
    {
        public bool CanConvert<TResponseContent>(TwitchRequest<TResponseContent> request)
            => request.ConvertResponse is not null;
        public ValueTask<Result<TResponseContent>> Convert<TResponseContent>(TwitchRequest<TResponseContent> request, HttpResponseMessage response, CancellationToken ct)
            => request.ConvertResponse!.Invoke(request, response, ct);
    }

    /// <summary>
    /// The collection of response converters available to the client.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Converters are matched using <see cref="IResponseContentConverter.CanConvert{TResponseContent}(TwitchRequest{TResponseContent})"/>,
    /// with the first converter that returns <see langword="true"/> being used to convert the response content in <see cref="SendAsync{TResponseContent}(TwitchRequest{TResponseContent}, CancellationToken)"/>.
    /// </para>
    /// <para>
    /// By default, this contains a single converter to use the <see cref="TwitchRequest{T}.ConvertResponse"/> on each request.
    /// In addition, the <see cref="DefaultResponseConverter"/> will be used to convert requests that do not implement <see cref="TwitchRequest{T}.ConvertResponse"/>.
    /// </para>
    /// </remarks>
    // We use stack here to get delegate decorator like behavior (i.e. newest converter applies first).
    public ImmutableStack<IResponseContentConverter> ResponseConverters { get; init; }
        = [new RequestSpecificContentConverter()];

    /// <summary>
    /// The default response converter to use for requests.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Defaults to using <see cref="JsonSerializer"/>.
    /// </para>
    /// <para>
    /// This converter is called if no other <see cref="ResponseConverters"/> return <see langword="true"/>
    /// for <see cref="IResponseContentConverter.CanConvert{TResponseContent}(TwitchRequest{TResponseContent})"/>.
    /// As such, it should be able to convert any response content type (e.g. a JSON deserializer).
    /// </para>
    /// </remarks>
    public IResponseContentConverter DefaultResponseConverter { get; init; }
        = new SystemTextJsonContentConverter();

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
        public Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<Result<TwitchResponse<TResponseContent>>>> ConfigureSend<TResponseContent>(
            Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<Result<TwitchResponse<TResponseContent>>>> send
            )
            => (scope, ct) => SendAsync(send, scope, ct);

        /// <param name="send">The inner send function. Use this to send the request.</param>
        protected abstract ValueTask<Result<TwitchResponse<TResponseContent>>> SendAsync<TResponseContent>(Func<ITwitchRequestDependencyScope, CancellationToken, ValueTask<Result<TwitchResponse<TResponseContent>>>> send, ITwitchRequestDependencyScope scope, CancellationToken ct);
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
        // We have to get the typed content converter per request.
        IResponseContentConverter contentConverter = ResponseConverters.FirstOrDefault(rc => rc.CanConvert(request)) ?? DefaultResponseConverter;

        // This method MUST remain async otherwise the scope will be disposed before the response is resolved!
        await using MemoizingRequestDependencyScope requestScope = new(request, Resolvers.WithTwitchClient(this).WithTypedResponse<TResponseContent>(contentConverter));

        return await ConfiguredMiddleware.Aggregate((ITwitchRequestDependencyScope scope, CancellationToken ct)
            => scope.ResolveRequired<TwitchResponse<TResponseContent>>(ct), (send, next)
            => next.ConfigureSend(send))(requestScope, ct).MatchAsync(
            e => e switch
            {
                ExceptionError exceptionError => throw exceptionError.Exception,
                Error => throw new InvalidOperationException(e.Message)
            },
            valid => valid
            ).AsTask();
    }
}

public static class TwitchClientExtensions
{
    /// <summary>
    /// Add a specific client converter to the client.
    /// </summary>
    /// <remarks>
    /// Converters are evaluated from last added to first,
    /// with the first converter that returns <see langword="true"/> from <see cref="IResponseContentConverter.CanConvert{TResponseContent}(TwitchRequest{TResponseContent})"/>
    /// being used to convert the repsonse content.
    /// </remarks>
    /// <param name="client">The client to add the response converter to.</param>
    /// <param name="converter">The converter to add.</param>
    /// <returns>A new <see cref="TwitchClient"/> with the added response converter.</returns>
    public static TwitchClient AddResponseConverter(
        this TwitchClient client,
        IResponseContentConverter converter
        )
        => client with { ResponseConverters = client.ResponseConverters.Push(converter) };

    public static TwitchClient WithHttpClient(
        this TwitchClient client,
        HttpClient httpClient
        )
        => client.SetFixed(httpClient);
}

/// <summary>
/// Determines how to deserialize response content from the Twitch API.
/// </summary>
public interface IResponseContentConverter
{
    delegate ValueTask<Result<TResponseContent>> ConvertResponse<TResponseContent>(
        TwitchRequest<TResponseContent> request,
        HttpResponseMessage response,
        CancellationToken ct
        );

    // This interface allows us to inject generic behavior (required for binding response content).

    /// <summary>
    /// Determine if the converter can deserialize <typeparamref name="TResponseContent"/>.
    /// </summary>
    /// <typeparam name="TResponseContent">The content type to deserialize.</typeparam>
    /// <param name="request">The request for which to convert the response content.</param>
    /// <returns>A <see langword="bool"/> indicating if the converter can deserialize the request's response content into <typeparamref name="TResponseContent"/>.</returns>
    bool CanConvert<TResponseContent>(TwitchRequest<TResponseContent> request);

    /// <summary>
    /// Convert a <see cref="Stream"/> of response content into a <typeparamref name="TResponseContent"/>.
    /// </summary>
    /// <typeparam name="TResponseContent">The type to convert to.</typeparam>
    /// <param name="request"><inheritdoc cref="CanConvert{TResponseContent}(TwitchRequest{TResponseContent})"/></param>
    /// <param name="response">The response to convert from.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> containing the converted <typeparamref name="TResponseContent"/>.</returns>
    ValueTask<Result<TResponseContent>> Convert<TResponseContent>(
        TwitchRequest<TResponseContent> request,
        HttpResponseMessage response,
        CancellationToken ct
        );
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

    public static ITwitchRequestDependencyCollection WithTwitchApiExceptions(
        this ITwitchRequestDependencyCollection resolvers
        )
        => resolvers.Configure<ITwitchRequestDependencyCollection, HttpResponseMessage?>(
            next => (scope, ct) => next(scope, ct)
                .BindAsync<HttpResponseMessage?, HttpResponseMessage?>(async response => response is null || response.IsSuccessStatusCode
                    ? response
                    : new ExceptionError(await response.ToTwitchApiException(scope.Request, ct))));

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

    private static ValueTask<Result<TwitchResponse<TResponseContent>>> CreateResponse<TResponseContent>(
        this IResponseContentConverter responseContentConverter,
        HttpResponseMessage httpResponse,
        TwitchRequest<TResponseContent> request,
        CancellationToken ct
        )
        => responseContentConverter.Convert(request, httpResponse, ct)
            .MapAsync(content => httpResponse.ToTwitchResponse(request, content));

    public static ITwitchRequestDependencyCollection WithTypedResponse<TResponseContent>(
        this ITwitchRequestDependencyCollection resolvers,
        IResponseContentConverter responseContentConverter
        )
        => resolvers.TrySetResolver<ITwitchRequestDependencyCollection, TwitchResponse<TResponseContent>?>((scope, ct) =>
            scope.Request is not TwitchRequest<TResponseContent> typedRequest
                ? ValueTask.FromResult<Result<TwitchResponse<TResponseContent>?>>(new Error("Incongruent request and response content type."))
                : scope.ResolveRequired<HttpResponseMessage>(ct)
                    .BindAsync(response => responseContentConverter.CreateResponse(response, typedRequest, ct)
                        .MapAsync<TwitchResponse<TResponseContent>, TwitchResponse<TResponseContent>?>(r => r)));

    public static ITwitchRequestDependencyCollection WithTwitchClient(
        this ITwitchRequestDependencyCollection resolvers,
        ITwitchClient client
        )
        => resolvers.TrySetResolver((_, _) => ValueTask.FromResult(new Result<ITwitchClient>(client)));
}
