using System;
using System.Web;
using System.Configuration;

namespace Sendy.Net.Config
{
    [Serializable]
    public class AppConfig : ConfigurationSection
    {
        
        public static AppConfig getConfig()
        {
            return (AppConfig)ConfigurationManager.GetSection("Sendy.Net/Settings");
        }

        [ConfigurationProperty("Sendy")]
        public SendyElement Sendy
        {
            get { return (SendyElement)this["Sendy"]; }
            set { this["Sendy"] = value; }
        }

        public class SendyElement : ConfigurationElement
        {
            [ConfigurationProperty("InstallationUrl")]
            public string InstallationUrl { get { return (string)this["InstallationUrl"]; } set { this["InstallationUrl"] = value; } }    
            [ConfigurationProperty("ApiKey")]
            public string ApiKey { get { return (string)this["ApiKey"]; } set { this["ApiKey"] = value; } }       
        }
            
                
    }
}
