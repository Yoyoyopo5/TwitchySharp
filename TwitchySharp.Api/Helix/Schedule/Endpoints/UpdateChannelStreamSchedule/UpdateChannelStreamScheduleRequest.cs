using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;
using TwitchySharp.Helpers.JsonConverters;

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
    /// <param name="parameters">The request parameters.</param>
    public UpdateChannelStreamScheduleRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        UpdateChannelStreamScheduleRequestParameters parameters
        ) : base(
            "/schedule/settings",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
                .Add("is_vacation_enabled", parameters.IsVacationEnabled?.ToString())
                .Add("vacation_start_time", parameters.VacationStartTime?.ToUniversalTwitchQueryString())
                .Add("vacation_end_time", parameters.VacationEndTime?.ToUniversalTwitchQueryString())
                .Add("timezone", parameters.Timezone?.Id)
            )
    {
        Method = HttpMethod.Patch;
    }
}

/// <summary>
/// Request parameters for a <see cref="UpdateChannelStreamScheduleRequest"/>.
/// </summary>
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
    public required UserId BroadcasterId { get; set; }
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
    /// </summary>
    [JsonConverter(typeof(IanaTimeZoneJsonConverter))]
    public TimeZoneInfo? Timezone { get; private set; }
}
