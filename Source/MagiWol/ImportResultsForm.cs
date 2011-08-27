using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class ImportResultsForm : Form {

        Font _fixedSizeFont;

        public ImportResultsForm(IList<MagiWolDocument.Address> addresses) {
            this._fixedSizeFont = new Font("Courier New", SystemFonts.MessageBoxFont.Size, SystemFonts.MessageBoxFont.Style);

            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;


            foreach (var iAddress in addresses) {
                var item = new ListViewItem();
                item.UseItemStyleForSubItems = false;
                item.Text = iAddress.Text;
                item.SubItems.Add(iAddress.Mac);
                item.SubItems[1].Font = this._fixedSizeFont;
                list.Items.Add(item);
            }
        }

        private void WakeForm_Load(object sender, EventArgs e) {

        }

        private void WakeForm_Resize(object sender, EventArgs e) {
            list.Columns[1].Width = this.CreateGraphics().MeasureString(" XX-XX-XX-XX-XX-XX ", this._fixedSizeFont).ToSize().Width;
            list.Columns[0].Width = list.Width - list.Columns[1].Width - SystemInformation.VerticalScrollBarWidth - 2 * SystemInformation.Border3DSize.Width;
        }

    }
}
