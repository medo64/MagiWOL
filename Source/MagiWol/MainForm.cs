using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MagiWol {

    internal partial class MainForm : Form {

        private MagiWolDocument.Document Document;
        private Medo.Configuration.RecentFiles Recent;


        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            mnu.Renderer = new ToolStripBorderlessProfessionalRenderer();

            Medo.Windows.Forms.TaskbarProgress.DefaultOwner = this;
            Medo.Windows.Forms.TaskbarProgress.DoNotThrowNotImplementedException = true;

            if (Settings.IsInstalled == false) {
                mnuImport0.Visible = false;
            }

            this.Document = new MagiWolDocument.Document();
            this.Recent = new Medo.Configuration.RecentFiles();

            list.ListViewItemSorter = _listColumnSorter;
            this._listColumnSorter.SortColumn = 0;
            this._listColumnSorter.Order = SortOrder.Ascending;
            list.Sort();

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this, list);
        }


        private bool SuppressMenuKey = false;

        protected override bool ProcessDialogKey(Keys keyData) {
            if (((keyData & Keys.Alt) == Keys.Alt) && (keyData != (Keys.Alt | Keys.Menu))) { this.SuppressMenuKey = true; }

            switch (keyData) {

                case Keys.F10:
                    ToggleMenu();
                    return true;

                case Keys.Control | Keys.N:
                case Keys.Alt | Keys.N:
                    mnuNew.PerformClick();
                    return true;

                case Keys.Alt | Keys.O:
                    mnuOpenRoot.ShowDropDown();
                    return true;

                case Keys.Control | Keys.O:
                    mnuOpen.PerformClick();
                    return true;

                case Keys.Alt | Keys.S:
                    mnuSaveRoot.ShowDropDown();
                    return true;

                case Keys.Control | Keys.S:
                    mnuSave.PerformClick();
                    return true;

                case Keys.Control | Keys.Shift | Keys.S:
                    mnuSaveAs.PerformClick();
                    return true;

                case Keys.F5:
                    mnuRefresh_Click(null, null);
                    return true;

                case Keys.Control | Keys.X:
                    mnuCut.PerformClick();
                    return true;

                case Keys.Control | Keys.C:
                    mnuCopy.PerformClick();
                    return true;

                case Keys.Control | Keys.V:
                    mnuPaste.PerformClick();
                    return true;

                case Keys.Control | Keys.A:
                    mnuSelectAll_Click(null, null);
                    return true;

                case Keys.Insert:
                    mnuAdd.PerformClick();
                    return true;

                case Keys.F4:
                    mnuChange.PerformClick();
                    return true;

                case Keys.Delete:
                    mnuRemove.PerformClick();
                    return true;

                case Keys.F6:
                    mnuWake.PerformClick();
                    return true;

                case Keys.Shift | Keys.F6:
                    mnuWakeAll.PerformClick();
                    return true;

                case Keys.Control | Keys.W:
                    mnuQuickWake.PerformClick();
                    return true;

                case Keys.F1:
                    mnuApp.ShowDropDown();
                    mnuAppAbout.Select();
                    return true;



            }

            return base.ProcessDialogKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            if (e.KeyData == Keys.Menu) {
                if (this.SuppressMenuKey) { this.SuppressMenuKey = false; return; }
                ToggleMenu();
                e.Handled = true;
                e.SuppressKeyPress = true;
            } else {
                base.OnKeyDown(e);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            if (e.KeyData == Keys.Menu) {
                if (this.SuppressMenuKey) { this.SuppressMenuKey = false; return; }
                ToggleMenu();
                e.Handled = true;
                e.SuppressKeyPress = true;
            } else {
                base.OnKeyUp(e);
            }
        }

        private void ToggleMenu() {
            if (mnu.ContainsFocus) {
                list.Select();
            } else {
                mnu.Select();
                mnu.Items[0].Select();
            }
        }


        private void Form_Load(object sender, EventArgs e) {
            mnuImport.Visible = !MainForm.IsRunningOnMono;
            mnuImport0.Visible = !MainForm.IsRunningOnMono;
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



        private void UpdateData(IEnumerable<MagiWolDocument.Address> selection) {
            if (this.Document == null) { return; }

            this.Document.FillListView(list, selection);

            this.Text = this.Document.FileTitle + " - " + Medo.Reflection.EntryAssembly.Title;

            for (int i = mnuOpenRoot.DropDownItems.Count - 1; i >= 0; i--) {
                if (mnuOpenRoot.DropDownItems[i].Tag == null) { break; }
                mnuOpenRoot.DropDownItems.RemoveAt(i);
            }
            mnuImport0.Visible = (this.Recent.Count > 0);
            foreach (var iRecentFile in this.Recent.Items) {
                var item = new ToolStripMenuItem(iRecentFile.Title);
                item.Tag = iRecentFile;
                item.Click += new EventHandler(recentItem_Click);
                mnuOpenRoot.DropDownItems.Add(item);
            }
        }

        void recentItem_Click(object sender, EventArgs e) {
            var item = (ToolStripMenuItem)sender;
            var recentItem = (Medo.Configuration.RecentFile)item.Tag;
            if (ProceedWithNewDocument()) {
                try {
                    Document = MagiWolDocument.Document.Open(recentItem.FileName);
                    Recent.Push(recentItem.FileName);
                } catch (Exception ex) {
                    var exFile = new FileInfo(recentItem.FileName);
                    Medo.MessageBox.ShowError(this, string.Format("Cannot open \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                }
                UpdateData(null);
            }
        }


        private bool ProceedWithNewDocument() {
            if (!Document.HasChanged) { return true; }

            switch (Medo.MessageBox.ShowQuestion(this, "Current file is not saved. Save changes now?", MessageBoxButtons.YesNoCancel, MessageBoxDefaultButton.Button1)) {
                case DialogResult.Yes:
                    mnuSave_Click(null, null);
                    break;

                case DialogResult.No:
                    return true;

                case DialogResult.Cancel:
                    return false;
            }

            return !Document.HasChanged;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e) {
            if (!ProceedWithNewDocument()) {
                e.Cancel = true;
                return;
            }
            this.Recent.Save();
        }


        #region Menu

        private void mnuNew_Click(object sender, EventArgs e) {
            if (ProceedWithNewDocument()) {
                Document = new MagiWolDocument.Document();
                UpdateData(null);
            }
        }

        private void mnuOpen_Click(object sender, EventArgs e) {
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
                            Document = MagiWolDocument.Document.Open(dialog.FileName);
                            Recent.Push(dialog.FileName);
                        } catch (Exception ex) {
                            var exFile = new FileInfo(dialog.FileName);
                            Medo.MessageBox.ShowError(this, string.Format("Cannot open \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                        }
                        UpdateData(null);
                    }
                }
            }
        }

        private void mnuImport_Click(object sender, EventArgs e) {
            if (MainForm.IsRunningOnMono) {
                Medo.MessageBox.ShowInformation(this, "This operation is not supported on Mono.");
                return;
            }

            using (var form = new ImportFromNetworkForm()) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    foreach (var iAddress in form.ImportedAddresses) {
                        if (this.Document.HasAddress(iAddress)) {
                            if (Medo.MessageBox.ShowQuestion(this, string.Format("MAC address \"{0}\" is already in list.\nDo you wish to add it anyhow?", iAddress.Mac), MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2) == DialogResult.Yes) {
                                this.Document.AddAddress(iAddress, false);
                            }
                        } else {
                            this.Document.AddAddress(iAddress, false);
                        }
                    }
                    UpdateData(form.ImportedAddresses);
                }
            }
        }

        private void mnuSave_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(this.Document.FileName) || !System.IO.File.Exists(this.Document.FileName)) {
                mnuSaveAs_Click(null, null);
            } else {
                try {
                    this.Document.Save();
                } catch (Exception ex) {
                    var exFile = new FileInfo(this.Document.FileName);
                    Medo.MessageBox.ShowError(this, string.Format("Cannot save \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                }
            }
            if (this.Document.FileName != null) {
                Recent.Push(this.Document.FileName);
            }
            UpdateData(null);
        }

        private void mnuSaveAs_Click(object sender, EventArgs e) {
            using (var dialog = new SaveFileDialog()) {
                dialog.AddExtension = true;
                dialog.CheckPathExists = true;
                dialog.DefaultExt = "magiwol";
                dialog.Filter = "MagiWOL files (*.magiwol)|*.magiwol|All files (*.*)|*.*";
                dialog.OverwritePrompt = true;
                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    try {
                        this.Document.Save(dialog.FileName);
                        if (this.Document.FileName != null) {
                            Recent.Push(this.Document.FileName);
                        }
                    } catch (Exception ex) {
                        var exFile = new FileInfo(dialog.FileName);
                        Medo.MessageBox.ShowError(this, string.Format("Cannot save \"{0}\".\n\n{1}", exFile.Name, ex.Message));
                    }
                }
            }
            UpdateData(null);
        }


        private void mnuRefresh_Click(object sender, EventArgs e) {
            var addrs = new List<MagiWolDocument.Address>();
            foreach (var iItem in list.SelectedItems) {
                var iAddress = (MagiWolDocument.Address)iItem;
                addrs.Add(iAddress);
            }
            UpdateData(addrs);
        }


        private void mnuCut_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addrs.Add(iAddress);
                }
                try {
                    this.Document.Cut(addrs);
                } catch (ExternalException ex) {
                    Medo.MessageBox.ShowError(this, "Cut error.\n\n" + ex.Message);
                }
                UpdateData(null);
            }
        }

        private void mnuCopy_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    addrs.Add(iAddress);
                }
                try {
                    this.Document.Copy(addrs);
                } catch (ExternalException ex) {
                    Medo.MessageBox.ShowError(this, "Copy error.\n\n" + ex.Message);
                }
                UpdateData(addrs);
            }
        }

        private void mnuPaste_Click(object sender, EventArgs e) {
            try {
                var pasted = this.Document.Paste();
                UpdateData(pasted);
            } catch (ExternalException ex) {
                Medo.MessageBox.ShowError(this, "Paste error.\n\n" + ex.Message);
            }
        }

        private void mnuSelectAll_Click(object sender, EventArgs e) {
            foreach (var iItem in list.Items) {
                ((MagiWolDocument.Address)iItem).Selected = true;
            }
        }


        private void mnuAdd_Click(object sender, EventArgs e) {
            using (var frm = new DetailForm(null)) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    if (this.Document.HasAddress(frm.Destination)) {
                        if (Medo.MessageBox.ShowQuestion(this, string.Format("MAC address \"{0}\" is already in list.\nDo you wish to add it anyhow?", frm.Destination.Mac), MessageBoxButtons.YesNo) == DialogResult.Yes) {
                            this.Document.AddAddress(frm.Destination, true);
                        }
                    } else {
                        this.Document.AddAddress(frm.Destination, false);
                    }
                    UpdateData(new MagiWolDocument.Address[] { frm.Destination });
                }
            }
        }

        private void mnuChange_Click(object sender, EventArgs e) {
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

        private void mnuRemove_Click(object sender, EventArgs e) {
            if (list.SelectedItems.Count > 0) {
                var addrs = new List<MagiWolDocument.Address>();
                foreach (var iItem in list.SelectedItems) {
                    var iAddress = (MagiWolDocument.Address)iItem;
                    this.Document.RemoveAddress(iAddress);
                }
                UpdateData(null);
            }
        }


        private void mnuWake_Click(object sender, EventArgs e) {
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

        private void mnuWakeAll_Click(object sender, EventArgs e) {
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

        private void mnuQuickWake_Click(object sender, EventArgs e) {
            using (var form = new QuickWakeForm()) {
                form.ShowDialog(this);
            }
        }


        private void mnuOptions_Click(object sender, EventArgs e) {
            using (var form = new SettingsForm()) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    mnuRefresh_Click(null, null);
                }
            }
        }


        private void mnuAppFeedback_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("http://jmedved.com/feedback/"));
        }

        private void mnuAppUpgrade_Click(object sender, EventArgs e) {
            Medo.Services.Upgrade.ShowDialog(this, new Uri("http://jmedved.com/upgrade/"));
        }

        private void mnuAppDonate_Click(object sender, EventArgs e) {
            Process.Start("http://www.jmedved.com/donate/");
        }

        private void mnuAppAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("http://www.jmedved.com/magiwol/"));
        }

        #endregion



        #region ContextMenu: List

        private void mnxList_Opening(object sender, CancelEventArgs e) {
            mnxListCut.Enabled = (list.SelectedItems.Count > 0);
            mnxListCopy.Enabled = (list.SelectedItems.Count > 0);
            mnxListPaste.Enabled = this.Document.CanPaste();
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
                this.Document.MarkAsChanged();
                UpdateData(new MagiWolDocument.Address[] { iAddress });
            } else {
                e.CancelEdit = true;
            }
        }

        private void list_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.F2) {
                if (list.SelectedItems.Count == 1) {
                    if (list.FocusedItem != null) {
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
            mnuCut.Enabled = (list.SelectedItems.Count > 0);
            mnuCopy.Enabled = (list.SelectedItems.Count > 0);
            mnuPaste.Enabled = Document.CanPaste();

            mnuChange.Enabled = (list.SelectedItems.Count == 1);
            mnuRemove.Enabled = (list.SelectedItems.Count > 0);

            mnuWake.Enabled = (list.SelectedItems.Count > 0);
            mnuWakeAll.Enabled = (list.Items.Count > 0);
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
                    Document = MagiWolDocument.Document.Open(iFile.FullName);
                    Recent.Push(iFile.FullName);

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
                        System.Environment.Exit(1);
                    }
                }
            }
        }

    }

}
