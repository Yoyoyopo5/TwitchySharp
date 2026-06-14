using TwitchySharp.Api.Helix.Games;

namespace TwitchySharp.Api.Tests.Unit.Helix.Games;

public class Test_GetGamesRequest
{
    [Fact]
    public void SingleGameIdQuery_QueryString_ContainsId()
    {
        GetGamesRequest request = new()
        {
            Games = [new GameIdQuery(new GameId("123"))]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
    }

    [Fact]
    public void MultipleGameIdQueries_QueryString_ContainsAllIds()
    {
        GetGamesRequest request = new()
        {
            Games = [
                new GameIdQuery(new GameId("123")),
                new GameIdQuery(new GameId("456")),
                new GameIdQuery(new GameId("789"))
            ]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
        Assert.Contains("id=456", queryString);
        Assert.Contains("id=789", queryString);
    }

    [Fact]
    public void SingleGameNameQuery_QueryString_ContainsName()
    {
        GetGamesRequest request = new()
        {
            Games = [new GameNameQuery("Fortnite")]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("name=Fortnite", queryString);
    }

    [Fact]
    public void MultipleGameNameQueries_QueryString_ContainsAllNames()
    {
        GetGamesRequest request = new()
        {
            Games = [
                new GameNameQuery("Fortnite"),
                new GameNameQuery("Minecraft")
            ]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("name=Fortnite", queryString);
        Assert.Contains("name=Minecraft", queryString);
    }

    [Fact]
    public void SingleIgdbQuery_QueryString_ContainsIgdbId()
    {
        GetGamesRequest request = new()
        {
            Games = [new GameIgdbQuery(new IgdbId("1905"))]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("igdb_id=1905", queryString);
    }

    [Fact]
    public void MixedQueryTypes_QueryString_ContainsAllParams()
    {
        GetGamesRequest request = new()
        {
            Games = [
                new GameIdQuery(new GameId("123")),
                new GameNameQuery("Fortnite"),
                new GameIgdbQuery(new IgdbId("1905"))
            ]
        };

        string queryString = request.RequestUri.Query;

        Assert.Contains("id=123", queryString);
        Assert.Contains("name=Fortnite", queryString);
        Assert.Contains("igdb_id=1905", queryString);
    }
}
