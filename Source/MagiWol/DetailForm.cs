using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class DetailForm : Form {

        public MagiWolDocument.Address Destination { get; private set; }


        public DetailForm(MagiWolDocument.Address destination) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            var fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            this.textMac.Font = fixedSizeFont;
            this.textSecureOn.Font = fixedSizeFont;

            this.Destination = destination;

            //foreach (Control iControl in this.Controls) {
            foreach (Control iControl in new Control[] { textTitle, textMac, textSecureOn, checkBroadcastAddress, checkBroadcastPort }) { //because of Mono
                erp.SetIconPadding(iControl, 4);
                erp.SetIconAlignment(iControl, ErrorIconAlignment.MiddleLeft);
            }
        }

        private void DetailForm_Load(object sender, EventArgs e) {
            if (this.Destination != null) {
                textTitle.Text = this.Destination.Title;
                textMac.Text = this.Destination.Mac;
                textSecureOn.Text = this.Destination.SecureOn;
                checkBroadcastAddress.Checked = this.Destination.IsBroadcastHostValid;
                textBroadcastAddress.Text = this.Destination.BroadcastHost;
                checkBroadcastPort.Checked = this.Destination.IsBroadcastPortValid;
                textBroadcastPort.Text = this.Destination.BroadcastPort.ToString(CultureInfo.InvariantCulture);
                textNotes.Text = this.Destination.Notes;
            } else {
                textBroadcastAddress.Text = Settings.DefaultBroadcastHost;
                textBroadcastPort.Text = Settings.DefaultBroadcastPort.ToString(CultureInfo.InvariantCulture);
            }

            CheckForm();
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            if (this.Destination == null) {
                this.Destination = new MagiWolDocument.Address();
            }

            this.Destination.Title = textTitle.Text;
            this.Destination.Mac = textMac.Text;
            this.Destination.SecureOn = textSecureOn.Text;

            string host;
            if (checkBroadcastAddress.Checked) {
                if (string.IsNullOrEmpty(textBroadcastAddress.Text.Trim()) == false) {
                    host = textBroadcastAddress.Text.Trim();
                    this.Destination.IsBroadcastHostValid = true;
                } else {
                    host = Settings.DefaultBroadcastHost;
                    this.Destination.IsBroadcastHostValid = false;
                }
            } else {
                host = this.Destination.BroadcastHost;
                this.Destination.IsBroadcastHostValid = false;
            }

            int port;
            if (checkBroadcastPort.Checked) {
                if (int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port)) {
                    if ((port >= 0) || (port <= 65535)) {
                        this.Destination.IsBroadcastPortValid = true;
                    } else {
                        port = Settings.DefaultBroadcastPort;
                        this.Destination.IsBroadcastPortValid = false;
                    }
                } else {
                    port = Settings.DefaultBroadcastPort;
                    this.Destination.IsBroadcastPortValid = false;
                }
            } else {
                port = this.Destination.BroadcastPort;
                this.Destination.IsBroadcastPortValid = false;
            }

            this.Destination.BroadcastHost = host;
            this.Destination.BroadcastPort = port;

            this.Destination.Notes = textNotes.Text;
        }

        private void checkBroadcastAddress_CheckedChanged(object sender, EventArgs e) {
            textBroadcastAddress.Enabled = checkBroadcastAddress.Checked;
        }

        private void checkBroadcastPort_CheckedChanged(object sender, EventArgs e) {
            textBroadcastPort.Enabled = checkBroadcastPort.Checked;
        }


        private void textBroadcastAddress_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(textBroadcastAddress.Text)) {
                if (this.Destination != null) {
                    textBroadcastAddress.Text = this.Destination.BroadcastHost;
                } else {
                    textBroadcastAddress.Text = Settings.DefaultBroadcastHost;
                }
            }
        }

        private void textBroadcastPort_Validating(object sender, CancelEventArgs e) {
            int port;
            if (!(int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port) && (port >= 0) && (port <= 65535))) {
                if (this.Destination != null) {
                    textBroadcastPort.Text = this.Destination.BroadcastPort.ToString(CultureInfo.InvariantCulture);
                } else {
                    textBroadcastPort.Text = Settings.DefaultBroadcastPort.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void DetailForm_Shown(object sender, EventArgs e) {
            textTitle.SelectAll();
        }

        private void textTitle_TextChanged(object sender, EventArgs e) {
            CheckForm();
        }

        private void buttonTest_Click(object sender, EventArgs e) {
            try {
                try {
                    Cursor.Current = Cursors.WaitCursor;
                    try {
                        Magic.SendMagicPacket(textMac.Text, textSecureOn.Text, textBroadcastAddress.Text, checkBroadcastAddress.Checked, textBroadcastPort.Text, checkBroadcastPort.Checked);
                    } catch (InvalidOperationException ex) {
                        Medo.MessageBox.ShowError(this, ex.Message);
                    }
                } finally {
                    Cursor.Current = Cursors.Default;
                }
            } catch (FormatException ex) {
                Medo.MessageBox.ShowError(this, string.Format("Invalid format.\n\n{0}", ex.Message));
            } catch (SocketException sex) {
                Medo.MessageBox.ShowError(this, string.Format("Socket exception occurred.\n\n{0}", sex.Message));
            }
        }


        private void textMac_TextChanged(object sender, EventArgs e) {
            CheckForm();
        }

        private void textSecureOn_TextChanged(object sender, EventArgs e) {
            CheckForm();
        }

        private void textBroadcastAddress_TextChanged(object sender, EventArgs e) {
            CheckForm();
        }

        private void textBroadcastPort_TextChanged(object sender, EventArgs e) {
            CheckForm();
        }


        private void CheckForm() {
            if (textTitle.Text.Length == 0) {
                erp.SetError(textTitle, "Text cannot be empty.");
            } else {
                erp.SetError(textTitle, null);
            }

            if (!Medo.Net.WakeOnLan.IsMacAddressValid(textMac.Text)) {
                erp.SetError(textMac, "MAC address is not valid.");
            } else {
                erp.SetError(textMac, null);
            }

            if (!Medo.Net.WakeOnLan.IsSecureOnPasswordValid(textSecureOn.Text)) {
                erp.SetError(textSecureOn, "SecureOn password is not valid.");
            } else {
                erp.SetError(textSecureOn, null);
            }

            if (checkBroadcastAddress.Checked) {
                if (string.IsNullOrEmpty(textBroadcastAddress.Text.Trim())) {
                    erp.SetError(checkBroadcastAddress, "Host cannot be empty.");
                } else {
                    erp.SetError(checkBroadcastAddress, null);
                }
            } else {
                erp.SetError(checkBroadcastAddress, null);
            }

            int port;
            if (checkBroadcastPort.Checked) {
                if (int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port) && ((port >= 0) && (port <= 65535))) {
                    erp.SetError(checkBroadcastPort, null);
                } else {
                    erp.SetError(checkBroadcastPort, "Port is not valid.");
                }
            } else {
                erp.SetError(checkBroadcastPort, null);
            }

            buttonOk.Enabled = (textTitle.Text.Length > 0) && (Medo.Net.WakeOnLan.IsMacAddressValid(textMac.Text));
            buttonTest.Enabled = (Medo.Net.WakeOnLan.IsMacAddressValid(textMac.Text));
        }

        private void textNotes_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyData == (Keys.Control | Keys.A)) {
                textNotes.SelectAll();
            }
        }

    }
}
