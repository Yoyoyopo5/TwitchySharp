using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix;
/// <summary>
/// Used to create a request to a Twitch Helix API endpoint.
/// </summary>
/// <typeparam name="TResponseContent">The response content type of the request.</typeparam>
public abstract record TwitchHelixRequest<TResponseContent>
    : TwitchRequest<TResponseContent>, IRequireAuthorization
{
    private const string TWITCH_HELIX_HOST = "api.twitch.tv";
    private const string HELIX_BASE_PATH = "/helix";
    protected abstract string Path { get; }
    public TwitchApiIdentity Identity
    {
        get => _configuredIdentity ?? DefaultIdentity; // Allow override of default identity.
        init => _configuredIdentity = value;
    }
    private TwitchApiIdentity? _configuredIdentity;
    protected virtual TwitchApiIdentity DefaultIdentity { get; } = TwitchApiIdentity.Default;
    public virtual IEnumerable<Scope> ValidScopes { get; } = [];
    public virtual AccessToken? OverrideAccessToken { get; init; }
    protected virtual HttpQueryParameters? QueryParameters { get; }
    public override Uri RequestUri => new UriBuilder
    {
        Scheme = "https",
        Host = TWITCH_HELIX_HOST,
        Path = HELIX_BASE_PATH + Path,
        Query = QueryParameters?.ToString()
    }.Uri;
}
