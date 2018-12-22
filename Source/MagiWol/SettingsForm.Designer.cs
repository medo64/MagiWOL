namespace MagiWol {
    partial class SettingsForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabs_pagPacket = new System.Windows.Forms.TabPage();
            this.labelBroadcastAddress = new System.Windows.Forms.Label();
            this.checkProtocolIPv6 = new System.Windows.Forms.CheckBox();
            this.labelProtocol = new System.Windows.Forms.Label();
            this.checkProtocolIPv4 = new System.Windows.Forms.CheckBox();
            this.buttonCheckHost = new System.Windows.Forms.Button();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.labelBroadcastPort = new System.Windows.Forms.Label();
            this.textBroadcastPort = new System.Windows.Forms.TextBox();
            this.textBroadcastAddress = new System.Windows.Forms.TextBox();
            this.tabs_pagColumns = new System.Windows.Forms.TabPage();
            this.chbNotes = new System.Windows.Forms.CheckBox();
            this.chbBroadcastPort = new System.Windows.Forms.CheckBox();
            this.chbBroadcastHost = new System.Windows.Forms.CheckBox();
            this.chbSecureOn = new System.Windows.Forms.CheckBox();
            this.chbMac = new System.Windows.Forms.CheckBox();
            this.chbTitle = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            this.tabs.SuspendLayout();
            this.tabs_pagPacket.SuspendLayout();
            this.tabs_pagColumns.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(282, 226);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 0;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(176, 226);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // erp
            // 
            this.erp.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erp.ContainerControl = this;
            this.erp.Icon = ((System.Drawing.Icon)(resources.GetObject("erp.Icon")));
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Controls.Add(this.tabs_pagPacket);
            this.tabs.Controls.Add(this.tabs_pagColumns);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(370, 197);
            this.tabs.TabIndex = 1;
            // 
            // tabs_pagPacket
            // 
            this.tabs_pagPacket.Controls.Add(this.labelBroadcastAddress);
            this.tabs_pagPacket.Controls.Add(this.checkProtocolIPv6);
            this.tabs_pagPacket.Controls.Add(this.labelProtocol);
            this.tabs_pagPacket.Controls.Add(this.checkProtocolIPv4);
            this.tabs_pagPacket.Controls.Add(this.buttonCheckHost);
            this.tabs_pagPacket.Controls.Add(this.buttonDefault);
            this.tabs_pagPacket.Controls.Add(this.labelBroadcastPort);
            this.tabs_pagPacket.Controls.Add(this.textBroadcastPort);
            this.tabs_pagPacket.Controls.Add(this.textBroadcastAddress);
            this.tabs_pagPacket.Location = new System.Drawing.Point(4, 25);
            this.tabs_pagPacket.Name = "tabs_pagPacket";
            this.tabs_pagPacket.Padding = new System.Windows.Forms.Padding(3);
            this.tabs_pagPacket.Size = new System.Drawing.Size(362, 168);
            this.tabs_pagPacket.TabIndex = 1;
            this.tabs_pagPacket.Text = "WOL packet";
            this.tabs_pagPacket.UseVisualStyleBackColor = true;
            // 
            // labelBroadcastAddress
            // 
            this.labelBroadcastAddress.AutoSize = true;
            this.labelBroadcastAddress.Location = new System.Drawing.Point(6, 36);
            this.labelBroadcastAddress.Name = "labelBroadcastAddress";
            this.labelBroadcastAddress.Size = new System.Drawing.Size(131, 17);
            this.labelBroadcastAddress.TabIndex = 3;
            this.labelBroadcastAddress.Text = "Broadcast address:";
            // 
            // checkProtocolIPv6
            // 
            this.checkProtocolIPv6.AutoSize = true;
            this.checkProtocolIPv6.Location = new System.Drawing.Point(219, 6);
            this.checkProtocolIPv6.Name = "checkProtocolIPv6";
            this.checkProtocolIPv6.Size = new System.Drawing.Size(57, 21);
            this.checkProtocolIPv6.TabIndex = 2;
            this.checkProtocolIPv6.Text = "IPv6";
            this.checkProtocolIPv6.UseVisualStyleBackColor = true;
            this.checkProtocolIPv6.CheckedChanged += new System.EventHandler(this.checkProtocol_CheckedChanged);
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(6, 7);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(64, 17);
            this.labelProtocol.TabIndex = 0;
            this.labelProtocol.Text = "Protocol:";
            // 
            // checkProtocolIPv4
            // 
            this.checkProtocolIPv4.AutoSize = true;
            this.checkProtocolIPv4.Enabled = false;
            this.checkProtocolIPv4.Location = new System.Drawing.Point(156, 6);
            this.checkProtocolIPv4.Name = "checkProtocolIPv4";
            this.checkProtocolIPv4.Size = new System.Drawing.Size(57, 21);
            this.checkProtocolIPv4.TabIndex = 1;
            this.checkProtocolIPv4.Text = "IPv4";
            this.checkProtocolIPv4.UseVisualStyleBackColor = true;
            this.checkProtocolIPv4.CheckedChanged += new System.EventHandler(this.checkProtocol_CheckedChanged);
            // 
            // buttonCheckHost
            // 
            this.buttonCheckHost.Location = new System.Drawing.Point(6, 89);
            this.buttonCheckHost.Name = "buttonCheckHost";
            this.buttonCheckHost.Size = new System.Drawing.Size(100, 28);
            this.buttonCheckHost.TabIndex = 7;
            this.buttonCheckHost.Text = "Check host";
            this.buttonCheckHost.UseVisualStyleBackColor = true;
            this.buttonCheckHost.Click += new System.EventHandler(this.buttonCheckHost_Click);
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefault.Location = new System.Drawing.Point(256, 89);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(100, 28);
            this.buttonDefault.TabIndex = 8;
            this.buttonDefault.Text = "Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // labelBroadcastPort
            // 
            this.labelBroadcastPort.AutoSize = true;
            this.labelBroadcastPort.Location = new System.Drawing.Point(6, 64);
            this.labelBroadcastPort.Name = "labelBroadcastPort";
            this.labelBroadcastPort.Size = new System.Drawing.Size(105, 17);
            this.labelBroadcastPort.TabIndex = 5;
            this.labelBroadcastPort.Text = "Broadcast port:";
            // 
            // textBroadcastPort
            // 
            this.textBroadcastPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastPort.Location = new System.Drawing.Point(156, 61);
            this.textBroadcastPort.MaxLength = 0;
            this.textBroadcastPort.Name = "textBroadcastPort";
            this.textBroadcastPort.Size = new System.Drawing.Size(200, 22);
            this.textBroadcastPort.TabIndex = 6;
            // 
            // textBroadcastAddress
            // 
            this.textBroadcastAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastAddress.Location = new System.Drawing.Point(156, 33);
            this.textBroadcastAddress.MaxLength = 0;
            this.textBroadcastAddress.Name = "textBroadcastAddress";
            this.textBroadcastAddress.Size = new System.Drawing.Size(200, 22);
            this.textBroadcastAddress.TabIndex = 4;
            // 
            // tabs_pagColumns
            // 
            this.tabs_pagColumns.Controls.Add(this.chbNotes);
            this.tabs_pagColumns.Controls.Add(this.chbBroadcastPort);
            this.tabs_pagColumns.Controls.Add(this.chbBroadcastHost);
            this.tabs_pagColumns.Controls.Add(this.chbSecureOn);
            this.tabs_pagColumns.Controls.Add(this.chbMac);
            this.tabs_pagColumns.Controls.Add(this.chbTitle);
            this.tabs_pagColumns.Location = new System.Drawing.Point(4, 25);
            this.tabs_pagColumns.Name = "tabs_pagColumns";
            this.tabs_pagColumns.Padding = new System.Windows.Forms.Padding(3);
            this.tabs_pagColumns.Size = new System.Drawing.Size(362, 168);
            this.tabs_pagColumns.TabIndex = 2;
            this.tabs_pagColumns.Text = "Columns";
            this.tabs_pagColumns.UseVisualStyleBackColor = true;
            // 
            // chbNotes
            // 
            this.chbNotes.AutoSize = true;
            this.chbNotes.Location = new System.Drawing.Point(6, 141);
            this.chbNotes.Name = "chbNotes";
            this.chbNotes.Size = new System.Drawing.Size(67, 21);
            this.chbNotes.TabIndex = 5;
            this.chbNotes.Text = "Notes";
            this.chbNotes.UseVisualStyleBackColor = true;
            // 
            // chbBroadcastPort
            // 
            this.chbBroadcastPort.AutoSize = true;
            this.chbBroadcastPort.Location = new System.Drawing.Point(6, 114);
            this.chbBroadcastPort.Name = "chbBroadcastPort";
            this.chbBroadcastPort.Size = new System.Drawing.Size(123, 21);
            this.chbBroadcastPort.TabIndex = 4;
            this.chbBroadcastPort.Text = "Broadcast port";
            this.chbBroadcastPort.UseVisualStyleBackColor = true;
            // 
            // chbBroadcastHost
            // 
            this.chbBroadcastHost.AutoSize = true;
            this.chbBroadcastHost.Location = new System.Drawing.Point(6, 87);
            this.chbBroadcastHost.Name = "chbBroadcastHost";
            this.chbBroadcastHost.Size = new System.Drawing.Size(125, 21);
            this.chbBroadcastHost.TabIndex = 3;
            this.chbBroadcastHost.Text = "Broadcast host";
            this.chbBroadcastHost.UseVisualStyleBackColor = true;
            // 
            // chbSecureOn
            // 
            this.chbSecureOn.AutoSize = true;
            this.chbSecureOn.Location = new System.Drawing.Point(6, 60);
            this.chbSecureOn.Name = "chbSecureOn";
            this.chbSecureOn.Size = new System.Drawing.Size(158, 21);
            this.chbSecureOn.TabIndex = 2;
            this.chbSecureOn.Text = "SecureOn password";
            this.chbSecureOn.UseVisualStyleBackColor = true;
            // 
            // chbMac
            // 
            this.chbMac.AutoSize = true;
            this.chbMac.Location = new System.Drawing.Point(6, 33);
            this.chbMac.Name = "chbMac";
            this.chbMac.Size = new System.Drawing.Size(114, 21);
            this.chbMac.TabIndex = 1;
            this.chbMac.Text = "MAC address";
            this.chbMac.UseVisualStyleBackColor = true;
            // 
            // chbTitle
            // 
            this.chbTitle.AutoSize = true;
            this.chbTitle.Checked = true;
            this.chbTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbTitle.Enabled = false;
            this.chbTitle.Location = new System.Drawing.Point(6, 6);
            this.chbTitle.Name = "chbTitle";
            this.chbTitle.Size = new System.Drawing.Size(67, 21);
            this.chbTitle.TabIndex = 0;
            this.chbTitle.Text = "Name";
            this.chbTitle.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(394, 266);
            this.Controls.Add(this.tabs);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            this.tabs.ResumeLayout(false);
            this.tabs_pagPacket.ResumeLayout(false);
            this.tabs_pagPacket.PerformLayout();
            this.tabs_pagColumns.ResumeLayout(false);
            this.tabs_pagColumns.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabs_pagPacket;
        private System.Windows.Forms.Button buttonDefault;
        private System.Windows.Forms.Label labelBroadcastPort;
        private System.Windows.Forms.TextBox textBroadcastPort;
        private System.Windows.Forms.TextBox textBroadcastAddress;
        private System.Windows.Forms.TabPage tabs_pagColumns;
        private System.Windows.Forms.CheckBox chbNotes;
        private System.Windows.Forms.CheckBox chbBroadcastPort;
        private System.Windows.Forms.CheckBox chbBroadcastHost;
        private System.Windows.Forms.CheckBox chbSecureOn;
        private System.Windows.Forms.CheckBox chbMac;
        private System.Windows.Forms.CheckBox chbTitle;
        private System.Windows.Forms.Button buttonCheckHost;
        private System.Windows.Forms.CheckBox checkProtocolIPv6;
        private System.Windows.Forms.Label labelProtocol;
        private System.Windows.Forms.CheckBox checkProtocolIPv4;
        private System.Windows.Forms.Label labelBroadcastAddress;
    }
}
