using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class QuickWakeForm : Form {

        public QuickWakeForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            var fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);
            this.textMac.Font = fixedSizeFont;
            this.textSecureOn.Font = fixedSizeFont;

            foreach (Control iControl in new Control[] { textMac, textSecureOn, checkBroadcastAddress, checkBroadcastPort }) { //because of Mono
                erp.SetIconPadding(iControl, 4);
                erp.SetIconAlignment(iControl, ErrorIconAlignment.MiddleLeft);
            }
        }

        public QuickWakeForm(MagiWolDocument.Address address)
            : this() {
            if (address != null) {
                textMac.Text = address.Mac;
                textSecureOn.Text = address.SecureOn;
                if (address.IsBroadcastHostValid) {
                    checkBroadcastAddress.Checked = true;
                    textBroadcastAddress.Text = address.BroadcastHost;
                }
                if (address.IsBroadcastPortValid) {
                    checkBroadcastPort.Checked = true;
                    textBroadcastPort.Text = address.BroadcastPort.ToString(CultureInfo.CurrentCulture);
                }
            }
        }


        private void DetailForm_Load(object sender, EventArgs e) {
            checkProtocolIPv4.Checked = Settings.UseIPv4;
            checkProtocolIPv6.Checked = Settings.UseIPv6;
            checkProtocol_CheckedChanged(null, null);

            textBroadcastAddress.Text = Settings.BroadcastHost;
            textBroadcastPort.Text = Settings.BroadcastPort.ToString(CultureInfo.InvariantCulture);

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
                    host = Settings.BroadcastHost;
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
                        port = Settings.BroadcastPort;
                        destination.IsBroadcastPortValid = false;
                    }
                } else {
                    port = Settings.BroadcastPort;
                    destination.IsBroadcastPortValid = false;
                }
            } else {
                port = destination.BroadcastPort;
                destination.IsBroadcastPortValid = false;
            }

            destination.BroadcastHost = host;
            destination.BroadcastPort = port;

            destination.Notes = null;
        }

        private void checkProtocol_CheckedChanged(object sender, EventArgs e) {
            if (sender != null) {
                var chb = (CheckBox)sender;
                if ((checkProtocolIPv4.Checked == false) && (checkProtocolIPv6.Checked == false)) {
                    chb.Checked = true;
                }
            }
            labelBroadcastAddress.Enabled = checkProtocolIPv4.Checked;
            textBroadcastAddress.Enabled = checkProtocolIPv4.Checked && checkBroadcastAddress.Checked;
        }

        private void checkBroadcastAddress_CheckedChanged(object sender, EventArgs e) {
            textBroadcastAddress.Enabled = checkBroadcastAddress.Checked;
        }

        private void checkBroadcastPort_CheckedChanged(object sender, EventArgs e) {
            textBroadcastPort.Enabled = checkBroadcastPort.Checked;
        }


        private void textBroadcastAddress_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrEmpty(textBroadcastAddress.Text.Trim())) {
                checkBroadcastAddress.Checked = false;
                textBroadcastAddress.Text = Settings.BroadcastHost;
            }
        }

        private void textBroadcastPort_Validating(object sender, CancelEventArgs e) {
            int port;
            if (!(int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.InvariantCulture, out port) && (port >= 0) && (port <= 65535))) {
                textBroadcastPort.Text = Settings.BroadcastPort.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void DetailForm_Shown(object sender, EventArgs e) {
            textMac.SelectAll();
        }

        private void buttonTest_Click(object sender, EventArgs e) {
            try {
                try {
                    Cursor.Current = Cursors.WaitCursor;
                    try {
                        if (checkProtocolIPv4.Checked) {
                            Magic.SendMagicPacket(textMac.Text, textSecureOn.Text, textBroadcastAddress.Text, checkBroadcastAddress.Checked, textBroadcastPort.Text, checkBroadcastPort.Checked);
                        }
                        if (checkProtocolIPv6.Checked) {
                            Magic.SendMagicPacketIPv6(textMac.Text, textSecureOn.Text, textBroadcastPort.Text, checkBroadcastPort.Checked);
                        }
                    } catch (InvalidOperationException ex) {
                        Medo.MessageBox.ShowError(this, ex.Message);
                    }
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

            buttonWake.Enabled = (Medo.Net.WakeOnLan.IsMacAddressValid(textMac.Text));
        }

    }
}
