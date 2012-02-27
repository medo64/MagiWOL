namespace MagiWol
{
    partial class QuickWakeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuickWakeForm));
            this.labelMac = new System.Windows.Forms.Label();
            this.labelSecureOn = new System.Windows.Forms.Label();
            this.textBroadcastAddress = new System.Windows.Forms.TextBox();
            this.labelBroadcastAddress = new System.Windows.Forms.Label();
            this.textBroadcastPort = new System.Windows.Forms.TextBox();
            this.labelBroadcastPort = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBroadcastAddress = new System.Windows.Forms.CheckBox();
            this.checkBroadcastPort = new System.Windows.Forms.CheckBox();
            this.buttonWake = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.textSecureOn = new MagiWol.SecurBox();
            this.textMac = new MagiWol.MacBox();
            this.checkProtocolIPv4 = new System.Windows.Forms.CheckBox();
            this.labelProtocol = new System.Windows.Forms.Label();
            this.checkProtocolIPv6 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMac
            // 
            this.labelMac.AutoSize = true;
            this.labelMac.Location = new System.Drawing.Point(12, 15);
            this.labelMac.Name = "labelMac";
            this.labelMac.Size = new System.Drawing.Size(96, 17);
            this.labelMac.TabIndex = 0;
            this.labelMac.Text = "MAC address:";
            // 
            // labelSecureOn
            // 
            this.labelSecureOn.AutoSize = true;
            this.labelSecureOn.Location = new System.Drawing.Point(12, 43);
            this.labelSecureOn.Name = "labelSecureOn";
            this.labelSecureOn.Size = new System.Drawing.Size(140, 17);
            this.labelSecureOn.TabIndex = 2;
            this.labelSecureOn.Text = "SecureOn password:";
            // 
            // textBroadcastAddress
            // 
            this.textBroadcastAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastAddress.Enabled = false;
            this.textBroadcastAddress.Location = new System.Drawing.Point(206, 95);
            this.textBroadcastAddress.MaxLength = 0;
            this.textBroadcastAddress.Name = "textBroadcastAddress";
            this.textBroadcastAddress.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastAddress.TabIndex = 9;
            this.textBroadcastAddress.TextChanged += new System.EventHandler(this.textBroadcastAddress_TextChanged);
            this.textBroadcastAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastAddress_Validating);
            // 
            // labelBroadcastAddress
            // 
            this.labelBroadcastAddress.AutoSize = true;
            this.labelBroadcastAddress.Location = new System.Drawing.Point(12, 98);
            this.labelBroadcastAddress.Name = "labelBroadcastAddress";
            this.labelBroadcastAddress.Size = new System.Drawing.Size(131, 17);
            this.labelBroadcastAddress.TabIndex = 7;
            this.labelBroadcastAddress.Text = "Broadcast address:";
            // 
            // textBroadcastPort
            // 
            this.textBroadcastPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastPort.Enabled = false;
            this.textBroadcastPort.Location = new System.Drawing.Point(206, 123);
            this.textBroadcastPort.MaxLength = 0;
            this.textBroadcastPort.Name = "textBroadcastPort";
            this.textBroadcastPort.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastPort.TabIndex = 12;
            this.textBroadcastPort.TextChanged += new System.EventHandler(this.textBroadcastPort_TextChanged);
            this.textBroadcastPort.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastPort_Validating);
            // 
            // labelBroadcastPort
            // 
            this.labelBroadcastPort.AutoSize = true;
            this.labelBroadcastPort.Location = new System.Drawing.Point(12, 126);
            this.labelBroadcastPort.Name = "labelBroadcastPort";
            this.labelBroadcastPort.Size = new System.Drawing.Size(105, 17);
            this.labelBroadcastPort.TabIndex = 10;
            this.labelBroadcastPort.Text = "Broadcast port:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(302, 163);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 28);
            this.buttonClose.TabIndex = 14;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // checkBroadcastAddress
            // 
            this.checkBroadcastAddress.AutoSize = true;
            this.checkBroadcastAddress.Location = new System.Drawing.Point(182, 98);
            this.checkBroadcastAddress.Name = "checkBroadcastAddress";
            this.checkBroadcastAddress.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastAddress.TabIndex = 8;
            this.checkBroadcastAddress.UseVisualStyleBackColor = true;
            this.checkBroadcastAddress.CheckedChanged += new System.EventHandler(this.checkBroadcastAddress_CheckedChanged);
            // 
            // checkBroadcastPort
            // 
            this.checkBroadcastPort.AutoSize = true;
            this.checkBroadcastPort.Location = new System.Drawing.Point(182, 127);
            this.checkBroadcastPort.Name = "checkBroadcastPort";
            this.checkBroadcastPort.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastPort.TabIndex = 11;
            this.checkBroadcastPort.UseVisualStyleBackColor = true;
            this.checkBroadcastPort.CheckedChanged += new System.EventHandler(this.checkBroadcastPort_CheckedChanged);
            // 
            // buttonWake
            // 
            this.buttonWake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWake.Location = new System.Drawing.Point(196, 163);
            this.buttonWake.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonWake.Name = "buttonWake";
            this.buttonWake.Size = new System.Drawing.Size(100, 28);
            this.buttonWake.TabIndex = 13;
            this.buttonWake.Text = "Wake";
            this.buttonWake.UseVisualStyleBackColor = true;
            this.buttonWake.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // erp
            // 
            this.erp.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.erp.ContainerControl = this;
            this.erp.Icon = ((System.Drawing.Icon)(resources.GetObject("erp.Icon")));
            // 
            // textSecureOn
            // 
            this.textSecureOn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSecureOn.Location = new System.Drawing.Point(182, 40);
            this.textSecureOn.MaxLength = 0;
            this.textSecureOn.Name = "textSecureOn";
            this.textSecureOn.Size = new System.Drawing.Size(220, 22);
            this.textSecureOn.TabIndex = 3;
            this.textSecureOn.TextChanged += new System.EventHandler(this.textSecureOn_TextChanged);
            // 
            // textMac
            // 
            this.textMac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textMac.Location = new System.Drawing.Point(182, 12);
            this.textMac.MaxLength = 0;
            this.textMac.Name = "textMac";
            this.textMac.Size = new System.Drawing.Size(220, 22);
            this.textMac.TabIndex = 1;
            this.textMac.TextChanged += new System.EventHandler(this.textMac_TextChanged);
            // 
            // checkProtocolIPv4
            // 
            this.checkProtocolIPv4.AutoSize = true;
            this.checkProtocolIPv4.Location = new System.Drawing.Point(182, 68);
            this.checkProtocolIPv4.Name = "checkProtocolIPv4";
            this.checkProtocolIPv4.Size = new System.Drawing.Size(57, 21);
            this.checkProtocolIPv4.TabIndex = 5;
            this.checkProtocolIPv4.Text = "IPv4";
            this.checkProtocolIPv4.UseVisualStyleBackColor = true;
            this.checkProtocolIPv4.CheckedChanged += new System.EventHandler(this.checkProtocol_CheckedChanged);
            // 
            // labelProtocol
            // 
            this.labelProtocol.AutoSize = true;
            this.labelProtocol.Location = new System.Drawing.Point(12, 69);
            this.labelProtocol.Name = "labelProtocol";
            this.labelProtocol.Size = new System.Drawing.Size(64, 17);
            this.labelProtocol.TabIndex = 4;
            this.labelProtocol.Text = "Protocol:";
            // 
            // checkProtocolIPv6
            // 
            this.checkProtocolIPv6.AutoSize = true;
            this.checkProtocolIPv6.Location = new System.Drawing.Point(245, 68);
            this.checkProtocolIPv6.Name = "checkProtocolIPv6";
            this.checkProtocolIPv6.Size = new System.Drawing.Size(57, 21);
            this.checkProtocolIPv6.TabIndex = 6;
            this.checkProtocolIPv6.Text = "IPv6";
            this.checkProtocolIPv6.UseVisualStyleBackColor = true;
            this.checkProtocolIPv6.CheckedChanged += new System.EventHandler(this.checkProtocol_CheckedChanged);
            // 
            // QuickWakeForm
            // 
            this.AcceptButton = this.buttonWake;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(414, 203);
            this.Controls.Add(this.checkProtocolIPv6);
            this.Controls.Add(this.labelProtocol);
            this.Controls.Add(this.checkProtocolIPv4);
            this.Controls.Add(this.buttonWake);
            this.Controls.Add(this.checkBroadcastPort);
            this.Controls.Add(this.checkBroadcastAddress);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelBroadcastPort);
            this.Controls.Add(this.textBroadcastPort);
            this.Controls.Add(this.labelBroadcastAddress);
            this.Controls.Add(this.textBroadcastAddress);
            this.Controls.Add(this.textSecureOn);
            this.Controls.Add(this.labelSecureOn);
            this.Controls.Add(this.textMac);
            this.Controls.Add(this.labelMac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickWakeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick wake";
            this.Load += new System.EventHandler(this.DetailForm_Load);
            this.Shown += new System.EventHandler(this.DetailForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MacBox textMac;
        private System.Windows.Forms.Label labelMac;
        private SecurBox textSecureOn;
        private System.Windows.Forms.Label labelSecureOn;
        private System.Windows.Forms.TextBox textBroadcastAddress;
        private System.Windows.Forms.Label labelBroadcastAddress;
        private System.Windows.Forms.TextBox textBroadcastPort;
        private System.Windows.Forms.Label labelBroadcastPort;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBroadcastAddress;
        private System.Windows.Forms.CheckBox checkBroadcastPort;
        private System.Windows.Forms.Button buttonWake;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider erp;
        private System.Windows.Forms.CheckBox checkProtocolIPv6;
        private System.Windows.Forms.Label labelProtocol;
        private System.Windows.Forms.CheckBox checkProtocolIPv4;
    }
}