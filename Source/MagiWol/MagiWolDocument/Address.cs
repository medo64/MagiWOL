using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.Globalization;

namespace MagiWol.MagiWolDocument {

    internal class Address : ListViewItem {

        private static Font _fixedSizeFont;
        internal Document Parent { get; set; }

        public Address()
            : this("", "", "", "", null, null, false, false) {
        }

        public Address(string title, string mac, string secureOn, string notes, string broadcastHost, int? broadcastPort, bool isHostValid, bool isPortValid)
            : base() {
            if (_fixedSizeFont == null) {
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            } else if (_fixedSizeFont.Size != base.Font.Size) {
                _fixedSizeFont.Dispose();
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            }

            this.Title = title;
            this.Mac = mac;
            this.SecureOn = secureOn;
            this.Notes = notes;
            if ((broadcastHost != null) && (broadcastPort != null)) {
                this.BroadcastHost = broadcastHost;
                this.BroadcastPort = broadcastPort.Value;
                this.IsBroadcastHostValid = isHostValid;
                this.IsBroadcastPortValid = isPortValid;
            } else if (broadcastHost != null) {
                this.BroadcastHost = broadcastHost;
                this.BroadcastPort = Settings.DefaultBroadcastPort;
                this.IsBroadcastHostValid = true;
                this.IsBroadcastPortValid = false;
            } else if (broadcastPort != null) {
                this.BroadcastHost = Settings.DefaultBroadcastHost;
                this.BroadcastPort = broadcastPort.Value;
                this.IsBroadcastHostValid = false;
                this.IsBroadcastPortValid = true;
            } else {
                this.BroadcastHost = Settings.DefaultBroadcastHost;
                this.BroadcastPort = Settings.DefaultBroadcastPort;
                this.IsBroadcastHostValid = false;
                this.IsBroadcastPortValid = false;
            }


            base.Text = title;
            base.UseItemStyleForSubItems = false;
            RefreshColumns();
        }


        public new string Text {
            get {
                return this.Title;
            }
            set {
                this.Title = value;
            }
        }


        private new string Name {
            get {
                return null;
            }
            set { }
        }

        public string Title {
            get {
                return base.Text;
            }
            set {
                string oldValue = base.Text;
                base.Text = value;
                if ((this.Parent != null) && (oldValue != base.Text)) { this.Parent.HasChanged = true; }
            }
        }

        private string _mac;
        public string Mac {
            get {
                return this._mac;
            }
            set {
                string oldValue = this._mac;
                this._mac = GetProperMAC(value);
                RefreshColumns();
                if ((this.Parent != null) && (oldValue != this._mac)) { this.Parent.HasChanged = true; }
            }
        }

        private string _secureOn;
        public string SecureOn {
            get {
                return _secureOn;
            }
            set {
                string oldValue = this._secureOn;
                this._secureOn = GetProperMAC(value);
                RefreshColumns();
                if ((this.Parent != null) && (oldValue != this._secureOn)) { this.Parent.HasChanged = true; }
            }
        }

        private string _broadcastHost;
        public string BroadcastHost {
            get { return this._broadcastHost; }
            set {
                string oldValue = this._broadcastHost;
                this._broadcastHost = value;
                RefreshColumns();
                if ((this.Parent != null) && (oldValue != this._broadcastHost)) { this.Parent.HasChanged = true; }
            }
        }

        private int _broadcastPort;
        public int BroadcastPort {
            get { return this._broadcastPort; }
            set {
                int oldValue = this._broadcastPort;
                this._broadcastPort = value;
                RefreshColumns();
                if ((this.Parent != null) && (oldValue != this._broadcastPort)) { this.Parent.HasChanged = true; }
            }
        }

        private string _notes;
        public string Notes {
            get { return this._notes; }
            set {
                string oldValue = this._notes;
                this._notes = value;
                RefreshColumns();
                if ((this.Parent != null) && (oldValue != this._notes)) { this.Parent.HasChanged = true; }
            }
        }

        public bool IsBroadcastHostValid { get; set; }
        public bool IsBroadcastPortValid { get; set; }


        public override bool Equals(object obj) {
            var other = obj as Address;
            if ((other != null) && (string.Compare(this.Mac, other.Mac, StringComparison.OrdinalIgnoreCase) == 0)) { return true; }
            return false;
        }

        public override int GetHashCode() {
            return this.Mac.GetHashCode();
        }

        public override string ToString() {
            if (string.IsNullOrEmpty(this.Title)) {
                return string.Format("{0}", this.Mac);
            } else {
                return string.Format("{0} ({1})", this.Title, this.Mac);
            }
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

        internal void RefreshColumns() {
            for (int i = base.SubItems.Count - 1; i >= 1; i--) {
                base.SubItems.RemoveAt(i);
            }
            int index = 1;
            if (Settings.ShowColumnMac) {
                base.SubItems.Add(this.Mac); //mac
                base.SubItems[index].Font = _fixedSizeFont;
                index += 1;
            }
            if (Settings.ShowColumnSecureOn) {
                base.SubItems.Add(this.SecureOn); //secureon
                base.SubItems[index].Font = _fixedSizeFont;
                index += 1;
            }
            if (Settings.ShowColumnBroadcastHost) {
                base.SubItems.Add(this.IsBroadcastHostValid ? this.BroadcastHost : Settings.DefaultBroadcastHost);
                base.SubItems[index].ForeColor = this.IsBroadcastHostValid ? SystemColors.WindowText : SystemColors.GrayText;
                index += 1;
            }
            if (Settings.ShowColumnBroadcastPort) {
                base.SubItems.Add(this.IsBroadcastPortValid ? this.BroadcastPort.ToString(CultureInfo.InvariantCulture) : Settings.DefaultBroadcastPort.ToString(CultureInfo.InvariantCulture));
                base.SubItems[index].ForeColor = this.IsBroadcastPortValid ? SystemColors.WindowText : SystemColors.GrayText;
                index += 1;
            }
            if (Settings.ShowColumnNotes) {
                base.SubItems.Add(this.Notes);
                index += 1;
            }
        }

    }
}
