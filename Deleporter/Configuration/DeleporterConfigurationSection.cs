using System.Configuration;

namespace DeleporterCore.Configuration
{
    public class DeleporterConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("Port", DefaultValue = NetworkConfiguration.DefaultPort)]
        public int Port {
            get { return (int) this["Port"]; }
            set { this["Port"] = value; }
        }

        [ConfigurationProperty("Host", DefaultValue = NetworkConfiguration.DefaultHost)]
        public string Host {
            get { return (string)this["Host"]; }
            set { this["Host"] = value; }
        }

        [ConfigurationProperty("ServiceName", DefaultValue = NetworkConfiguration.DefaultServiceName)]
        public string ServiceName {
            get { return (string)this["ServiceName"]; }
            set { this["ServiceName"] = value; }
        }

    }
}