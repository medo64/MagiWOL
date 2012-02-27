using System;
using System.Net;

namespace MagiWol {

    internal static class Settings {

        public static bool IsInstalled {
            get { return Medo.Configuration.Settings.Read("Installed", false); }
        }


        public static bool UseIPv4 {
            get { return Medo.Configuration.Settings.Read("UseIPv4", true) || (UseIPv6 == false); }
            set { Medo.Configuration.Settings.Write("UseIPv4", value); }
        }

        public static bool UseIPv6 {
            get { return Medo.Configuration.Settings.Read("UseIPv6", false); }
            set { Medo.Configuration.Settings.Write("UseIPv6", value); }
        }


        public static string DefaultBroadcastHost = "255.255.255.255";

        public static string BroadcastHost {
            get {
                string addressValue = Medo.Configuration.Settings.Read("DefaultBroadcastAddress", Settings.DefaultBroadcastHost);
                if (string.IsNullOrEmpty(addressValue)) { return "255.255.255.255"; }
                return addressValue;
            }
            set {
                Medo.Configuration.Settings.Write("DefaultBroadcastAddress", value.Trim());
            }
        }

        public static int DefaultBroadcastPort = 9;

        public static int BroadcastPort {
            get {
                var portValue = Medo.Configuration.Settings.Read("DefaultBroadcastPort", Settings.DefaultBroadcastPort);
                if ((portValue < 0) || (portValue > 65535)) { return 9; }
                return portValue;
            }
            set {
                Medo.Configuration.Settings.Write("DefaultBroadcastPort", value);
            }
        }

        [Obsolete]
        public static IPEndPoint DefaultPacketEndPoint {
            get {
                string addressValue = Medo.Configuration.Settings.Read("DefaultBroadcastAddress", "255.255.255.255");
                int portValue = Medo.Configuration.Settings.Read("DefaultBroadcastPort", 9);
                try {
                    return new IPEndPoint(IPAddress.Parse(addressValue), portValue);
                } catch (ArgumentOutOfRangeException) {
                    return new IPEndPoint(IPAddress.Broadcast, 9);
                } catch (FormatException) {
                    return new IPEndPoint(IPAddress.Broadcast, 9);
                }
            }
            set {
                if (value.Address == IPAddress.Any) { return; }
                Medo.Configuration.Settings.Write("DefaultBroadcastAddress", value.Address.ToString());
                Medo.Configuration.Settings.Write("DefaultBroadcastPort", value.Port);
            }
        }



        public static int WolCount {
            get {
                return Medo.Configuration.Settings.Read("WolCount", 2);
            }
            set {
                if ((value < 1) || (value > 10)) { return; }
                Medo.Configuration.Settings.Write("WolCount", value);
            }
        }

        public static int WolSleepInterval {
            get {
                return Medo.Configuration.Settings.Read("WolSleepInterval", 250);
            }
            set {
                if ((value < 0) || (value > 1000)) { return; }
                Medo.Configuration.Settings.Write("WolSleepInterval", value);
            }
        }


        public static bool ShowColumnTitle {
            get { return true; }
        }

        public static bool ShowColumnMac {
            get { return Medo.Configuration.Settings.Read("ShowColumnMac", true); }
            set { Medo.Configuration.Settings.Write("ShowColumnMac", value); }
        }

        public static bool ShowColumnSecureOn {
            get { return Medo.Configuration.Settings.Read("ShowColumnSecureOn", false); }
            set { Medo.Configuration.Settings.Write("ShowColumnSecureOn", value); }
        }

        public static bool ShowColumnBroadcastHost {
            get { return Medo.Configuration.Settings.Read("ShowColumnBroadcastHost", false); }
            set { Medo.Configuration.Settings.Write("ShowColumnBroadcastHost", value); }
        }

        public static bool ShowColumnBroadcastPort {
            get { return Medo.Configuration.Settings.Read("ShowColumnBroadcastPort", false); }
            set { Medo.Configuration.Settings.Write("ShowColumnBroadcastPort", value); }
        }

        public static bool ShowColumnNotes {
            get { return Medo.Configuration.Settings.Read("ShowColumnNotes", true); }
            set { Medo.Configuration.Settings.Write("ShowColumnNotes", value); }
        }


        public static class Runtime { //reseted upon every application start

            public static int WolWaitBetweenComputersIntervalSeconds { get; set; }

        }

    }

}
