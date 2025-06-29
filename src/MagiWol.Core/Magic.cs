namespace MagiWol;

using System;
using System.Net;
using System.Net.Sockets;
using System.Globalization;
using System.Threading;

public static class Magic {

    public static void SendMagicPacket(Address address) {
        SendMagicPacket(address, useIPv4: true, useIPv6: true);
    }

    public static void SendMagicPacket(Address address, bool useIPv4, bool useIPv6) {
        var broadcastPort = address.IsBroadcastPortValid ? address.BroadcastPort : Address.DefaultBroadcastPort;

        if (useIPv4) {
            SendMagicPacket(address.Mac, address.SecureOn, address.BroadcastHostOrDefault, address.BroadcastPortOrDefault);
        }
        if (useIPv6) {
            Medo.Net.WakeOnLan.SendMagicPacketIPv6(address.Mac, address.SecureOn ?? "", IPAddress.IPv6Any, address.BroadcastPortOrDefault);
        }
    }

    public static void SendMagicPacket(string macAddress, string? secureOnPassword, string? broadcastHost, int? broadcastPort) {
        try {
            broadcastHost ??= Address.DefaultBroadcastHost;
            broadcastPort ??= Address.DefaultBroadcastPort;
            if (IPAddress.TryParse(broadcastHost, out var ip)) {
                Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword ?? "", ip, broadcastPort.Value);
            } else {
                bool hasSent = false;
                foreach (var address in Dns.GetHostAddresses(broadcastHost)) {
                    if (address.AddressFamily == AddressFamily.InterNetwork) {
                        Medo.Net.WakeOnLan.SendMagicPacket(macAddress, secureOnPassword ?? "", address, broadcastPort.Value);
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

    public static void SendMagicPacket(string macAddress) {
        Medo.Net.WakeOnLan.SendMagicPacket(macAddress);
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

        string broadcastHost = Address.DefaultBroadcastHost;
        if (useBroadcastAddress) {
            if (string.IsNullOrEmpty(broadcastHost)) {
                throw new FormatException("Invalid broadcast host.");
            } else {
                broadcastHost = broadcastAddressText.Trim();
            }
        }

        var broadcastPort = Address.DefaultBroadcastPort;
        if (useBroadcastPort) {
            if (!int.TryParse(broadcastPortText, System.Globalization.NumberStyles.Integer, System.Threading.Thread.CurrentThread.CurrentCulture, out broadcastPort)) {
                throw new FormatException("Invalid broadcast port.");
            }
        }

        SendMagicPacket(macAddress, secureOnPassword, broadcastHost, broadcastPort);
    }

    public static void SendMagicPacketIPv6(string macAddress) {
        Medo.Net.WakeOnLan.SendMagicPacketIPv6(macAddress);
    }

    public static void SendMagicPacketIPv6(string macAddress, string? secureOnPassword, int? broadcastPort) {
        if (!Medo.Net.WakeOnLan.IsMacAddressValid(macAddress)) {
            throw new FormatException("Invalid MAC address.");
        }

        if (!Medo.Net.WakeOnLan.IsSecureOnPasswordValid(secureOnPassword)) {
            throw new FormatException("Invalid SecureOn password.");
        }

        broadcastPort ??= Address.DefaultBroadcastPort;
        Medo.Net.WakeOnLan.SendMagicPacketIPv6(macAddress, secureOnPassword, IPAddress.IPv6Any, broadcastPort.Value);
    }

}
