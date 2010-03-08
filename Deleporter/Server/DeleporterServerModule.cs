using System.Web;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using DeleporterCore.Configuration;

namespace DeleporterCore.Server
{
    public class DeleporterServerModule : IHttpModule
    {
        private IChannel _remotingChannel;

        public void Init(HttpApplication context)
        {
            // Start listening for connections
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(DeleporterService), NetworkConfiguration.ServiceName, WellKnownObjectMode.Singleton);
            _remotingChannel = NetworkConfiguration.CreateChannel();
            ChannelServices.RegisterChannel(_remotingChannel, false);
        }

        public void Dispose()
        {
            if (_remotingChannel != null)
                ChannelServices.UnregisterChannel(_remotingChannel);
        }
    }
}


