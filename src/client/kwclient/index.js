mp.gui.chat.push('Hello World');
var vehicleBrowser = null;

// Mit M Motor starten
mp.keys.bind(0x4D, true, function() {
    // Check : Spieler im Fahrzeug
    if(mp.players.local.vehicle) {   
        mp.game.vehicle.defaultEngineBehaviour = false;
        mp.players.local.setConfigFlag(429, true);       
        // Prüfung, ob Motor läuft
        if(mp.players.local.vehicle.getIsEngineRunning()) {
            mp.players.local.vehicle.setEngineOn(false, false, false); // Motor aus
        } else {
            mp.players.local.vehicle.setEngineOn(true, false, false); // Motor an
        }
    }
});

// Mit K Katalog öffnen
mp.keys.bind(0x4B, true, function() {
    mp.gui.chat.push('K key is pressed.'); 
    mp.events.callRemote("vehicleList");
});

// Wird vom Backend über [RemoteEvent("vehicleList")] aufgerufen
mp.events.add("receivevehicles", (jsonDaten) => {
    jsonDaten = JSON.parse(jsonDaten);
    erstelleUI(jsonDaten);
});

// Funktion zum Erstellen der Ingame-Benutzeroberfläche
function erstelleUI(jsonDaten) {
    let htmlCode = `<table class="display"><tr><th>Name</th><th>Hash</th><th>Kategorie</th><th>Aktion</th></tr>`;

    jsonDaten.forEach((datensatz, index) => {
        htmlCode += `<tr><td id="vehicleName">${datensatz.Name}</td><td>${datensatz.Hash}</td><td>${datensatz.Kategorie}</td><td><input type="checkbox" id="datensatz${index}"/></td></tr>`;
    });

    htmlCode += `<tr><td colspan="4"><button onclick="aktionAuslösen()">Aktion auslösen</button></td></tr>`;
    htmlCode += `</table>`;

    // Browser wurde erstellt ?
    if (vehicleBrowser) {
        mp.gui.chat.push('Browser wird geschlossen!');
        mp.gui.cursor.show(false, false);
        vehicleBrowser.destroy();
        vehicleBrowser = null; // Setze die Variable zurück
    } else {
        mp.gui.cursor.show(true, true);
        mp.gui.chat.push('Browser wird geöffnet!');
        vehicleBrowser = mp.browsers.new('package://kwclient/vehicle.html'); //Browser wird erstellt
        vehicleBrowser.execute(`document.getElementById('inhalt').innerHTML = '${htmlCode}';`); //Ersteller Code wird ans HTML geschickt
    } 
};

// Funktion zum Auslösen der Aktion
function aktionAuslösen() {
    const ausgewählteDatensätze = [];
    
    // Iteriere über die Checkboxen und füge ausgewählte Datensätze hinzu
    jsonDaten.forEach((datensatz, index) => {
        const checkbox = document.getElementById(`datensatz${index}`);
        if (checkbox.checked) {
            // Hier wird der Fahrzeugname direkt aus dem HTML-Code des Browsers abgerufen
            const selectedVehicleName = document.getElementById('vehicleName').innerText;
            mp.gui.chat.push(selectedVehicleName);
            mp.events.callRemote("spawnVehicle", selectedVehicleName);
        }
    });
}