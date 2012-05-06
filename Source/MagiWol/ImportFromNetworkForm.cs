using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class ImportFromNetworkForm : Form {

        public IList<MagiWolDocument.Address> ImportedAddresses { get; private set; }

        public ImportFromNetworkForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }


        private void Form_Load(object sender, EventArgs e) {
            textList.Text = Settings.LastImportText;
        }


        private void buttonImport_Click(object sender, EventArgs e) {
            Settings.LastImportText = textList.Text;
            using (var form = new ImportFromNetworkProgressForm(textList.Text)) {
                if (form.ShowDialog(this) == DialogResult.OK) {
                    using (var resultsForm = new ImportResultsForm(form.ParsedAddresses)) {
                        if (resultsForm.ShowDialog(this) == DialogResult.OK) {
                            this.ImportedAddresses = form.ParsedAddresses;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
        }

        private void textList_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                textList.SelectAll();
            }
        }

    }
}
