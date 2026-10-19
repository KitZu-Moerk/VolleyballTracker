using System.Text.Json;

string tekst = File.ReadAllText("../../../../ligaer.json");

var ligaer = JsonSerializer.Deserialize<List<Liga>>(tekst);

foreach (var liga in ligaer)
{
    try
    {
        var henter = new StillingHenter(liga.LeagueId, 2026);
        var stilling = await henter.HentStilling();
        henter.GemSomJson(stilling, "../../../../" + liga.Fil);
        Console.WriteLine("Gemt: " + liga.Fil);
    }
    catch (Exception fejl)
    {
        Console.WriteLine("Fejl ved " + liga.Fil + ": " + fejl.Message);
    }
}