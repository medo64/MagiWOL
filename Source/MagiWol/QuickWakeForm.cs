using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Net;

namespace MagiWol {
    internal partial class QuickWakeForm : Form {

        public QuickWakeForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            var fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            this.textMac.Font = fixedSizeFont;
            this.textSecureOn.Font = fixedSizeFont;

            //foreach (Control iControl in this.Controls) {
            foreach (Control iControl in new Control[] { textMac, textSecureOn, checkBroadcastAddress, checkBroadcastPort }) { //because of Mono
                erp.SetIconPadding(iControl, 4);
                erp.SetIconAlignment(iControl, ErrorIconAlignment.MiddleLeft);
            }
        }

        public QuickWakeForm(string defaultMac)
            : this() {
            this.textMac.Text = defaultMac;
        }


        private void DetailForm_Load(object sender, EventArgs e) {
            textBroadcastAddress.Text = Settings.DefaultBroadcastHost;
            textBroadcastPort.Text = Settings.DefaultBroadcastPort.ToString(CultureInfo.InvariantCulture);

            CheckForm();
        }

        private void buttonOk_Click(object sender, EventArgs e) {
            var destination = new MagiWolDocument.Address();

            destination.Title = null;
            destination.Mac = textMac.Text;
            destination.SecureOn = textSecureOn.Text;

            string host;
            if (checkBroadcastAddress.Checked) {
                if (string.IsNullOrEmpty(textBroadcastAddress.Text.Trim()) == false) {
                    host = textBroadcastAddress.Text.Trim();
                    destination.IsBroadcastHostValid = true;
                } else {
                    host = Settings.DefaultBroadcastHost;
                    destination.IsBroadcastHostValid = false;
                }
            } else {
                host = destination.BroadcastHost;
                destination.IsBroadcastHostValid = false;
            }

            int port;
            if (checkBroadcastPort.Checked) {
                if (int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port)) {
                    if ((port >= 0) || (port <= 65535)) {
                        destination.IsBroadcastPortValid = true;
                    } else {
                        port = Settings.DefaultBroadcastPort;
                        destination.IsBroadcastPortValid = false;
                    }
                } else {
                    port = Settings.DefaultBroadcastPort;
                    destination.IsBroadcastPortValid = false;
                }
            } else {
                port = destination.BroadcastPort;
                destination.IsBroadcastPortValid = false;
            }

            destination.BroadcastHost = host;
            destination.BroadcastPort = port;

            destination.Notes = null;

            //do something
        }

        private void checkBroadcastAddress_CheckedChanged(object sender, EventArgs e) {
            textBroadcastAddress.Enabled = checkBroadcastAddress.Checked;
        }

        private void checkBroadcastPort_CheckedChanged(object sender, EventArgs e) {
            textBroadcastPort.Enabled = checkBroadcastPort.Checked;
        }


        private void textBroadcastAddress_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(textBroadcastAddress.Text.Trim())== false) {
                textBroadcastAddress.Text = Settings.DefaultBroadcastHost;
            }
        }

        private void textBroadcastPort_Validating(object sender, CancelEventArgs e) {
            int port;
            if (!(int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port) && (port >= 0) && (port <= 65535))) {
                textBroadcastPort.Text = Settings.DefaultBroadcastPort.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void DetailForm_Shown(object sender, EventArgs e) {
            textMac.SelectAll();
        }

        private void buttonTest_Click(object sender, EventArgs e) {
            try {
                try {
                    Cursor.Current = Cursors.WaitCursor;
                    Magic.SendMagicPacket(textMac.Text, textSecureOn.Text, textBroadcastAddress.Text, checkBroadcastAddress.Checked, textBroadcastPort.Text, checkBroadcastPort.Checked);
                    System.Threading.Thread.Sleep(Settings.WolSleepInterval);
                } finally {
                    Cursor.Current = Cursors.Default;
                }
            } catch (FormatException ex) {
                Medo.MessageBox.ShowError(this, ex.Message);
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
                IPAddress address;
                if (IPAddress.TryParse(textBroadcastAddress.Text, out address)) {
                    erp.SetError(checkBroadcastAddress, null);
                } else {
                    erp.SetError(checkBroadcastAddress, "IP address is not valid.");
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

            buttonWake.Enabled = (Medo.Net.WakeOnLan.IsMacAddressValid(textMac.Text));
        }

    }
}
