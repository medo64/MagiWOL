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
            this.chbShowMenu = new System.Windows.Forms.CheckBox();
            this.groupPacketEndPoint = new System.Windows.Forms.GroupBox();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.labelBroadcastPort = new System.Windows.Forms.Label();
            this.textBroadcastPort = new System.Windows.Forms.TextBox();
            this.labelBroadcastAddress = new System.Windows.Forms.Label();
            this.textBroadcastAddress = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupPacketEndPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            this.SuspendLayout();
            // 
            // chbShowMenu
            // 
            this.chbShowMenu.AutoSize = true;
            this.chbShowMenu.Location = new System.Drawing.Point(12, 141);
            this.chbShowMenu.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.chbShowMenu.Name = "chbShowMenu";
            this.chbShowMenu.Size = new System.Drawing.Size(103, 21);
            this.chbShowMenu.TabIndex = 1;
            this.chbShowMenu.Text = "Show menu";
            this.chbShowMenu.UseVisualStyleBackColor = true;
            // 
            // groupPacketEndPoint
            // 
            this.groupPacketEndPoint.Controls.Add(this.buttonDefault);
            this.groupPacketEndPoint.Controls.Add(this.labelBroadcastPort);
            this.groupPacketEndPoint.Controls.Add(this.textBroadcastPort);
            this.groupPacketEndPoint.Controls.Add(this.labelBroadcastAddress);
            this.groupPacketEndPoint.Controls.Add(this.textBroadcastAddress);
            this.groupPacketEndPoint.Location = new System.Drawing.Point(12, 12);
            this.groupPacketEndPoint.Name = "groupPacketEndPoint";
            this.groupPacketEndPoint.Size = new System.Drawing.Size(370, 111);
            this.groupPacketEndPoint.TabIndex = 0;
            this.groupPacketEndPoint.TabStop = false;
            this.groupPacketEndPoint.Text = "WOL packet end-point";
            // 
            // buttonDefault
            // 
            this.buttonDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDefault.Location = new System.Drawing.Point(264, 77);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(100, 28);
            this.buttonDefault.TabIndex = 4;
            this.buttonDefault.Text = "Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelBroadcastPort
            // 
            this.labelBroadcastPort.AutoSize = true;
            this.labelBroadcastPort.Location = new System.Drawing.Point(6, 52);
            this.labelBroadcastPort.Name = "labelBroadcastPort";
            this.labelBroadcastPort.Size = new System.Drawing.Size(105, 17);
            this.labelBroadcastPort.TabIndex = 2;
            this.labelBroadcastPort.Text = "Broadcast port:";
            // 
            // textBroadcastPort
            // 
            this.textBroadcastPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastPort.Location = new System.Drawing.Point(164, 49);
            this.textBroadcastPort.MaxLength = 0;
            this.textBroadcastPort.Name = "textBroadcastPort";
            this.textBroadcastPort.Size = new System.Drawing.Size(200, 22);
            this.textBroadcastPort.TabIndex = 3;
            this.textBroadcastPort.TextChanged += new System.EventHandler(this.textBroadcastPort_TextChanged);
            this.textBroadcastPort.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastPort_Validating);
            // 
            // labelBroadcastAddress
            // 
            this.labelBroadcastAddress.AutoSize = true;
            this.labelBroadcastAddress.Location = new System.Drawing.Point(6, 24);
            this.labelBroadcastAddress.Name = "labelBroadcastAddress";
            this.labelBroadcastAddress.Size = new System.Drawing.Size(131, 17);
            this.labelBroadcastAddress.TabIndex = 0;
            this.labelBroadcastAddress.Text = "Broadcast address:";
            // 
            // textBroadcastAddress
            // 
            this.textBroadcastAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastAddress.Location = new System.Drawing.Point(164, 21);
            this.textBroadcastAddress.MaxLength = 0;
            this.textBroadcastAddress.Name = "textBroadcastAddress";
            this.textBroadcastAddress.Size = new System.Drawing.Size(200, 22);
            this.textBroadcastAddress.TabIndex = 1;
            this.textBroadcastAddress.TextChanged += new System.EventHandler(this.textBroadcastAddress_TextChanged);
            this.textBroadcastAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastAddress_Validating);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(282, 180);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(176, 180);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 2;
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
            // SettingsForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(394, 220);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupPacketEndPoint);
            this.Controls.Add(this.chbShowMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupPacketEndPoint.ResumeLayout(false);
            this.groupPacketEndPoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbShowMenu;
        private System.Windows.Forms.GroupBox groupPacketEndPoint;
        private System.Windows.Forms.Label labelBroadcastPort;
        private System.Windows.Forms.TextBox textBroadcastPort;
        private System.Windows.Forms.Label labelBroadcastAddress;
        private System.Windows.Forms.TextBox textBroadcastAddress;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.Button buttonDefault;
    }
}