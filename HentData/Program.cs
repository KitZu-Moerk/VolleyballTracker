using System.Text.Json;


var testClient = new HttpClient();
testClient.DefaultRequestHeaders.Add("x-rapidapi-key", Environment.GetEnvironmentVariable("HIGHLIGHTLY_KEY"));

foreach (int offset in new[] { 0, 100, 200 })
{
    string testSvar = await testClient.GetStringAsync($"https://volleyball.highlightly.net/leagues?offset={offset}");
    JsonDocument testDoc = JsonDocument.Parse(testSvar);

    foreach (var l in testDoc.RootElement.GetProperty("data").EnumerateArray())
    {
        string land = l.GetProperty("country").GetProperty("name").GetString();
        string navn = l.GetProperty("name").GetString();
        int id = l.GetProperty("id").GetInt32();
        var saesoner = l.GetProperty("seasons").EnumerateArray().Select(s => s.GetProperty("season").GetInt32()).OrderBy(s => s);

        Console.WriteLine($"{land} | {navn} | {id} | {string.Join(", ", saesoner)}");
    }
}
return;

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