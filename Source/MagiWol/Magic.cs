using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MagiWol {
    internal static class Magic {

        public static void SendMagicPacket(MagiWolDocument.Address address) {
            var broadcastHost = Settings.DefaultBroadcastHost;
            if (address.IsPacketEndPointHostValid) { broadcastHost = address.BroadcastHost; }
            var broadcastPort = Settings.DefaultBroadcastPort;
            if (address.IsPacketEndPointPortValid) { broadcastPort = address.BroadcastPort; }

            SendMagicPacket(address.Mac, address.SecureOn, broadcastHost, broadcastPort);
        }

        public static void SendMagicPacket(string macAddress, string secureOnPassword, string broadcastHost, int broadcastPort) {
            int count = 0;
            try {
                IPAddress ip;
                if (IPAddress.TryParse(broadcastHost, out ip)) {
                    Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, ip, broadcastPort);
                    count += 1;
                } else {
                    foreach (var address in Dns.GetHostAddresses(broadcastHost)) {
                        if (address.AddressFamily == AddressFamily.InterNetwork) {
                            Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, address, broadcastPort);
                            count += 1;
                        }
                    }
                }
            } catch (SocketException) { } //No such host is known
            if (count == 0) {
                try {
                    IPAddress ip;
                    if (IPAddress.TryParse(Settings.DefaultBroadcastHost, out ip)) {
                        Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, ip, broadcastPort);
                    } else {
                        foreach (var address in Dns.GetHostAddresses(Settings.DefaultBroadcastHost)) {
                            if (address.AddressFamily == AddressFamily.InterNetwork) {
                                Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, address, broadcastPort);
                            }
                        }
                    }
                } catch (SocketException) { //No such host is known
                    Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, IPAddress.Broadcast, broadcastPort);
                }
            }
        }

        public static void SendMagicPacket(string macAddress, string secureOnPassword, IPAddress broadcastAddress, int broadcastPort) {
            Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, broadcastAddress, broadcastPort);
        }

        public static void SendMagicPacket(string macAddress, string secureOnPassword, string broadcastAddressText, bool useBroadcastAddress, string broadcastPortText, bool useBroadcastPort) {
            if (!Medo.Net.WakeOnLan.IsMacAddressValid(macAddress)) {
                throw new FormatException("Invalid MAC address.");
            }

            if (!Medo.Net.WakeOnLan.IsSecureOnPasswordValid(secureOnPassword)) {
                throw new FormatException("Invalid SecureOn password.");
            }

            string broadcastHost = Settings.DefaultBroadcastHost;
            if (useBroadcastAddress) {
                if (string.IsNullOrEmpty(broadcastAddressText.Trim())) {
                    throw new FormatException("Invalid broadcast address.");
                }
            }
            var broadcastPort = Settings.DefaultBroadcastPort;
            if (useBroadcastPort) {
                if (!int.TryParse(broadcastPortText, System.Globalization.NumberStyles.Integer, System.Threading.Thread.CurrentThread.CurrentCulture, out broadcastPort)) {
                    throw new FormatException("Invalid broadcast port.");
                }
            }

            SendMagicPacket(macAddress, secureOnPassword, broadcastHost, broadcastPort);
        }

    }
}
