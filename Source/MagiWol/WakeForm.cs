using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class WakeForm : Form {

        private IList<MagiWolDocument.AddressItem> _addresses;
        Font _fixedSizeFont;

        public WakeForm(IList<MagiWolDocument.AddressItem> addresses) {
            this._fixedSizeFont = new Font("Courier New", SystemFonts.MessageBoxFont.Size, SystemFonts.MessageBoxFont.Style);

            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            this._addresses = addresses;

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
            nudPause.Value = Settings.Runtime.WolWaitBetweenComputersIntervalSeconds;
        }

        private void WakeForm_Resize(object sender, EventArgs e) {
            var macWidth = this.CreateGraphics().MeasureString(" XX-XX-XX-XX-XX-XX ", this._fixedSizeFont).ToSize().Width; //Mono CreateGraphics has problem without visible form.
            list.Columns[0].Width = list.Width - macWidth - SystemInformation.VerticalScrollBarWidth - 2 * SystemInformation.Border3DSize.Width;
            list.Columns[1].Width = macWidth;
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            using (var form = new WakeProgressForm(this._addresses, (int)nudPause.Value)) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    Settings.Runtime.WolWaitBetweenComputersIntervalSeconds = (int)nudPause.Value;
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void WakeForm_Shown(object sender, EventArgs e) { //because of Mono compatiblity
            WakeForm_Resize(null, null);
        }

    }
}
