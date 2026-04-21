using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp.Api.Helix.CCLs;

/// <summary>
/// Contains static definitions for CCL locales.
/// </summary>
/// <param name="Value">The string value of the CCL locale.</param>
[Wrapper<string>]
public readonly partial record struct ContentClassificationLocale(string Value)
{
    public static ContentClassificationLocale Bulgarian { get; } = new("bg-BG");
    public static ContentClassificationLocale Czech { get; } = new("cs-CZ");
    public static ContentClassificationLocale Danish { get; } = new("da-DK");
    public static ContentClassificationLocale German { get; } = new("de-DE");
    public static ContentClassificationLocale Greek { get; } = new("el-GR");
    public static ContentClassificationLocale EnglishUnitedKingdom { get; } = new("en-GB");
    public static ContentClassificationLocale EnglishUnitedStates { get; } = new("en-US");
    public static ContentClassificationLocale SpanishSpain { get; } = new("es-ES");
    public static ContentClassificationLocale SpanishMexico { get; } = new("es-MX");
    public static ContentClassificationLocale Finnish { get; } = new("fi-FI");
    public static ContentClassificationLocale French { get; } = new("fr-FR");
    public static ContentClassificationLocale Hungarian { get; } = new("hu-HU");
    public static ContentClassificationLocale Italian { get; } = new("it-IT");
    public static ContentClassificationLocale Japanese { get; } = new("ja-JP");
    public static ContentClassificationLocale Korean { get; } = new("ko-KR");
    public static ContentClassificationLocale Dutch { get; } = new("nl-NL");
    public static ContentClassificationLocale Norwegian { get; } = new("no-NO");
    public static ContentClassificationLocale Polish { get; } = new("pl-PL");
    public static ContentClassificationLocale PortugueseBrazil { get; } = new("pt-BR"); // Assuming "pt-BR" instead of "pt-BT" as stated in documentation.
    public static ContentClassificationLocale PortuguesePortugal { get; } = new("pt-PT");
    public static ContentClassificationLocale Romanian { get; } = new("ro-RO");
    public static ContentClassificationLocale Russian { get; } = new("ru-RU");
    public static ContentClassificationLocale Slovak { get; } = new("sk-SK");
    public static ContentClassificationLocale Swedish { get; } = new("sv-SE");
    public static ContentClassificationLocale Thai { get; } = new("th-TH");
    public static ContentClassificationLocale Turkish { get; } = new("tr-TR");
    public static ContentClassificationLocale Vietnamese { get; } = new("vi-VN");
    public static ContentClassificationLocale ChineseSimplified { get; } = new("zh-CN");
    public static ContentClassificationLocale ChineseTraditional { get; } = new("zh-TW");
}
