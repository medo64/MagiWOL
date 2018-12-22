using System;
using System.Net;
using Medo.Configuration;

namespace MagiWol {

    internal static class Settings {

        public static bool UseIPv4 {
            get { return Config.Read("UseIPv4", true) || (UseIPv6 == false); }
            set { Config.Write("UseIPv4", value); }
        }

        public static bool UseIPv6 {
            get { return Config.Read("UseIPv6", false); }
            set { Config.Write("UseIPv6", value); }
        }


        public static string DefaultBroadcastHost = "255.255.255.255";

        public static string BroadcastHost {
            get {
                string addressValue = Config.Read("DefaultBroadcastAddress", Settings.DefaultBroadcastHost);
                if (string.IsNullOrEmpty(addressValue)) { return "255.255.255.255"; }
                return addressValue;
            }
            set {
                Config.Write("DefaultBroadcastAddress", value.Trim());
            }
        }

        public static int DefaultBroadcastPort = 9;

        public static int BroadcastPort {
            get {
                var portValue = Config.Read("DefaultBroadcastPort", Settings.DefaultBroadcastPort);
                if ((portValue < 0) || (portValue > 65535)) { return 9; }
                return portValue;
            }
            set {
                Config.Write("DefaultBroadcastPort", value);
            }
        }

        [Obsolete]
        public static IPEndPoint DefaultPacketEndPoint {
            get {
                string addressValue = Config.Read("DefaultBroadcastAddress", "255.255.255.255");
                int portValue = Config.Read("DefaultBroadcastPort", 9);
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
                Config.Write("DefaultBroadcastAddress", value.Address.ToString());
                Config.Write("DefaultBroadcastPort", value.Port);
            }
        }



        public static int WolCount {
            get {
                return Config.Read("WolCount", 2);
            }
            set {
                if ((value < 1) || (value > 10)) { return; }
                Config.Write("WolCount", value);
            }
        }

        public static int WolSleepInterval {
            get {
                return Config.Read("WolSleepInterval", 250);
            }
            set {
                if ((value < 0) || (value > 1000)) { return; }
                Config.Write("WolSleepInterval", value);
            }
        }


        public static bool ShowColumnMac {
            get { return Config.Read("ShowColumnMac", true); }
            set { Config.Write("ShowColumnMac", value); }
        }

        public static bool ShowColumnSecureOn {
            get { return Config.Read("ShowColumnSecureOn", false); }
            set { Config.Write("ShowColumnSecureOn", value); }
        }

        public static bool ShowColumnBroadcastHost {
            get { return Config.Read("ShowColumnBroadcastHost", false); }
            set { Config.Write("ShowColumnBroadcastHost", value); }
        }

        public static bool ShowColumnBroadcastPort {
            get { return Config.Read("ShowColumnBroadcastPort", false); }
            set { Config.Write("ShowColumnBroadcastPort", value); }
        }

        public static bool ShowColumnNotes {
            get { return Config.Read("ShowColumnNotes", true); }
            set { Config.Write("ShowColumnNotes", value); }
        }


        public static string LastImportText {
            get { return Config.Read("LastImportText", ""); }
            set { Config.Write("LastImportText", value); }
        }

        public static double ScaleBoost {
            get { return Config.Read("ScaleBoost", 0.00); }
            set {
                if ((value < -1) || (value > 4)) { return; }
                Config.Write("ScaleBoost", value);
            }
        }


        public static class Runtime { //reseted upon every application start

            public static int WolWaitBetweenComputersIntervalSeconds { get; set; }

        }

    }

}
