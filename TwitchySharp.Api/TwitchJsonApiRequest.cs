using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace TwitchySharp.Api;
public abstract class TwitchJsonApiRequest<TResponse, TRequestData>
    : TwitchApiRequest<TResponse>
{
    private string _data;
    public sealed override string? Data => _data;
    public sealed override string? ContentType => "application/json";
    public override HttpMethod Method => HttpMethod.Post;
    public TwitchJsonApiRequest(string path, TRequestData data)
        : base(path)
    {
        _data = JsonSerializer.Serialize(data, JsonConfig.ApiOptions);
    }
}
