using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.AuthorizationResolution;

public delegate ValueTask<ClientSecret?> ClientSecretResolver(ClientId? clientId, CancellationToken ct = default);
