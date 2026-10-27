// This example acquires a user access token at startup and uses it to change the user's chat color.

using System.Net;
using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Api.Helix.Chat;

// Get these from https://dev.twitch.tv/console
// Avoid hardcoding client secrets. This is provided just for example.
ClientId clientId = new("12345");
ClientSecret clientSecret = new("12345");
RedirectUri redirectUri = new("http://localhost:5000/");

// Function to set up a simple HTTP listener to receive the code from the browser.
async Task<string> waitForCode(CancellationToken ct)
{
    using HttpListener codeListener = new();
    codeListener.Prefixes.Add(redirectUri.ToString());
    codeListener.Start();
    HttpListenerContext context = await codeListener.GetContextAsync();
    try
    {
        string? queryCode = context.Request.QueryString.GetValues("code")?.FirstOrDefault();
        string? queryState = context.Request.QueryString.GetValues("state")?.FirstOrDefault();

        async Task writeResponse(string message, CancellationToken ct)
        {
            ReadOnlyMemory<byte> buffer = Encoding.UTF8.GetBytes(message);
            context.Response.ContentLength64 = buffer.Length;
            await context.Response.OutputStream.WriteAsync(buffer, ct);
        }

        if (queryCode is null || queryState != authState)
        {
            context.Response.StatusCode = 400;
            await writeResponse("Authentication failed: " + queryCode switch
            {
                null => "code query parameter was null.",
                _ when queryState != state => "state query parameter was null or invalid.",
                _ => "unknown error."
            }, ct);
            throw new Exception("Invalid code request parameters.");
        }
        await writeResponse("Authentication code received.", ct);
        return queryCode;
    }
    finally
    {
        context.Response.Close();
        codeListener.Stop();
    }
};

string authState = Guid.CreateVersion7().ToString();
AuthorizationCodeGrantUrl oauthUrl = new()
{
    ClientId = clientId,
    RedirectUri = redirectUri,
    Scopes = [ Scope.OpenId, Scope.UserManageChatColor ], // The OpenId scope lets us get the authorizing user's id without making a `ValidateAccessTokenRequest`.
    State = authState,
    Nonce = authState
};

// Open the auth url in the default browser.
System.Diagnostics.Process.Start(new ProcessStartInfo(oauthUrl.Uri.AbsoluteUri) { UseShellExecute = true });

string code = await waitForCode(default);

AuthorizationCodeRequest authorizationRequest = new()
{
    ClientId = clientId,
    ClientSecret = clientSecret,
    Code = code,
    RedirectUri = redirectUri
};

var client = new TwitchClient()
    .WithClient(clientId, clientSecret);
    // You could also use .UseUserAccessTokens here if you need automatic token refreshing and caching.
    // Since we are just making one request, we will demonstrate setting the token manually.

TwitchResponse<AuthorizationCodeResponseContent> response = await client.SendAsync(authorizationRequest);

if (response.Content is null)
{
    // You can inspect the response's error via response.Error
    Console.WriteLine("An error occurred.");
    return;
}

if (response.Content.GetOidc()?.Sub is not UserId userId)
{
    Console.WriteLine("Could not get user id from OpenId JWT.");
    return;
}

UserAccessToken accessToken = response.Content.AccessToken;

UpdateUserChatColorRequest updateColorRequest = new()
{
    UserId = userId,
    Color = ChatColor.Blue
};

// Here we set the bearer token manually, preserving the other properties of the AuthenticationContext.
updateColorRequest = updateColorRequest with
{
    AuthenticationContext = updateColorRequest.AuthenticationContext with
    {
        BearerToken = accessToken
    }
};

TwitchResponse<UpdateUserChatColorResponseContent> updateColorResponse = await client.SendAsync(updateColorRequest);

if (updateColorRequest.Error is not null)
{
    Console.WriteLine("An error occurred while changing chat color.");
    return;
}

Console.WriteLine("Chat color changed to blue.");