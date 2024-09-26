using System;
using System.Collections.Generic;
using System.Text;

namespace TwitchySharp.Api.Helix.Channels;
public abstract class HelixChannelRequest<TResponse, TRequestData>(string path, string clientId, string accessToken, TRequestData data)
    : HelixApiRequest<TResponse, TRequestData>("/channels" + path, clientId, accessToken, data);