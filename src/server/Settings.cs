using System;
using System.IO;
using GTANetworkAPI;

namespace GTA_KW
{
    class Settings
    {
        public static Settings _settings;
        public string Host { get; set; }
        public string Port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public static bool LoadServerSettings()
        {
            string directory = "./serverdata/settings.json";
            if (File.Exists(directory))
            {
                string settings = File.ReadAllText(directory);
                _settings = NAPI.Util.FromJson<Settings>(settings);
                NAPI.Util.ConsoleOutput("[Settings] -> Serversettings geladen!");
                return true;
            }
            else
            {
                NAPI.Util.ConsoleOutput("[Settings] -> Serversettings nicht geladen!");
                NAPI.Task.Run(() =>
                {
                    Environment.Exit(0);
                }, delayTime: 5000);
                return false;
            }
        }
    }
}
