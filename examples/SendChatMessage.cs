// This example sends a chat message to the broadcaster's chat as the broadcaster.

using TwitchySharp.Api;
using TwitchySharp.Api.Authentication;
using TwitchySharp.Api.Helix.Chat;

// Get these from https://dev.twitch.tv/console
// Avoid hardcoding client secrets. This is provided just for example.
ClientId clientId = new("12345"); 
ClientSecret clientSecret = new("1237429");

UserId broadcasterId = new("12345");

var client = new TwitchClient()
    .UseAppAccessTokens()
    .WithClient(clientId, clientSecret)
    .AlwaysUsePriorAuthorization(); 
    
// AlwaysUsePriorAuthorization will try to use app tokens for supported user authorized endpoints (e.g. "SendChatMessageRequest"). 
// Requires the user to have authorized the app previously with the correct scopes (Scope.UserWriteChat, Scope.UserBot, and Scope.ChannelBot, in this case).

SendChatMessageRequest request = new()
{
    Message = new()
    {
        BroadcasterId = broadcasterId,
        SenderId = broadcasterId,
        Message = "Hello from TwitchySharp!"
    }
};

await client.SendAsync(request);