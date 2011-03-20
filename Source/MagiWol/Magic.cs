using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace MagiWol {
    internal static class Magic {

        public static void SendMagicPacket(MagiWolDocument.Address address) {
            var broadcastHost = Settings.DefaultBroadcastHost;
            if (address.IsBroadcastHostValid) { broadcastHost = address.BroadcastHost; }
            var broadcastPort = Settings.DefaultBroadcastPort;
            if (address.IsBroadcastPortValid) { broadcastPort = address.BroadcastPort; }

            SendMagicPacket(address.Mac, address.SecureOn, broadcastHost, broadcastPort);
        }

        public static void SendMagicPacket(string macAddress, string secureOnPassword, string broadcastHost, int broadcastPort) {
            try {
                IPAddress ip;
                if (IPAddress.TryParse(broadcastHost, out ip)) {
                    Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, ip, broadcastPort);
                } else {
                    bool hasSent = false;
                    foreach (var address in Dns.GetHostAddresses(broadcastHost)) {
                        if (address.AddressFamily == AddressFamily.InterNetwork) {
                            Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword, address, broadcastPort);
                            hasSent = true;
                        }
                    }
                    if (hasSent == false) {
                        throw new InvalidOperationException(string.Format("Cannot find IP address for broadcast host \"{0}\".", broadcastHost));
                    }
                }
            } catch (SocketException ex) { //No such host is known
                throw new InvalidOperationException(string.Format("Broadcast host \"{0}\" cannot be resolved.", broadcastHost), ex);
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
                if (string.IsNullOrEmpty(broadcastHost)) {
                    throw new FormatException("Invalid broadcast host.");
                } else {
                    broadcastHost = broadcastAddressText.Trim();
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
