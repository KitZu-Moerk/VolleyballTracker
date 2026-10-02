using HtmlAgilityPack;
using System.Text.Json;

var client = new HttpClient();
string url = "https://resultater.volleyball.dk/tms/Turneringer-og-resultater/Pulje-Stilling.aspx?PuljeId=4124";
string html = await client.GetStringAsync(url);

var doc = new HtmlDocument();
doc.LoadHtml(html);

var rækker = doc.DocumentNode.SelectNodes("//tr[td[@class='c02']]");

var stilling = new List<object>();

foreach (var række in rækker)
{
    string hold = række.SelectSingleNode("td[@class='c02']").InnerText.Trim();
    string point = række.SelectSingleNode("td[@class='c12']").InnerText.Trim();

    stilling.Add(new {Hold = hold, Point = point});
}
string json = JsonSerializer.Serialize(stilling);
File.WriteAllText("../../../../season-resultat.json", json);