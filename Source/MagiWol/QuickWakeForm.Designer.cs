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
            this.textBroadcastAddress.Location = new System.Drawing.Point(206, 68);
            this.textBroadcastAddress.MaxLength = 0;
            this.textBroadcastAddress.Name = "textBroadcastAddress";
            this.textBroadcastAddress.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastAddress.TabIndex = 6;
            this.textBroadcastAddress.TextChanged += new System.EventHandler(this.textBroadcastAddress_TextChanged);
            this.textBroadcastAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastAddress_Validating);
            // 
            // labelBroadcastAddress
            // 
            this.labelBroadcastAddress.AutoSize = true;
            this.labelBroadcastAddress.Location = new System.Drawing.Point(12, 71);
            this.labelBroadcastAddress.Name = "labelBroadcastAddress";
            this.labelBroadcastAddress.Size = new System.Drawing.Size(131, 17);
            this.labelBroadcastAddress.TabIndex = 4;
            this.labelBroadcastAddress.Text = "Broadcast address:";
            // 
            // textBroadcastPort
            // 
            this.textBroadcastPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastPort.Enabled = false;
            this.textBroadcastPort.Location = new System.Drawing.Point(206, 96);
            this.textBroadcastPort.MaxLength = 0;
            this.textBroadcastPort.Name = "textBroadcastPort";
            this.textBroadcastPort.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastPort.TabIndex = 9;
            this.textBroadcastPort.TextChanged += new System.EventHandler(this.textBroadcastPort_TextChanged);
            this.textBroadcastPort.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastPort_Validating);
            // 
            // labelBroadcastPort
            // 
            this.labelBroadcastPort.AutoSize = true;
            this.labelBroadcastPort.Location = new System.Drawing.Point(12, 99);
            this.labelBroadcastPort.Name = "labelBroadcastPort";
            this.labelBroadcastPort.Size = new System.Drawing.Size(105, 17);
            this.labelBroadcastPort.TabIndex = 7;
            this.labelBroadcastPort.Text = "Broadcast port:";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(302, 136);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(100, 28);
            this.buttonClose.TabIndex = 11;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            // 
            // checkBroadcastAddress
            // 
            this.checkBroadcastAddress.AutoSize = true;
            this.checkBroadcastAddress.Location = new System.Drawing.Point(182, 71);
            this.checkBroadcastAddress.Name = "checkBroadcastAddress";
            this.checkBroadcastAddress.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastAddress.TabIndex = 5;
            this.checkBroadcastAddress.UseVisualStyleBackColor = true;
            this.checkBroadcastAddress.CheckedChanged += new System.EventHandler(this.checkBroadcastAddress_CheckedChanged);
            // 
            // checkBroadcastPort
            // 
            this.checkBroadcastPort.AutoSize = true;
            this.checkBroadcastPort.Location = new System.Drawing.Point(182, 100);
            this.checkBroadcastPort.Name = "checkBroadcastPort";
            this.checkBroadcastPort.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastPort.TabIndex = 8;
            this.checkBroadcastPort.UseVisualStyleBackColor = true;
            this.checkBroadcastPort.CheckedChanged += new System.EventHandler(this.checkBroadcastPort_CheckedChanged);
            // 
            // buttonWake
            // 
            this.buttonWake.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWake.Location = new System.Drawing.Point(196, 136);
            this.buttonWake.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonWake.Name = "buttonWake";
            this.buttonWake.Size = new System.Drawing.Size(100, 28);
            this.buttonWake.TabIndex = 10;
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
            // QuickWakeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(414, 176);
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
    }
}