using System;
using System.Collections.Generic;
using System.Globalization;

namespace MagiWolConsole {

    internal static class App {

        [STAThread]
        static void Main() {

            if (Medo.Application.Args.Current.ContainsKey("?") || Medo.Application.Args.Current.ContainsKey("help")) {

                Console.WriteLine(@"Sends Wake-on-LAN packet to computers on network.");
                Console.WriteLine(@"");
                Console.WriteLine(@"MAGIWOL [/wake:""address1 address2 ... addressN""]");
                Console.WriteLine(@"");
                Console.WriteLine(@"  /wake       Wakes computers based on given list of MAC addresses");

            } else {

                foreach (var iKey in Medo.Application.Args.Current.GetKeys()) {

                    if (string.Compare(iKey, "wake", StringComparison.CurrentCultureIgnoreCase) == 0) {

                        var addrs = new List<string>();
                        addrs.AddRange(Medo.Application.Args.Current.GetValues(iKey));
                        addrs.AddRange(Medo.Application.Args.Current.GetValues(""));
                        foreach (var iMacAddr in addrs) {
                            if (!string.IsNullOrEmpty(iMacAddr)) {
                                if (Medo.Net.WakeOnLan.IsMacAddressValid(iMacAddr)) {
                                    Medo.Net.WakeOnLan.SendMagicPacket(iMacAddr);
                                    Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  Wake-on-LAN message sent.", iMacAddr.PadRight(6 * 2 + 5)));
                                } else {
                                    Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0}  Invalid MAC address.", iMacAddr.PadRight(6 * 2 + 5)));
                                }
                            }
                        }

                    } else {

                        if (!string.IsNullOrEmpty(iKey)) {
                            Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "Argument \"{0}\" is not recognized.", iKey));
                        }

                    }

                }

            }

        }

    }

}
