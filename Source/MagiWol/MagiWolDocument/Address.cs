using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Net;

namespace MagiWol.MagiWolDocument {

    internal class Address : ListViewItem {

        private static Font _fixedSizeFont;
        internal Document Parent { get; set; }

        public Address()
            : this("", "", "", "", null, false, false) {
        }

        public Address(string title, string mac, string secureOn, string notes, IPEndPoint packetEndPoint, bool isAddressValid, bool isPortValid)
            : base() {
            if (_fixedSizeFont == null) {
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            } else if (_fixedSizeFont.Size != base.Font.Size) {
                _fixedSizeFont.Dispose();
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            }

            base.Text = title; //title
            base.SubItems.Add(mac); //mac
            base.SubItems.Add(secureOn); //secureon

            base.UseItemStyleForSubItems = false;
            base.SubItems[1].Font = _fixedSizeFont;
            base.SubItems[2].Font = _fixedSizeFont;

            this.Title = title;
            this.Mac = mac;
            this.SecureOn = secureOn;
            this.Notes = notes;
            if (packetEndPoint != null) {
                this.PacketEndPoint = packetEndPoint;
                this.IsPacketEndPointAddressValid = isAddressValid;
                this.IsPacketEndPointPortValid = isPortValid;
            } else {
                this.PacketEndPoint = Settings.DefaultPacketEndPoint;
                this.IsPacketEndPointAddressValid = false;
                this.IsPacketEndPointPortValid = false;
            }
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
                base.SubItems[1].Text = this._mac;
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
                base.SubItems[2].Text = this._secureOn;
                if ((this.Parent != null) && (oldValue != this._secureOn)) { this.Parent.HasChanged = true; }
            }
        }

        private string _notes;
        public string Notes {
            get { return this._notes; }
            set {
                string oldValue = this._notes;
                this._notes = value;
                if ((this.Parent != null) && (oldValue != this._notes)) { this.Parent.HasChanged = true; }
            }
        }

        private IPEndPoint _packetEndPoint;
        public IPEndPoint PacketEndPoint {
            get { return this._packetEndPoint; }
            set {
                IPEndPoint oldValue = this._packetEndPoint;
                this._packetEndPoint = value;
                if ((this.Parent != null) && (oldValue != this._packetEndPoint)) { this.Parent.HasChanged = true; }
            }
        }

        public bool IsPacketEndPointAddressValid {
            get;
            set;
        }

        public bool IsPacketEndPointPortValid {
            get;
            set;
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
