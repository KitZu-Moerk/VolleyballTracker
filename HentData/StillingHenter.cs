using System.Text.Json;
using HtmlAgilityPack;


public class StillingHenter
{
    private readonly HttpClient client = new HttpClient();
    private readonly int leagueId;
    private readonly int season;

    public StillingHenter(int leagueId, int season)
    {
        this.leagueId = leagueId;
        this.season = season;
        var nøgle = Environment.GetEnvironmentVariable("HIGHLIGHTLY_KEY");
        client.DefaultRequestHeaders.Add("x-rapidapi-key", nøgle);
    }

    public async Task<List<object>> HentStilling()
    {
        string url = $"https://volleyball.highlightly.net/standings?leagueId={leagueId}&season={season}";
        string svar = await client.GetStringAsync(url);
        
        JsonDocument doc = JsonDocument.Parse(svar);
        var holdListe = doc.RootElement.GetProperty("groups")[0].GetProperty("standings");
        var stilling = new List<object>();

        foreach (var holdData in holdListe.EnumerateArray())
        {
            string hold = holdData.GetProperty("team").GetProperty("name").GetString();
            int point = holdData.GetProperty("points").GetInt32();
            stilling.Add(new { Hold = hold, Point = point });
        }
        return stilling;
    }

    public void GemSomJson(List<object> stilling, string filnavn)
    {
        string json = JsonSerializer.Serialize(stilling);
        File.WriteAllText(filnavn, json);
    }
}