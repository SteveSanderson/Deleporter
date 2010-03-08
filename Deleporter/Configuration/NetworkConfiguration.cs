using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;

namespace DeleporterCore.Configuration
{
    public static class NetworkConfiguration
    {
        public static string HostName { get; set; }
        public static string ServiceName { get; set; }
        public static int PortNumber { get; set; }

        static NetworkConfiguration()
        {
            HostName = "localhost";
            ServiceName = "Deleporter.rm";
            PortNumber = 39084;
        }

        public static IChannel CreateChannel()
        {
            IDictionary props = new Hashtable { {"port", PortNumber}, {"typeFilterLevel", TypeFilterLevel.Full} };

            return new TcpChannel(props, null, new BinaryServerFormatterSinkProvider {
                TypeFilterLevel = TypeFilterLevel.Full
            });
        }

        public static string HostAddress
        {
            get { return string.Format("tcp://{0}:{1}/{2}", HostName, PortNumber, ServiceName); }
        }
    }
}