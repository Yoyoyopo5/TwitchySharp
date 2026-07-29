using System.Collections.Immutable;
using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Updates the broadcaster's schedule settings, such as scheduling a vacation.
/// </summary>
/// <remarks>
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ChannelManageSchedule"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-channel-stream-schedule">Update Channel Stream Schedule</see> for more information.
/// </remarks>
public record UpdateChannelStreamScheduleRequest
    : TwitchHelixRequest<UpdateChannelStreamScheduleResponse>
{
    protected override string Path => "/schedule/settings";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(Settings.BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageSchedule)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", Settings.BroadcasterId)
            .Add("is_vacation_enabled", Settings.IsVacationEnabled?.ToString())
            .Add("vacation_start_time", Settings.VacationStartTime?.UtcDateTime.ToRfc3339())
            .Add("vacation_end_time", Settings.VacationEndTime?.UtcDateTime.ToRfc3339())
            .Add("timezone", Settings.Timezone?.ToIanaId());

    /// <summary>
    /// The request parameters.
    /// </summary>
    public required UpdateChannelStreamScheduleRequestParameters Settings { get; init; }

    protected override ValueTask<UpdateChannelStreamScheduleResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
        => ValueTask.FromResult(new UpdateChannelStreamScheduleResponse());
}

/// <summary>
/// Request parameters for a <see cref="UpdateChannelStreamScheduleRequest"/>.
/// </summary>
/// <remarks>
/// Use <see cref="EnableVacationMode(DateTimeOffset, DateTimeOffset, TimeZoneInfo)"/> and <see cref="DisableVacationMode"/>.
/// </remarks>
public record UpdateChannelStreamScheduleRequestParameters
{
    /// <summary>
    /// Sets the schedule settings to enable vacation mode.
    /// </summary>
    /// <param name="start"><inheritdoc cref="VacationStartTime" path="/summary"/></param>
    /// <param name="end"><inheritdoc cref="VacationEndTime" path="/summary"/></param>
    /// <param name="timezone"><inheritdoc cref="Timezone" path="/summary"/></param>
    /// <returns>A new instance of <see cref="UpdateChannelStreamScheduleRequestParameters"/> with vacation mode enabled.</returns>
    public UpdateChannelStreamScheduleRequestParameters EnableVacationMode(DateTimeOffset start, DateTimeOffset end, TimeZoneInfo timezone)
        => this with
        {
            IsVacationEnabled = true,
            VacationStartTime = start,
            VacationEndTime = end,
            Timezone = timezone
        };

    /// <summary>
    /// Sets the schedule settings to disable vacation mode.
    /// </summary>
    /// <returns>A new instance of <see cref="UpdateChannelStreamScheduleRequestParameters"/> with vacation mode disabled.</returns>
    public UpdateChannelStreamScheduleRequestParameters DisableVacationMode()
        => this with
        {
            IsVacationEnabled = false,
            VacationStartTime = null,
            VacationEndTime = null,
            Timezone = null
        };
    /// <summary>
    /// The user id of the broadcaster (channel) to update schedule settings for.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// Determines whether the broadcaster has scheduled a vacation. 
    /// Set to <see langword="true"/> to enable Vacation Mode and add vacation dates, or <see langword="false"/> to cancel a previously scheduled vacation.
    /// </summary>
    public bool? IsVacationEnabled { get; private init; }
    /// <summary>
    /// The date and time of when the broadcasterÅfs vacation starts. 
    /// </summary>
    public DateTimeOffset? VacationStartTime { get; private init; }
    /// <summary>
    /// The date and time of when the broadcasterÅfs vacation ends.
    /// </summary>
    public DateTimeOffset? VacationEndTime { get; private init; }
    /// <summary>
    /// The time zone that the broadcaster broadcasts from.
    /// </summary>
    [JsonConverter(typeof(IanaTimeZoneJsonConverter))]
    public TimeZoneInfo? Timezone { get; private init; }
}
