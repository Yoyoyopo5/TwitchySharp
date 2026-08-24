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
/// <see cref="Remove{T}"/>
/// and <see cref="Configure{T}(Func{ResolveRequestDependency{T}, ResolveRequestDependency{T}})"/> methods,
/// as well as various built-in extension methods.
/// </remarks>
/// <param name="HttpClient">The <see cref="System.Net.Http.HttpClient"/> to send requests with.</param>
public record TwitchClient(HttpClient HttpClient) : ITwitchClient
{
    private ImmutableDictionary<Type, ResolveRequestDependency> Resolvers { get; init; }
        = ImmutableDictionary<Type, ResolveRequestDependency>.Empty // Default Pipeline
            // HttpContent
            .UseRequestContent()
            .WithSystemTextJsonRequestContentObjectConverter(JsonConfig.ApiOptions)
            // HttpRequestMessage
            .SetResolver<HttpRequestMessage>(async (context, ct) =>
                await context.GetOrDefault<HttpContent>(ct) switch
                {
                    { Error: Error error } result => new DependencyResult<HttpRequestMessage>(error, result.UpdatedScope),
                    { } result => new DependencyResult<HttpRequestMessage>(new HttpRequestMessage(context.Request.Method, context.Request.RequestUri)
                    {
                        Content = result.Value
                    }, result.UpdatedScope)
                }
            )
            .ConfigureFor<IAuthenticatedTwitchRequest, HttpRequestMessage>(
                next => next.WithAuthenticationHeaders()
            )
            // HttpResponseMessage
            .WithHttpClient(HttpClient)
            .WithTwitchApiExceptions();
            // TwitchResponse<T> is added inside of SendAsync<T>

    /// <summary>
    /// Set the client's request dependency resolver for <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// This overwrites the existing resolver for <typeparamref name="T"/>,
    /// including any configuration applied to it via <see cref="Configure{T}(Func{ResolveRequestDependency{T}, ResolveRequestDependency{T}})"/>.
    /// </remarks>
    /// <typeparam name="T">The type of dependency to set a resolver for.</typeparam>
    /// <param name="resolve">The resolver.</param>
    /// <returns>
    /// A new <see cref="TwitchClient"/> that will resolve <typeparamref name="T"/>
    /// per-request using <paramref name="resolve"/>.
    /// </returns>
    public TwitchClient Set<T>(ResolveRequestDependency<T> resolve)
        => this with { Resolvers = Resolvers.SetResolver(resolve) };

    /// <summary>
    /// Configure the client's request dependency resolver for <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>
    /// If an existing resolver for <typeparamref name="T"/> is not set, a default resolver
    /// returning <see langword="default"/> will be added before configuring.
    /// </remarks>
    /// <typeparam name="T">The type of dependency to configure the resolver for.</typeparam>
    /// <param name="configure">
    /// The configuration function for the resolver.
    /// Accepts a single parameter of the last configured resolver for <typeparamref name="T"/>
    /// and returns a new resolver of the same type which will be used to resolve <typeparamref name="T"/> per-request.
    /// </param>
    /// <returns>
    /// A new <see cref="TwitchClient"/> with the resolver configuration applied.
    /// </returns>
    public TwitchClient Configure<T>(Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure)
        => this with { Resolvers = Resolvers.Configure(configure) };

    /// <summary>
    /// Configure the client's request dependency resolver for <typeparamref name="T"/> for requests of type <typeparamref name="TRequest"/>.
    /// </summary>
    /// <remarks>
    /// The result of <paramref name="configure"/> will only be run against requests of type <typeparamref name="TRequest"/>.
    /// </remarks>
    /// <typeparam name="TRequest">The request type to branch resolver configuration for.</typeparam>
    /// <typeparam name="T">The dependency type to configure the resolver for.</typeparam>
    /// <param name="configure">
    /// The configuration function for the resolver.
    /// Accepts a single parameter of the last configured resolver for <typeparamref name="T"/>
    /// and returns a new resolver of the same type which will be used to resolve <typeparamref name="T"/> per-request of type <typeparamref name="TRequest"/>.
    /// </param>
    /// <returns>
    /// A new <see cref="TwitchClient"/> with the <typeparamref name="TRequest"/>-specific resolver configuration applied.
    /// </returns>
    public TwitchClient ConfigureFor<TRequest, T>(Func<ResolveRequestDependency<T>, ResolveRequestDependency<T>> configure)
        => this with { Resolvers = Resolvers.ConfigureFor<TRequest, T>(configure) };

