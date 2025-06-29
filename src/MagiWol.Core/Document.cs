namespace MagiWol;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

public class Document {

    protected List<Address> _addresses = new();
    public IEnumerable<Address> Addresses { get { return _addresses; } }


    public Document() {
    }


    public string? FileName { get; protected set; }


    public static Document Open(string fileName) {
        var doc = new Document();
        Load(fileName, doc);
        return doc;
    }

    protected static void Load(string fileName, Document doc) {
        doc.FileName = fileName;
        foreach (var iAddress in GetAddressesFromXml(doc, System.IO.File.ReadAllText(fileName))) {
            doc._addresses.Add(iAddress);
            iAddress.Parent = doc;
        }
    }


    public void Save() {
        if (FileName == null) { throw new InvalidOperationException("Cannot save without a file name."); }
        Save(FileName);
    }

    public void Save(string fileName) {
        File.WriteAllText(fileName, GetXmlFromAddresses(_addresses));
        FileName = fileName;
    }


    public void AddAddress(Address item, bool allowDuplicates) {
        if ((allowDuplicates == true) || (_addresses.Contains(item) == false)) {
            item.Parent = this;
            _addresses.Add(item);
            HasChanged = true;
        }
    }

    public void RemoveAddress(Address item) {
        _addresses.Remove(item);
        HasChanged = true;
    }

    public bool HasAddress(Address item) {
        return _addresses.Contains(item);
    }


    protected static IEnumerable<Address> GetAddressesFromXml(Document document, string xmlContent) {
        var all = new List<Address>();

        StringReader? sr = null;
        try {
            sr = new StringReader(xmlContent);

            using var xr = new XmlTextReader(sr);
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
                                    string aName = xr.GetAttribute("name") ?? "";
                                    string aMac = xr.GetAttribute("macAddress") ?? "";
                                    string aSecureOn = xr.GetAttribute("secureOnPassword") ?? "";
                                    string aHost = xr.GetAttribute("broadcastAddress") ?? "";
                                    string aPort = xr.GetAttribute("broadcastPort") ?? "";
                                    string aDescription = xr.GetAttribute("description") ?? "";

                                    string? host;
                                    if ((aHost != null) && !string.IsNullOrEmpty(aHost)) {
                                        host = aHost;
                                    } else {
                                        host = null;
                                    }

                                    int? port;
                                    if ((aPort != null) && int.TryParse(aPort, NumberStyles.Integer, CultureInfo.InvariantCulture, out var portNumber)) {
                                        if ((portNumber >= 1) || (portNumber <= 65535)) {
                                            port = portNumber;
                                        } else {
                                            port = null;
                                        }
                                    } else {
                                        port = null;
                                    }

                                    var addr = new Address(aMac, aName, aDescription, aSecureOn, host, port);
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

        } finally {
            if (sr != null) { sr.Dispose(); }
        }
        return all.AsReadOnly();
    }

    protected static string GetXmlFromAddresses(IEnumerable<Address> addresses) {
        var sb = new StringBuilder();
        StringWriter? sw = null;
        try {
            sw = new StringWriter(sb);
            using var xw = new Medo.Xml.XmlTagWriter(sw);
            sw = null;
            xw.WriteStartDocument();
            xw.StartTag("MagiWOL", "version", "2.00");
            xw.StartTag("Addresses");
            foreach (var iAddress in addresses) {
                List<string> paramValuePairs = new List<string>();
                paramValuePairs.AddRange(new string[] { "name", iAddress.Title });
                paramValuePairs.AddRange(new string[] { "macAddress", iAddress.Mac });
                if (iAddress.SecureOn != null) {
                    paramValuePairs.AddRange(new string[] { "secureOnPassword", iAddress.SecureOn });
                }
                if (iAddress.BroadcastHost != null) {
                    paramValuePairs.AddRange(new string[] { "broadcastAddress", iAddress.BroadcastHost });
                }
                if (iAddress.BroadcastPort != null) {
                    paramValuePairs.AddRange(new string[] { "broadcastPort", iAddress.BroadcastPort.Value.ToString(CultureInfo.InvariantCulture) });
                }
                paramValuePairs.AddRange(new string[] { "description", iAddress.Notes });
                xw.WriteTag("Address", paramValuePairs.ToArray());
            }
            xw.EndTag(); //Addresses
            xw.EndTag(); //MagiWOL
        } finally {
            if (sw != null) { sw.Dispose(); }
        }
        return sb.ToString();
    }


    private static Regex _rxMacValid = new Regex(@"(^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$)|(^[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}:[0-9A-F]{2}$)|(^[0-9A-F]{4}\.[0-9A-F]{4}\.[0-9A-F]{4}$)|(^[0-9A-F]{12}$)", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    protected static Address? GetAddressFromLine(string line) {
        string[] parts = line.Split(new char[] { ' ', '\t' });
        for (int i = 0; i < parts.Length; i++) {
            if (_rxMacValid.IsMatch(parts[i])) {
                if (i == 0) {
                    var name = string.Join(" ", parts, 1, parts.Length - 1);
                    var mac = Address.GetFormattedMacAddress(parts[i]);
                    if (mac != null) {
                        return new Address(mac, name, string.Empty, string.Empty, null, null);
                    }
                } else {
                    var name = string.Join(" ", parts, 0, i);
                    var mac = Address.GetFormattedMacAddress(parts[i]);
                    var desc = string.Join(" ", parts, i + 1, parts.Length - i - 1);
                    if (mac != null) {
                        return new Address(mac, name, desc, null, null, null);
                    }
                }
            }
        }
        return null;
    }


    public bool HasChanged { get; internal set; }

    internal void MarkAsChanged() {
        HasChanged = true;
    }

}
