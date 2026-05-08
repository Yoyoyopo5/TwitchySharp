using TwitchySharp.Api.Helix.Games;

namespace TwitchySharp.Api.Tests.Unit.Helix.Games;

public class Test_GetGamesRequest
{
    [Fact]
    public void SingleGameIdQuery_QueryString_ContainsId()
    {
        var request = new GetGamesRequest
        {
            Games = [new GameIdQuery(new GameId("123"))]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
    }

    [Fact]
    public void MultipleGameIdQueries_QueryString_ContainsAllIds()
    {
        var request = new GetGamesRequest
        {
            Games = [
                new GameIdQuery(new GameId("123")),
                new GameIdQuery(new GameId("456")),
                new GameIdQuery(new GameId("789"))
            ]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
        Assert.Contains("id=456", queryString);
        Assert.Contains("id=789", queryString);
    }

    [Fact]
    public void SingleGameNameQuery_QueryString_ContainsName()
    {
        var request = new GetGamesRequest
        {
            Games = [new GameNameQuery("Fortnite")]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("name=Fortnite", queryString);
    }

    [Fact]
    public void MultipleGameNameQueries_QueryString_ContainsAllNames()
    {
        var request = new GetGamesRequest
        {
            Games = [
                new GameNameQuery("Fortnite"),
                new GameNameQuery("Minecraft")
            ]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("name=Fortnite", queryString);
        Assert.Contains("name=Minecraft", queryString);
    }

    [Fact]
    public void SingleIgdbQuery_QueryString_ContainsIgdbId()
    {
        var request = new GetGamesRequest
        {
            Games = [new GameIgdbQuery(new IgdbId("1905"))]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("igdb_id=1905", queryString);
    }

    [Fact]
    public void MixedQueryTypes_QueryString_ContainsAllParams()
    {
        var request = new GetGamesRequest
        {
            Games = [
                new GameIdQuery(new GameId("123")),
                new GameNameQuery("Fortnite"),
                new GameIgdbQuery(new IgdbId("1905"))
            ]
        };

        var queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
        Assert.Contains("name=Fortnite", queryString);
        Assert.Contains("igdb_id=1905", queryString);
    }
}
