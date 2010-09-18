//Josip Medved <jmedved@jmedved.com> http://www.jmedved.com

//2007-10-30: Moved to common.
//2008-01-03: Added Resources.
//2008-03-15: Added IsMacAddressValid and IsSecureOnPasswordValid.
//2008-11-07: Refactored to enable testing.


using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace Medo.Net {

    /// <summary>
    /// Class for performing Wake on Lan calls
    /// </summary>
    public static class WakeOnLan {

        private static System.Text.RegularExpressions.Regex _rxMacValid = new System.Text.RegularExpressions.Regex(@"(^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$)|(^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$)|(^[0-9A-F]{4}\.[0-9A-F]{4}\.[0-9A-F]{4}$)|(^[0-9A-F]{12}$)", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        private static System.Text.RegularExpressions.Regex _rxMacFilter = new System.Text.RegularExpressions.Regex(@"[^0-9A-F]", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);


        /// <summary>
        /// Sends Wake up signal to all computers on local network via UDP.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up</param>
        /// <exception cref="System.ArgumentException">MAC address is in wrong format.</exception>
        public static void SendMagicPacket(string macAddress) {
            SendMagicPacket(macAddress, null);
        }


        /// <summary>
        /// Sends Wake up signal to all computers on local network via UDP.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up</param>
        /// <param name="secureOnPassword">SecureOn password of computer to wake up</param>
        /// <exception cref="System.ArgumentException">MAC address is in wrong format.</exception>
        public static void SendMagicPacket(string macAddress, string secureOnPassword) {
            SendMagicPacket(macAddress, secureOnPassword, System.Net.IPAddress.Broadcast, 9);
        }


        /// <summary>
        /// Sends Wake up and SecureOn signal to all computers on local network via UDP.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up.</param>
        /// <param name="secureOnPassword">SecureOn password of computer to wake up.</param>
        /// <param name="address">IP address to which UPD MagicPacket should be sent.</param>
        /// <param name="port">Port on which UDP MagicPacket should be sent. Default value is 9, but other common ports include 0, 7, 9, 2304, 5555, 12287 and 65535.</param>
        /// <exception cref="System.ArgumentException">MAC address -or- SecureOn password is in wrong format.</exception>
        public static void SendMagicPacket(string macAddress, string secureOnPassword, System.Net.IPAddress address, int port) {
            if ((macAddress == null) || (!_rxMacValid.IsMatch(macAddress))) {
                throw new ArgumentException(Resources.ExceptionMacAddressIsInWrongFormat);
            }
            if ((!string.IsNullOrEmpty(secureOnPassword)) && (!_rxMacValid.IsMatch(secureOnPassword))) {
                throw new ArgumentException(Resources.ExceptionSecureOnPasswordIsInWrongFormat);
            }

            byte[] packet = GetPacketBytes(macAddress, secureOnPassword);

            Socket socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

            IPEndPoint ipEP = new System.Net.IPEndPoint(address, port);
            socket.SendTo(packet, ipEP);
        }

        private static byte[] GetAddressBytes(string address) {
            if (string.IsNullOrEmpty(address)) {
                return null;
            } else {
                string addressWithoutDelimiters = _rxMacFilter.Replace(address, "");
                byte[] ret = new byte[6];
                for (int i = 0; i < 6; ++i) {
                    ret[i] = byte.Parse(addressWithoutDelimiters.Substring(i * 2, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }
                return ret;
            }
        }

        private static byte[] GetPacketBytes(string macAddress, string secureOnPassword) {
            return GetPacketBytes(GetAddressBytes(macAddress), GetAddressBytes(secureOnPassword));
        }

        private static byte[] GetPacketBytes(byte[] macAddress, byte[] secureOnPassword) {
            byte[] packetBytes;
            if (secureOnPassword != null) {
                packetBytes = new byte[6 + 16 * 6 + 6];            //1x(6xFF) 16x(MAC) 1x(SECUREON)
            } else {
                packetBytes = new byte[6 + 16 * 6];//1x(6xFF) 16x(MAC)
            }

            for (int j = 0; j < 6; j++) { //1x(6xFF)
                packetBytes[0 + j] = 0xFF;
            }

            for (int i = 0; i < 16; ++i) { //16x(MAC)
                packetBytes[6 + i * 6 + 0] = macAddress[0];
                packetBytes[6 + i * 6 + 1] = macAddress[1];
                packetBytes[6 + i * 6 + 2] = macAddress[2];
                packetBytes[6 + i * 6 + 3] = macAddress[3];
                packetBytes[6 + i * 6 + 4] = macAddress[4];
                packetBytes[6 + i * 6 + 5] = macAddress[5];
            }

            if (secureOnPassword != null) { //1x(SECUREON)
                for (int j = 0; j < 6; ++j) {
                    packetBytes[6 + 16 * 6 + 0] = secureOnPassword[0];
                    packetBytes[6 + 16 * 6 + 1] = secureOnPassword[1];
                    packetBytes[6 + 16 * 6 + 2] = secureOnPassword[2];
                    packetBytes[6 + 16 * 6 + 3] = secureOnPassword[3];
                    packetBytes[6 + 16 * 6 + 4] = secureOnPassword[4];
                    packetBytes[6 + 16 * 6 + 5] = secureOnPassword[5];
                }
            }

            return packetBytes;
        }

        /// <summary>
        /// Returns true if MAC address is in valid form.
        /// </summary>
        /// <param name="macAddress">MAC address to check.</param>
        public static bool IsMacAddressValid(string macAddress) {
            if ((macAddress == null) || (!_rxMacValid.IsMatch(macAddress))) {
                return false;
            } else {
                return true;
            }
        }

        /// <summary>
        /// Returns true if SecureOn password is in valid form.
        /// </summary>
        /// <param name="secureOnPassword">SecureOn password to check.</param>
        public static bool IsSecureOnPasswordValid(string secureOnPassword) {
            if ((!string.IsNullOrEmpty(secureOnPassword)) && (!_rxMacValid.IsMatch(secureOnPassword))) {
                return false;
            } else {
                return true;
            }
        }


        private static class Resources {

            internal static string ExceptionMacAddressIsInWrongFormat { get { return "MAC address is in wrong format."; } }
            internal static string ExceptionSecureOnPasswordIsInWrongFormat { get { return "SecureOn password is in wrong format."; } }

        }

    }

}
