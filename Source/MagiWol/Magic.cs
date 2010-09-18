using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace MagiWol {
    internal static class Magic {

        public static void SendMagicPacket(MagiWolDocument.Address address) {
            var broadcastAddress = Settings.DefaultPacketEndPoint.Address;
            if (address.IsPacketEndPointAddressValid) { broadcastAddress = address.PacketEndPoint.Address; }
            var broadcastPort = Settings.DefaultPacketEndPoint.Port;
            if (address.IsPacketEndPointPortValid) { broadcastPort = address.PacketEndPoint.Port; }

            SendMagicPacket(address.Mac, address.SecureOn, broadcastAddress, broadcastPort);
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

            var broadcastAddress = Settings.DefaultPacketEndPoint.Address;
            if (useBroadcastAddress) {
                if (!IPAddress.TryParse(broadcastAddressText, out broadcastAddress)) {
                    throw new FormatException("Invalid broadcast address.");
                }
            }
            var broadcastPort = Settings.DefaultPacketEndPoint.Port;
            if (useBroadcastPort) {
                if (!int.TryParse(broadcastPortText, System.Globalization.NumberStyles.Integer, System.Threading.Thread.CurrentThread.CurrentCulture, out broadcastPort)) {
                    throw new FormatException("Invalid broadcast port.");
                }
            }

            SendMagicPacket(macAddress, secureOnPassword, broadcastAddress, broadcastPort);
        }

    }
}
