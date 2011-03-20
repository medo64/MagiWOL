namespace MagiWol {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mnu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileImportFromNetwork = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditChange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionWake = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuActionWakeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuActionQuickWake = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpReportABug = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnx = new System.Windows.Forms.ToolStrip();
            this.mnxFileNew = new System.Windows.Forms.ToolStripButton();
            this.mnxFileOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.mnxFileSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxEditCut = new System.Windows.Forms.ToolStripButton();
            this.mnxEditCopy = new System.Windows.Forms.ToolStripButton();
            this.mnxEditPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxEditAdd = new System.Windows.Forms.ToolStripButton();
            this.mnxEditChange = new System.Windows.Forms.ToolStripButton();
            this.mnxEditRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxActionWake = new System.Windows.Forms.ToolStripButton();
            this.mnxActionWakeAll = new System.Windows.Forms.ToolStripButton();
            this.mnxAbout = new System.Windows.Forms.ToolStripButton();
            this.mnxReportABug = new System.Windows.Forms.ToolStripButton();
            this.mnxToolsOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxActionQuickWake = new System.Windows.Forms.ToolStripButton();
            this.list = new System.Windows.Forms.ListView();
            this.list_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.list_MAC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.list_Notes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnxList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxListCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxListCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxListPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxListEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxListAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxListChange = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxListRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxListActionWake = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxListQuickWake = new System.Windows.Forms.ToolStripMenuItem();
            this.timerEnableDisable = new System.Windows.Forms.Timer(this.components);
            this.tmrReSort = new System.Windows.Forms.Timer(this.components);
            this.mnu.SuspendLayout();
            this.mnx.SuspendLayout();
            this.mnxList.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnu
            // 
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuAction,
            this.mnuTools,
            this.mnuHelp});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Size = new System.Drawing.Size(662, 28);
            this.mnu.TabIndex = 0;
            this.mnu.MenuDeactivate += new System.EventHandler(this.mnu_MenuDeactivate);
            this.mnu.VisibleChanged += new System.EventHandler(this.mnu_VisibleChanged);
            this.mnu.Leave += new System.EventHandler(this.mnu_Leave);
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileRecent,
            this.mnuFileImport,
            this.toolStripMenuItem1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.toolStripMenuItem2,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(44, 24);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileNew.Image")));
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(219, 24);
            this.mnuFileNew.Text = "&New";
            this.mnuFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileOpen.Image")));
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(219, 24);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnuFileRecent
            // 
            this.mnuFileRecent.Name = "mnuFileRecent";
            this.mnuFileRecent.Size = new System.Drawing.Size(219, 24);
            this.mnuFileRecent.Text = "&Recent files";
            // 
            // mnuFileImport
            // 
            this.mnuFileImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileImportFromNetwork});
            this.mnuFileImport.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileImport.Image")));
            this.mnuFileImport.Name = "mnuFileImport";
            this.mnuFileImport.Size = new System.Drawing.Size(219, 24);
            this.mnuFileImport.Text = "&Import";
            // 
            // mnuFileImportFromNetwork
            // 
            this.mnuFileImportFromNetwork.Name = "mnuFileImportFromNetwork";
            this.mnuFileImportFromNetwork.Size = new System.Drawing.Size(169, 24);
            this.mnuFileImportFromNetwork.Text = "From &network";
            this.mnuFileImportFromNetwork.Click += new System.EventHandler(this.mnuFileImportFromNetwork_Click_1);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSave.Image")));
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(219, 24);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.mnuFileSaveAs.Size = new System.Drawing.Size(219, 24);
            this.mnuFileSaveAs.Text = "Save &As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.mnuFileSaveAs_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(216, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(219, 24);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.toolStripMenuItem4,
            this.mnuEditSelectAll,
            this.toolStripMenuItem8,
            this.mnuEditAdd,
            this.mnuEditChange,
            this.mnuEditRemove});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(47, 24);
            this.mnuEdit.Text = "&Edit";
            this.mnuEdit.DropDownOpening += new System.EventHandler(this.mnuEdit_DropDownOpening);
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCut.Image")));
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(190, 24);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCopy.Image")));
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(190, 24);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditPaste.Image")));
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(190, 24);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuEditSelectAll
            // 
            this.mnuEditSelectAll.Name = "mnuEditSelectAll";
            this.mnuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuEditSelectAll.Size = new System.Drawing.Size(190, 24);
            this.mnuEditSelectAll.Text = "&Select all";
            this.mnuEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(187, 6);
            // 
            // mnuEditAdd
            // 
            this.mnuEditAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditAdd.Image")));
            this.mnuEditAdd.Name = "mnuEditAdd";
            this.mnuEditAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mnuEditAdd.Size = new System.Drawing.Size(190, 24);
            this.mnuEditAdd.Text = "&Add";
            this.mnuEditAdd.Click += new System.EventHandler(this.mnuEditAdd_Click);
            // 
            // mnuEditChange
            // 
            this.mnuEditChange.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditChange.Image")));
            this.mnuEditChange.Name = "mnuEditChange";
            this.mnuEditChange.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnuEditChange.Size = new System.Drawing.Size(190, 24);
            this.mnuEditChange.Text = "C&hange";
            this.mnuEditChange.Click += new System.EventHandler(this.mnuEditChange_Click);
            // 
            // mnuEditRemove
            // 
            this.mnuEditRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditRemove.Image")));
            this.mnuEditRemove.Name = "mnuEditRemove";
            this.mnuEditRemove.ShortcutKeyDisplayString = "";
            this.mnuEditRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnuEditRemove.Size = new System.Drawing.Size(190, 24);
            this.mnuEditRemove.Text = "&Remove";
            this.mnuEditRemove.Click += new System.EventHandler(this.mnuEditRemove_Click);
            // 
            // mnuAction
            // 
            this.mnuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuActionWake,
            this.mnuActionWakeAll,
            this.toolStripMenuItem3,
            this.mnuActionQuickWake});
            this.mnuAction.Name = "mnuAction";
            this.mnuAction.Size = new System.Drawing.Size(64, 24);
            this.mnuAction.Text = "&Action";
            // 
            // mnuActionWake
            // 
            this.mnuActionWake.Image = ((System.Drawing.Image)(resources.GetObject("mnuActionWake.Image")));
            this.mnuActionWake.Name = "mnuActionWake";
            this.mnuActionWake.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnuActionWake.Size = new System.Drawing.Size(209, 24);
            this.mnuActionWake.Text = "&Wake selected";
            this.mnuActionWake.Click += new System.EventHandler(this.mnuActionWake_Click);
            // 
            // mnuActionWakeAll
            // 
            this.mnuActionWakeAll.Image = ((System.Drawing.Image)(resources.GetObject("mnuActionWakeAll.Image")));
            this.mnuActionWakeAll.Name = "mnuActionWakeAll";
            this.mnuActionWakeAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F6)));
            this.mnuActionWakeAll.Size = new System.Drawing.Size(209, 24);
            this.mnuActionWakeAll.Text = "Wake &all";
            this.mnuActionWakeAll.Click += new System.EventHandler(this.mnuActionWakeAll_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(206, 6);
            // 
            // mnuActionQuickWake
            // 
            this.mnuActionQuickWake.Image = ((System.Drawing.Image)(resources.GetObject("mnuActionQuickWake.Image")));
            this.mnuActionQuickWake.Name = "mnuActionQuickWake";
            this.mnuActionQuickWake.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mnuActionQuickWake.Size = new System.Drawing.Size(209, 24);
            this.mnuActionQuickWake.Text = "&Quick wake";
            this.mnuActionQuickWake.Click += new System.EventHandler(this.mnuActionQuickWake_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsRefresh,
            this.toolStripMenuItem12,
            this.mnuToolsOptions});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(57, 24);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsRefresh
            // 
            this.mnuToolsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuToolsRefresh.Image")));
            this.mnuToolsRefresh.Name = "mnuToolsRefresh";
            this.mnuToolsRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuToolsRefresh.Size = new System.Drawing.Size(151, 24);
            this.mnuToolsRefresh.Text = "&Refresh";
            this.mnuToolsRefresh.Click += new System.EventHandler(this.mnuToolsRefresh_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(148, 6);
            // 
            // mnuToolsOptions
            // 
            this.mnuToolsOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuToolsOptions.Image")));
            this.mnuToolsOptions.Name = "mnuToolsOptions";
            this.mnuToolsOptions.Size = new System.Drawing.Size(151, 24);
            this.mnuToolsOptions.Text = "&Options";
            this.mnuToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpReportABug,
            this.toolStripMenuItem5,
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(53, 24);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpReportABug
            // 
            this.mnuHelpReportABug.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelpReportABug.Image")));
            this.mnuHelpReportABug.Name = "mnuHelpReportABug";
            this.mnuHelpReportABug.Size = new System.Drawing.Size(165, 24);
            this.mnuHelpReportABug.Text = "Report a &bug";
            this.mnuHelpReportABug.Click += new System.EventHandler(this.mnuHelpReportABug_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(162, 6);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelpAbout.Image")));
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(165, 24);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // mnx
            // 
            this.mnx.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxFileNew,
            this.mnxFileOpen,
            this.mnxFileSave,
            this.toolStripSeparator1,
            this.mnxEditCut,
            this.mnxEditCopy,
            this.mnxEditPaste,
            this.toolStripSeparator3,
            this.mnxEditAdd,
            this.mnxEditChange,
            this.mnxEditRemove,
            this.toolStripSeparator2,
            this.mnxActionWake,
            this.mnxActionWakeAll,
            this.mnxAbout,
            this.mnxReportABug,
            this.mnxToolsOptions,
            this.toolStripSeparator5,
            this.mnxActionQuickWake});
            this.mnx.Location = new System.Drawing.Point(0, 28);
            this.mnx.Name = "mnx";
            this.mnx.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.mnx.Size = new System.Drawing.Size(662, 27);
            this.mnx.Stretch = true;
            this.mnx.TabIndex = 1;
            // 
            // mnxFileNew
            // 
            this.mnxFileNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxFileNew.Image = ((System.Drawing.Image)(resources.GetObject("mnxFileNew.Image")));
            this.mnxFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxFileNew.Name = "mnxFileNew";
            this.mnxFileNew.Size = new System.Drawing.Size(23, 24);
            this.mnxFileNew.Text = "New";
            this.mnxFileNew.ToolTipText = "New file (Ctrl+N)";
            this.mnxFileNew.Click += new System.EventHandler(this.mnuFileNew_Click);
            // 
            // mnxFileOpen
            // 
            this.mnxFileOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxFileOpen.Image = ((System.Drawing.Image)(resources.GetObject("mnxFileOpen.Image")));
            this.mnxFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxFileOpen.Name = "mnxFileOpen";
            this.mnxFileOpen.Size = new System.Drawing.Size(32, 24);
            this.mnxFileOpen.Text = "Open";
            this.mnxFileOpen.ToolTipText = "Open file (Ctrl+O)";
            this.mnxFileOpen.ButtonClick += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // mnxFileSave
            // 
            this.mnxFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mnxFileSave.Image")));
            this.mnxFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxFileSave.Name = "mnxFileSave";
            this.mnxFileSave.Size = new System.Drawing.Size(23, 24);
            this.mnxFileSave.Text = "Save";
            this.mnxFileSave.ToolTipText = "Save file (Ctrl+S)";
            this.mnxFileSave.Click += new System.EventHandler(this.mnuFileSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // mnxEditCut
            // 
            this.mnxEditCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditCut.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditCut.Image")));
            this.mnxEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditCut.Name = "mnxEditCut";
            this.mnxEditCut.Size = new System.Drawing.Size(23, 24);
            this.mnxEditCut.Text = "Cut";
            this.mnxEditCut.ToolTipText = "Cut (Ctrl+X)";
            this.mnxEditCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnxEditCopy
            // 
            this.mnxEditCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditCopy.Image")));
            this.mnxEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditCopy.Name = "mnxEditCopy";
            this.mnxEditCopy.Size = new System.Drawing.Size(23, 24);
            this.mnxEditCopy.Text = "Copy";
            this.mnxEditCopy.ToolTipText = "Copy (Ctrl+C)";
            this.mnxEditCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnxEditPaste
            // 
            this.mnxEditPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditPaste.Image")));
            this.mnxEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditPaste.Name = "mnxEditPaste";
            this.mnxEditPaste.Size = new System.Drawing.Size(23, 24);
            this.mnxEditPaste.Text = "Paste";
            this.mnxEditPaste.ToolTipText = "Paste (Ctrl+V)";
            this.mnxEditPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // mnxEditAdd
            // 
            this.mnxEditAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditAdd.Image")));
            this.mnxEditAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditAdd.Name = "mnxEditAdd";
            this.mnxEditAdd.Size = new System.Drawing.Size(23, 24);
            this.mnxEditAdd.Text = "Add";
            this.mnxEditAdd.ToolTipText = "Add (Ins)";
            this.mnxEditAdd.Click += new System.EventHandler(this.mnuEditAdd_Click);
            // 
            // mnxEditChange
            // 
            this.mnxEditChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditChange.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditChange.Image")));
            this.mnxEditChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditChange.Name = "mnxEditChange";
            this.mnxEditChange.Size = new System.Drawing.Size(23, 24);
            this.mnxEditChange.Text = "Change";
            this.mnxEditChange.ToolTipText = "Change (F4)";
            this.mnxEditChange.Click += new System.EventHandler(this.mnuEditChange_Click);
            // 
            // mnxEditRemove
            // 
            this.mnxEditRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxEditRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnxEditRemove.Image")));
            this.mnxEditRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxEditRemove.Name = "mnxEditRemove";
            this.mnxEditRemove.Size = new System.Drawing.Size(23, 24);
            this.mnxEditRemove.Text = "Remove";
            this.mnxEditRemove.ToolTipText = "Remove (Del)";
            this.mnxEditRemove.Click += new System.EventHandler(this.mnuEditRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // mnxActionWake
            // 
            this.mnxActionWake.Image = ((System.Drawing.Image)(resources.GetObject("mnxActionWake.Image")));
            this.mnxActionWake.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxActionWake.Name = "mnxActionWake";
            this.mnxActionWake.Size = new System.Drawing.Size(125, 24);
            this.mnxActionWake.Text = "Wake selected";
            this.mnxActionWake.ToolTipText = "Wake selected (F6)";
            this.mnxActionWake.Click += new System.EventHandler(this.mnuActionWake_Click);
            // 
            // mnxActionWakeAll
            // 
            this.mnxActionWakeAll.Image = ((System.Drawing.Image)(resources.GetObject("mnxActionWakeAll.Image")));
            this.mnxActionWakeAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxActionWakeAll.Name = "mnxActionWakeAll";
            this.mnxActionWakeAll.Size = new System.Drawing.Size(86, 24);
            this.mnxActionWakeAll.Text = "Wake all";
            this.mnxActionWakeAll.ToolTipText = "Wake all (Shift+F6)";
            this.mnxActionWakeAll.Click += new System.EventHandler(this.mnuActionWakeAll_Click);
            // 
            // mnxAbout
            // 
            this.mnxAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnxAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnxAbout.Image")));
            this.mnxAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxAbout.Name = "mnxAbout";
            this.mnxAbout.Size = new System.Drawing.Size(23, 24);
            this.mnxAbout.Text = "About";
            this.mnxAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // mnxReportABug
            // 
            this.mnxReportABug.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnxReportABug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxReportABug.Image = ((System.Drawing.Image)(resources.GetObject("mnxReportABug.Image")));
            this.mnxReportABug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxReportABug.Name = "mnxReportABug";
            this.mnxReportABug.Size = new System.Drawing.Size(23, 24);
            this.mnxReportABug.Text = "Report a bug";
            this.mnxReportABug.Click += new System.EventHandler(this.mnuHelpReportABug_Click);
            // 
            // mnxToolsOptions
            // 
            this.mnxToolsOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnxToolsOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnxToolsOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnxToolsOptions.Image")));
            this.mnxToolsOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxToolsOptions.Name = "mnxToolsOptions";
            this.mnxToolsOptions.Size = new System.Drawing.Size(23, 24);
            this.mnxToolsOptions.Text = "Options";
            this.mnxToolsOptions.Click += new System.EventHandler(this.mnuToolsOptions_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // mnxActionQuickWake
            // 
            this.mnxActionQuickWake.Image = ((System.Drawing.Image)(resources.GetObject("mnxActionQuickWake.Image")));
            this.mnxActionQuickWake.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnxActionQuickWake.Name = "mnxActionQuickWake";
            this.mnxActionQuickWake.Size = new System.Drawing.Size(104, 24);
            this.mnxActionQuickWake.Text = "Quick wake";
            this.mnxActionQuickWake.ToolTipText = "Quick wake (Ctrl+W)";
            this.mnxActionQuickWake.Click += new System.EventHandler(this.mnuActionQuickWake_Click);
            // 
            // list
            // 
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.list_Name,
            this.list_MAC,
            this.list_Notes});
            this.list.ContextMenuStrip = this.mnxList;
            this.list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list.FullRowSelect = true;
            this.list.GridLines = true;
            this.list.HideSelection = false;
            this.list.LabelEdit = true;
            this.list.Location = new System.Drawing.Point(0, 55);
            this.list.Name = "list";
            this.list.ShowGroups = false;
            this.list.Size = new System.Drawing.Size(662, 300);
            this.list.TabIndex = 2;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            this.list.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.list_AfterLabelEdit);
            this.list.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.list_ColumnClick);
            this.list.ItemActivate += new System.EventHandler(this.list_ItemActivate);
            this.list.KeyUp += new System.Windows.Forms.KeyEventHandler(this.list_KeyUp);
            // 
            // list_Name
            // 
            this.list_Name.Tag = "Name";
            this.list_Name.Text = "Name";
            this.list_Name.Width = 150;
            // 
            // list_MAC
            // 
            this.list_MAC.Tag = "MAC";
            this.list_MAC.Text = "MAC address";
            this.list_MAC.Width = 150;
            // 
            // list_Notes
            // 
            this.list_Notes.Tag = "Notes";
            this.list_Notes.Text = "Notes";
            this.list_Notes.Width = 150;
            // 
            // mnxList
            // 
            this.mnxList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxListCut,
            this.mnxListCopy,
            this.mnxListPaste,
            this.toolStripMenuItem6,
            this.mnxListEditSelectAll,
            this.toolStripMenuItem9,
            this.mnxListAdd,
            this.mnxListChange,
            this.mnxListRemove,
            this.toolStripMenuItem7,
            this.mnxListActionWake,
            this.toolStripMenuItem10,
            this.mnxListQuickWake});
            this.mnxList.Name = "mnxListAddress";
            this.mnxList.Size = new System.Drawing.Size(210, 244);
            this.mnxList.Opening += new System.ComponentModel.CancelEventHandler(this.mnxList_Opening);
            // 
            // mnxListCut
            // 
            this.mnxListCut.Image = ((System.Drawing.Image)(resources.GetObject("mnxListCut.Image")));
            this.mnxListCut.Name = "mnxListCut";
            this.mnxListCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnxListCut.Size = new System.Drawing.Size(209, 24);
            this.mnxListCut.Text = "Cu&t";
            this.mnxListCut.Click += new System.EventHandler(this.mnuEditCut_Click);
            // 
            // mnxListCopy
            // 
            this.mnxListCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnxListCopy.Image")));
            this.mnxListCopy.Name = "mnxListCopy";
            this.mnxListCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnxListCopy.Size = new System.Drawing.Size(209, 24);
            this.mnxListCopy.Text = "&Copy";
            this.mnxListCopy.Click += new System.EventHandler(this.mnuEditCopy_Click);
            // 
            // mnxListPaste
            // 
            this.mnxListPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnxListPaste.Image")));
            this.mnxListPaste.Name = "mnxListPaste";
            this.mnxListPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnxListPaste.Size = new System.Drawing.Size(209, 24);
            this.mnxListPaste.Text = "&Paste";
            this.mnxListPaste.Click += new System.EventHandler(this.mnuEditPaste_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(206, 6);
            // 
            // mnxListEditSelectAll
            // 
            this.mnxListEditSelectAll.Name = "mnxListEditSelectAll";
            this.mnxListEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnxListEditSelectAll.Size = new System.Drawing.Size(209, 24);
            this.mnxListEditSelectAll.Text = "&Select all";
            this.mnxListEditSelectAll.Click += new System.EventHandler(this.mnuEditSelectAll_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(206, 6);
            // 
            // mnxListAdd
            // 
            this.mnxListAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnxListAdd.Image")));
            this.mnxListAdd.Name = "mnxListAdd";
            this.mnxListAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mnxListAdd.Size = new System.Drawing.Size(209, 24);
            this.mnxListAdd.Text = "&Add";
            this.mnxListAdd.Click += new System.EventHandler(this.mnuEditAdd_Click);
            // 
            // mnxListChange
            // 
            this.mnxListChange.Image = ((System.Drawing.Image)(resources.GetObject("mnxListChange.Image")));
            this.mnxListChange.Name = "mnxListChange";
            this.mnxListChange.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.mnxListChange.Size = new System.Drawing.Size(209, 24);
            this.mnxListChange.Text = "C&hange";
            this.mnxListChange.Click += new System.EventHandler(this.mnuEditChange_Click);
            // 
            // mnxListRemove
            // 
            this.mnxListRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnxListRemove.Image")));
            this.mnxListRemove.Name = "mnxListRemove";
            this.mnxListRemove.ShortcutKeyDisplayString = "Del";
            this.mnxListRemove.Size = new System.Drawing.Size(209, 24);
            this.mnxListRemove.Text = "&Remove";
            this.mnxListRemove.Click += new System.EventHandler(this.mnuEditRemove_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(206, 6);
            // 
            // mnxListActionWake
            // 
            this.mnxListActionWake.Image = ((System.Drawing.Image)(resources.GetObject("mnxListActionWake.Image")));
            this.mnxListActionWake.Name = "mnxListActionWake";
            this.mnxListActionWake.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.mnxListActionWake.Size = new System.Drawing.Size(209, 24);
            this.mnxListActionWake.Text = "&Wake selected";
            this.mnxListActionWake.Click += new System.EventHandler(this.mnuActionWake_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(206, 6);
            // 
            // mnxListQuickWake
            // 
            this.mnxListQuickWake.Name = "mnxListQuickWake";
            this.mnxListQuickWake.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.mnxListQuickWake.Size = new System.Drawing.Size(209, 24);
            this.mnxListQuickWake.Text = "&Quick wake";
            this.mnxListQuickWake.Click += new System.EventHandler(this.mnxListQuickWake_Click);
            // 
            // timerEnableDisable
            // 
            this.timerEnableDisable.Enabled = true;
            this.timerEnableDisable.Interval = 500;
            this.timerEnableDisable.Tick += new System.EventHandler(this.timerEnableDisable_Tick);
            // 
            // tmrReSort
            // 
            this.tmrReSort.Tick += new System.EventHandler(this.tmrReSortAfterRename_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 355);
            this.Controls.Add(this.list);
            this.Controls.Add(this.mnx);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnu;
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "MainForm";
            this.Text = "MagiWOL";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.mnx.ResumeLayout(false);
            this.mnx.PerformLayout();
            this.mnxList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnu;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNew;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSave;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuAction;
        private System.Windows.Forms.ToolStripMenuItem mnuActionWake;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStrip mnx;
        private System.Windows.Forms.ToolStripButton mnxFileNew;
        private System.Windows.Forms.ToolStripButton mnxFileSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnxActionWake;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem mnuActionQuickWake;
        private System.Windows.Forms.ListView list;
        private System.Windows.Forms.ColumnHeader list_Name;
        private System.Windows.Forms.ColumnHeader list_MAC;
        private System.Windows.Forms.ColumnHeader list_Notes;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuEditAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuEditChange;
        private System.Windows.Forms.ToolStripMenuItem mnuEditRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripButton mnxEditAdd;
        private System.Windows.Forms.ToolStripButton mnxEditChange;
        private System.Windows.Forms.ToolStripButton mnxEditRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuFileRecent;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCut;
        private System.Windows.Forms.ToolStripMenuItem mnuEditCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuEditPaste;
        private System.Windows.Forms.ToolStripSplitButton mnxFileOpen;
        private System.Windows.Forms.Timer timerEnableDisable;
        private System.Windows.Forms.ToolStripButton mnxActionQuickWake;
        private System.Windows.Forms.ContextMenuStrip mnxList;
        private System.Windows.Forms.ToolStripMenuItem mnxListCut;
        private System.Windows.Forms.ToolStripMenuItem mnxListCopy;
        private System.Windows.Forms.ToolStripMenuItem mnxListPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem mnxListAdd;
        private System.Windows.Forms.ToolStripMenuItem mnxListChange;
        private System.Windows.Forms.ToolStripMenuItem mnxListRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem mnxListQuickWake;
        private System.Windows.Forms.ToolStripButton mnxEditCut;
        private System.Windows.Forms.ToolStripButton mnxEditCopy;
        private System.Windows.Forms.ToolStripButton mnxEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem mnxListEditSelectAll;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem mnxListActionWake;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem mnuActionWakeAll;
        private System.Windows.Forms.ToolStripButton mnxActionWakeAll;
        private System.Windows.Forms.ToolStripMenuItem mnuFileImport;
        private System.Windows.Forms.ToolStripMenuItem mnuFileImportFromNetwork;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpReportABug;
        private System.Windows.Forms.Timer tmrReSort;
        private System.Windows.Forms.ToolStripMenuItem mnuToolsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
        private System.Windows.Forms.ToolStripButton mnxToolsOptions;
        private System.Windows.Forms.ToolStripButton mnxAbout;
        private System.Windows.Forms.ToolStripButton mnxReportABug;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}

