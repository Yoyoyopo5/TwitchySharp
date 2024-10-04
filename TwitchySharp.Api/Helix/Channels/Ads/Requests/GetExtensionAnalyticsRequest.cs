using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Helix.Channels.Ads.Responses;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Channels.Ads.Requests;
/// <summary>
/// Gets an analytics report for one or more extensions. 
/// The response contains the URLs used to download the reports (CSV files).
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-analytics">get extension analytics</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.AnalyticsReadExtensions"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.AnalyticsReadExtensions"/>.</param>
/// <param name="extensionId">
/// The extension's client id. 
/// If specified, the response contains a report for the specified extension. 
/// If not specified, the response includes a report for each extension that the authenticated user owns.
/// </param>
/// <param name="type">The type of analytics report to get.</param>
/// <param name="startedAt">
/// The reporting window's start date. 
/// The start date must be on or after January 31, 2018. 
/// If you specify an earlier date, the API ignores it and uses January 31, 2018. 
/// If you specify a start date, you must specify an end date. 
/// If you don't specify a start and end date, the report includes all available data since January 31, 2018. 
/// The report contains one row of data for each day in the reporting window.
/// </param>
/// <param name="endedAt">
/// The reporting window's end date.
/// The report is inclusive of the end date.
/// Specify an end date only if you provide a start date. 
/// Because it can take up to two days for the data to be available, you must specify an end date that's earlier than today minus one to two days. 
/// If not, the API ignores your end date and uses an end date that is today minus one to two days.
/// </param>
/// <param name="first">
/// The maximum number of report URLs to return per page in the response. 
/// The minimum page size is 1 URL per page and the maximum is 100 URLs per page. 
/// The default is 20.
/// <br/>
/// <b>NOTE:</b> While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.
/// </param>
/// <param name="after">
/// The cursor used to get the next page of results. 
/// The <see cref="Pagination"/> object in the response contains the cursor’s value.
/// This parameter is ignored if the <paramref name="extensionId"/> is not null.
/// </param>
public class GetExtensionAnalyticsRequest(
    string clientId,
    string accessToken,
    string? extensionId = null,
    ExtensionAnalyticsReportType? type = null,
    DateTimeOffset? startedAt = null,
    DateTimeOffset? endedAt = null,
    int? first = null,
    string? after = null)
    : HelixApiRequest<GetExtensionAnalyticsResponse>(
        "/analytics/extensions" + (new Dictionary<string, string?>() 
        {
            { "extension_id", extensionId },
            { "type", type?.Value },
            { "started_at", startedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ") },
            { "ended_at", endedAt?.UtcDateTime.Date.ToString("yyyy-MM-dd'T'HH:mm:ssZ") },
            { "first", first.ToString() },
            { "after", after }
        }.ToHttpQueryString()),
        clientId,
        accessToken
        );

public record ExtensionAnalyticsReportType : ValueBackedEnum<string>
{
    public static ExtensionAnalyticsReportType OverviewV2 { get; } = new ExtensionAnalyticsReportType("overview_v2");
    private ExtensionAnalyticsReportType(string twitchString) : base(twitchString) { }
}
