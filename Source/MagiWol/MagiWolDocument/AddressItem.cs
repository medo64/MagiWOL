using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MagiWol.MagiWolDocument {

    internal class AddressItem : ListViewItem {

        private static Font _fixedSizeFont;

        public AddressItem()
            : this(new Address()) {
        }

        public AddressItem(string title, string mac, string secureOn, string notes, string broadcastHost, int? broadcastPort, bool isHostValid, bool isPortValid)
            : this(new Address(title, mac, secureOn, notes, broadcastHost, broadcastPort, isHostValid, isPortValid)) {
        }

        public AddressItem(Address address)
            : base() {
            if (_fixedSizeFont == null) {
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            } else if (_fixedSizeFont.Size != base.Font.Size) {
                _fixedSizeFont.Dispose();
                _fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            }

            this.Address = address;

            base.Text = address.Title;
            base.UseItemStyleForSubItems = false;
            RefreshColumns();
        }


        public Address Address { get; }


        public new string Text {
            get { return this.Title; }
            set { this.Title = value; }
        }


        private new string Name {
            get { return null; }
            set { }
        }

        public string Title {
            get { return base.Text; }
            set {
                this.Address.Title = value;
                RefreshColumns();
            }
        }

        public string Mac {
            get { return this.Address.Mac; }
            set {
                this.Address.Mac = value;
                RefreshColumns();
            }
        }

        public string SecureOn {
            get { return this.Address.SecureOn; }
            set {
                this.Address.SecureOn = value;
                RefreshColumns();
            }
        }

        public string BroadcastHost {
            get { return this.Address.BroadcastHost; }
            set {
                this.Address.BroadcastHost = value;
                RefreshColumns();
            }
        }

        public int BroadcastPort {
            get { return this.Address.BroadcastPort; }
            set {
                this.Address.BroadcastPort = value;
                RefreshColumns();
            }
        }

        public string Notes {
            get { return this.Address.Notes; }
            set {
                this.Address.Notes = value;
                RefreshColumns();
            }
        }

        public bool IsBroadcastHostValid {
            get { return this.Address.IsBroadcastHostValid; }
            set { this.Address.IsBroadcastHostValid = value; }
        }

        public bool IsBroadcastPortValid {
            get { return this.Address.IsBroadcastPortValid; }
            set { this.Address.IsBroadcastPortValid = value; }
        }


        public override bool Equals(object obj) {
            var otherItem = obj as AddressItem;
            if ((otherItem != null) && (string.Compare(this.Mac, otherItem.Mac, StringComparison.OrdinalIgnoreCase) == 0)) { return true; }

            var otherAddress = obj as Address;
            if ((otherAddress != null) && (string.Compare(this.Mac, otherItem.Mac, StringComparison.OrdinalIgnoreCase) == 0)) { return true; }

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
                base.SubItems.Add(this.IsBroadcastHostValid ? this.BroadcastHost : Settings.BroadcastHost);
                base.SubItems[index].ForeColor = this.IsBroadcastHostValid ? SystemColors.WindowText : SystemColors.GrayText;
                index += 1;
            }
            if (Settings.ShowColumnBroadcastPort) {
                base.SubItems.Add(this.IsBroadcastPortValid ? this.BroadcastPort.ToString(CultureInfo.InvariantCulture) : Settings.BroadcastPort.ToString(CultureInfo.InvariantCulture));
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
