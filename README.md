# kw_aufgabe
Aufgabe für KW GTA5 Bewerbung

Sourcecode besteht aus C#, Javascript, HTML, CSS und SQL.

In dem Ordner "src" stehen alle Sourcen bereit.
In dem Ordner "server" steht jeglicher Server Sourcecode mit C# geschrieben.
	In dem Inhalt ist eine Datenbankverbindung aufgebaut, welches die Informationen aus dem Ordner
	Serverdata, aus der Datei "settings.json" zieht und damit eine Datenbankverbindung aufbaut.
	
	Weitere Beispiele in den Dateien sind folgende:
	- 3 verschiedene Commands
		- Spawn Vehicle (/spawnvehicle)
		- Repair Vehicle (/fixvehicle)
		- Give Weapon (/weapon)
	- Playerevents in Zusammenarbeit mit den Javascript und HTML Dateien (Nicht vollständig funktionsfähig)
	- Servereevents beim Start des Servers und beim Verbinden des Spielers
	- Laden der Serversettings für die Datenbankverbindung
	
In dem Ordner "client" ist der reine Client Sourcecode. 
	Hierbei wurden verschiedene Dinge programmiert.
	
	Beispiele dazu sind:
	- Mittels Taste "M" den Motor starten/beenden, wenn der Spieler im Auto sitzt
	- Mittels Taste "K" wird ingame eine Tabelle mit verschiedenen Fahrzeugen geöffnet
		(Der Spawn der Fahrzeuge funktioniert leider nicht)
		Das UI dafür wird über die Funktion "erstelleUI" dynamisch für das HTML erstellt
		
In dem Ordner "serverdata" sind nochmal die "settings.json" eingetragen für die Datenbankverbindung. Zusätzlich
steht dort die Tabelle für den dynamischen Spawn der Fahrzeuge, mit einigen Informationen mit SQL geschrieben.
