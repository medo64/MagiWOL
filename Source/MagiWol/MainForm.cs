using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Runtime.InteropServices;

namespace MagiWol {

    internal partial class MainForm : Form {

        private MagiWolDocument.Document _document;
        private Medo.Configuration.RecentFiles _recent;


        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            Medo.Windows.Forms.TaskbarProgress.DefaultOwner = this;
            Medo.Windows.Forms.TaskbarProgress.DoNotThrowNotImplementedException = true;

            float dpiRatioX, dpiRatioY;
            using (var g = this.CreateGraphics()) {
                var dpiX = g.DpiX;
                var dpiY = g.DpiY;
                dpiRatioX = (float)Math.Round(dpiX / 96, 2);
                dpiRatioY = (float)Math.Round(dpiY / 96, 2);
            }
            mnu.ImageScalingSize = new Size((int)(16 * dpiRatioX), (int)(16 * dpiRatioY));
            mnu.Scale(new SizeF(dpiRatioX, dpiRatioY));
            mnx.ImageScalingSize = new Size((int)(16 * dpiRatioX), (int)(16 * dpiRatioY));
            mnx.Scale(new SizeF(dpiRatioX, dpiRatioY));

            if (Medo.Configuration.Settings.NoRegistryWrites) {
                mnuFileRecent.Enabled = false;
                mnxImport0.Visible = false;
            }

            this._document = new MagiWolDocument.Document();
            this._recent = new Medo.Configuration.RecentFiles();

            list.ListViewItemSorter = _listColumnSorter;
            this._listColumnSorter.SortColumn = 0;
            this._listColumnSorter.Order = SortOrder.Ascending;
            list.Sort();
        }

        private bool _suppressMenuKey;

        private void MainForm_Deactivate(object sender, EventArgs e) {
            mnu.Visible = Settings.Runtime.ShowMenu;
        }

        private void MainForm_Load(object sender, EventArgs e) {
            mnu.Visible = Settings.Runtime.ShowMenu;
            mnxImport.Visible = !MainForm.IsRunningOnMono;
            mnxImport0.Visible = !MainForm.IsRunningOnMono;
            Medo.Windows.Forms.State.Load(this, list);
            OpenFromCommandLineArgs();
            UpdateData(null);
        }

