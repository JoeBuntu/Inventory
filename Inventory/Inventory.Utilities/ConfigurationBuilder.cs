using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate;
using NHibernate.Cfg;


namespace Inventory.Utilities
{
    public class ConfigurationBuilder
    {
        private const string SERIALIZED_CFG = "configuration.bin";

        public Configuration Build()
        {
            Configuration cfg = LoadConfigurationFromFile();
            if (cfg == null)
            {
                cfg = new Configuration().Configure();
                SaveConfigurationToFile(cfg);
            }
            return cfg;
        }

        private Configuration LoadConfigurationFromFile()
        {
            if (!IsConfigurationFileValid())
            {
                return null;
            }
            try
            {
                using (var file = File.Open(SERIALIZED_CFG, FileMode.Open))
                {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as Configuration;
                }
            }
            catch (Exception)
            {
                //something went wrong, just build a new one
                return null;
            }
        }

        private bool IsConfigurationFileValid()
        {
            //if we don't have a cached config
            //force a new one to be built
            if (!File.Exists(SERIALIZED_CFG))
            {
                return false;
            }

            FileInfo configInfo = new FileInfo(SERIALIZED_CFG);
            Assembly asm = Assembly.GetExecutingAssembly();
            if (asm.Location == null)
            {
                return false;
            }

            //if the assembly is newer, the serialized config is stale
            FileInfo asmInfo = new FileInfo(asm.Location);
            if (asmInfo.LastWriteTime > configInfo.LastWriteTime)
            {
                return false;
            }

            //if the app.config is newer, the serialized config is stale
            AppDomain appDomain = AppDomain.CurrentDomain;
            string appConfigPath = appDomain.SetupInformation.ConfigurationFile;
            FileInfo appConfigInfo = new FileInfo(appConfigPath);
            if (appConfigInfo.LastWriteTime > configInfo.LastWriteTime)
            {
                return false;
            }

            //it's still fress
            return true;
        }

        private void SaveConfigurationToFile(Configuration cfg)
        {
            using (FileStream file = File.Open(SERIALIZED_CFG, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, cfg);
            }
        }
    }
}