    /// <summary>
    /// Remove the client's request dependency resolver for <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The dependency typeto remove the resolver for.</typeparam>
    /// <returns>A new <see cref="TwitchClient"/> without a resolver for <typeparamref name="T"/>.</returns>
    public TwitchClient Remove<T>()
        => this with { Resolvers = Resolvers.Remove(typeof(T)) };

    private class SystemTextJsonContentConverter : IResponseContentConverter
    {
        public bool CanConvert<TResponseContent>(TwitchRequest<TResponseContent> request)
            => true;
        public ValueTask<TResponseContent> Convert<TResponseContent>(TwitchRequest<TResponseContent> request, Stream contentStream, CancellationToken ct)
            => JsonSerializer.DeserializeAsync<TResponseContent>(contentStream, JsonConfig.ApiOptions, ct)!;
    }

    private class RequestSpecificContentConverter : IResponseContentConverter
    {
        public bool CanConvert<TResponseContent>(TwitchRequest<TResponseContent> request)
            => request.ConvertResponseContent is not null;
        public ValueTask<TResponseContent> Convert<TResponseContent>(TwitchRequest<TResponseContent> request, Stream content, CancellationToken ct)
            => request.ConvertResponseContent!.Invoke(content, ct);
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
    /// By default, this contains a single converter to use the <see cref="TwitchRequest{T}.ConvertResponseContent"/> on each request.
    /// In addition, the <see cref="DefaultResponseConverter"/> will be used to convert requests that do not implement <see cref="TwitchRequest{T}.ConvertResponseContent"/>.
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
    /// Send a <see cref="TwitchRequest"/> using the configured client.
    /// </summary>
    /// <typeparam name="TResponseContent">The response content type.</typeparam>
    /// <param name="request"><inheritdoc path="/param[@name='request']"/></param>
    /// <param name="ct"><inheritdoc path="/param[@name='ct']"/></param>
    /// <returns><inheritdoc/></returns>
    /// <exception cref="TwitchApiException">When the HTTP response is a non-success status code.</exception>
    /// <exception cref="InvalidOperationException">When another error occurred.</exception>
    public async Task<TwitchResponse<TResponseContent>> SendAsync<TResponseContent>(TwitchRequest<TResponseContent> request, CancellationToken ct = default)
    {
        // We have to get the typed content converter per request.
        IResponseContentConverter contentConverter = ResponseConverters.FirstOrDefault(rc => rc.CanConvert(request)) ?? DefaultResponseConverter;

        using MemoizingRequestDependencyScope requestScope = new()
        {
            Request = request,
            Resolvers = Resolvers.WithTypedResponse<TResponseContent>(contentConverter)
        };

        return await requestScope.GetOrDefault<TwitchResponse<TResponseContent>>(ct) switch
        {
            { Error: not null } invalid => invalid.Error switch
            {
                ExceptionError exceptionError => throw exceptionError.Exception,
                Error => throw new InvalidOperationException(invalid.Error.Message)
            },
            { Value: not null } valid => valid.Value,
            _ => throw new InvalidOperationException("The response resolved to null.")
        };
    }
}

public static class TwitchClientExtensions
{
    /// <summary>
    /// Configure the client's request dependency resolver for <typeparamref name="T"/>
    /// so that the result from the previously configured resolver for <typeparamref name="T"/>
    /// is used unless it is <see langword="null"/>, in which <paramref name="resolver"/> result is used instead.
    /// </summary>
    /// <typeparam name="T"><inheritdoc cref="TwitchClient.Configure{T}(Func{ResolveRequestDependency{T}, ResolveRequestDependency{T}})" path="/typeparam[@name='T']"/></typeparam>
    /// <param name="client">The client to configure.</param>
    /// <param name="resolver">The resolver to call if previously configured resolver returned a result with a <see langword="null"/> value.</param>
    /// <returns>A new <see cref="TwitchClient"/> with the configured null coalesce resolver.</returns>
    public static TwitchClient ConfigureAsNullCoalesce<T>(
        this TwitchClient client,
        ResolveRequestDependency<T> resolver
        )
        => client.Configure<T>(next => async (context, ct) =>
            await next(context, ct) switch
            {
                { Error: Error error } errored => new DependencyResult<T>(error, errored.UpdatedScope),
                { Value: T value } resolved => new DependencyResult<T>(value, resolved.UpdatedScope),
                { } none => await resolver(none.UpdatedScope, ct)
            });

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
}

/// <summary>
/// Determines how to deserialize response content from the Twitch API.
/// </summary>
public interface IResponseContentConverter
{
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
    /// <param name="content">The content <see cref="Stream"/> to convert from.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>A <see cref="ValueTask"/> containing the converted <typeparamref name="TResponseContent"/>.</returns>
    ValueTask<TResponseContent> Convert<TResponseContent>(
        TwitchRequest<TResponseContent> request,
        Stream content,
        CancellationToken ct
        );
}

internal static class DefaultRequestPipelineExtensions
{
    public static ImmutableDictionary<Type, ResolveRequestDependency> WithHttpClient(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        HttpClient client)
        => resolvers
            .SetResolver<HttpClient>((context, ct) => ValueTask.FromResult(new DependencyResult<HttpClient>(client, context)))
            .SetResolver<HttpResponseMessage>(async (context, ct) =>
            {
                DependencyResult<HttpClient> clientResult = await context.GetOrDefault<HttpClient>(ct);
                (HttpRequestMessage? request, RequestDependencyScope updatedScope, Error? error)
                    = await clientResult.GetOrDefault<HttpRequestMessage>(ct);

                return error is not null
                    ? new DependencyResult<HttpResponseMessage>(error, updatedScope)
                    : clientResult.Value is not HttpClient client
                    ? new DependencyResult<HttpResponseMessage>(new Error("No HttpClient was configured."), updatedScope)
                    : request is not HttpRequestMessage message
                    ? new DependencyResult<HttpResponseMessage>(new Error("No HttpRequestMessage resolver was configured."), updatedScope)
                    : new DependencyResult<HttpResponseMessage>(await client.SendAsync(message, ct), updatedScope);
            });

