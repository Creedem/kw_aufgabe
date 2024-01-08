using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;


namespace GTA_KW
{
    class ServerEvents : Script
    {
        [ServerEvent(Event.ResourceStart)]
        public void OnResourceStart()
        {
            NAPI.Util.ConsoleOutput("[Server] Der Server wurde gestartet.");
            if (!Settings.LoadServerSettings())
            {
                return;
            }
            Datenbank.InitConnection();
        }

        [ServerEvent(Event.PlayerConnected)]
        public void OnPlayerConnected(Player player)
        {
            player.SendChatMessage("Willkommen auf dem Server!");
        }
    }
}
