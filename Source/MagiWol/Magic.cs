using System;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;

namespace MagiWol {
    internal static class Magic {

        public static void SendMagicPacket(MagiWolDocument.AddressItem address) {
            var broadcastPort = Settings.BroadcastPort;
            if (address.IsBroadcastPortValid) { broadcastPort = address.BroadcastPort; }

            if (Settings.UseIPv4) {
                var broadcastHost = Settings.BroadcastHost;
                if (address.IsBroadcastHostValid) { broadcastHost = address.BroadcastHost; }

                SendMagicPacket(address.Mac, address.SecureOn, broadcastHost, broadcastPort);
            }
            if (Settings.UseIPv6) {
                Medo.Net.WakeOnLan.SendMagicPacketIPv6(address.Mac, address.SecureOn, IPAddress.IPv6Any, broadcastPort);
            }
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

            string broadcastHost = Settings.BroadcastHost;
            if (useBroadcastAddress) {
                if (string.IsNullOrEmpty(broadcastHost)) {
                    throw new FormatException("Invalid broadcast host.");
                } else {
                    broadcastHost = broadcastAddressText.Trim();
                }
            }

            var broadcastPort = Settings.BroadcastPort;
            if (useBroadcastPort) {
                if (!int.TryParse(broadcastPortText, System.Globalization.NumberStyles.Integer, System.Threading.Thread.CurrentThread.CurrentCulture, out broadcastPort)) {
                    throw new FormatException("Invalid broadcast port.");
                }
            }

            SendMagicPacket(macAddress, secureOnPassword, broadcastHost, broadcastPort);
        }

        public static void SendMagicPacketIPv6(string macAddress, string secureOnPassword, string broadcastPortText, bool useBroadcastPort) {
            if (!Medo.Net.WakeOnLan.IsMacAddressValid(macAddress)) {
                throw new FormatException("Invalid MAC address.");
            }

            if (!Medo.Net.WakeOnLan.IsSecureOnPasswordValid(secureOnPassword)) {
                throw new FormatException("Invalid SecureOn password.");
            }

            var broadcastPort = Settings.BroadcastPort;
            if (useBroadcastPort) {
                if (!int.TryParse(broadcastPortText, NumberStyles.Integer, Thread.CurrentThread.CurrentCulture, out broadcastPort)) {
                    throw new FormatException("Invalid broadcast port.");
                }
            }

            Medo.Net.WakeOnLan.SendMagicPacketIPv6(macAddress, secureOnPassword, IPAddress.IPv6Any, broadcastPort);
        }

    }
}
