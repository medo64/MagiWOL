using System;
using System.Net;

namespace MagiWol {

    internal static class Settings {

        public static class Runtime { //reseted upon every application start

            public static bool ShowMenu { get; set; }

            public static int WolWaitBetweenComputersIntervalSeconds { get; set; }
        
        }



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

    }

}
