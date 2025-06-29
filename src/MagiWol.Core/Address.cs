namespace MagiWol;

using System;
using System.Text;

public class Address {

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="mac">MAC address</param>
    public Address(string mac)
        : this(
              mac,
              title: "",
              notes: "",
              secureOn: null,
              broadcastHost: null,
              broadcastPort: null
        ) {
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="title">Title of the entry.</param>
    /// <param name="mac">MAC address.</param>
    /// <param name="secureOn">SecureOn password.</param>
    public Address(string mac, string title, string notes, string? secureOn, string? broadcastHost, int? broadcastPort) {
        Mac = mac ?? "";
        Title = title ?? "";
        Notes = notes ?? "";
        SecureOn = secureOn ?? "";
        if (!string.IsNullOrEmpty(broadcastHost) && (broadcastPort != null)) {
            BroadcastHost = broadcastHost;
            BroadcastPort = broadcastPort.Value;
        } else if (!string.IsNullOrEmpty(broadcastHost)) {
            BroadcastHost = broadcastHost;
        } else if (broadcastPort != null) {
            BroadcastHost = DefaultBroadcastHost;
            BroadcastPort = broadcastPort.Value;
        } else {
            BroadcastHost = DefaultBroadcastHost;
            BroadcastPort = DefaultBroadcastPort;
        }
    }


    private string _mac = string.Empty;
    /// <summary>
    /// Gets or sets the MAC address.
    /// </summary>
    public string Mac {
        get { return this._mac; }
        set {
            var oldValue = this._mac;
            this._mac = GetFormattedMacAddress(value) ?? throw new ArgumentOutOfRangeException(nameof(value), "Cannot parse MAC address.");
            if ((this.Parent != null) && (oldValue != this._mac)) { this.Parent.HasChanged = true; }
        }
    }

    private string _title = string.Empty;
    /// <summary>
    /// Gets or sets the title of the entry.
    /// </summary>
    public string Title {
        get { return this._title; }
        set {
            var oldValue = this._title;
            _title = value ?? string.Empty;
            if ((this.Parent != null) && (oldValue != this._title)) { this.Parent.HasChanged = true; }
        }
    }

    private string _notes = string.Empty;
    public string Notes {
        get { return this._notes; }
        set {
            var oldValue = this._notes;
            _notes = value ?? string.Empty;
            if ((this.Parent != null) && (oldValue != this._notes)) { this.Parent.HasChanged = true; }
        }
    }

    private string? _secureOn;
    /// <summary>
    /// Gets or sets the SecureOn password.
    /// </summary>
    public string? SecureOn {
        get { return _secureOn; }
        set {
            var oldValue = this._secureOn;
            string? newValue;
            if (string.IsNullOrEmpty(value)) {
                newValue = null;
            } else {
                newValue = GetFormattedMacAddress(value);
                if (newValue == null) { throw new ArgumentOutOfRangeException(nameof(value), "Cannot parse SecureOn MAC address."); }
            }
            _secureOn = newValue;
            if ((this.Parent != null) && (oldValue != newValue)) { Parent.HasChanged = true; }
        }
    }

    private string? _broadcastHost;
    /// <summary>
    /// Gets or sets the broadcast host.
    /// </summary>
    public string? BroadcastHost {
        get { return _broadcastHost; }
        set {
            var oldValue = _broadcastHost;
            this._broadcastHost = string.IsNullOrEmpty(value) ? null : value;
            if ((this.Parent != null) && (oldValue != this._broadcastHost)) { this.Parent.HasChanged = true; }
        }
    }

    /// <summary>
    /// Gets the broadcast host or default value.
    /// </summary>
    public string BroadcastHostOrDefault {
        get { return BroadcastHost ?? DefaultBroadcastHost; }
    }

    /// <summary>
    /// Gets or sets whether the broadcast host is valid.
    /// </summary>
    public bool IsBroadcastHostValid {
        get { return !string.IsNullOrEmpty(this._broadcastHost); }
    }

    private int? _broadcastPort;
    /// <summary>
    /// Gets or sets the broadcast port.
    /// </summary>
    public int? BroadcastPort {
        get { return _broadcastPort; }
        set {
            var oldValue = _broadcastPort;
            if ((value is not null) && (value is < 0 or > 65535)) { throw new ArgumentOutOfRangeException(nameof(value), "Cannot parse port."); }
            this._broadcastPort = value;
            if ((this.Parent != null) && (oldValue != this._broadcastPort)) { this.Parent.HasChanged = true; }
        }
    }

    /// <summary>
    /// Gets the broadcast port or default value.
    /// </summary>
    public int BroadcastPortOrDefault {
        get { return BroadcastPort ?? DefaultBroadcastPort; }
    }

    /// <summary>
    /// Gets if broadcast port is valid.
    /// </summary>
    public bool IsBroadcastPortValid {
        get { return BroadcastPort != null; }
    }


    /// <summary>
    /// Returns true if instances are equal.
    /// </summary>
    /// <param name="obj">Other instance.</param>
    public override bool Equals(object? obj) {
        var other = obj as Address;
        if ((other != null) && (string.Compare(this.Mac, other.Mac, StringComparison.OrdinalIgnoreCase) == 0)) { return true; }
        return false;
    }

    /// <summary>
    /// Gets hash code of the instance.
    /// </summary>
    public override int GetHashCode() {
        return Mac.GetHashCode();
    }

    /// <summary>
    /// Gets the string representation of the instance.
    /// </summary>
    public override string ToString() {
        if (string.IsNullOrEmpty(this.Title)) {
            return string.Format("{0}", this.Mac);
        } else {
            return string.Format("{0} ({1})", this.Title, this.Mac);
        }
    }


    /// <summary>
    /// Returns properly formatted MAC address if text can be parsed, otherwise returns null.
    /// </summary>
    /// <param name="text">Text.</param>
    public static string? GetFormattedMacAddress(string text) {
        string addressText = System.Text.RegularExpressions.Regex.Replace(text.ToUpper(), "[^0-9A-F]", ":") + ":";
        var newText = new StringBuilder();

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
                case '-':
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
                        if (newText.Length > 0) { newText.Append(':'); }
                        newText.Append(tmpPart);
                    }
                    break;

                case ' ':
                    break;

                default:
                    return null;
            }
        }  // for

        if (newText.ToString().Length == 2 * 6 + 5) {
            return newText.ToString();
        } else {
            return null;
        }

    }


    /// <summary>
    /// Gets default broadcast host.
    /// </summary>
    public static string DefaultBroadcastHost { get; } = "255.255.255.255";  // IPv6 always uses IPAddress.IPv6Any

    /// <summary>
    /// Gets default broadcast port.
    /// </summary>
    public static int DefaultBroadcastPort { get; } = 9;


    /// <summary>
    /// Gets or sets the parent document.
    /// Used for change tracking.
    /// </summary>
    internal Document? Parent { get; set; }

}
