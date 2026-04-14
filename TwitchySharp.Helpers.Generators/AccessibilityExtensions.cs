using Microsoft.CodeAnalysis;

namespace TwitchySharp.Helpers.Generators;

internal static class AccessibilityExtensions
{
    public static string ToCodeString(this Accessibility accessibility)
        => accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Internal => "internal",
            Accessibility.Private => "private",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedAndInternal => "private protected",
            Accessibility.ProtectedOrInternal => "protected internal",
            _ => string.Empty
        };
}
