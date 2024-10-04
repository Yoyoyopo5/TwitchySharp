using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Helix.Channels.Ads.Responses;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels.Ads.Requests;
/// <summary>
/// Gets an analytics report for one or more games. 
/// The response contains the URLs used to download the reports (CSV files).
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-game-analytics">get game analytics</see> for more information.
/// <br/>
/// Requires a user access token with <see cref="Scope.AnalyticsReadGames"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token with <see cref="Scope.AnalyticsReadGames"/>.</param>
/// <param name="gameId">
/// The game’s client ID. 
/// If specified, the response contains a report for the specified game. 
/// If not specified, the response includes a report for each of the authenticated user’s games.
/// </param>
/// <param name="type">The type of analytics report to get.</param>
/// <param name="startedAt">
/// The reporting window’s start date.
/// If you specify a start date, you must specify an end date.
/// The start date must be within one year of today’s date. 
/// If you specify an earlier date, the API ignores it and uses a date that’s one year prior to today’s date. 
/// If you don’t specify a start and end date, the report includes all available data for the last 365 days from today. 
/// The report contains one row of data for each day in the reporting window.
/// </param>
/// <param name="endedAt">
/// The reporting window’s end date.
/// The report is inclusive of the end date.
/// Specify an end date only if you provide a start date. 
/// Because it can take up to two days for the data to be available, you must specify an end date that’s earlier than today minus one to two days. 
/// If not, the API ignores your end date and uses an end date that is today minus one to two days.
/// </param>
/// <param name="first">
/// The maximum number of report URLs to return per page in the response. 
/// The minimum page size is 1 URL per page and the maximum is 100 URLs per page. The default is 20.
/// <b>NOTE:</b> While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> property in the response contains the cursor’s value.
/// This parameter is ignored if <paramref name="gameId"/> is not null.
/// </param>
public class GetGameAnalyticsRequest(
    string clientId, 
    string accessToken,
    string? gameId = null,
    GameAnalyticsReportType? type = null,
    DateTimeOffset? startedAt = null,
    DateTimeOffset? endedAt = null,
    int? first = null,
    string? after = null
    )
    : HelixApiRequest<GetGameAnalyticsResponse>(
        "/analytics/games" + (new Dictionary<string, string?>()
        {
            { "game_id", gameId },
            { "type", type?.Value },
            { "started_at", startedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ") },
            { "ended_at", endedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ") },
            { "first", first.ToString() },
            { "after", after }
        }.ToHttpQueryString()),
        clientId,
        accessToken
        );

/// <summary>
/// Contains valid report types for use with <see cref="GetGameAnalyticsRequest"/>.
/// </summary>
public record GameAnalyticsReportType : ValueBackedEnum<string>
{
    public static GameAnalyticsReportType OverviewV2 { get; } = new GameAnalyticsReportType("overview_v2");
    private GameAnalyticsReportType(string twitchString) : base(twitchString) { }
}