    public static ImmutableDictionary<Type, ResolveRequestDependency> UseRequestContent(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers
        )
        => resolvers.Configure<HttpContent>(next => (context, ct) =>
            context.Request.Content is not null
                ? ValueTask.FromResult(new DependencyResult<HttpContent>(context.Request.Content, context))
                : next(context, ct));

    public static ImmutableDictionary<Type, ResolveRequestDependency> WithSystemTextJsonRequestContentObjectConverter(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        JsonSerializerOptions options
        )
        => resolvers.Configure<HttpContent>(next => (context, ct) =>
            context.Request.ContentObject is null
            ? next(context, ct)
            : ValueTask.FromResult(new DependencyResult<HttpContent>(JsonContent.Create(context.Request.ContentObject, options: options), context)));

    public static ImmutableDictionary<Type, ResolveRequestDependency> WithTwitchApiExceptions(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers
        )
        => resolvers.Configure<HttpResponseMessage>(next => async (context, ct) =>
        {
            (HttpResponseMessage? response, RequestDependencyScope nextContext, Error? error)
                = await next(context, ct);

            return response is null || response.IsSuccessStatusCode
                ? new DependencyResult<HttpResponseMessage>(response, nextContext)
                : new DependencyResult<HttpResponseMessage>(new ExceptionError(await response.ToTwitchApiException(context.Request, ct)), nextContext);
        });

