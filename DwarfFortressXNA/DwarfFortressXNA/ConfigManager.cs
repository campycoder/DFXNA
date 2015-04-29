using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DwarfFortressXNA
{
    public class ConfigManager
    {
        public Dictionary<string, string> configValues;

        public ConfigManager()
        {
            configValues = new Dictionary<string, string>();
        }

        public void LoadConfigFiles()
        {
            StreamReader init = new StreamReader("./Data/Init/init.txt", Encoding.UTF8, true);
            string line;
            while ((line = init.ReadLine()) != null)
            {
                line = line.Replace("\t", "");
                if (line.Length > 0 && line[0] == '[')
                {
                    string key = line.Split(new char[] { ':' })[0].Replace("[", "");
                    string value = line.Split(new char[] { ':' })[1].Replace("]", "");
                    configValues.Add(key, value);
                }
            }
        }

        public bool GetConfigValueAsBool(string key)
        {
            if (!configValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            if (configValues[key] != "YES" && configValues[key] != "NO") throw new Exception("Config value " + key + " not a bool: " + configValues[key] + "!");
            return configValues[key] == "YES";
        }

        public int GetConfigValueAsInt(string key)
        {
            if (!configValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            int value;
            try
            {
                value = Convert.ToInt32(configValues[key]);
            }
            catch(Exception e)
            {
                throw new Exception("Config value " + key + " not an int: " + configValues[key] + "!");
            }
            return value;
        }

        public string GetConfigValue(string key)
        {
            if (!configValues.ContainsKey(key)) throw new Exception("Bad config key " + key + " requested!");
            return configValues[key];
        }
    }
}
