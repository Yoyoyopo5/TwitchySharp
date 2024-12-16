using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.User;
/// <summary>
/// A user has updated their account.
/// </summary>
/// <remarks>
/// No authorization required. 
/// If the client id used to make the request has a user access token that includes <see cref="Scope.UserReadEmail"/>, the notification will include the email field.
/// </remarks>
/// <param name="UserId">The user id for the user you want update notifications for.</param>
public sealed record UserUpdate(string UserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.USER_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("user_id", UserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
