using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MagiWol.MagiWolDocument {

    internal class DocumentEx : Document {

        public DocumentEx() : base() { }


        public static new DocumentEx Open(string fileName) {
            var doc = new DocumentEx();
            Load(fileName, doc);
            doc.HasChanged = false;
            return doc;
        }


        public new void Save() {
            base.Save();
            this.HasChanged = false;
        }

        public new void Save(string fileName) {
            base.Save(fileName);
            this.HasChanged = false;
        }


        public void AddAddress(AddressItem item, bool allowDuplicates) {
            base.AddAddress(item.Address, allowDuplicates);
        }

        public void RemoveAddress(AddressItem item) {
            base.RemoveAddress(item.Address);
        }

        public bool HasAddress(AddressItem item) {
            return base.HasAddress(item.Address);
        }


        public void FillListView(ListView list, IEnumerable<AddressItem> selectedItems) {
            var selectionList = new List<AddressItem>();
            if (selectedItems != null) {
                foreach (var iItem in selectedItems) {
                    selectionList.Add(iItem);
                }
            }

            MagiWolDocument.AddressItem potentialFocus = null;
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
                var address = _addresses[i];
                var lvi = new AddressItem(_addresses[i]);
                lvi.Selected = selectionList.Contains(lvi);
                lvi.RefreshColumns();
                list.Items.Add(lvi);
                if (selectionList.Contains(lvi)) {
                    potentialFocus = lvi;
                }
            }

            list.EndUpdate();

            if (potentialFocus != null) { list.FocusedItem = potentialFocus; }
        }


        public void Cut(IEnumerable<AddressItem> addressItems) {
            bool isChanged = false;
            Copy(addressItems);

            var addresses = new List<Address>();
            foreach (var item in addressItems) {
                addresses.Add(item.Address);
            }

            foreach (var iAddress in addresses) {
                if (this._addresses.Contains(iAddress)) {
                    this._addresses.Remove(iAddress);
                    isChanged = true;
                }
            }
            if (isChanged) { this.HasChanged = true; }
        }

        public void Copy(IEnumerable<AddressItem> addressItems) {
            var addresses = new List<Address>();
            foreach (var item in addressItems) {
                addresses.Add(item.Address);
            }

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

        public IEnumerable<AddressItem> Paste() {
            var pastedAddressItems = new List<AddressItem>();

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
                return pastedAddressItems;
            }

            bool isChanged = false;
            if (xmlData != null) {
                foreach (var iAddress in GetAddressesFromXml(this, xmlData)) {
                    if (!this.HasAddress(iAddress)) {
                        this.AddAddress(iAddress, false);
                        pastedAddressItems.Add(new AddressItem(iAddress));
                        isChanged = true;
                    } else {
                        foreach (var feAddress in this._addresses) {
                            if (feAddress.Equals(iAddress)) {
                                pastedAddressItems.Add(new AddressItem(feAddress));
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
                            pastedAddressItems.Add(new AddressItem(iAddress));
                            isChanged = true;
                        } else {
                            foreach (var feAddress in this._addresses) {
                                if (feAddress.Equals(iAddress)) {
                                    pastedAddressItems.Add(new AddressItem(feAddress));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (isChanged) { this.HasChanged = true; }


            return pastedAddressItems.AsReadOnly();
        }

        public bool CanPaste() {
            try {
                return Clipboard.ContainsData("MagiWOL") || Clipboard.ContainsText();
            } catch (ExternalException) {
                return false;
            }
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

    }
}
