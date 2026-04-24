using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using TwitchySharp.Api;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Authorization.ClientUrls;
using TwitchySharp.Api.AuthorizationResolution;
using TwitchySharp.Api.Tests.E2E.AcquireUserAccessToken;
using TwitchySharp.Shared.Models;

IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables()
    .Build();

Options options = config.GetRequiredSection("Twitch").Get<Options>()!;
ClientId clientId = new(options.ClientId);
ClientSecret clientSecret = new(options.ClientSecret);
RedirectUri redirectUri = new(options.RedirectUrl);

JsonSerializerOptions serializerOptions = new()
{
    WriteIndented = true,
};

TwitchClientBuilder builder = new() { HttpClient = new() };
ITwitchClient client = builder.Build();

string state = Guid.NewGuid().ToString();
string nonce = Guid.NewGuid().ToString();

AuthorizationCodeGrantUrl authUrl = new()
{
    ClientId = clientId,
    Scopes = Scope.All,
    RedirectUri = redirectUri,
    Claims = new(),
    State = state,
    Nonce = nonce
};

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

        if (queryCode is null || queryState != state)
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

async ValueTask<AccessTokenDetails.User> acquireToken(string code, CancellationToken ct)
    => (await client.SendAsync(new AuthorizationCodeRequest()
    {
        ClientId = clientId,
        ClientSecret = clientSecret,
        Code = code,
        RedirectUri = redirectUri
    }, ct)).Content switch
    {
        { } response => new AccessTokenDetails.User()
        {
            AccessToken = response.AccessToken,
            Scopes = response.Scope?.ToHashSet() ?? [],
            ExpiresAt = DateTimeOffset.UtcNow + response.ExpiresIn,
            Identity = new TwitchIdentity.User(
                new(response.GetOidc()?.Sub 
                    ?? throw new Exception("OIDC claims were not present, and the authorizing user could not be identified.")), 
                clientId
                ),
            RefreshToken = response.RefreshToken
        }
    };

string clipboard(string text)
{
    TextCopy.ClipboardService.SetText(text);
    return text;
}

Console.WriteLine("--- Acquire New Twitch User Access Token ---");
Console.WriteLine("This program will generate a new user access token with all scopes.");
Console.WriteLine("Once generated, the access token details will be copied to the clipboard.");
Console.WriteLine("Use with care!");
Console.WriteLine();
Console.WriteLine("--- Current Configuration ---");
Console.WriteLine($"Client Id: {options.ClientId}");
Console.WriteLine($"Client Secret: {(string.IsNullOrWhiteSpace(options.ClientSecret) ? "null" : "[REDACTED]")}");
Console.WriteLine($"Redirect URL: {options.RedirectUrl}");
Console.WriteLine();
Console.WriteLine("--- Authorization Code Url ---");
Console.WriteLine("Open this in a web browser to begin authentication. It will also be copied to the clipboard.");
Console.WriteLine();
Console.WriteLine(clipboard(authUrl.Uri.AbsoluteUri));
Console.WriteLine();
Console.WriteLine("Waiting for redirect...");
Console.WriteLine();
try
{
    string code = await waitForCode(default);
    Console.Clear();
    Console.WriteLine("Authentication code received, requesting token from Twitch API...");
    Console.WriteLine();
    var tokenDetails = await acquireToken(code, new CancellationTokenSource(10_000).Token);
    Console.WriteLine($"Request success.");
    clipboard(JsonSerializer.Serialize(tokenDetails, serializerOptions));
    Console.WriteLine("Access token details were copied to the clipboard.");
}
catch (OperationCanceledException cancelEx) {  Console.WriteLine(cancelEx.Message); }
catch (TwitchApiException apiEx) { Console.WriteLine($"An error occurred when requesting the token: {apiEx.StatusCode} {apiEx.Message} {Encoding.UTF8.GetString(apiEx.Content)} {((FormUrlEncodedContent)apiEx.Request.Content!).ReadAsStringAsync().Result}"); }
catch (Exception ex) { Console.WriteLine($"An error occurred when waiting for the code: {ex.Message}"); }

Console.WriteLine();
Console.WriteLine("Press enter to exit the program...");
Console.ReadLine();