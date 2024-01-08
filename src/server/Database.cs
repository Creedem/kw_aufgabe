using System;
using GTANetworkAPI;
using MySql.Data.MySqlClient;

namespace GTA_KW
{
    class Datenbank
    {
        public static bool DatenbankVerbindung = false;
        public static MySqlConnection Connection;
        public string Host { get; set; }
        public string Port { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public Datenbank()
        {
            this.Host = Settings._settings.Host;
            this.Port = Settings._settings.Port;
            this.username = Settings._settings.username;
            this.password = Settings._settings.password;
            this.database = Settings._settings.database;
        }

        public static String GetConnectionString()
        {
            Datenbank sql = new Datenbank();
            return $"SERVER={sql.Host}; PORT={sql.Port}; DATABASE={sql.database}; UID={sql.username}; PASSWORD={sql.password}";
        }
        public static void InitConnection()
        {
            Connection = new MySqlConnection(GetConnectionString());
            try
            {
                Connection.Open();
                DatenbankVerbindung = true;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Verbindung erfolgreich aufgebaut!");
            }
            catch (Exception e)
            {
                DatenbankVerbindung = false;
                NAPI.Util.ConsoleOutput("[MYSQL] -> Verbindung konnte nicht aufgebaut werden!");
                NAPI.Util.ConsoleOutput("[MYSQL] ->" + e.ToString());
                NAPI.Task.Run(() =>
                {
                    Environment.Exit(0);
                }, delayTime: 5000);
            }
        }
    }
}
