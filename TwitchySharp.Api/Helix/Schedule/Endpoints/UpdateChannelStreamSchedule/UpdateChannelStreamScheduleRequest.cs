using System;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Updates the broadcaster’s schedule settings, such as scheduling a vacation.
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
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token that includes <see cref="Scope.ChannelManageSchedule"/>.</param>
    /// <param name="broadcasterId">
    /// The user id of the broadcaster (channel) to update schedule settings for.
    /// This must be the same user that created the <paramref name="accessToken"/>.
    /// </param>
    /// <param name="scheduleSettings">The settings to update.</param>
    public UpdateChannelStreamScheduleRequest(
        string clientId,
        string accessToken,
        string broadcasterId,
        UpdateChannelStreamScheduleRequestData scheduleSettings
        ) : base(
            "/schedule/settings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", broadcasterId)
                .Add("is_vacation_enabled", scheduleSettings.IsVacationEnabled?.ToString())
                .Add("vacation_start_time", scheduleSettings.VacationStartTime?.ToUniversalTwitchQueryString())
                .Add("vacation_end_time", scheduleSettings.VacationEndTime?.ToUniversalTwitchQueryString())
                .Add("timezone", scheduleSettings.Timezone)
            )
    {
        Method = HttpMethod.Patch;
    }
}

/// <summary>
/// Used to set a broadcaster's stream schedule settings.
/// </summary>
public record UpdateChannelStreamScheduleRequestData
{
    /// <summary>
    /// Sets the schedule settings to enable vacation mode.
    /// </summary>
    /// <param name="start"><inheritdoc cref="VacationStartTime" path="/summary"/></param>
    /// <param name="end"><inheritdoc cref="VacationEndTime" path="/summary"/></param>
    /// <param name="timezone"><inheritdoc cref="Timezone" path="/summary"/></param>
    /// <returns>A new instance of <see cref="UpdateChannelStreamScheduleRequestData"/> with vacation mode enabled.</returns>
    public UpdateChannelStreamScheduleRequestData EnableVacationMode(DateTimeOffset start, DateTimeOffset end, string timezone)
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
    /// <returns>A new instance of <see cref="UpdateChannelStreamScheduleRequestData"/> with vacation mode disabled.</returns>
    public UpdateChannelStreamScheduleRequestData DisableVacationMode()
        => this with
        {
            IsVacationEnabled = false,
            VacationStartTime = null,
            VacationEndTime = null,
            Timezone = null
        };

    /// <summary>
    /// Determines whether the broadcaster has scheduled a vacation. 
    /// Set to <see langword="true"/> to enable Vacation Mode and add vacation dates, or <see langword="false"/> to cancel a previously scheduled vacation.
    /// </summary>
    public bool? IsVacationEnabled { get; private set; }
    /// <summary>
    /// The date and time of when the broadcaster’s vacation starts. 
    /// </summary>
    public DateTimeOffset? VacationStartTime { get; private set; }
    /// <summary>
    /// The date and time of when the broadcaster’s vacation ends.
    /// </summary>
    public DateTimeOffset? VacationEndTime { get; private set; }
    /// <summary>
    /// The time zone that the broadcaster broadcasts from. 
    /// Specify the time zone using <see href="https://www.iana.org/time-zones">IANA time zone database</see> format (for example, <c>"America/New_York"</c>).
    /// </summary>
    public string? Timezone { get; private set; }
}
