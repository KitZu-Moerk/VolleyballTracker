let ligaer = [];

// Fylder én menu med valgmuligheder.
function fyldMenu(id, værdier) {
    const menu = document.querySelector("#" + id);
    menu.innerHTML = "";
    værdier.forEach(v => {
        menu.innerHTML += `<option>${v}</option>`;
    });
}

// Kører først: henter ligalisten og fylder land-menuen.
async function start() {
    const svar = await fetch("ligaer.json");
    ligaer = await svar.json();

    const lande = [...new Set(ligaer.map(l => l.Land))];
    fyldMenu("land", lande);
    fyldNiveauer();
}

// Fylder niveau-menuen ud fra det valgte land.
function fyldNiveauer() {
    const land = document.querySelector("#land").value;
    const niveauer = [...new Set(ligaer.filter(l => l.Land === land).map(l => l.Niveau))];
    fyldMenu("niveau", niveauer);
    fyldKoen();
}

// Fylder køn-menuen ud fra det valgte land og niveau.
function fyldKoen() {
    const land = document.querySelector("#land").value;
    const niveau = document.querySelector("#niveau").value;
    const koen = [...new Set(ligaer.filter(l => l.Land === land && l.Niveau === niveau).map(l => l.Koen))];
    fyldMenu("koen", koen);
    visValgtLiga();
}

// Finder den valgte liga og viser dens stilling i tabellen.
async function visValgtLiga() {
    const land = document.querySelector("#land").value;
    const niveau = document.querySelector("#niveau").value;
    const koen = document.querySelector("#koen").value;

    const liga = ligaer.find(l => l.Land === land && l.Niveau === niveau && l.Koen === koen);
    document.querySelector("#overskrift").textContent = liga.Niveau + " - " + liga.Koen;

    const svar = await fetch(liga.Fil);
    const data = await svar.json();

    const tabel = document.querySelector("#stilling-result");
    tabel.innerHTML = "";
    data.forEach(obj => {
        const logo = obj.Logo ? `<img src="${obj.Logo}" class="logo">` : "";
        tabel.innerHTML +=
            `<tr><td>${logo}${obj.Hold}</td><td>${obj.Kampe}</td><td>${obj.Vundne}</td><td>${obj.Tabte}</td><td>${obj.SætVundet} - ${obj.SætTabt}</td><td>${obj.Point}</td></tr>`;
    });
}

document.querySelector("#land").addEventListener("change", fyldNiveauer);
document.querySelector("#niveau").addEventListener("change", fyldKoen);
document.querySelector("#koen").addEventListener("change", visValgtLiga);

start();