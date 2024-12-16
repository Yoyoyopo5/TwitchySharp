using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.User.Authorization;
/// <summary>
/// A user’s authorization has been revoked for your client id.
/// Use this webhook to meet government requirements for handling user data, such as GDPR, LGPD, or CCPA.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported with the webhook transport. It cannot be used with WebSockets.
/// Requires an app access token created by the same client id as the <paramref name="ClientId"/> parameter.
/// </remarks>
/// <param name="ClientId">
/// The client id of the application to get authorization revocation notifications for. 
/// This must match the client id in the application access token used to make the request.
/// </param>
public sealed record UserAuthorizationRevoke(string ClientId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.USER_AUTHORIZATION_REVOKE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("client_id", ClientId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
