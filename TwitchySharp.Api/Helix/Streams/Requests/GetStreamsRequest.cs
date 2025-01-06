using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// Gets a list of all streams.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-streams">get streams</see> for more information.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <para>
/// The returned list will be in descending order by the number of viewers watching the stream. 
/// Because viewers come and go during a stream, it’s possible to find duplicate or missing streams in the list as you page through the results.
/// </para>
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">An app or user access token.</param>
/// <param name="searchQuery">
/// The query to use when searching active streams. 
/// Leave this <see langword="null"/> to return all streams.
/// </param>
/// <param name="first">
/// The maximum number of items to return per page in the response. 
/// The minimum page size is 1 item per page and the maximum is 100 items per page. 
/// The default is 20.
/// </param>
/// <param name="before">
/// The cursor used to get the previous page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// </param>
public class GetStreamsRequest(
    string clientId,
    string accessToken,
    StreamQuery? searchQuery = null,
    int? first = null,
    string? before = null,
    string? after = null
    )
    : HelixApiRequest<GetStreamsResponse>(
        "/streams" +
        new HttpQueryParameters()
            .Add("user_id", searchQuery?.UserIds)
            .Add("user_login", searchQuery?.UserLogins)
            .Add("game_id", searchQuery?.GameIds)
            .Add("type", searchQuery?.Type?.Value)
            .Add("language", searchQuery?.Languages)
            .Add("first", first?.ToString())
            .Add("before", before)
            .Add("after", after),
        clientId,
        accessToken
        );

/// <summary>
/// Used to search for active livestreams.
/// </summary>
public record StreamQuery
{
    /// <summary>
    /// A list of user ids used to filter the list of streams. 
    /// Returns only the streams of those users that are broadcasting. 
    /// You may specify a maximum of 100 ids.
    /// </summary>
    public IEnumerable<string>? UserIds { get; set; }
    /// <summary>
    /// A list of user logins (usernames) used to filter the list of streams. 
    /// Returns only the streams of those users that are broadcasting. 
    /// You may specify a maximum of 100 login names.
    /// </summary>
    public IEnumerable<string>? UserLogins { get; set; }
    /// <summary>
    /// A game (category) id used to filter the list of streams. 
    /// Returns only the streams that are broadcasting the game (category). 
    /// You may specify a maximum of 100 ids. 
    /// </summary>
    public IEnumerable<string>? GameIds { get; set; }
    /// <summary>
    /// The type of stream to filter the list of streams by.
    /// The default is <see cref="StreamType.All"/>.
    /// </summary>
    public StreamType? Type { get; set; }
    /// <summary>
    /// A language code used to filter the list of streams. 
    /// Returns only streams that broadcast in the specified language. 
    /// Specify the language using an ISO 639-1 two-letter language code or other if the broadcast uses a language not in the list of <see href="https://help.twitch.tv/s/article/languages-on-twitch#streamlang">supported stream languages</see>.
    /// You may specify a maximum of 100 language codes.
    /// </summary>
    public IEnumerable<string>? Languages { get; set; }
}

/// <summary>
/// Contains static definitions for stream types.
/// </summary>
/// <remarks>
/// Dev note: not sure what the difference is between <see cref="All"/> and <see cref="Live"/> at this point.
/// </remarks>
/// <param name="Value">
/// The custom value for a stream type.
/// Don't use this unless you know what you're doing. 
/// Prefer using the static definitions on this class instead.
/// </param>
public record StreamType(string Value)
    : ValueBackedEnum<string>(Value)
{
    public static StreamType All { get; } = new("all");
    public static StreamType Live { get; } = new("live");
}
