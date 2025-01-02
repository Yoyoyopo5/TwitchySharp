using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Polls;
/// <summary>
/// Contains a list of polls belonging to a specific channel.
/// </summary>
public record GetPollsResponse
{
    /// <summary>
    /// The list of polls.
    /// The polls are returned in descending order of <see cref="ChatPoll.StartedAt"/> unless you specify ids in the request, in which case they're returned in the same order as you passed them in the request.
    /// The list is empty if the broadcaster hasn't created polls.
    /// </summary>
    public required ChatPoll[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
