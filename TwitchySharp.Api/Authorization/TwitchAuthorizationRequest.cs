using System;
using System.IO;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Authorization;

public abstract record TwitchAuthorizationRequest<TResponseContent>
    : TwitchRequest<TResponseContent>
{
    private const string TWITCH_AUTHORIZATION_HOST = "id.twitch.tv";
    private const string AUTHORIZATION_BASE_PATH = "/oauth2";
    protected abstract string Path { get; }
    protected virtual HttpQueryParameters? QueryParameters { get; }
    public override Uri RequestUri => new UriBuilder()
    {
        Scheme = "https",
        Host = TWITCH_AUTHORIZATION_HOST,
        Path = AUTHORIZATION_BASE_PATH + Path,
        Query = QueryParameters?.ToString()
    }.Uri;
}