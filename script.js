async function visSeasonResultat() {
    
    const responseDKLigaMænd = await fetch("meand-season-resultat.json");
    const responseDKLigaKvinder = await fetch("kvinder-season-resultat.json");
    const dataDKLigaMænd = await responseDKLigaMænd.json()
    const dataDKLigaKvinder = await responseDKLigaKvinder.json()
    
    document.querySelector("#meand-season-result").innerHTML = "";
    dataDKLigaMænd.forEach(obj => {
        const logo = obj.Logo ? `<img src="${obj.Logo}" class="logo">` : "";
        document.querySelector("#meand-season-result").innerHTML +=
            `<tr><td>${logo}${obj.Hold}</td><td>${obj.Kampe}</td><td>${obj.Vundne}</td><td>${obj.Tabte}</td><td>${obj.SætVundet} - ${obj.SætTabt}</td><td>${obj.Point}</td></tr>`;
    })
    document.querySelector("#kvinder-season-result").innerHTML = "";
    dataDKLigaKvinder.forEach(obj => {
        const logo = obj.Logo ? `<img src="${obj.Logo}" class="logo">` : "";
        document.querySelector("#kvinder-season-result").innerHTML +=
            `<tr><td>${logo}${obj.Hold}</td><td>${obj.Kampe}</td><td>${obj.Vundne}</td><td>${obj.Tabte}</td><td>${obj.SætVundet} - ${obj.SætTabt}</td><td>${obj.Point}</td></tr>`;
    })
}
visSeasonResultat()