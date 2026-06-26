namespace TwitchySharp.Api.Tests.Unit.Client.RateLimiting;

public class Test_ToTwitchRateLimitDetails
{
    [Fact]
    public void ToTwitchRateLimitDetails_ValidHeaders_ReturnsExpectedTwitchRateLimitDetails()
    {
        const int LIMIT = 100;
        const int REMAINING = 99;
        const int RESET = 1782516706; // Unix Timestamp (seconds)

        HttpResponseMessage stubMessage = new();
        stubMessage.Headers.Add("Ratelimit-Limit", LIMIT.ToString());
        stubMessage.Headers.Add("Ratelimit-Remaining", REMAINING.ToString());
        stubMessage.Headers.Add("Ratelimit-Reset", RESET.ToString());

        TwitchRateLimitDetails result = stubMessage.Headers.ToTwitchRateLimitDetails();

        Assert.Equal(LIMIT, result.Limit);
        Assert.Equal(REMAINING, result.Remaining);
        Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(RESET), result.Reset);
    }

    [Fact]
    public void ToTwitchRateLimitDetails_MissingHeaders_ReturnsTwitchRateLimitDetailsWithNullValues()
    {
        HttpResponseMessage stubMessage = new();

        TwitchRateLimitDetails result = stubMessage.Headers.ToTwitchRateLimitDetails();

        Assert.Null(result.Limit);
        Assert.Null(result.Remaining);
        Assert.Null(result.Reset);
    }
}
