using System.Text.Json;
namespace HentData.Tests;

public class UnitTest1
{
    [Fact]
    public void GemSomJson_GemmerHoldIFil()
    {
        // Arrange
        var liste = new List<object> { new { Hold = "Testhold", Point = 10 } };
        var henter = new StillingHenter(0, 0);

        // Act
        henter.GemSomJson(liste, "test.json");

        // Assert
        string indhold = File.ReadAllText("test.json");
        Assert.Contains("Testhold", indhold);
    }

    [Fact]
    public void LæsStilling_LæserHoldFraJson()
    {
        // Arrange
        string json = "{\"groups\":[{\"standings\":[{\"team\":{\"name\":\"Testhold\",\"logo\":null},\"points\":10,\"gamesPlayed\":5,\"wins\":3,\"loses\":2,\"scoredPoints\":9,\"receivedPoints\":6}]}]}";
        var henter = new StillingHenter(0, 0);

        // Act
        var stilling = henter.LæsStilling(json);

        // Assert
        string resultat = JsonSerializer.Serialize(stilling);
        Assert.Contains("Testhold", resultat);
    }
}