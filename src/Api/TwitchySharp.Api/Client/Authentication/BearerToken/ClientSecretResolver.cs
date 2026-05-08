namespace TwitchySharp.Api;

public delegate ValueTask<ClientSecret?> ClientSecretResolver(ClientId? clientId, CancellationToken ct = default);
