using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    : TwitchHelixRequest<GetExtensionAnalyticsResponse>, IPageableRequest
{
    protected override string Path => "/analytics/extensions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override TwitchApiIdentity DefaultIdentity => User;
    public override IReadOnlySet<Scope> ValidScopes => ImmutableHashSet.Create(Scope.AnalyticsReadExtensions);
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("extension_id", ExtensionId)
            .Add("type", Type?.Value)
            .Add("started_at", StartedAt?.UtcDateTime.Date.ToRfc3339())
            .Add("ended_at", EndedAt?.UtcDateTime.Date.ToRfc3339())
            .Add("first", First?.ToString())
            .Add("after", After?.Value);

    /// <summary>
    /// The user to get extension analytics as.
    /// </summary>
    public required UserIdentity User { get; init; }

    /// <summary>
    /// The extension's client id.
    /// </summary>
    /// <remarks>
    /// If specified, the response contains a report for the specified extension.
    /// If left <see langword="null"/>, the response includes a report for each extension that the authenticated user owns.
    /// </remarks>
    public ExtensionId? ExtensionId { get; init; }

    /// <summary>
    /// The type of analytics report to get.
    /// </summary>
    public ExtensionAnalyticsReportType? Type { get; init; }

    /// <summary>
    /// The reporting window's start date.
    /// </summary>
    /// <remarks>
    /// Use <see cref="WithinDateRange(DateTimeOffset, DateTimeOffset)"/> to set.
    /// </remarks>
    public DateTimeOffset? StartedAt { get; private init; }

    /// <summary>
    /// The reporting window's end date.
    /// </summary>
    /// <remarks>
    /// Use <see cref="WithinDateRange(DateTimeOffset, DateTimeOffset)"/> to set.
    /// </remarks>
    public DateTimeOffset? EndedAt { get; private init; }

    /// <summary>
    /// <inheritdoc cref="PaginationAmount"/>
    /// </summary>
    /// <remarks>
    /// While you may specify a maximum value of 100, the response will contain at most 20 URLs per page.
    /// </remarks>
    public PaginationAmount? First { get; init; }

    /// <summary>
    /// <inheritdoc cref="PaginationCursor"/>
    /// </summary>
    /// <remarks>
    /// This parameter is ignored if <see cref="ExtensionId"/> is not <see langword="null"/>.
    /// </remarks>
    public PaginationCursor? After { get; init; }

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
    /// <returns>A new request with the date range set.</returns>
    public GetExtensionAnalyticsRequest WithinDateRange(DateTimeOffset startedAt, DateTimeOffset endedAt)
        => this with { StartedAt = startedAt, EndedAt = endedAt };
}
