using Android.Content;
using Android.Net;
using LotteryApp.Core.Services;

namespace LotteryApp.Android.Services
{
    public class ConnectivityService : IConnectivityService
    {
        private readonly Context _context;

        public ConnectivityService(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Check connection.
        /// </summary>
        /// <returns>bool if connected.</returns>
        public bool IsConnected()
        {
            var connectivityManager = (ConnectivityManager)_context.GetSystemService(Context.ConnectivityService);
            var network = connectivityManager.ActiveNetwork;
            if (network == null)
            {
                return false;
            }

            var networkCapabilities = connectivityManager.GetNetworkCapabilities(network);
            return networkCapabilities != null &&
                   (networkCapabilities.HasTransport(TransportType.Wifi) ||
                    networkCapabilities.HasTransport(TransportType.Cellular) ||
                    networkCapabilities.HasTransport(TransportType.Ethernet) ||
                    networkCapabilities.HasTransport(TransportType.Bluetooth));
        }
    }
}
