using System.Collections.Immutable;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Updates the broadcaster's AutoMod settings.
/// </summary>
/// <remarks>
/// The settings are used to automatically block inappropriate or harassing messages from appearing in the broadcaster's chat room.
/// <para>
/// Requires a user access token with <see cref="Scope.ModeratorManageAutomodSettings"/>, or
/// an app access token where the application, through a prior authorization, has <see cref="Scope.ModeratorManageAutomodSettings"/> for the <see cref="ModeratorId"/>.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#update-automod-settings">Update AutoMod Settings</see> for more information.
/// </remarks>
public record UpdateAutoModSettingsRequest
    : TwitchHelixRequest<UpdateAutoModSettingsResponseContent>,
    IAuthenticatedTwitchRequest<UserSupportingPriorAuthorizationAuthenticationContext>
{
    protected override string Path => "/moderation/automod/settings";
    public override HttpMethod Method => HttpMethod.Put;
    private UserSupportingPriorAuthorizationAuthenticationContext DefaultAuthenticationContext => new()
    {
        Identity = new TwitchIdentity.User(ModeratorId),
        ValidScopes = ImmutableHashSet.Create(Scope.ModeratorManageAutomodSettings)
    };
    public UserSupportingPriorAuthorizationAuthenticationContext AuthenticationContext
    {
        get => field ?? DefaultAuthenticationContext;
        init;
    }
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId)
            .Add("moderator_id", ModeratorId);
    public override object? ContentObject => Settings;

    /// <summary>
    /// The user id of the broadcaster (channel) that you want to update AutoMod settings for.
    /// </summary>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The user id of the broadcaster or a moderator of the broadcaster's channel.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token.
    /// </remarks>
    public required UserId ModeratorId { get; init; }

    /// <summary>
    /// The settings to update.
    /// Use derived classes <see cref="UpdateAutoModOverallLevelData"/> and <see cref="UpdateAutoModCustomLevelsData"/>.
    /// </summary>
    public required UpdateAutoModSettingsRequestData Settings { get; init; }
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
    public AutomodFilteringLevel? OverallLevel { get; protected init; }
    public AutomodFilteringLevel? Aggression { get; protected init; }
    public AutomodFilteringLevel? Bullying { get; protected init; }
    public AutomodFilteringLevel? Disability { get; protected init; }
    public AutomodFilteringLevel? Misogyny { get; protected init; }
    public AutomodFilteringLevel? RaceEthnicityOrReligion { get; protected init; }
    public AutomodFilteringLevel? SexBasedTerms { get; protected init; }
    public AutomodFilteringLevel? SexualitySexOrGender { get; protected init; }
    public AutomodFilteringLevel? Swearing { get; protected init; }
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
    public new AutomodFilteringLevel Aggression { get; init; }
    /// <inheritdoc cref="AutoModSettings.Bullying"/>
    public new AutomodFilteringLevel Bullying { get; init; }
    /// <inheritdoc cref="AutoModSettings.Disability"/>
    public new AutomodFilteringLevel Disability { get; init; }
    /// <inheritdoc cref="AutoModSettings.Misogyny"/>
    public new AutomodFilteringLevel Misogyny { get; init; }
    /// <inheritdoc cref="AutoModSettings.RaceEthnicityOrReligion"/>
    public new AutomodFilteringLevel RaceEthnicityOrReligion { get; init; }
    /// <inheritdoc cref="AutoModSettings.SexBasedTerms"/>
    public new AutomodFilteringLevel SexBasedTerms { get; init; }
    /// <inheritdoc cref="AutoModSettings.SexualitySexOrGender"/>
    public new AutomodFilteringLevel SexualitySexOrGender { get; init; }
    /// <inheritdoc cref="AutoModSettings.Swearing"/>
    public new AutomodFilteringLevel Swearing { get; init; }

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
