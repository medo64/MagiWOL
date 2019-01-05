using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class SettingsForm : Form {
        public SettingsForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            var fixedSizeFont = new Font("Courier New", base.Font.Size, base.Font.Style);

            foreach (Control iControl in new Control[] { textBroadcastAddress, textBroadcastPort }) { //because of Mono
                erp.SetIconPadding(iControl, 4);
                erp.SetIconAlignment(iControl, ErrorIconAlignment.MiddleLeft);
            }
        }


        private void Form_Load(object sender, EventArgs e) {
            textBroadcastAddress.Text = Settings.BroadcastHost;
            textBroadcastPort.Text = Settings.BroadcastPort.ToString(CultureInfo.InvariantCulture);

            checkProtocolIPv4.Checked = Settings.UseIPv4;
            checkProtocolIPv6.Checked = Settings.UseIPv6;
            checkProtocol_CheckedChanged(null, null);
            chbMac.Checked = Settings.ShowColumnMac;
            chbSecureOn.Checked = Settings.ShowColumnSecureOn;
            chbBroadcastHost.Checked = Settings.ShowColumnBroadcastHost;
            chbBroadcastPort.Checked = Settings.ShowColumnBroadcastPort;
            chbNotes.Checked = Settings.ShowColumnNotes;
        }


        private void buttonOk_Click(object sender, EventArgs e) {
            Settings.UseIPv4 = checkProtocolIPv4.Checked;
            Settings.UseIPv6 = checkProtocolIPv6.Checked;

            Settings.BroadcastHost = textBroadcastAddress.Text;
            if (int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var port) && (port >= 0) && (port <= 65535)) {
                Settings.BroadcastPort = port;
            }

            Settings.ShowColumnMac = chbMac.Checked;
            Settings.ShowColumnSecureOn = chbSecureOn.Checked;
            Settings.ShowColumnBroadcastHost = chbBroadcastHost.Checked;
            Settings.ShowColumnBroadcastPort = chbBroadcastPort.Checked;
            Settings.ShowColumnNotes = chbNotes.Checked;
        }

        private void textBroadcastAddress_Validating(object sender, CancelEventArgs e) {
            EnableDisableOk();
        }

        private void textBroadcastPort_Validating(object sender, CancelEventArgs e) {
            EnableDisableOk();
        }

        private void EnableDisableOk() {
            bool isEnabled = true;

            IPAddress address;
            if (IPAddress.TryParse(textBroadcastAddress.Text, out address) && (address.AddressFamily == AddressFamily.InterNetwork)) {
                erp.SetError(textBroadcastAddress, null);
            } else {
                erp.SetError(textBroadcastAddress, "Invalid IP address.");
                isEnabled = false;
            }

            if (int.TryParse(textBroadcastPort.Text, NumberStyles.Integer, CultureInfo.CurrentCulture, out var port) && (port >= 0) && (port <= 65535)) {
                erp.SetError(textBroadcastPort, null);
            } else {
                erp.SetError(textBroadcastPort, "Invalid port number.");
                isEnabled = false;
            }

            buttonOk.Enabled = isEnabled;
        }

        private void checkProtocol_CheckedChanged(object sender, EventArgs e) {
            if (sender != null) {
                var chb = (CheckBox)sender;
                if ((checkProtocolIPv4.Checked == false) && (checkProtocolIPv6.Checked == false)) {
                    chb.Checked = true;
                }
            }
            labelBroadcastAddress.Enabled = checkProtocolIPv4.Checked;
            textBroadcastAddress.Enabled = checkProtocolIPv4.Checked;
            buttonCheckHost.Enabled = checkProtocolIPv4.Checked;
            buttonDefault.Enabled = checkProtocolIPv4.Checked;
        }

        private void textBroadcastAddress_TextChanged(object sender, EventArgs e) {
            textBroadcastAddress_Validating(null, null);
        }

        private void textBroadcastPort_TextChanged(object sender, EventArgs e) {
            textBroadcastPort_Validating(null, null);
        }

        private void buttonCheckHost_Click(object sender, EventArgs e) {
            var broadcastHost = textBroadcastAddress.Text.Trim();
            try {
                Cursor.Current = Cursors.WaitCursor;
                IPAddress ip;
                if (IPAddress.TryParse(broadcastHost, out ip)) {
                    Medo.MessageBox.ShowInformation(this, string.Format("Broadcast IP {0} is valid.", ip.ToString()));
                } else {
                    List<IPAddress> resolved = new List<IPAddress>();
                    foreach (var address in Dns.GetHostAddresses(broadcastHost)) {
                        if (address.AddressFamily == AddressFamily.InterNetwork) {
                            resolved.Add(address);
                        }
                    }
                    if (resolved.Count > 0) {
                        Medo.Text.StringAdder sb = new Medo.Text.StringAdder(", ");
                        foreach (var address in resolved) {
                            sb.Append(address.ToString());
                        }
                        Medo.MessageBox.ShowInformation(this, string.Format("Broadcast host \"{0}\" has been resolved to {1}.", broadcastHost, sb.ToString()));
                    } else {
                        Medo.MessageBox.ShowWarning(this, string.Format("Cannot find valid IP address for broadcast host \"{0}\".", broadcastHost));
                    }
                }
            } catch (SocketException) { //No such host is known
                Medo.MessageBox.ShowWarning(this, string.Format("Broadcast host \"{0}\" cannot be resolved.", broadcastHost));
            } finally {
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonDefault_Click(object sender, EventArgs e) {
            textBroadcastAddress.Text = Settings.DefaultBroadcastHost;
            textBroadcastPort.Text = Settings.DefaultBroadcastPort.ToString();
        }

    }
}
