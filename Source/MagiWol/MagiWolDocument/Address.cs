using System;

namespace MagiWol.MagiWolDocument {

    internal class Address {

        internal Document Parent { get; set; }

        public Address()
            : this("", "", "", "", null, null, false, false) {
        }

        public Address(string title, string mac, string secureOn, string notes, string broadcastHost, int? broadcastPort, bool isHostValid, bool isPortValid) {
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
                this.BroadcastPort = Settings.BroadcastPort;
                this.IsBroadcastHostValid = true;
                this.IsBroadcastPortValid = false;
            } else if (broadcastPort != null) {
                this.BroadcastHost = Settings.BroadcastHost;
                this.BroadcastPort = broadcastPort.Value;
                this.IsBroadcastHostValid = false;
                this.IsBroadcastPortValid = true;
            } else {
                this.BroadcastHost = Settings.BroadcastHost;
                this.BroadcastPort = Settings.BroadcastPort;
                this.IsBroadcastHostValid = false;
                this.IsBroadcastPortValid = false;
            }
        }


        private string _title;
        public string Title {
            get { return this._title; }
            set {
                var oldValue = this._title;
                this._title = value;
                if ((this.Parent != null) && (oldValue != this._title)) { this.Parent.HasChanged = true; }
            }
        }

        private string _mac;
        public string Mac {
            get { return this._mac; }
            set {
                var oldValue = this._mac;
                this._mac = GetProperMAC(value);
                if ((this.Parent != null) && (oldValue != this._mac)) { this.Parent.HasChanged = true; }
            }
        }

        private string _secureOn;
        public string SecureOn {
            get { return _secureOn; }
            set {
                var oldValue = this._secureOn;
                this._secureOn = GetProperMAC(value);
                if ((this.Parent != null) && (oldValue != this._secureOn)) { this.Parent.HasChanged = true; }
            }
        }

        private string _broadcastHost;
        public string BroadcastHost {
            get { return this._broadcastHost; }
            set {
                var oldValue = this._broadcastHost;
                this._broadcastHost = value;
                if ((this.Parent != null) && (oldValue != this._broadcastHost)) { this.Parent.HasChanged = true; }
            }
        }

        private int _broadcastPort;
        public int BroadcastPort {
            get { return this._broadcastPort; }
            set {
                var oldValue = this._broadcastPort;
                if ((value >= 0) && (value <= 65535)) {
                    this._broadcastPort = value;
                }
                if ((this.Parent != null) && (oldValue != this._broadcastPort)) { this.Parent.HasChanged = true; }
            }
        }

        private string _notes;
        public string Notes {
            get { return this._notes; }
            set {
                var oldValue = this._notes;
                this._notes = value;
                if ((this.Parent != null) && (oldValue != this._notes)) { this.Parent.HasChanged = true; }
            }
        }

        private bool _isBroadcastHostValid;
        public bool IsBroadcastHostValid {
            get { return _isBroadcastHostValid; }
            set {
                var oldValue = this._isBroadcastHostValid;
                this._isBroadcastHostValid = value;
                if ((this.Parent != null) && (oldValue != this._isBroadcastHostValid)) { this.Parent.HasChanged = true; }
            }
        }

        private bool _isBroadcastPortValid;
        public bool IsBroadcastPortValid {
            get { return _isBroadcastPortValid; }
            set {
                var oldValue = this._isBroadcastPortValid;
                this._isBroadcastPortValid = value;
                if ((this.Parent != null) && (oldValue != this._isBroadcastPortValid)) { this.Parent.HasChanged = true; }
            }
        }


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

    }
}
