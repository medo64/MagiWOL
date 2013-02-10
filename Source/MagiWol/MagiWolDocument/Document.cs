using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace MagiWol.MagiWolDocument {

    internal class Document {

        private List<Address> _addresses = new List<Address>();


        public Document() {
        }

        public void FillListView(ListView list, IEnumerable<Address> selectedItems) {
            var selectionList = new List<Address>();
            if (selectedItems != null) {
                foreach (var iItem in selectedItems) {
                    selectionList.Add(iItem);
                }
            }

            MagiWolDocument.Address potentialFocus = null;

            list.BeginUpdate();

            Medo.Windows.Forms.State.Save(list);
            list.Columns.Clear();
            list.Columns.Add(new ColumnHeader() { Tag = "Name", Text = "Name", Width = 175 });
            if (Settings.ShowColumnMac) { list.Columns.Add(new ColumnHeader() { Tag = "Mac", Text = "Mac", Width = 175 }); }
            if (Settings.ShowColumnSecureOn) { list.Columns.Add(new ColumnHeader() { Tag = "SecureOn", Text = "SecureOn", Width = 175 }); }
            if (Settings.ShowColumnBroadcastHost) { list.Columns.Add(new ColumnHeader() { Tag = "BroadcastHost", Text = "Host", Width = 150 }); }
            if (Settings.ShowColumnBroadcastPort) { list.Columns.Add(new ColumnHeader() { Tag = "BroadcastPort", Text = "Port", Width = 60 }); }
            if (Settings.ShowColumnNotes) { list.Columns.Add(new ColumnHeader() { Text = "Notes", Tag = "Notes", Width = 270 }); }
            Medo.Windows.Forms.State.Load(list);

            list.Items.Clear();
            for (int i = 0; i < this._addresses.Count; ++i) {
                _addresses[i].Selected = selectionList.Contains(_addresses[i]);
                _addresses[i].RefreshColumns();
                list.Items.Add(_addresses[i]);
                if (selectionList.Contains(_addresses[i])) {
                    potentialFocus = _addresses[i];
                }
            }

            list.EndUpdate();

            if (potentialFocus != null) { list.FocusedItem = potentialFocus; }
        }

        public string FileName { get; private set; }


        public static Document Open(string fileName) {
            var doc = new Document();
            doc.FileName = fileName;
            foreach (var iAddress in GetAddressesFromXml(doc, System.IO.File.ReadAllText(fileName))) {
                doc.AddAddress(iAddress, true);
            }
            doc.HasChanged = false;
            return doc;
        }

        public void Save() {
            this.Save(this.FileName);
        }

        public void Save(string fileName) {
            File.WriteAllText(fileName, GetXmlFromAddresses(this._addresses));
            this.FileName = fileName;
            this.HasChanged = false;
        }

        public bool HasChanged { get; internal set; }
        internal void MarkAsChanged() {
            this.HasChanged = true;
        }

        public string FileTitle {
            get {
                string fileNamePart;
                if (this.FileName == null) {
                    fileNamePart = "Untitled";
                } else {
                    var fi = new FileInfo(this.FileName);
                    fileNamePart = fi.Name;
                }
                if (this.HasChanged) {
                    return fileNamePart + "*";
                } else {
                    return fileNamePart;
                }
            }
        }

        public bool HasAddress(Address item) {
            return this._addresses.Contains(item);
        }


        public void AddAddress(Address item, bool allowDuplicates) {
            if ((allowDuplicates == true) || (this._addresses.Contains(item) == false)) {
                item.Parent = this;
                _addresses.Add(item);
                this.HasChanged = true;
            }
        }

        public void RemoveAddress(Address item) {
            this._addresses.Remove(item);
            this.HasChanged = true;
        }

        public void Cut(IEnumerable<Address> addresses) {
            bool isChanged = false;
            Copy(addresses);
            foreach (var iAddress in addresses) {
                if (this._addresses.Contains(iAddress)) {
                    this._addresses.Remove(iAddress);
                    isChanged = true;
                }
            }
            if (isChanged) { this.HasChanged = true; }
        }

        public void Copy(IEnumerable<Address> addresses) {
            var sb = new StringBuilder();
            foreach (var iAddress in addresses) {
                sb.AppendLine(iAddress.Mac + " " + iAddress.Title);
            }

            try {
                Clipboard.Clear();
                DataObject clipData = new DataObject();
                clipData.SetData(DataFormats.UnicodeText, true, sb.ToString());
                clipData.SetData("MagiWOL", false, GetXmlFromAddresses(addresses));
                Clipboard.SetDataObject(clipData, true);
            } catch (ExternalException) { }
        }

        public IEnumerable<Address> Paste() {
            var pastedAddresses = new List<Address>();

            string xmlData = null;
            string dataText = null;
            try {
                IDataObject clipData = Clipboard.GetDataObject();
                xmlData = clipData.GetData("MagiWOL") as string;
                if (xmlData == null) {
                    dataText = clipData.GetData(DataFormats.UnicodeText, true) as string;
                    if (dataText == null) { dataText = clipData.GetData(DataFormats.Text) as string; }
                }
            } catch (ExternalException) {
                return pastedAddresses;
            }

            bool isChanged = false;
            if (xmlData != null) {
                foreach (var iAddress in GetAddressesFromXml(this, xmlData)) {
                    if (!this.HasAddress(iAddress)) {
                        this.AddAddress(iAddress, false);
                        pastedAddresses.Add(iAddress);
                        isChanged = true;
                    } else {
                        foreach (var feAddress in this._addresses) {
                            if (feAddress.Equals(iAddress)) {
                                pastedAddresses.Add(feAddress);
                                break;
                            }
                        }
                    }
                }
            } else if (dataText != null) {
                foreach (var iLine in dataText.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
                    var iAddress = GetAddressFromLine(iLine);
                    if (iAddress != null) {
                        if (!this.HasAddress(iAddress)) {
                            this.AddAddress(iAddress, false);
                            pastedAddresses.Add(iAddress);
                            isChanged = true;
                        } else {
                            foreach (var feAddress in this._addresses) {
                                if (feAddress.Equals(iAddress)) {
                                    pastedAddresses.Add(feAddress);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (isChanged) { this.HasChanged = true; }


            return pastedAddresses.AsReadOnly();
        }

        public bool CanPaste() {
            try {
                return Clipboard.ContainsData("MagiWOL") || Clipboard.ContainsText();
            } catch (ExternalException) {
                return false;
            }
        }

        private static IEnumerable<Address> GetAddressesFromXml(Document document, string xmlContent) {
            List<Address> all = new List<Address>();

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

                                            Address addr = new Address(aName, aMac, aSecureOn, aDescription, host, port, isHostValid, isPortValid);
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

        private static string GetXmlFromAddresses(IEnumerable<Address> addresses) {
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

        private static Address GetAddressFromLine(string line) {
            string[] parts = line.Split(new char[] { ' ', '\t' });
            for (int i = 0; i < parts.Length; i++) {
                if (_rxMacValid.IsMatch(parts[i])) {
                    if (i == 0) {
                        string name = string.Join(" ", parts, 1, parts.Length - 1);
                        string mac = GetProperMAC(parts[i]);
                        return new Address(name, mac, string.Empty, string.Empty, null, null, false, false);
                    } else {
                        string name = string.Join(" ", parts, 0, i);
                        string mac = GetProperMAC(parts[i]);
                        string desc = string.Join(" ", parts, i + 1, parts.Length - i - 1);
                        return new Address(name, mac, string.Empty, desc, null, null, false, false);
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

    }
}
