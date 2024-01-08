using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GTA_KW
{
    class Commands : Script
    {
        // Spawn ein Vehicle mit dem Namen und der Farbe 
        [Command("spawnvehicle", Alias = "vehicle")]
        public void cmd_Veh(Player player, string vehName, int color1, int color2)
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
                                                     color1,
                                                     color2);
            veh.NumberPlate = "KW";
            veh.Locked = false;
            veh.EngineStatus = false;
            player.SetIntoVehicle(veh, (int)VehicleSeat.Driver);
        }

        // Repaiere das Auto indem der Spieler sitzt
        [Command("fixvehicle", Alias = "repair")]
        public void cmd_FixVehicle(Player player)
        {
            if (!player.IsInVehicle)
            {
                player.SendChatMessage("~w~Du befindest dich in keinem Fahrzeug!");
                return;
            }

            player.Vehicle.Repair();
            player.SendNotification("Fahrzeug repariert");
        }

        // Gebe dem Spieler eine Waffe
        [Command("weapon", Alias = "giveweapon")]
        public void cmd_Weapon(Player player, string input = "")
        {
            string[] args = input.Split(" ");

            if (args.Length < 1)
            {
                player.SendChatMessage("~w~Nutze: /weapon [Waffenname]");
                return;
            }

            WeaponHash hash = NAPI.Util.WeaponNameToModel(args[0]);
            if (hash == 0)
            {
                player.SendChatMessage("~w~Ungültiger Waffenname!");
                return;
            }

            player.GiveWeapon(hash, 1000);
            player.SendNotification($"Waffe erhalten: ~b~{input}");
        }
    }
}
