using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.AuthorizationResolution;

public delegate ValueTask<ClientSecret?> ClientSecretResolver(ClientId? clientId, CancellationToken ct = default);
