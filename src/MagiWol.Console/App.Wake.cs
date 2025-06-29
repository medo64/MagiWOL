namespace MagiWol;
using System;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Extensions.Logging;

internal static partial class App {

    public static void Wake(FileInfo file, LogLevel loglevel) {
        var doc = Document.Open(file.FullName);
        foreach (var address in doc.Addresses) {
            Magic.SendMagicPacket(address);
        }
    }

    public static void Wake(string[] macs, LogLevel loglevel) {
        foreach (var mac in macs) {
            var aMac = Address.GetFormattedMacAddress(mac);
            if (aMac != null) {
                Magic.SendMagicPacket(aMac);
                Magic.SendMagicPacketIPv6(aMac);
            }
        }
    }

}
