namespace MagiWol;
using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

internal static partial class App {

    public static void Shut(FileInfo file, LogLevel loglevel) {
        var doc = Document.Open(file.FullName);
        foreach (var address in doc.Addresses) {
            var aMac = Address.GetFormattedMacAddress(address.Mac);
            if (aMac != null) {
                var aMacParts = aMac.Split(':');
                Array.Reverse(aMacParts);
                var rMac = string.Join(':', aMacParts);
                Magic.SendMagicPacket(rMac, address.SecureOn, address.BroadcastHost, address.BroadcastPort);
                Magic.SendMagicPacketIPv6(rMac, address.SecureOn, address.BroadcastPort);
            }

        }
    }

    public static void Shut(string[] macs, LogLevel loglevel) {
        foreach (var mac in macs) {
            var aMac = Address.GetFormattedMacAddress(mac);
            if (aMac != null) {
                var aMacParts = aMac.Split(':');
                Array.Reverse(aMacParts);
                var rMac = string.Join(':', aMacParts);
                Magic.SendMagicPacket(rMac);
                Magic.SendMagicPacketIPv6(rMac);
            }
        }
    }

}
