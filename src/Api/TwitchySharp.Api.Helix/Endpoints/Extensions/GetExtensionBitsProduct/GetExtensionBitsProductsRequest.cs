namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Gets the list of Bits products that belong to the extension.
/// </summary>
/// <remarks>
/// The client id identifies the extension (this must be the same application that created the access token).
/// <br/>
/// Requires an app access token.
/// The client id that created the app access token must be the extension's client id.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-extension-bits-products">Get Extension Bits Products</see> for more information.
/// </remarks>
public record GetExtensionBitsProductsRequest
    : TwitchHelixRequest<GetExtensionBitsProductsResponse>
{
    protected override string Path => "/bits/extensions";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("should_include_all", ShouldIncludeAll?.ToString());
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.Client(ExtensionId)
    };

    /// <summary>
    /// The client id of the extension to get bits products for.
    /// </summary>
    public required ExtensionId ExtensionId { get; init; }

    /// <summary>
    /// Determines whether to include disabled or expired Bits products in the response.
    /// </summary>
    /// <remarks>
    /// The default is <see langword="false"/>.
    /// </remarks>
    public bool? ShouldIncludeAll { get; init; }
}
