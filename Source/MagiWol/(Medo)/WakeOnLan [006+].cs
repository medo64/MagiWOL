//Copyright (c) 2007 Josip Medved <jmedved@jmedved.com>

//2007-10-30: Moved to common.
//2008-01-03: Added Resources.
//2008-03-15: Added IsMacAddressValid and IsSecureOnPasswordValid.
//2008-11-07: Refactored to enable testing.
//2012-02-26: Added IPv6 support.
//2012-05-16: GetPacketBytes are internal. 
//2019-01-05: Reworked IPv6 packet error handling.

using System;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Medo.Net {

    /// <summary>
    /// Class for performing Wake on Lan calls
    /// </summary>
    public static class WakeOnLan {

        private static readonly Regex _rxMacValid = new Regex(@"(^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$)|(^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$)|(^[0-9A-F]{4}\.[0-9A-F]{4}\.[0-9A-F]{4}$)|(^[0-9A-F]{12}$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _rxMacFilter = new Regex(@"[^0-9A-F]", RegexOptions.Compiled | RegexOptions.IgnoreCase);


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
            SendMagicPacket(macAddress, secureOnPassword, IPAddress.Broadcast, 9);
        }


        /// <summary>
        /// Sends Wake up and SecureOn signal to all computers on local network via UDP.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up.</param>
        /// <param name="secureOnPassword">SecureOn password of computer to wake up.</param>
        /// <param name="address">IP address to which UPD MagicPacket should be sent.</param>
        /// <param name="port">Port on which UDP MagicPacket should be sent. Default value is 9, but other common ports include 0, 7, 9, 2304, 5555, 12287 and 65535.</param>
        /// <exception cref="System.ArgumentException">MAC address -or- SecureOn password is in wrong format.</exception>
        public static void SendMagicPacket(string macAddress, string secureOnPassword, IPAddress address, int port) {
            if ((macAddress == null) || (!_rxMacValid.IsMatch(macAddress))) { throw new ArgumentException(Resources.ExceptionMacAddressIsInWrongFormat); }
            if ((!string.IsNullOrEmpty(secureOnPassword)) && (!_rxMacValid.IsMatch(secureOnPassword))) { throw new ArgumentException(Resources.ExceptionSecureOnPasswordIsInWrongFormat); }

            byte[] packet = GetPacketBytes(macAddress, secureOnPassword);

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)) {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                var ipEP = new IPEndPoint(address, port);
                socket.SendTo(packet, ipEP);
            }
        }


        /// <summary>
        /// Sends Wake-up and SecureOn signal to computers on local network using each IPv6 interface.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up.</param>
        /// <exception cref="System.InvalidOperationException">Cannot find any IPv6 address.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pv", Justification = "IPv6 is intentional spelling.")]
        public static void SendMagicPacketIPv6(string macAddress) {
            SendMagicPacketIPv6(macAddress, null, IPAddress.IPv6Any, 9);
        }

        /// <summary>
        /// Sends Wake-up and SecureOn signal to computers on local network using each IPv6 interface.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up.</param>
        /// <param name="secureOnPassword">SecureOn password of computer to wake up.</param>
        /// <exception cref="System.InvalidOperationException">Cannot find any IPv6 address.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pv", Justification = "IPv6 is intentional spelling.")]
        public static void SendMagicPacketIPv6(string macAddress, string secureOnPassword) {
            SendMagicPacketIPv6(macAddress, secureOnPassword, IPAddress.IPv6Any, 9);
        }

        /// <summary>
        /// Sends Wake-up and SecureOn signal to computers on local network.
        /// </summary>
        /// <param name="macAddress">MAC address of computer to wake up.</param>
        /// <param name="secureOnPassword">SecureOn password of computer to wake up.</param>
        /// <param name="localIPv6Address">IP address from which packet should be sent.</param>
        /// <param name="port">Port on which UDP MagicPacket should be sent. Default value is 9, but other common ports include 0, 7, 9, 2304, 5555, 12287 and 65535.</param>
        /// <exception cref="System.ArgumentNullException">IP address cannot be null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">IP address must be IPv6.</exception>
        /// <exception cref="System.InvalidOperationException">Cannot find any IPv6 address.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Pv", Justification = "IPv6 is intentional spelling.")]
        public static void SendMagicPacketIPv6(string macAddress, string secureOnPassword, IPAddress localIPv6Address, int port) {
            if ((macAddress == null) || (!_rxMacValid.IsMatch(macAddress))) { throw new ArgumentException(Resources.ExceptionMacAddressIsInWrongFormat); }
            if ((!string.IsNullOrEmpty(secureOnPassword)) && (!_rxMacValid.IsMatch(secureOnPassword))) { throw new ArgumentException(Resources.ExceptionSecureOnPasswordIsInWrongFormat); }
            if (localIPv6Address == null) { throw new ArgumentNullException("localIPv6Address", Resources.ExceptionIPAddressCannotBeNull); }
            if (localIPv6Address.AddressFamily != AddressFamily.InterNetworkV6) { throw new ArgumentOutOfRangeException("localIPv6Address", Resources.ExceptionIPAddressMustBeIPv6); }

            byte[] packet = GetPacketBytes(macAddress, secureOnPassword);
            if (localIPv6Address == IPAddress.IPv6Any) {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                bool didSendOne = false;
                Exception firstException = null;
                foreach (IPAddress address in host.AddressList) {
                    if (address.AddressFamily == AddressFamily.InterNetworkV6) {
                        try {
                            SendIPv6Packet(address, port, packet);
                            didSendOne = true;
                        } catch (SocketException ex) {
                            firstException = ex;
                        }
                    }
                }
                if (didSendOne == false) {
                    if (firstException != null) {
                        throw new InvalidOperationException(Resources.ExceptionCannotFindAnyIPv6Address + ": " + firstException.Message, firstException);
                    } else {
                        throw new InvalidOperationException(Resources.ExceptionCannotFindAnyIPv6Address);
                    }
                }
            } else {
                SendIPv6Packet(localIPv6Address, port, packet);
            }
        }

        private readonly static IPAddress IPv6MulticastAddress = IPAddress.Parse("FF02::1");


        private static void SendIPv6Packet(IPAddress address, int port, byte[] packet) {
            using (var socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp)) {
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                socket.Bind(new IPEndPoint(address, port));
                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(WakeOnLan.IPv6MulticastAddress));
                socket.SendTo(packet, new IPEndPoint(WakeOnLan.IPv6MulticastAddress, port));
                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, new IPv6MulticastOption(WakeOnLan.IPv6MulticastAddress));
            }
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


        internal static byte[] GetPacketBytes(string macAddress, string secureOnPassword) {
            return GetPacketBytes(GetAddressBytes(macAddress), GetAddressBytes(secureOnPassword));
        }

        internal static byte[] GetPacketBytes(byte[] macAddress, byte[] secureOnPassword) {
            byte[] packetBytes;
            if (secureOnPassword != null) {
                packetBytes = new byte[6 + 16 * 6 + 6]; //1x(6xFF) 16x(MAC) 1x(SECUREON)
            } else {
                packetBytes = new byte[6 + 16 * 6]; //1x(6xFF) 16x(MAC)
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
            internal static string ExceptionIPAddressCannotBeNull { get { return "IP address cannot be null."; } }
            internal static string ExceptionIPAddressMustBeIPv6 { get { return "IP address must be IPv6."; } }
            internal static string ExceptionCannotFindAnyIPv6Address { get { return "Cannot find any IPv6 address"; } }

        }

    }

}