    public static ImmutableDictionary<Type, ResolveRequestDependency> WithAuthenticationHeaders(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers
        )
        => resolvers.Configure<HttpRequestMessage>(next => next.WithAuthenticationHeaders());

    public static ResolveRequestDependency<HttpRequestMessage> WithAuthenticationHeaders(
        this ResolveRequestDependency<HttpRequestMessage> resolveHttpRequestMessage
        )
        => resolveHttpRequestMessage.WithClientIdHeader().WithAuthorizationBearerHeader();

    public static ResolveRequestDependency<HttpRequestMessage> WithClientIdHeader(
        this ResolveRequestDependency<HttpRequestMessage> resolveHttpRequest
        )
        => async (context, ct) =>
        {
            (HttpRequestMessage? httpRequest, RequestDependencyScope nextContext, Error? error)
                = await resolveHttpRequest(context, ct);

            if (error is not null)
                return new DependencyResult<HttpRequestMessage>(error, nextContext);

            if (httpRequest is null)
                return new DependencyResult<HttpRequestMessage>(httpRequest, nextContext);

            (ClientId? clientId, nextContext, error)
                = await nextContext.GetOrDefault<ClientId?>(ct);

            if (clientId.HasValue)
                httpRequest.Headers.AddOrUpdate("Client-Id", clientId.Value);

            return new DependencyResult<HttpRequestMessage>(httpRequest, nextContext);
        };

    public static ResolveRequestDependency<HttpRequestMessage> WithAuthorizationBearerHeader(
        this ResolveRequestDependency<HttpRequestMessage> resolveHttpRequest
        )
        => async (context, ct) =>
        {
            (HttpRequestMessage? httpRequest, RequestDependencyScope nextContext, Error? error)
                = await resolveHttpRequest(context, ct);

            if (error is not null)
                return new DependencyResult<HttpRequestMessage>(error, nextContext);

            if (httpRequest is null)
                return new DependencyResult<HttpRequestMessage>(httpRequest, nextContext);

            (BearerToken? bearerToken, nextContext, error)
                = await nextContext.GetOrDefault<BearerToken?>(ct);

            if (bearerToken.HasValue)
                httpRequest.Headers.Authorization = new("Bearer", bearerToken.Value);

            return new DependencyResult<HttpRequestMessage>(httpRequest, nextContext);
        };

    public static ImmutableDictionary<Type, ResolveRequestDependency> WithTypedResponse<TResponseContent>(
        this ImmutableDictionary<Type, ResolveRequestDependency> resolvers,
        IResponseContentConverter responseContentConverter
        )
        => resolvers.TrySetResolver<TwitchResponse<TResponseContent>>(async (context, ct) =>
        {
            if (context.Request is not TwitchRequest<TResponseContent> typedRequest)
                return new DependencyResult<TwitchResponse<TResponseContent>>(new Error("Incongruent request and response content type."), context);

            (HttpResponseMessage? response, RequestDependencyScope nextContext, Error? error)
                = await context.GetOrDefault<HttpResponseMessage>(ct);

            return error is not null
                ? new DependencyResult<TwitchResponse<TResponseContent>>(error, nextContext)
                : response is null
                ? new DependencyResult<TwitchResponse<TResponseContent>>(new Error("An HttpResponseMessage was unable to be resolved. Did you forget to add an HttpClient?"), nextContext)
                : new DependencyResult<TwitchResponse<TResponseContent>>(response.ToTwitchResponse(typedRequest, await responseContentConverter.Convert(
                    typedRequest,
                    await response.Content.ReadAsStreamAsync(ct),
                    ct
                    )), nextContext);
        });
}