        private static ProcessStartInfo GetProcessStartInfo(string arguments) {
            var startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = Process.GetCurrentProcess().StartInfo.CreateNoWindow;
            startInfo.FileName = Assembly.GetExecutingAssembly().Location;
            startInfo.Arguments = arguments;
            startInfo.WorkingDirectory = System.Environment.CurrentDirectory;
            return startInfo;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e) {
            Debug.WriteLine("MainForm_KeyDown: " + e.KeyData.ToString());

            if (!Settings.Runtime.ShowMenu) {
                switch (e.KeyData) {

                    case (Keys.Alt | Keys.F):
                        mnu.Visible = true;
                        mnuFile.ShowDropDown();
                        this._suppressMenuKey = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    case (Keys.Alt | Keys.E):
                        mnu.Visible = true;
                        mnuEdit.ShowDropDown();
                        this._suppressMenuKey = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    case (Keys.Alt | Keys.A):
                        mnu.Visible = true;
                        mnuAction.ShowDropDown();
                        this._suppressMenuKey = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    case (Keys.Alt | Keys.T):
                        mnu.Visible = true;
                        mnuTools.ShowDropDown();
                        this._suppressMenuKey = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    case (Keys.Alt | Keys.H):
                        mnu.Visible = true;
                        mnuHelp.ShowDropDown();
                        this._suppressMenuKey = true;
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    case (Keys.Alt | Keys.Menu):
                        break;

                    default:
                        if (e.Alt) { this._suppressMenuKey = true; }
                        break;

                }
            }//if(!ShowMenu)
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e) {
            Debug.WriteLine("MainForm_KeyUp: " + e.KeyData.ToString());

            if (!Settings.Runtime.ShowMenu) {
                if (e.KeyData == Keys.Menu) {
                    if (this._suppressMenuKey) {
                        this._suppressMenuKey = false;
                    } else {
                        if (!mnu.Visible) {
                            mnu.Visible = true;
                            mnu.Select();
                            mnuFile.Select();
                        } else {
                            mnu.Visible = false;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        private void mnu_Leave(object sender, EventArgs e) {
            if (!Settings.Runtime.ShowMenu) {
                if (mnu.Visible) { mnu.Visible = false; }
            }
        }
        private void mnu_MenuDeactivate(object sender, EventArgs e) {
            mnu_Leave(null, null);
        }


        private void mnu_VisibleChanged(object sender, EventArgs e) {
            MainForm_Resize(null, null);
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            list.Left = this.ClientRectangle.Left + 3;
            if (mnu.Visible) {
                list.Top = this.ClientRectangle.Top + mnu.Height + mnx.Height + 3;
            } else {
                list.Top = this.ClientRectangle.Top + mnx.Height + 3;
            }
            list.Width = this.ClientRectangle.Width - 6;
            list.Height = this.ClientRectangle.Height - list.Top - 3;
        }



        private void UpdateData(IEnumerable<MagiWolDocument.Address> selection) {
            if (this._document == null) { return; }

            this._document.FillListView(list, selection);

            this.Text = this._document.FileTitle + " - " + Medo.Reflection.EntryAssembly.Title;

            mnuFileRecent.DropDownItems.Clear();
            for (int i = mnxFileOpen.DropDownItems.Count - 1; i >= 2; i--) {
                mnxFileOpen.DropDownItems.RemoveAt(i);
            }
            foreach (var iRecentFile in this._recent.AsReadOnly()) {
                var item = new ToolStripMenuItem(iRecentFile.Title);
                item.Tag = iRecentFile;
                item.Click += new EventHandler(recentItem_Click);
                mnuFileRecent.DropDownItems.Add(item);

                var item2 = new ToolStripMenuItem(iRecentFile.Title);
                item2.Tag = iRecentFile;
                item2.Click += new EventHandler(recentItem_Click);
                mnxFileOpen.DropDownItems.Add(item2);
            }
            if (mnuFileRecent.DropDownItems.Count == 0) { mnxFileOpen.DropDownButtonWidth = 0; }
        }

        void recentItem_Click(object sender, EventArgs e) {
            var item = (ToolStripMenuItem)sender;
            var recentItem = (Medo.Configuration.RecentFile)item.Tag;
            if (ProceedWithNewDocument()) {
                try {
                    _document = MagiWolDocument.Document.Open(recentItem.FileName);
                    _recent.Push(recentItem.FileName);
                } catch (Exception ex) {
                    var exFile = new FileInfo(recentItem.FileName);
                    Medo.MessageBox.ShowError(this, string.Format("Cannot open \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                }
                UpdateData(null);
            }
        }


        private bool ProceedWithNewDocument() {
            if (!_document.HasChanged) { return true; }

            switch (Medo.MessageBox.ShowQuestion(this, "Current file is not saved. Save changes now?", MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)) {
                case DialogResult.Yes:
                    mnuFileSave_Click(null, null);
                    break;

                case DialogResult.No:
                    return true;

                case DialogResult.Cancel:
                    return false;
            }

            return !_document.HasChanged;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!ProceedWithNewDocument()) {
                e.Cancel = true;
                return;
            }
            Medo.Windows.Forms.State.Save(this, list);
            this._recent.Save();
        }


        #region Menu: File

        private void mnuFileNew_Click(object sender, EventArgs e) {
            if (ProceedWithNewDocument()) {
                _document = new MagiWolDocument.Document();
                UpdateData(null);
            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e) {
            if (ProceedWithNewDocument()) {
                using (var dialog = new OpenFileDialog()) {
                    dialog.AddExtension = true;
                    dialog.CheckFileExists = true;
                    dialog.CheckPathExists = true;
                    dialog.DefaultExt = "magiwol";
                    dialog.Filter = "MagiWOL files (*.magiwol)|*.magiwol|All files (*.*)|*.*";
                    dialog.Multiselect = false;
                    dialog.ShowReadOnly = false;
                    if (dialog.ShowDialog(this) == DialogResult.OK) {
                        try {
                            _document = MagiWolDocument.Document.Open(dialog.FileName);
                            _recent.Push(dialog.FileName);
                        } catch (Exception ex) {
                            var exFile = new FileInfo(dialog.FileName);
                            Medo.MessageBox.ShowError(this, string.Format("Cannot open \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                        }
                        UpdateData(null);
                    }
                }
            }
        }

        private void mnuFileImportFromNetwork_Click_1(object sender, EventArgs e) {
            if (MainForm.IsRunningOnMono) {
                Medo.MessageBox.ShowInformation(this, "This operation is not supported on Mono.");
                return;
            }

            using (var form = new ImportFromNetworkForm()) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    foreach (var iAddress in form.ImportedAddresses) {
                        if (this._document.HasAddress(iAddress)) {
                            if (Medo.MessageBox.ShowQuestion(this, string.Format("MAC address \"{0}\" is already in list.\nDo you wish to add it anyhow?", iAddress.Mac), MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                                this._document.AddAddress(iAddress, false);
                            }
                        } else {
                            this._document.AddAddress(iAddress, false);
                        }
                    }
                    UpdateData(form.ImportedAddresses);
                }
            }
        }

        private void mnuFileSave_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(this._document.FileName) || !System.IO.File.Exists(this._document.FileName)) {
                mnuFileSaveAs_Click(null, null);
            } else {
                try {
                    this._document.Save();
                } catch (Exception ex) {
                    var exFile = new FileInfo(this._document.FileName);
                    Medo.MessageBox.ShowError(this, string.Format("Cannot save \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                }
            }
            if (this._document.FileName != null) {
                _recent.Push(this._document.FileName);
            }
            UpdateData(null);
        }

        private void mnuFileSaveAs_Click(object sender, EventArgs e) {
            using (var dialog = new SaveFileDialog()) {
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = "magiwol";
                dialog.Filter = "MagiWOL files (*.magiwol)|*.magiwol|All files (*.*)|*.*";
                dialog.OverwritePrompt = true;
                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    try {
                        this._document.Save(dialog.FileName);
                        if (this._document.FileName != null) {
                            _recent.Push(this._document.FileName);
                        }
                    } catch (Exception ex) {
                        var exFile = new FileInfo(dialog.FileName);
                        Medo.MessageBox.ShowError(this, string.Format("Cannot save \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                    }
                }
            }
            UpdateData(null);
        }

        private void mnuFileExit_Click(object sender, EventArgs e) {
            this.Close();
        }

        #endregion


        #region Menu: Edit

        private void mnuEdit_DropDownOpening(object sender, EventArgs e) {
            mnuEditCut.Enabled = (list.SelectedItems.Count > 0);
            mnuEditCopy.Enabled = (list.SelectedItems.Count > 0);
            mnuEditPaste.Enabled = (Clipboard.ContainsData("MagiWOL") || Clipboard.ContainsData(DataFormats.UnicodeText) || Clipboard.ContainsData(DataFormats.Text));
            mnuEditChange.Enabled = (list.FocusedItem != null);
            mnuEditRemove.Enabled = (list.SelectedItems.Count > 0);
        }


        private void mnuEditCut_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addrs.Add(iAddress);
                }
                try {
                    this._document.Cut(addrs);
                } catch (ExternalException ex) {
                    Medo.MessageBox.ShowError(this, "Cut error.\n\n" + ex.Message);
                }
                UpdateData(null);
            }
        }

        private void mnuEditCopy_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addrs.Add(iAddress);
                }
                try {
                    this._document.Copy(addrs);
                } catch (ExternalException ex) {
                    Medo.MessageBox.ShowError(this, "Copy error.\n\n" + ex.Message);
                }
                UpdateData(addrs);
            }
        }

        private void mnuEditPaste_Click(object sender, EventArgs e) {
            try {
                var pasted = this._document.Paste();
                UpdateData(pasted);
            } catch (ExternalException ex) {
                Medo.MessageBox.ShowError(this, "Paste error.\n\n" + ex.Message);
            }
        }

        private void mnuEditSelectAll_Click(object sender, EventArgs e) {
            foreach (var iItem in list.Items) {
                ((MagiWolDocument.Address)iItem).Selected = true;
            }
        }

        private void mnuEditAdd_Click(object sender, EventArgs e) {
            using (var frm = new DetailForm(null)) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    if (this._document.HasAddress(frm.Destination)) {
                        if (Medo.MessageBox.ShowQuestion(this, string.Format("MAC address \"{0}\" is already in list.\nDo you wish to add it anyhow?", frm.Destination.Mac), MessageBoxButtons.YesNo) == DialogResult.Yes) {
                            this._document.AddAddress(frm.Destination, true);
                        }
                    } else {
                        this._document.AddAddress(frm.Destination, false);
                    }
                    UpdateData(new MagiWolDocument.Address[] { frm.Destination });
                }
            }
        }

        private void mnuEditChange_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count == 1) {
                MagiWolDocument.Address currDestination = (MagiWolDocument.Address)list.SelectedItems[0];
                using (var frm = new DetailForm(currDestination)) {
                    if (frm.ShowDialog(this) == DialogResult.OK) {
                        currDestination = frm.Destination;
                        UpdateData(new MagiWolDocument.Address[] { currDestination });
                    }
                }
            }
        }

        private void mnuEditRemove_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    this._document.RemoveAddress(iAddress);
                }
                UpdateData(null);
            }
        }

