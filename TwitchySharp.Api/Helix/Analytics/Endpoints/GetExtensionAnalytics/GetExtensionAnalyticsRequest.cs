using System;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Analytics;
/// <summary>
/// Gets an analytics report for one or more extensions. 
/// </summary>
/// <remarks>
/// The response contains the URLs used to download the reports (CSV files).
/// <br/>
/// Requires a user access token that includes <see cref="Scope.AnalyticsReadExtensions"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-analytics">Get Extension Analytics</see> for more information.
/// </remarks>
public record GetExtensionAnalyticsRequest
    : TwitchHelixRequest<GetExtensionAnalyticsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.AnalyticsReadExtensions"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetExtensionAnalyticsRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        GetExtensionAnalyticsRequestParameters? parameters = null
        )
        : base(
            "/analytics/extensions",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("extension_id", parameters?.ExtensionId)
                .Add("type", parameters?.Type?.Value)
                .Add("started_at", parameters?.StartedAt?.ToUniversalTwitchQueryString())
                .Add("ended_at", parameters?.EndedAt?.ToUniversalTwitchQueryString())
                .Add("first", parameters?.First?.ToString())
                .Add("after", parameters?.After?.Value)
            )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetExtensionAnalyticsRequest"/>.
/// </summary>
public record GetExtensionAnalyticsRequestParameters
    : IPageableRequest
{
    /// <summary>
    /// The extension's client id. 
    /// </summary>
    /// <remarks>
    /// If specified, the response contains a report for the specified extension. 
    /// If left <see langword="null"/>, the response includes a report for each extension that the authenticated user owns.
    /// </remarks>
    public ExtensionId? ExtensionId { get; set; }
    /// <summary>
    /// The type of analytics report to get.
    /// </summary>
    public ExtensionAnalyticsReportType? Type { get; set; }
    /// <summary>
    /// The reporting window's start date. 
    /// </summary>
    /// <remarks>
    /// Use <see cref="WithinDateRange(DateTimeOffset, DateTimeOffset)"/> to set.
    /// </remarks>
    public DateTimeOffset? StartedAt { get; private set; }
    /// <summary>
    /// The reporting window's end date.
    /// </summary>
    /// <remarks>
    /// Use <see cref="WithinDateRange(DateTimeOffset, DateTimeOffset)"/> to set.
    /// </remarks>
    public DateTimeOffset? EndedAt { get; private set; }
    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.
    /// </remarks>
    public PaginationAmount? First { get; set; }
    /// <summary>
    /// <inheritdoc cref="PaginationCursor"/>
    /// </summary>
    /// <remarks>
    /// This parameter is ignored if <see cref="ExtensionId"/> is not <see langword="null"/>.
    /// </remarks>
    public PaginationCursor? After { get; set; }

    /// <summary>
    /// Sets the reporting window for the request (the <see cref="StartedAt"/> and <see cref="EndedAt"/> parameters). 
    /// </summary>
    /// <param name="startedAt">
    /// The reporting window's start date.
    /// The start date must be on or after January 31, 2018. 
    /// If you specify an earlier date, the API ignores it and uses January 31, 2018.
    /// </param>
    /// <param name="endedAt">
    /// The reporting window's end date.
    /// The report is inclusive of the end date.
    /// Because it can take up to two days for the data to be available, you must specify an end date that's earlier than today minus one to two days. 
    /// If not, the API ignores your end date and uses an end date that is today minus one to two days.
    /// </param>
    /// <returns>This object.</returns>
    public GetExtensionAnalyticsRequestParameters WithinDateRange(DateTimeOffset startedAt, DateTimeOffset endedAt)
    {
        // I'm choosing not to throw runtime exceptions for invalid arguments here.
        // Behavior seems well defined in the spec, so we'll let the users handle it.
        StartedAt = startedAt;
        EndedAt = endedAt;
        return this;
    }
}