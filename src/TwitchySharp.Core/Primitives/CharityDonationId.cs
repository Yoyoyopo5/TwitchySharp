using Yoyoyopo5.ValueWrapper;

namespace TwitchySharp;

/// <summary>
/// The id of a specific charity donation.
/// </summary>
/// <param name="Value">The string value of the donation id.</param>
[Wrapper<string>]
public readonly partial record struct CharityDonationId(string Value);