        #endregion


        #region Menu: Action

        private void mnuActionWake_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addresses = new List<MagiWolDocument.Address>();
                foreach (ListViewItem iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addresses.Add(iAddress);
                }
                using (var form = new WakeForm(addresses.AsReadOnly())) {
                    form.ShowDialog(this);
                }
            }
        }

        private void mnuActionWakeAll_Click(object sender, EventArgs e) {
            if (list.Items.Count > 0) {
                var addresses = new List<MagiWolDocument.Address>();
                foreach (ListViewItem iItem in list.Items) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addresses.Add(iAddress);
                }
                using (var form = new WakeForm(addresses.AsReadOnly())) {
                    form.ShowDialog(this);
                }
            }
        }

        private void mnuActionQuickWake_Click(object sender, EventArgs e) {
            using (var form = new QuickWakeForm()) {
                form.ShowDialog(this);
            }
        }

        #endregion

        #region Menu: Tools

        private void mnuToolsOptions_Click(object sender, EventArgs e) {
            using (var form = new SettingsForm()) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    mnu.Visible = Settings.Runtime.ShowMenu;
                    mnuToolsRefresh_Click(null, null);
                }
            }
        }

        #endregion

        #region Menu: Help

        private void mnuHelpReportABug_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("http://jmedved.com/ErrorReport/"));
        }

        private void mnuHelpAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("http://www.jmedved.com/magiwol/"));
        }

        #endregion


        #region ContextMenu: List

        private void mnxList_Opening(object sender, CancelEventArgs e) {
            mnxListCut.Enabled = (list.SelectedItems.Count > 0);
            mnxListCopy.Enabled = (list.SelectedItems.Count > 0);
            mnxListPaste.Enabled = this._document.CanPaste();
            mnxListEditSelectAll.Enabled = (list.SelectedItems.Count > 0);
            mnxListAdd.Enabled = true;
            mnxListChange.Enabled = (list.SelectedItems.Count == 1);
            mnxListRemove.Enabled = (list.SelectedItems.Count > 0);
            mnxListActionWake.Enabled = (list.SelectedItems.Count > 0);
            mnxListQuickWake.Enabled = (list.SelectedItems.Count == 1);
        }

        private void mnxListQuickWake_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count == 1) {
                var address = (MagiWolDocument.Address)list.SelectedItems[0];
                using (var form = new QuickWakeForm(address.Mac)) {
                    form.ShowDialog(this);
                }
            }
        }

        #endregion


        private void list_AfterLabelEdit(object sender, LabelEditEventArgs e) {
            if ((e != null) && (e.Label != null) && (e.Label.Length > 0)) {
                var iAddress = ((MagiWolDocument.Address)list.Items[e.Item]);
                iAddress.Title = e.Label;
                this.list.Enabled = false;
                tmrReSort.Enabled = true;
                this._document.MarkAsChanged();
                UpdateData(new MagiWolDocument.Address[] { iAddress });
            } else {
                e.CancelEdit = true;
            }
            mnuEditRemove.ShortcutKeys = Keys.Delete;
        }

        private void list_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.F2) {
                if (list.SelectedItems.Count == 1) {
                    if (list.FocusedItem != null) {
                        mnuEditRemove.ShortcutKeys = Keys.None;
                        list.FocusedItem.BeginEdit();
                    }
                }
                e.SuppressKeyPress = true;
            }
        }

        private void list_ItemActivate(object sender, EventArgs e) {
            var addresses = new List<MagiWolDocument.Address>();
            foreach (ListViewItem iItem in list.SelectedItems) {
                var iAddress = (MagiWolDocument.Address)iItem;
                addresses.Add(iAddress);
            }
            if (addresses.Count > 0) {
                using (var form = new WakeProgressForm(addresses, 0)) {
                    form.ShowDialog(this);
                }
            }
        }

        private void timerEnableDisable_Tick(object sender, EventArgs e) {
            mnuEditCut.Enabled = (list.SelectedItems.Count > 0);
            mnxEditCut.Enabled = mnuEditCut.Enabled;
            mnuEditCopy.Enabled = (list.SelectedItems.Count > 0);
            mnxEditCopy.Enabled = mnuEditCopy.Enabled;
            mnuEditPaste.Enabled = _document.CanPaste();
            mnxEditPaste.Enabled = mnuEditPaste.Enabled;

            mnuEditSelectAll.Enabled = (list.SelectedItems.Count > 0);

            mnuEditChange.Enabled = (list.SelectedItems.Count == 1);
            mnxEditChange.Enabled = mnuEditChange.Enabled;
            mnuEditRemove.Enabled = (list.SelectedItems.Count > 0);
            mnxEditRemove.Enabled = mnuEditRemove.Enabled;

            mnuActionWake.Enabled = (list.SelectedItems.Count > 0);
            mnxActionWake.Enabled = mnuActionWake.Enabled;

            mnuActionWakeAll.Enabled = (list.Items.Count > 0);
            mnxActionWakeAll.Enabled = mnuActionWakeAll.Enabled;
        }


        private ListViewColumnSorter _listColumnSorter = new ListViewColumnSorter();

        private void list_ColumnClick(object sender, ColumnClickEventArgs e) {
            if (e.Column == _listColumnSorter.SortColumn) {
                // Reverse the current sort direction for this column.
                if (this._listColumnSorter.Order == SortOrder.Ascending) {
                    this._listColumnSorter.Order = SortOrder.Descending;
                } else {
                    this._listColumnSorter.Order = SortOrder.Ascending;
                }
            } else {
                // Set the column number that is to be sorted; default to ascending.
                this._listColumnSorter.SortColumn = e.Column;
                this._listColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.list.Sort();
        }

        private void tmrReSortAfterRename_Tick(object sender, EventArgs e) {
            tmrReSort.Enabled = false;
            this.list.Sort();
            this.list.Enabled = true;
            this.list.Focus();
            //UpdateData(new MagiWolDocument.Address[] { iAddress });
        }

        private void mnuToolsRefresh_Click(object sender, EventArgs e) {
            var addrs = new List<MagiWolDocument.Address>();
            foreach (var iItem in list.SelectedItems) {
                var iAddress = (MagiWolDocument.Address)iItem;
                addrs.Add(iAddress);
            }
            UpdateData(addrs);
        }


        private static bool IsRunningOnMono {
            get {
                return (Type.GetType("Mono.Runtime") != null);
            }
        }

        private void OpenFromCommandLineArgs() { //goes through all files until it can open one and redirects all other files to new instances with /OpenOrExit argument. That argument ensures that each new instance will close in case of error.
            var filesToOpen = Medo.Application.Args.Current.GetValues(null);
            FileInfo iFile;
            for (int i = 0; i < filesToOpen.Length; ++i) {
                iFile = new FileInfo(filesToOpen[i]);
                try {
                    _document = MagiWolDocument.Document.Open(iFile.FullName);
                    _recent.Push(iFile.FullName);

                    //send all other files to second instances
                    for (int j = i + 1; j < filesToOpen.Length; ++j) {
                        var jFile = new FileInfo(filesToOpen[j]);
                        Process.Start(GetProcessStartInfo(@"""" + jFile.FullName + @""" /OpenOrExit"));
                        Thread.Sleep(100 / Environment.ProcessorCount);
                    }
                    break; //i

                } catch (Exception ex) {
                    Medo.MessageBox.ShowError(this, string.Format("Cannot open \"{0}\".\n\n{1}", iFile.Name, ex.Message));
                    if (Medo.Application.Args.Current.ContainsKey("OpenOrExit")) {
                        this.Close();
                        System.Environment.Exit(1);
                    }
                }
            }
        }

    }

}
