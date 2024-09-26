using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace TwitchySharp.Api.Helix;
public abstract class HelixApiRequest<TResponse, TRequestData>
    : TwitchJsonApiRequest<TResponse, TRequestData>
{
    public override string BaseUrl => "https://api.twitch.tv";
    public HelixApiRequest(string path, string clientId, string accessToken, TRequestData data)
        : base("/helix" + path, data)
    {
        ClientId = clientId;
        AccessToken = accessToken;
    }
}
