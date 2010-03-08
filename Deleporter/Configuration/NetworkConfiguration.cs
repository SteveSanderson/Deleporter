using System.Collections;
using System.Configuration;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace DeleporterCore.Configuration
{
    public static class NetworkConfiguration
    {
        internal const int DefaultPort = 38473;
        internal const string DefaultHost = "localhost";
        internal const string DefaultServiceName = "Deleporter.rm";

        public static int Port { get; private set; }
        public static string Host { get; private set; }
        public static string ServiceName { get; private set; }

        static NetworkConfiguration()
        {
            var config = (DeleporterConfigurationSection)ConfigurationManager.GetSection("deleporter");
            if (config != null)
            {
                Port = config.Port;
                Host = config.Host;
                ServiceName = config.ServiceName;
            } 
            else
            {
                Port = DefaultPort;
                Host = DefaultHost;
                ServiceName = DefaultServiceName;
            }
        }

        public static IChannel CreateChannel()
        {
            IDictionary props = new Hashtable { { "port", Port }, { "typeFilterLevel", TypeFilterLevel.Full } };

            return new TcpChannel(props, null, new BinaryServerFormatterSinkProvider {
                TypeFilterLevel = TypeFilterLevel.Full
            });
        }

        public static string HostAddress
        {
            get { return string.Format("tcp://{0}:{1}/{2}", Host, Port, ServiceName); }
        }
    }
}