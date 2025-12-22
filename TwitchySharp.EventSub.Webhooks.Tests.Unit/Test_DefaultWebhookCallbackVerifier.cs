using TwitchySharp.EventSub.Webhooks.Responses;

namespace TwitchySharp.EventSub.Webhooks.Tests.Unit;

public class Test_DefaultWebhookCallbackVerifier
{
    [Fact]
    public async Task VerifyCallback_ValidChallenge_ReturnValidResponse()
    {
        const string FAKE_CHALLENGE = "test_challenge";
        CallbackVerificationResponseData expectedResponseData = new()
        {
            Challenge = FAKE_CHALLENGE
        };

        DefaultWebhookCallbackVerifier stubVerifier = new();
        CallbackVerificationResponseData actualResponseData = await stubVerifier.VerifyCallback(FAKE_CHALLENGE);

        Assert.Equal(expectedResponseData, actualResponseData);
    }
}