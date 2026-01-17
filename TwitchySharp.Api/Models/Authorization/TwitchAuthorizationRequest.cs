using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Core;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Authorization;
public record TwitchAuthorizationRequest<TResponseContent>
    : TwitchRequest<TResponseContent>
{
    private const string TWITCH_AUTHORIZATION_HOST = "id.twitch.tv";
    private const string AUTHORIZATION_BASE_PATH = "/oauth2";
    public TwitchAuthorizationRequest(string path, HttpQueryParameters? queryParams = null)
    {
        RequestUri = new UriBuilder()
        {
            Scheme = "https",
            Host = TWITCH_AUTHORIZATION_HOST,
            Path = AUTHORIZATION_BASE_PATH + path,
            Query = queryParams?.ToString()
        }.Uri;
    }
}