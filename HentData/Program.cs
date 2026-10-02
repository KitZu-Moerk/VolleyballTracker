
var henter = new StillingHenter(43334, 2025);
var stilling = await henter.HentStilling();
henter.GemSomJson(stilling, "../../../../season-resultat.json");



