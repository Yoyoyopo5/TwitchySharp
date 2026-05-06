using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Shared.Enums;

/// <summary>
/// Contains static definitions for Content Classification Label IDs.
/// These may change over time as Twitch updates their labels.
/// You can use the <see href="https://dev.twitch.tv/docs/api/reference/#get-content-classification-labels">Get Content Classification Labels</see> endpoint to get an up-to-date list.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[Wrapper<string>]
public readonly partial record struct ContentClassificationLabelId(string Value)
{
    /// <summary>
    /// Politics and Sensitive Social Issues
    /// <para>
    /// Discussions or debates about politics or sensitive social issues such as elections, civic integrity, military conflict, and civil rights in a polarizing manner.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId DebatedSocialIssuesAndPolitics { get; } = new("DebatedSocialIssuesAndPolitics");
    /// <summary>
    /// Drugs, Intoxication, or Excessive Tobacco Use
    /// <para>
    /// Excessive tobacco glorification or promotion, any marijuana consumption/use, legal drug and alcohol induced intoxication, discussions of illegal drugs.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId DrugsIntoxication { get; } = new("DrugsIntoxication");
    /// <summary>
    /// Gambling
    /// <para>
    /// Participating in online or in-person gambling, poker or fantasy sports, that involve the exchange of real money.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId Gambling { get; } = new("Gambling");
    /// <summary>
    /// Mature-rated game
    /// <para>
    /// Games that are rated Mature or less suitable for a younger audience.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId MatureGame { get; } = new("MatureGame");
    /// <summary>
    /// Significant Profanity or Vulgarity
    /// <para>
    /// Prolonged, and repeated use of obscenities, profanities, and vulgarities, especially as a regular part of speech.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId ProfanityVulgarity { get; } = new("ProfanityVulgarity");
    /// <summary>
    /// Sexual Themes
    /// <para>
    /// Content that focuses on sexualized physical attributes and activities, sexual topics, or experiences.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId SexualThemes { get; } = new("SexualThemes");
    /// <summary>
    /// Violent and Graphic Depictions
    /// <para>
    /// Simulations and/or depictions of realistic violence, gore, extreme injury, or death.
    /// </para>
    /// </summary>
    public static ContentClassificationLabelId ViolentGraphic { get; } = new("ViolentGraphic");
}
