using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MagiWol.MagiWolDocument;

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
                        }
                        break;

                    case "IPV6": {
                            useIPv6 = true;
                        }
                        break;

                    case "WAKE":
                        break;

                    default: {
                            Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "Argument \"{0}\" is not recognized.", iKey));
                            Console.Error.WriteLine();
                            showHelpWritter = Console.Error;
                        }
                        break;
                }
            }

            if (showHelpWritter != null) {
                ShowHelp(showHelpWritter);
                return;
            }

            var addrs = new List<string>();

            var inputs = new List<string>();
            inputs.AddRange(Medo.Application.Args.Current.GetValues(""));
            inputs.AddRange(Medo.Application.Args.Current.GetValues("wake"));
            foreach (var input in inputs) {
                if (!string.IsNullOrWhiteSpace(input)) {
                    if (Medo.Net.WakeOnLan.IsMacAddressValid(input)) {
                        addrs.Add(input);
                    } else {
                        if (File.Exists(input)) {
                            try {
                                var doc = Document.Open(input);
                                foreach (var addr in doc.Addresses) {
                                    addrs.Add(addr.Mac);
                                }
                            } catch (Exception ex) {
                                Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "Cannot read file {0}: {1}", input, ex.Message));
                            }
                        } else {
                            Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "Invalid MAC address {0}", input));
                        }
                    }
                }
            }

            foreach (var iMacAddr in addrs) {
                if (!string.IsNullOrEmpty(iMacAddr)) {
                    if (useIPv6) {
                        try {
                            Medo.Net.WakeOnLan.SendMagicPacketIPv6(iMacAddr);
                            Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  IPv6 wake-on-LAN message sent", iMacAddr.PadRight(6 * 2 + 5)));
                        } catch (InvalidOperationException ex) {
                            Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  {1}", iMacAddr.PadRight(6 * 2 + 5), ex.Message));
                        }
                    } else {
                        try {
                            Medo.Net.WakeOnLan.SendMagicPacket(iMacAddr);
                            Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  IPv4 wake-on-LAN message sent", iMacAddr.PadRight(6 * 2 + 5)));
                        } catch (InvalidOperationException ex) {
                            Console.Error.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  {1}", iMacAddr.PadRight(6 * 2 + 5), ex.Message));
                        }
                    }
                }
            }

#if DEBUG
            Console.ReadKey();
#endif
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
