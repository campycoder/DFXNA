using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DwarfFortressXNA.Objects;

namespace DwarfFortressXNA.Managers
{
    public class ConfigManager
    {
        public Dictionary<string, string> ConfigValues;

        public ConfigManager()
        {
            ConfigValues = new Dictionary<string, string>();
        }

        public void LoadConfigFiles()
        {
            var init = new StreamReader("./Data/Init/init.txt", Encoding.UTF8, true);
            string line;
            while ((line = init.ReadLine()) != null)
            {
                line = line.Replace("\t", "");
                if (line.Length > 0 && line[0] == '[')
                {
                    var key = line.Split(new[] { ':' })[0].Replace("[", "");
                    var value = RawFile.StripTokenEnding(line.Split(new[] { ':' })[1]);
                    ConfigValues.Add(key, value);
                }
            }
        }

        public bool GetConfigValueAsBool(string key)
        {
            if (!ConfigValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            if (ConfigValues[key] != "YES" && ConfigValues[key] != "NO") throw new Exception("Config value " + key + " not a bool: " + ConfigValues[key] + "!");
            return ConfigValues[key] == "YES";
        }

        public int GetConfigValueAsInt(string key)
        {
            if (!ConfigValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            int value;
            try
            {
                value = Convert.ToInt32(ConfigValues[key]);
            }
            catch(Exception)
            {
                throw new Exception("Config value " + key + " not an int: " + ConfigValues[key] + "!");
            }
            return value;
        }

        public string GetConfigValue(string key)
        {
            if (!ConfigValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            return ConfigValues[key];
        }
    }
}
