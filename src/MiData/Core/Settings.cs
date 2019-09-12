using MiData.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MiData.Core
{
    public class Settings
    {
        private static string json = string.Empty;
        private static SettingsModel data = null;
        public static SettingsModel GetSettings
        {
            get
            {

                if (File.Exists("settings.json"))
                {
                    if (data == null)
                    {
                        using (StreamReader sr = new StreamReader("Settings.json"))
                        {
                            json = sr.ReadToEnd();
                            data = JsonConvert.DeserializeObject<SettingsModel>(json);
                        }
                    }

                }
                else
                {

                    if (data == null)
                    {
                        data = new SettingsModel();
                        Console.WriteLine("WARNING: ============== Settings file not found using defaults ============== ");
                    }

                }

                return data;
            }

        }
    }
}
