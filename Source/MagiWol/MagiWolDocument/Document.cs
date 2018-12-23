using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace MagiWol.MagiWolDocument {

    internal class Document {

        protected List<AddressItem> _addresses = new List<AddressItem>();

        internal Document() {
        }


        public string FileName { get; protected set; }


        public static Document Open(string fileName) {
            var doc = new Document();
            Load(fileName, doc);
            return doc;
        }

        protected static void Load(string fileName, Document doc) {
            doc.FileName = fileName;
            foreach (var iAddress in GetAddressesFromXml(doc, System.IO.File.ReadAllText(fileName))) {
                doc._addresses.Add(iAddress);
                iAddress.Address.Parent = doc;
            }
        }


        public void Save() {
            this.Save(this.FileName);
        }

        public void Save(string fileName) {
            File.WriteAllText(fileName, GetXmlFromAddresses(this._addresses));
            this.FileName = fileName;
        }


        public bool HasAddress(AddressItem item) {
            return this._addresses.Contains(item);
        }


        protected static IEnumerable<AddressItem> GetAddressesFromXml(Document document, string xmlContent) {
            List<AddressItem> all = new List<AddressItem>();

            StringReader sr = null;
            try {
                sr = new StringReader(xmlContent);

                using (var xr = new XmlTextReader(sr)) {
                    sr = null;
                    while (xr.Read()) {
                        switch (xr.NodeType) {

                            case XmlNodeType.Element: {

                                    switch (xr.Name) {
                                        case "MagiWOL":
                                            break;
                                        case "Addresses":
                                            break;
                                        case "Address":
                                            string aName = xr.GetAttribute("name");
                                            string aMac = xr.GetAttribute("macAddress");
                                            string aSecureOn = xr.GetAttribute("secureOnPassword");
                                            string aHost = xr.GetAttribute("broadcastAddress");
                                            string aPort = xr.GetAttribute("broadcastPort");
                                            string aDescription = xr.GetAttribute("description");
                                            string aIsAddressValid = xr.GetAttribute("isBroadcastAddressValid");
                                            string aIsPortValid = xr.GetAttribute("isBroadcastPortValid");

                                            bool isHostValid;
                                            string host;
                                            if ((aHost != null) && (string.IsNullOrEmpty(aHost) == false)) {
                                                host = aHost;
                                                if (!bool.TryParse(aIsAddressValid, out isHostValid)) { //if it is overriden by version 2.00 setting
                                                    isHostValid = true;
                                                }
                                            } else {
                                                host = Settings.BroadcastHost;
                                                isHostValid = false;
                                            }

                                            bool isPortValid;
                                            int port;
                                            if ((aPort != null) && (int.TryParse(aPort, NumberStyles.Integer, CultureInfo.InvariantCulture, out port))) {
                                                if ((port >= 1) || (port <= 65535)) {
                                                    if (!bool.TryParse(aIsPortValid, out isPortValid)) { //if it is overriden by version 2.00 setting
                                                        isPortValid = true;
                                                    }
                                                } else {
                                                    port = Settings.BroadcastPort;
                                                    isPortValid = false;
                                                }
                                            } else {
                                                port = Settings.BroadcastPort;
                                                isPortValid = false;
                                            }

                                            AddressItem addr = new AddressItem(aName, aMac, aSecureOn, aDescription, host, port, isHostValid, isPortValid);
                                            all.Add(addr);
                                            break;
                                    }

                                }
                                break;

                            case XmlNodeType.EndElement: {
                                }
                                break;

                        }
                    }
                }

            } finally {
                if (sr != null) { sr.Dispose(); }
            }
            return all.AsReadOnly();
        }

        protected static string GetXmlFromAddresses(IEnumerable<AddressItem> addresses) {
            var sb = new StringBuilder();
            StringWriter sw = null;
            try {
                sw = new StringWriter(sb);
                using (var xw = new Medo.Xml.XmlTagWriter(sw)) {
                    sw = null;
                    xw.WriteStartDocument();
                    xw.StartTag("MagiWOL", "version", "2.00");
                    xw.StartTag("Addresses");
                    foreach (var iAddress in addresses) {
                        List<string> paramValuePairs = new List<string>();
                        paramValuePairs.AddRange(new string[] { "name", iAddress.Title });
                        paramValuePairs.AddRange(new string[] { "macAddress", iAddress.Mac });
                        paramValuePairs.AddRange(new string[] { "secureOnPassword", iAddress.SecureOn });
                        paramValuePairs.AddRange(new string[] { "broadcastAddress", iAddress.BroadcastHost });
                        paramValuePairs.AddRange(new string[] { "broadcastPort", iAddress.BroadcastPort.ToString(CultureInfo.InvariantCulture) });
                        paramValuePairs.AddRange(new string[] { "isBroadcastAddressValid", iAddress.IsBroadcastHostValid.ToString(CultureInfo.InvariantCulture) });
                        paramValuePairs.AddRange(new string[] { "isBroadcastPortValid", iAddress.IsBroadcastPortValid.ToString(CultureInfo.InvariantCulture) });
                        paramValuePairs.AddRange(new string[] { "description", iAddress.Notes });
                        xw.WriteTag("Address", paramValuePairs.ToArray());
                    }
                    xw.EndTag(); //Addresses
                    xw.EndTag(); //MagiWOL
                }
            } finally {
                if (sw != null) { sw.Dispose(); }
            }
            return sb.ToString();
        }


        private static Regex _rxMacValid = new Regex(@"(^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$)|(^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$)|(^[0-9A-F]{4}\.[0-9A-F]{4}\.[0-9A-F]{4}$)|(^[0-9A-F]{12}$)", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        protected static AddressItem GetAddressFromLine(string line) {
            string[] parts = line.Split(new char[] { ' ', '\t' });
            for (int i = 0; i < parts.Length; i++) {
                if (_rxMacValid.IsMatch(parts[i])) {
                    if (i == 0) {
                        string name = string.Join(" ", parts, 1, parts.Length - 1);
                        string mac = GetProperMAC(parts[i]);
                        return new AddressItem(name, mac, string.Empty, string.Empty, null, null, false, false);
                    } else {
                        string name = string.Join(" ", parts, 0, i);
                        string mac = GetProperMAC(parts[i]);
                        string desc = string.Join(" ", parts, i + 1, parts.Length - i - 1);
                        return new AddressItem(name, mac, string.Empty, desc, null, null, false, false);
                    }
                }
            }
            return null;
        }


        private static string GetProperMAC(string text) {
            string addressText = System.Text.RegularExpressions.Regex.Replace(text.ToUpper(), "[^0-9A-F]", ":") + ":";
            Medo.Text.StringAdder newText = new Medo.Text.StringAdder(":");

            string lastPart = string.Empty;
            for (int i = 0; i < addressText.Length; i++) {
                switch (addressText[i]) {

                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case 'A':
                    case 'B':
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                        lastPart += addressText.Substring(i, 1);
                        break;

                    case ':':
                        while (lastPart.Length > 0) {
                            string tmpPart = string.Empty;
                            if (lastPart.Length > 2) {
                                tmpPart = lastPart.Substring(0, 2);
                                lastPart = lastPart.Substring(2, lastPart.Length - 2);
                            } else {
                                tmpPart = lastPart;
                                if ((tmpPart.Length < 2) && (i < (addressText.Length - 1))) { tmpPart = tmpPart.PadLeft(2, '0'); }
                                lastPart = string.Empty;
                            }
                            newText.Append(tmpPart);
                        }
                        break;

                }
            }//for

            if (newText.ToString().Length > 2 * 6 + 5) {
                return newText.ToString().Substring(0, 2 * 6 + 5);
            } else {
                return newText.ToString();
            }

        }


        public bool HasChanged { get; internal set; }

        internal void MarkAsChanged() {
            this.HasChanged = true;
        }

    }
}
