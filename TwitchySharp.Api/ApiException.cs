using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TwitchySharp.Api;
public class ApiException : Exception
{
    /// <summary>
    /// The response from the API indicating the error.
    /// </summary>
    public HttpResponseMessage Response { get; }

    public ApiException(string message, HttpResponseMessage httpResponse)
        : base(message)
    {
        Response = httpResponse;
    }

    public ApiException(string message, HttpResponseMessage httpResponse, Exception innerException)
        : base(message, innerException)
    {
        Response = httpResponse;
    }
}
