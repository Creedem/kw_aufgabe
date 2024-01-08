using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace GTA_KW
{
    class PlayerEvents : Script
    {

        public class VehicleData
        {
            public string Name { get; set; }
            public string Hash { get; set; }
            public string Category { get; set; }
        }

        // CLient Aufruf zum Spawnvehicle aus Fahrzeug Tabelle
        [RemoteEvent("spawnVehicle")]
        public void SpawnVehicle(Player player, string vehName)
        {
            uint vehash = NAPI.Util.GetHashKey(vehName);
            if (vehash <= 0)
            {
                player.SendChatMessage("~r~Ungültiges Fahrzeug!");
                return;
            }
            Vehicle veh = NAPI.Vehicle.CreateVehicle(vehash,
                                                     player.Position,
                                                     player.Heading,
                                                     0,
                                                     0);
            veh.NumberPlate = "KW";
            veh.Locked = false;
            veh.EngineStatus = false;
        }

        // Aufruf Funktion über Client Daten mittels Key "K" 
        [RemoteEvent("vehicleList")]
        public void OnClientEventTrigger(Player player)
        {
            List<VehicleData> vehicleDataList = GetVehicleDataFromDatabase();
            // Konvertieren Sie die Liste in ein JSON-Format als Zeichenkette
            string jsonData = ConvertVehicleListToJson(vehicleDataList);

            NAPI.ClientEvent.TriggerClientEvent(player, "receivevehicles", jsonData);
        }

        //Gebe Vehicle Liste für Ingame Katalog zurück
        private List<VehicleData> GetVehicleDataFromDatabase()
        {
            List<VehicleData> vehicleDataList = new List<VehicleData>();
            MySqlCommand command = Datenbank.Connection.CreateCommand();
            command.CommandText = "SELECT * FROM vehicles order by category, name";

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    VehicleData vehicleData = new VehicleData
                    {
                        Name = reader.GetString("name"),
                        Hash = reader.GetString("hash"),
                        Category = reader.GetString("category")
                    };
                    vehicleDataList.Add(vehicleData);
                }
            }
            return vehicleDataList;
        }

        // Die Objektliste VehicleData in JSON-Format konvertieren
        private string ConvertVehicleListToJson(List<VehicleData> vehicleDataList)
        {
            string jsonData = "[";

            for (int i = 0; i < vehicleDataList.Count; i++)
            {
                VehicleData vehicleData = vehicleDataList[i];
                jsonData += $"{{\"Name\":\"{vehicleData.Name}\",\"Hash\":\"{vehicleData.Hash}\",\"Kategorie\":\"{vehicleData.Category}\"}}";

                if (i < vehicleDataList.Count - 1)
                {
                    jsonData += ",";
                }
            }

            jsonData += "]";
            return jsonData;
        }
    }
}
