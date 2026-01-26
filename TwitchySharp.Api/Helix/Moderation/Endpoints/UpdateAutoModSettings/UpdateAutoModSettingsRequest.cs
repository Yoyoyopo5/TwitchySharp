using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Updates the broadcaster's AutoMod settings.
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster's chat room.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ModeratorManageAutomodSettings"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-automod-settings">Update AutoMod Settings</see> for more information.
/// </remarks>
public record UpdateAutoModSettingsRequest
    : TwitchHelixRequest<UpdateAutoModSettingsResponse>
{
    protected override string Path => "/moderation/automod/settings";
    public override HttpMethod Method => HttpMethod.Put;
    protected override TwitchApiIdentity DefaultIdentity => new UserIdentity(ModeratorId);
    public override IEnumerable<Scope> ValidScopes => [ Scope.ModeratorManageAutomodSettings ];
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Settings;

    /// <summary>
    /// The user id of the broadcaster (channel) that you want to update AutoMod settings for.
    /// </summary>
    public required UserId BroadcasterId { get; set; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token.
    /// </remarks>
    public required UserId ModeratorId { get; set; }

    /// <summary>
    /// The settings to update.
    /// Use derived classes <see cref="UpdateAutoModOverallLevelData"/> and <see cref="UpdateAutoModCustomLevelsData"/>.
    /// </summary>
    public required UpdateAutoModSettingsRequestData Settings { get; set; }
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
    public AutomodFilteringLevel? OverallLevel { get; protected set; }
    public AutomodFilteringLevel? Aggression { get; protected set; }
    public AutomodFilteringLevel? Bullying { get; protected set; }
    public AutomodFilteringLevel? Disability { get; protected set; }
    public AutomodFilteringLevel? Misogyny { get; protected set; }
    public AutomodFilteringLevel? RaceEthnicityOrReligion { get; protected set; }
    public AutomodFilteringLevel? SexBasedTerms { get; protected set; }
    public AutomodFilteringLevel? SexualitySexOrGender { get; protected set; }
    public AutomodFilteringLevel? Swearing { get; protected set; }
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
    public UpdateAutoModOverallLevelData(AutomodFilteringLevel level)
        => OverallLevel = level;
}

/// <summary>
/// Used to set custom AutoMod levels for each category.
/// </summary>
/// <remarks>
/// All current levels are overwritten when using with <see cref="UpdateAutoModSettingsRequest"/>.
/// <b>Note:</b> Levels default to <see cref="AutomodFilteringLevel.None"/> when creating the object.
/// </remarks>
public record UpdateAutoModCustomLevelsData
    : UpdateAutoModSettingsRequestData
{
    /// <inheritdoc cref="AutoModSettings.Aggression"/>
    public new AutomodFilteringLevel Aggression { get; set; }
    /// <inheritdoc cref="AutoModSettings.Bullying"/>
    public new AutomodFilteringLevel Bullying { get; set; }
    /// <inheritdoc cref="AutoModSettings.Disability"/>
    public new AutomodFilteringLevel Disability { get; set; }
    /// <inheritdoc cref="AutoModSettings.Misogyny"/>
    public new AutomodFilteringLevel Misogyny { get; set; }
    /// <inheritdoc cref="AutoModSettings.RaceEthnicityOrReligion"/>
    public new AutomodFilteringLevel RaceEthnicityOrReligion { get; set; }
    /// <inheritdoc cref="AutoModSettings.SexBasedTerms"/>
    public new AutomodFilteringLevel SexBasedTerms { get; set; }
    /// <inheritdoc cref="AutoModSettings.SexualitySexOrGender"/>
    public new AutomodFilteringLevel SexualitySexOrGender { get; set; }
    /// <inheritdoc cref="AutoModSettings.Swearing"/>
    public new AutomodFilteringLevel Swearing { get; set; }

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
