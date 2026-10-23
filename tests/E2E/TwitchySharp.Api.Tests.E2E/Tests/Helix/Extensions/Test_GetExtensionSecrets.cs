using TwitchySharp.Api.Helix.Extensions;
using TwitchySharp.Tests.E2E;

namespace TwitchySharp.Api.Tests.E2E.Tests.Helix.Extensions;

public class Test_GetExtensionSecrets(TwitchClientFixture fixture)
{
    private readonly TwitchClientFixture _fixture = fixture;
    private static readonly TestName TestName = new("get-extension-secrets");

    [Fact]
    public async Task Send_GetExtensionSecretsRequest_ReturnSuccessResponse()
    {
        ExtensionConfiguration extensionConfig
            = _fixture.GetAuthorizingConfigForTestOrSkip<ExtensionConfiguration>(TestName);

        GetExtensionSecretsRequest request = new()
        {
            ExtensionId = extensionConfig.ExtensionId
        };

        TwitchResponse<GetExtensionSecretsResponseContent> response = await _fixture.GetTwitchApiClient().SendAsync(request, TestName, TestContext.Current.CancellationToken);
        
        Assert.Null(response.Error);
    }
}
