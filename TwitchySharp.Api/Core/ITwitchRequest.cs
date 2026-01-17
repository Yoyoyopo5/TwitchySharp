using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TwitchySharp.Api.Core;

public interface ITwitchRequest
{
    HttpRequestMessage ToHttpRequestMessage(JsonSerializerOptions serializerOptions); // Remember that HttpRequestMessage is not reusable, so a method is more appropriate than a property here.
}

public interface ITwitchRequest<TResponseContent> : ITwitchRequest;