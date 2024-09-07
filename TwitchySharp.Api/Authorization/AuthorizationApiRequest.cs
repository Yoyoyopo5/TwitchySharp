using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Authorization;
public abstract class AuthorizationApiRequest<TResponse>(string path)
    : TwitchApiRequest<TResponse>($"/oauth2{path}")
{
    public sealed override string BaseUrl => "https://id.twitch.tv";
    public override string? ContentType => "application/x-www-form-urlencoded";
}