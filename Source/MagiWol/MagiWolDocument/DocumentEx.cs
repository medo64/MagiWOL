using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MagiWol.MagiWolDocument {

    internal class DocumentEx : Document {

        public DocumentEx() : base() { }


        public static new DocumentEx Open(string fileName) {
            var doc = new DocumentEx();
            Load(fileName, doc);
            return doc;
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

    }
}
