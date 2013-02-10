namespace MagiWol {
    partial class WakeForm {
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
                _fixedSizeFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.list = new System.Windows.Forms.ListView();
            this.list_title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.list_Mac = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupOptions = new System.Windows.Forms.GroupBox();
            this.labelPause = new System.Windows.Forms.Label();
            this.nudPause = new System.Windows.Forms.NumericUpDown();
            this.groupOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPause)).BeginInit();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.list_title,
            this.list_Mac});
            this.list.FullRowSelect = true;
            this.list.GridLines = true;
            this.list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.list.Location = new System.Drawing.Point(12, 12);
            this.list.Name = "list";
            this.list.Size = new System.Drawing.Size(330, 259);
            this.list.TabIndex = 2;
            this.list.UseCompatibleStateImageBehavior = false;
            this.list.View = System.Windows.Forms.View.Details;
            // 
            // list_title
            // 
            this.list_title.Text = "Title";
            this.list_title.Width = 120;
            // 
            // list_Mac
            // 
            this.list_Mac.Text = "MAC";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(242, 347);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 28);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(136, 347);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 28);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Wake";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupOptions
            // 
            this.groupOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupOptions.Controls.Add(this.labelPause);
            this.groupOptions.Controls.Add(this.nudPause);
            this.groupOptions.Location = new System.Drawing.Point(12, 277);
            this.groupOptions.Name = "groupOptions";
            this.groupOptions.Size = new System.Drawing.Size(330, 49);
            this.groupOptions.TabIndex = 5;
            this.groupOptions.TabStop = false;
            this.groupOptions.Text = "Options";
            // 
            // labelPause
            // 
            this.labelPause.AutoSize = true;
            this.labelPause.Location = new System.Drawing.Point(6, 23);
            this.labelPause.Name = "labelPause";
            this.labelPause.Size = new System.Drawing.Size(219, 17);
            this.labelPause.TabIndex = 6;
            this.labelPause.Text = "Pause between wakes (seconds):";
            // 
            // nudPause
            // 
            this.nudPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPause.Location = new System.Drawing.Point(264, 21);
            this.nudPause.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudPause.Name = "nudPause";
            this.nudPause.Size = new System.Drawing.Size(60, 22);
            this.nudPause.TabIndex = 5;
            this.nudPause.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // WakeForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(354, 387);
            this.Controls.Add(this.groupOptions);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WakeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wake";
            this.Load += new System.EventHandler(this.WakeForm_Load);
            this.Shown += new System.EventHandler(this.WakeForm_Shown);
            this.Resize += new System.EventHandler(this.WakeForm_Resize);
            this.groupOptions.ResumeLayout(false);
            this.groupOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPause)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView list;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.ColumnHeader list_title;
        private System.Windows.Forms.ColumnHeader list_Mac;
        private System.Windows.Forms.GroupBox groupOptions;
        private System.Windows.Forms.Label labelPause;
        private System.Windows.Forms.NumericUpDown nudPause;
    }
}