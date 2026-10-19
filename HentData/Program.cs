
var mDKLiga = new StillingHenter(43334, 2026);
var wDKLiga = new StillingHenter(44185, 2026);

var mDKLigaStilling = await mDKLiga.HentStilling();
var wDKLigaStilling = await wDKLiga.HentStilling();

mDKLiga.GemSomJson(mDKLigaStilling, "../../../../meand-season-resultat.json");
wDKLiga.GemSomJson(wDKLigaStilling, "../../../../kvinder-season-resultat.json");



