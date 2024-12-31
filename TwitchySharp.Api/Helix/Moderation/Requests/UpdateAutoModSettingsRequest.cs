using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Updates the broadcaster’s AutoMod settings. 
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster’s chat room.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-automod-settings">update AutoMod settings</see> for more information.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomodSettings"/>.
/// </remarks>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ModeratorManageAutomodSettings"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster (channel) that you want to update AutoMod settings for.</param>
/// <param name="moderatorId">
/// The user id of the broadcaster or a moderator of the broadcaster's channel.
/// This must be the same user that created the <paramref name="accessToken"/>.
/// </param>
/// <param name="settings">
/// The settings to update.
/// Use derived classes <see cref="UpdateAutoModOverallLevelData"/> and <see cref="UpdateAutoModCustomLevelsData"/>.
/// </param>
public class UpdateAutoModSettingsRequest(
    string clientId,
    string accessToken,
    string broadcasterId,
    string moderatorId,
    UpdateAutoModSettingsRequestData settings
    )
    : HelixApiRequest<UpdateAutoModSettingsResponse, UpdateAutoModSettingsRequestData>(
        "/moderation/automod/settings" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("moderator_id", moderatorId),
        clientId,
        accessToken,
        settings
        )
{
    public override HttpMethod Method => HttpMethod.Put;
}

/// <summary>
/// Used to set a channel's AutoMod settings.
/// </summary>
/// <remarks>
/// This class cannot be constructed directly because the <see cref="OverallLevel"/> and custom level properties are mutually exclusive. 
/// Instead, use a new instance of <see cref="UpdateAutoModOverallLevelData"/> or <see cref="UpdateAutoModCustomLevelsData"/>.
/// Alternatively, you use the <see cref="FromSettings(AutoModSettings)"/> static factory method.
/// This ensures that the mutual exclusivity rules are not broken.
/// </remarks>
public record UpdateAutoModSettingsRequestData
{
    public AutoModFilteringLevel? OverallLevel { get; protected set; }
    public AutoModFilteringLevel? Aggression { get; protected set; }
    public AutoModFilteringLevel? Bullying { get; protected set; }
    public AutoModFilteringLevel? Disability { get; protected set; }
    public AutoModFilteringLevel? Misogyny { get; protected set; }
    public AutoModFilteringLevel? RaceEthnicityOrReligion { get; protected set; }
    public AutoModFilteringLevel? SexBasedTerms { get; protected set; }
    public AutoModFilteringLevel? SexualitySexOrGender { get; protected set; }
    public AutoModFilteringLevel? Swearing { get; protected set; }
    protected UpdateAutoModSettingsRequestData() { }
    /// <summary>
    /// Update settings using an instance of <see cref="AutoModSettings"/>.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    public static UpdateAutoModSettingsRequestData FromSettings(AutoModSettings settings)
        => settings.OverallLevel.HasValue ?
            new UpdateAutoModOverallLevelData(settings.OverallLevel.Value)
            : new UpdateAutoModCustomLevelsData()
            {
                Aggression = settings.Aggression,
                Bullying = settings.Bullying,
                Disability = settings.Disability,
                Misogyny = settings.Misogyny,
                RaceEthnicityOrReligion = settings.RaceEthnicityOrReligion,
                SexBasedTerms = settings.SexBasedTerms,
                SexualitySexOrGender = settings.SexualitySexOrGender,
                Swearing = settings.Swearing
            };

    public static explicit operator UpdateAutoModSettingsRequestData(AutoModSettings settings) => FromSettings(settings);
}

/// <summary>
/// Used to set an overall AutoMod level.
/// Overall levels are preset Twitch AutoMod settings.
/// You can use <see cref="UpdateAutoModCustomLevelsData"/> to set each level manually.
/// </summary>
public record UpdateAutoModOverallLevelData
    : UpdateAutoModSettingsRequestData
{
    /// <inheritdoc cref="UpdateAutoModOverallLevelData"/>
    /// <param name="level">The overall level to set.</param>
    public UpdateAutoModOverallLevelData(AutoModFilteringLevel level)
        => OverallLevel = level;
}

/// <summary>
/// Used to set custom AutoMod levels for each category.
/// </summary>
/// <remarks>
/// All current levels are overwritten when using with <see cref="UpdateAutoModSettingsRequest"/>.
/// <b>Note:</b> Levels default to <see cref="AutoModFilteringLevel.None"/> when creating the object.
/// </remarks>
public record UpdateAutoModCustomLevelsData
    : UpdateAutoModSettingsRequestData
{
    /// <inheritdoc cref="AutoModSettings.Aggression"/>
    public new AutoModFilteringLevel Aggression { get; set; }
    /// <inheritdoc cref="AutoModSettings.Bullying"/>
    public new AutoModFilteringLevel Bullying { get; set; }
    /// <inheritdoc cref="AutoModSettings.Disability"/>
    public new AutoModFilteringLevel Disability { get; set; }
    /// <inheritdoc cref="AutoModSettings.Misogyny"/>
    public new AutoModFilteringLevel Misogyny { get; set; }
    /// <inheritdoc cref="AutoModSettings.RaceEthnicityOrReligion"/>
    public new AutoModFilteringLevel RaceEthnicityOrReligion { get; set; }
    /// <inheritdoc cref="AutoModSettings.SexBasedTerms"/>
    public new AutoModFilteringLevel SexBasedTerms { get; set; }
    /// <inheritdoc cref="AutoModSettings.SexualitySexOrGender"/>
    public new AutoModFilteringLevel SexualitySexOrGender { get; set; }
    /// <inheritdoc cref="AutoModSettings.Swearing"/>
    public new AutoModFilteringLevel Swearing { get; set; }

    /// <inheritdoc cref="UpdateAutoModCustomLevelsData"/>
    public UpdateAutoModCustomLevelsData()
        => (
        base.Aggression,
        base.Bullying,
        base.Disability,
        base.Misogyny,
        base.RaceEthnicityOrReligion,
        base.SexBasedTerms,
        base.SexualitySexOrGender,
        base.Swearing
        ) =
        (
        Aggression,
        Bullying,
        Disability,
        Misogyny,
        RaceEthnicityOrReligion,
        SexBasedTerms,
        SexualitySexOrGender,
        Swearing
        );
}
