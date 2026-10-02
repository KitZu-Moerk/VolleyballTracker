async function visSeasonResultat() {
    
    const response = await fetch("season-resultat.json");
    const data = await response.json()
    
    document.querySelector("#season-result").innerHTML = "";
    data.forEach(obj => {
        document.querySelector("#season-result").innerHTML +=
            `<tr><td>${obj.Hold}</td><td>${obj.Kampe}</td><td>${obj.Vundne}</td><td>${obj.Tabte}</td><td>${obj.SætVundet} - ${obj.SætTabt}</td><td>${obj.Point}</td></tr>`;
    })
}
visSeasonResultat()