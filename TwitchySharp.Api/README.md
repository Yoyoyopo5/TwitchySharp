An open-source .NET Core library for integrating applications with Twitch HTTP API endpoints.

### Basic Example Usage (using app access token)
```C#
// Create an instance of the TwitchApi class.
// This class handles the sending and receiving of HTTP messages, as well as formatting and deserializing them.
// You can optionally provide an instance of HttpClient and/or RateLimiter when creating the TwitchHttpClient.
TwitchHttpClient httpClient = new TwitchHttpClient();
TwitchApi api = new TwitchApi(httpClient);

// Get an app access token.
// Note that most requests require a user access token, which requires a user to authorize the app through their browser.
const string CLIENT_ID = "12345"; // Provided by Twitch when creating an app via dev.twitch.tv developer console.
const string CLIENT_SECRET = "67890"; // Generated using dev.twitch.tv developer console.
ClientCredentialsRequest appTokenRequest = new ClientCredentialsRequest(CLIENT_ID, CLIENT_SECRET);
ClientCredentialsResponse appTokenResponse = await api.SendRequestAsync(appTokenRequest);
string appAccessToken = appTokenResponse.AccessToken;

// Let's get information about a specific user, like finding their Twitch user id.
const string USERNAME = "yoyoyopo5";
GetUsersRequest userInfoRequest = new GetUsersRequest(CLIENT_ID, appAccessToken, userLogins: [ USERNAME ]);
GetUsersResponse userInfoResponse = await api.SendRequestAsync(userInfoRequest);
string userId = userInfoResponse.Data.First().Id; // Note that most responses are in the form of an array of data, even if there is only one object.

// Print the user id to the console.
Console.WriteLine($"{USERNAME}'s user id is: {userId}");
```

### User Access Token Example
```C#
public async Task<string> GetUserAccessToken(TwitchApi api, string clientId, string clientSecret)
{
    // First, create the URL a user should visit to authorize your app on their Twitch account.
    // See in-code documentation for details on each argument.
    const string REDIRECT_URL = "http://localhost:5000";
    Scope[] authorizationScopes = [ Scope.UserReadFollows ];
    string authorizationUrl = new AuthorizationCodeGrantUrl(clientId, REDIRECT_URL, authorizationScopes).Url;

    // We can display this url to the user of our app.
    Console.WriteLine($"Please visit the following URL in your browser: {authorizationUrl}");

    // When a user authorizes the app, Twitch will send them to the REDIRECT_URL we used above with a code query parameter.
    // We use that code to get a user access token from Twitch.
    // Let's say the user just pastes the code into the console for this demo.
    // Of course, you can set up a simple webserver to do this step automatically too.
    Console.WriteLine("Paste your authorization code below: ");
    string code = Console.ReadLine();

    // Now, let's send the code to the API with our client credentials to get the actual user access token.
    // Note that the response also contains a refresh token, which can be used to get a fresh user access token without making the user re-authorize the app.
    AuthorizationCodeRequest userTokenRequest = new AuthorizationCodeRequest(clientId, clientSecret, code, REDIRECT_URL);
    AuthorizationCodeResponse userTokenResponse = await api.SendRequestAsync(userTokenRequest);
    return userTokenResponse.AccessToken;
}
```