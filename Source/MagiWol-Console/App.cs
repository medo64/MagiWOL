using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MagiWolConsole {
    internal static class App {

        [STAThread]
        static void Main() {

            TextWriter showHelpWritter = null;
            var useIPv6 = false;

            foreach (var iKey in Medo.Application.Args.Current.GetKeys()) {
                switch (iKey.ToUpperInvariant()) {
                    case "HELP": {
                            if (showHelpWritter == null) { showHelpWritter = Console.Out; }
                        } break;

                    case "IPV6": {
                            useIPv6 = true;
                        } break;

                    case "WAKE": break;

                    default: {
                            Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "Argument \"{0}\" is not recognized.", iKey));
                            Console.Error.WriteLine();
                            showHelpWritter = Console.Error;
                        } break;
                }
            }

            if (showHelpWritter != null) {
                ShowHelp(showHelpWritter);
                return;
            }

            var addrs = new List<string>();
            addrs.AddRange(Medo.Application.Args.Current.GetValues(""));
            addrs.AddRange(Medo.Application.Args.Current.GetValues("wake"));
            foreach (var iMacAddr in addrs) {
                if (!string.IsNullOrEmpty(iMacAddr)) {
                    if (Medo.Net.WakeOnLan.IsMacAddressValid(iMacAddr)) {
                        if (useIPv6) {
                            try {
                                Medo.Net.WakeOnLan.SendMagicPacketIPv6(iMacAddr);
                            } catch (InvalidOperationException ex) {
                                Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  {1}", iMacAddr.PadRight(6 * 2 + 5), ex.Message));
                            }
                        } else {
                            Medo.Net.WakeOnLan.SendMagicPacket(iMacAddr);
                        }
                        Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  Wake-on-LAN message sent.", iMacAddr.PadRight(6 * 2 + 5)));
                    } else {
                        Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  Invalid MAC address.", iMacAddr.PadRight(6 * 2 + 5)));
                    }
                }
            }

        }

        private static void ShowHelp(TextWriter output) {
            output.WriteLine(@"Sends Wake-on-LAN packet to computers on network.");
            output.WriteLine(@"");
            output.WriteLine(@"MAGIWOL [/ipv6] [/wake:""address1 address2 ... addressN""]");
            output.WriteLine(@"");
            output.WriteLine(@"  /wake       Wakes computers based on given list of MAC addresses.");
            output.WriteLine(@"  /ipv6       Uses IPv6 for wake-on-lan packages.");
        }

    }
}
