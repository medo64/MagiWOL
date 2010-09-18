namespace MagiWol
{
    partial class DetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailForm));
            this.labelName = new System.Windows.Forms.Label();
            this.textTitle = new System.Windows.Forms.TextBox();
            this.labelMac = new System.Windows.Forms.Label();
            this.labelSecureOn = new System.Windows.Forms.Label();
            this.textBroadcastAddress = new System.Windows.Forms.TextBox();
            this.labelNotes = new System.Windows.Forms.Label();
            this.labelBroadcastAddress = new System.Windows.Forms.Label();
            this.textNotes = new System.Windows.Forms.TextBox();
            this.textBroadcastPort = new System.Windows.Forms.TextBox();
            this.labelBroadcastPort = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBroadcastAddress = new System.Windows.Forms.CheckBox();
            this.checkBroadcastPort = new System.Windows.Forms.CheckBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.erp = new System.Windows.Forms.ErrorProvider(this.components);
            this.textSecureOn = new MagiWol.SecurBox();
            this.textMac = new MagiWol.MacBox();
            ((System.ComponentModel.ISupportInitialize)(this.erp)).BeginInit();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(49, 17);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // textTitle
            // 
            this.textTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textTitle.Location = new System.Drawing.Point(182, 10);
            this.textTitle.MaxLength = 0;
            this.textTitle.Name = "textTitle";
            this.textTitle.Size = new System.Drawing.Size(220, 22);
            this.textTitle.TabIndex = 1;
            this.textTitle.TextChanged += new System.EventHandler(this.textTitle_TextChanged);
            // 
            // labelMac
            // 
            this.labelMac.AutoSize = true;
            this.labelMac.Location = new System.Drawing.Point(12, 43);
            this.labelMac.Name = "labelMac";
            this.labelMac.Size = new System.Drawing.Size(96, 17);
            this.labelMac.TabIndex = 2;
            this.labelMac.Text = "MAC address:";
            // 
            // labelSecureOn
            // 
            this.labelSecureOn.AutoSize = true;
            this.labelSecureOn.Location = new System.Drawing.Point(12, 71);
            this.labelSecureOn.Name = "labelSecureOn";
            this.labelSecureOn.Size = new System.Drawing.Size(140, 17);
            this.labelSecureOn.TabIndex = 4;
            this.labelSecureOn.Text = "SecureOn password:";
            // 
            // textBroadcastAddress
            // 
            this.textBroadcastAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastAddress.Enabled = false;
            this.textBroadcastAddress.Location = new System.Drawing.Point(206, 96);
            this.textBroadcastAddress.MaxLength = 0;
            this.textBroadcastAddress.Name = "textBroadcastAddress";
            this.textBroadcastAddress.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastAddress.TabIndex = 8;
            this.textBroadcastAddress.TextChanged += new System.EventHandler(this.textBroadcastAddress_TextChanged);
            this.textBroadcastAddress.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastAddress_Validating);
            // 
            // labelNotes
            // 
            this.labelNotes.AutoSize = true;
            this.labelNotes.Location = new System.Drawing.Point(12, 155);
            this.labelNotes.Name = "labelNotes";
            this.labelNotes.Size = new System.Drawing.Size(49, 17);
            this.labelNotes.TabIndex = 12;
            this.labelNotes.Text = "Notes:";
            // 
            // labelBroadcastAddress
            // 
            this.labelBroadcastAddress.AutoSize = true;
            this.labelBroadcastAddress.Location = new System.Drawing.Point(12, 99);
            this.labelBroadcastAddress.Name = "labelBroadcastAddress";
            this.labelBroadcastAddress.Size = new System.Drawing.Size(131, 17);
            this.labelBroadcastAddress.TabIndex = 6;
            this.labelBroadcastAddress.Text = "Broadcast address:";
            // 
            // textNotes
            // 
            this.textNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textNotes.Location = new System.Drawing.Point(182, 152);
            this.textNotes.MaxLength = 0;
            this.textNotes.Multiline = true;
            this.textNotes.Name = "textNotes";
            this.textNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textNotes.Size = new System.Drawing.Size(220, 54);
            this.textNotes.TabIndex = 13;
            this.textNotes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textNotes_KeyDown);
            // 
            // textBroadcastPort
            // 
            this.textBroadcastPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBroadcastPort.Enabled = false;
            this.textBroadcastPort.Location = new System.Drawing.Point(206, 124);
            this.textBroadcastPort.MaxLength = 0;
            this.textBroadcastPort.Name = "textBroadcastPort";
            this.textBroadcastPort.Size = new System.Drawing.Size(196, 22);
            this.textBroadcastPort.TabIndex = 11;
            this.textBroadcastPort.TextChanged += new System.EventHandler(this.textBroadcastPort_TextChanged);
            this.textBroadcastPort.Validating += new System.ComponentModel.CancelEventHandler(this.textBroadcastPort_Validating);
            // 
            // labelBroadcastPort
            // 
            this.labelBroadcastPort.AutoSize = true;
            this.labelBroadcastPort.Location = new System.Drawing.Point(12, 127);
            this.labelBroadcastPort.Name = "labelBroadcastPort";
            this.labelBroadcastPort.Size = new System.Drawing.Size(105, 17);
            this.labelBroadcastPort.TabIndex = 9;
            this.labelBroadcastPort.Text = "Broadcast port:";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(196, 224);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 15;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(302, 224);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // checkBroadcastAddress
            // 
            this.checkBroadcastAddress.AutoSize = true;
            this.checkBroadcastAddress.Location = new System.Drawing.Point(182, 99);
            this.checkBroadcastAddress.Name = "checkBroadcastAddress";
            this.checkBroadcastAddress.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastAddress.TabIndex = 7;
            this.checkBroadcastAddress.UseVisualStyleBackColor = true;
            this.checkBroadcastAddress.CheckedChanged += new System.EventHandler(this.checkBroadcastAddress_CheckedChanged);
            // 
            // checkBroadcastPort
            // 
            this.checkBroadcastPort.AutoSize = true;
            this.checkBroadcastPort.Location = new System.Drawing.Point(182, 128);
            this.checkBroadcastPort.Name = "checkBroadcastPort";
            this.checkBroadcastPort.Size = new System.Drawing.Size(18, 17);
            this.checkBroadcastPort.TabIndex = 10;
            this.checkBroadcastPort.UseVisualStyleBackColor = true;
            this.checkBroadcastPort.CheckedChanged += new System.EventHandler(this.checkBroadcastPort_CheckedChanged);
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTest.Location = new System.Drawing.Point(12, 224);
            this.buttonTest.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(100, 28);
            this.buttonTest.TabIndex = 14;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
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
            this.textSecureOn.Location = new System.Drawing.Point(182, 68);
            this.textSecureOn.MaxLength = 0;
            this.textSecureOn.Name = "textSecureOn";
            this.textSecureOn.Size = new System.Drawing.Size(220, 22);
            this.textSecureOn.TabIndex = 5;
            this.textSecureOn.TextChanged += new System.EventHandler(this.textSecureOn_TextChanged);
            // 
            // textMac
            // 
            this.textMac.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textMac.Location = new System.Drawing.Point(182, 40);
            this.textMac.MaxLength = 0;
            this.textMac.Name = "textMac";
            this.textMac.Size = new System.Drawing.Size(220, 22);
            this.textMac.TabIndex = 3;
            this.textMac.TextChanged += new System.EventHandler(this.textMac_TextChanged);
            // 
            // DetailForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(414, 264);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.checkBroadcastPort);
            this.Controls.Add(this.checkBroadcastAddress);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelBroadcastPort);
            this.Controls.Add(this.textBroadcastPort);
            this.Controls.Add(this.textNotes);
            this.Controls.Add(this.labelBroadcastAddress);
            this.Controls.Add(this.textBroadcastAddress);
            this.Controls.Add(this.labelNotes);
            this.Controls.Add(this.textSecureOn);
            this.Controls.Add(this.labelSecureOn);
            this.Controls.Add(this.textMac);
            this.Controls.Add(this.labelMac);
            this.Controls.Add(this.textTitle);
            this.Controls.Add(this.labelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Details";
            this.Load += new System.EventHandler(this.DetailForm_Load);
            this.Shown += new System.EventHandler(this.DetailForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.erp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textTitle;
        private MacBox textMac;
        private System.Windows.Forms.Label labelMac;
        private SecurBox textSecureOn;
        private System.Windows.Forms.Label labelSecureOn;
        private System.Windows.Forms.TextBox textBroadcastAddress;
        private System.Windows.Forms.Label labelNotes;
        private System.Windows.Forms.Label labelBroadcastAddress;
        private System.Windows.Forms.TextBox textNotes;
        private System.Windows.Forms.TextBox textBroadcastPort;
        private System.Windows.Forms.Label labelBroadcastPort;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBroadcastAddress;
        private System.Windows.Forms.CheckBox checkBroadcastPort;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider erp;
    }
}