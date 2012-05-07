using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Medo.Localization.Croatia;

namespace MagiWol {
    internal partial class ImportFromNetworkProgressForm : Form {

        public IList<MagiWolDocument.Address> ParsedAddresses { get; private set; }
        public NumberDeclination nudHours = new NumberDeclination("hour", "hours", "hours");
        public NumberDeclination nudMinutes = new NumberDeclination("minute", "minutes", "minutes");
        public NumberDeclination nudSeconds = new NumberDeclination("second", "seconds", "seconds");

        public ImportFromNetworkProgressForm(string textList) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            Medo.Windows.Forms.TaskbarProgress.SetState(Medo.Windows.Forms.TaskbarProgressState.Normal);
            worker.RunWorkerAsync(textList);
        }


        private void buttonCancel_Click(object sender, EventArgs e) {
            try {
                if (worker.IsBusy) {
                    worker.CancelAsync();
                }
            } catch (InvalidOperationException) { }
        }

        private static Regex _rxIP = new Regex(@"^\d+\.\d+\.\d+\.\d+$", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        private static Regex _rxIPRange = new Regex(@"^\d+\.\d+\.\d+\.\d+-\s*\d+\.\d+\.\d+\.\d+$", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        //[DebuggerNonUserCodeAttribute] //to allow throwing exceptions from code - http://www.developerdotstar.com/community/node/671
        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            string textList = (string)e.Argument;

            var entries = new Dictionary<IPAddress, string>();

            foreach (var iRawEntry in textList.Split(new char[] { ',', ';', System.Convert.ToChar(13), System.Convert.ToChar(10) }, StringSplitOptions.RemoveEmptyEntries)) {
                worker.ReportProgress(0, new ImportProgressState(iRawEntry.ToString()));

                try {

                    if (_rxIPRange.IsMatch(iRawEntry.Replace(" ", ""))) { //IP range

                        string[] parts = iRawEntry.Split('-');
                        if (parts.Length != 2) { throw new FormatException("Invalid range."); }
                        string part1 = parts[0].Trim();
                        string part2 = parts[1].Trim();
                        if (part1.Length == 0) { throw new FormatException("Invalid range."); }
                        if (part2.Length == 0) { throw new FormatException("Invalid range."); }

                        System.Net.IPAddress ip1 = System.Net.IPAddress.Parse(part1);
                        byte[] ipByte1 = ip1.GetAddressBytes();
                        Array.Reverse(ipByte1);
                        int ipInt1 = BitConverter.ToInt32(ipByte1, 0);
                        if (ipInt1 == 0) { throw new System.FormatException("Invalid IP address."); }

                        System.Net.IPAddress ip2 = System.Net.IPAddress.Parse(part2);
                        byte[] ipByte2 = ip2.GetAddressBytes();
                        Array.Reverse(ipByte2);
                        int ipInt2 = BitConverter.ToInt32(ipByte2, 0);
                        if (ipInt2 == 0) { throw new System.FormatException("Invalid IP address."); }

                        if (ipInt1 > ipInt2) {
                            var ipIntX = ipInt1;
                            ipInt1 = ipInt2;
                            ipInt2 = ipIntX;
                        }

                        if (ipInt1 + 256 < ipInt2) {
                            throw new InvalidOperationException("Range cannot containg more than 256 elements.");
                        }

                        do {
                            byte[] ipByte = BitConverter.GetBytes(ipInt1);
                            Array.Reverse(ipByte);
                            var ipX = IPAddress.Parse(string.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", ipByte[0], ipByte[1], ipByte[2], ipByte[3]));
                            if (!entries.ContainsKey(ipX)) {
                                entries.Add(ipX, "");
                            }
                            ipInt1 += 1;
                        } while (ipInt1 <= ipInt2);

                    } else if (_rxIP.IsMatch(iRawEntry.Replace(" ", ""))) { //single IP

                        System.Net.IPAddress ip1 = System.Net.IPAddress.Parse(iRawEntry.Trim());
                        byte[] ipByte1 = ip1.GetAddressBytes();
                        Array.Reverse(ipByte1);
                        int ipInt1 = BitConverter.ToInt32(ipByte1, 0);
                        if (ipInt1 == 0) { throw new System.FormatException("Invalid IP address."); }
                        if (!entries.ContainsKey(ip1)) {
                            entries.Add(ip1, "");
                        }

                    } else { //host name

                        var host = System.Net.Dns.GetHostEntry(iRawEntry.Trim());
                        foreach (var iAddress in host.AddressList) {
                            if (!entries.ContainsKey(iAddress)) {
                                entries.Add(iAddress, string.Format("{1} ({0})", iAddress, host.HostName));
                            }
                        }

                    }

                } catch (Exception ex) {
                    throw new FormatException(string.Format("Cannot parse text near \"{0}\".\r\n\r\n{1}", iRawEntry, ex.Message));
                }

            }

            worker.ReportProgress(0, new ImportProgressState(""));

            this._asyncMacsList = new List<MagiWolDocument.Address>();
            this._asyncMacTotalCounter = 0;

            foreach (var iEntry in entries) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncRetrieveMac), iEntry);
            }

            var progressLastCounter = 0;
            var progressTime = Stopwatch.StartNew();
            var progressSecondsLeft = new Medo.Math.MovingAverage();
            int totalCount = entries.Count;
            while (true) { //just wait until everything is done.
                int currCounter = Interlocked.CompareExchange(ref this._asyncMacTotalCounter, totalCount, totalCount);
                if (currCounter == totalCount) { break; }
                if (progressTime.ElapsedMilliseconds >= 1000) {
                    var progressRemaining = totalCount - currCounter;
                    var progressDiff = currCounter - progressLastCounter;
                    progressLastCounter = currCounter;
                    var countPerSecond = progressDiff / (progressTime.ElapsedMilliseconds / 1000.0);
                    if (countPerSecond > 0) {
                        progressSecondsLeft.Add(progressRemaining / countPerSecond);
                        worker.ReportProgress(currCounter * 100 / totalCount, new ImportProgressState((int)Math.Round(progressSecondsLeft.Average)));
                    } else {
                        worker.ReportProgress(currCounter * 100 / totalCount, null);
                    }
                    progressTime = Stopwatch.StartNew();
                } else {
                    worker.ReportProgress(currCounter * 100 / totalCount, null);
                }
                Thread.Sleep(500);
            }

            if (this._asyncMacsList.Count == 0) { throw new InvalidOperationException("No addresses found."); }

            e.Result = this._asyncMacsList.AsReadOnly();
            return;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (e.UserState != null) {
                var state = (ImportProgressState)e.UserState;

                if (state.Text != null) {
                    labelStatus.Text = string.Format("{0}", state.Text);
                }

                if (state.SecondsRemaining != null) {
                    var h = state.SecondsRemaining.Value / 3600;
                    if (h > 0) {
                        labelTimeRemaining.Text = "Less than " + nudHours.GetText(h + 1) + " remaining.";
                    } else {
                        var m = state.SecondsRemaining.Value / 60;
                        if (m > 0) {
                            labelTimeRemaining.Text = "Less than " + nudMinutes.GetText(m + 1) + " remaining.";
                        } else {
                            var s = state.SecondsRemaining.Value;
                            labelTimeRemaining.Text = nudSeconds.GetText(s + 1) + " remaining.";
                        }
                    }
                }
            }

            if (e.ProgressPercentage >= 0) {
                progress.Value = e.ProgressPercentage;
                Medo.Windows.Forms.TaskbarProgress.SetPercentage(e.ProgressPercentage);
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error == null) {
                if (e.Cancelled) {
                    this.DialogResult = DialogResult.Cancel;
                } else {
                    this.ParsedAddresses = (IList<MagiWolDocument.Address>)e.Result;
                    this.DialogResult = DialogResult.OK;
                }
            } else {
                Medo.Windows.Forms.TaskbarProgress.SetState(Medo.Windows.Forms.TaskbarProgressState.Paused);
                Medo.MessageBox.ShowWarning(this, e.Error.Message);
                this.DialogResult = DialogResult.Cancel;
            }
            Medo.Windows.Forms.TaskbarProgress.SetState(Medo.Windows.Forms.TaskbarProgressState.NoProgress);
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                buttonCancel_Click(null, null);
                e.Cancel = true;
            }
        }


        private static string GetMacString(System.Net.IPAddress ip) {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                byte[] mac = new byte[6];
                int macLen = mac.Length;

                int dest = BitConverter.ToInt32(ip.GetAddressBytes(), 0);
                int res = NativeMethods.SendARP(dest, 0, mac, ref macLen);

                if ((res == 0) && ((mac[0] != 0) || (mac[1] != 0) || (mac[2] != 0) || (mac[3] != 0) || (mac[4] != 0) || (mac[5] != 0))) {
                    return (mac[0].ToString("x2") + ":" + mac[1].ToString("x2") + ":" + mac[2].ToString("x2") + ":" + mac[3].ToString("x2") + ":" + mac[4].ToString("x2") + ":" + mac[5].ToString("x2")).ToUpper();
                }
            }

            return null;
        }


        private int _asyncMacTotalCounter;
        private List<MagiWolDocument.Address> _asyncMacsList;
        private readonly object _syncRootAsyncMac = new object();

        private void AsyncRetrieveMac(object parameter) {
            var iEntry = (KeyValuePair<IPAddress, string>)parameter;
            var iEntryAddress = iEntry.Key;
            var iEntryTitle = iEntry.Value;

            if (iEntryAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                byte[] macBytes = new byte[6];
                int macBytesLength = macBytes.Length;

                int ipDestinationBytes = BitConverter.ToInt32(iEntryAddress.GetAddressBytes(), 0);
                int res = NativeMethods.SendARP(ipDestinationBytes, 0, macBytes, ref macBytesLength);

                if ((res == 0) && ((macBytes[0] != 0) || (macBytes[1] != 0) || (macBytes[2] != 0) || (macBytes[3] != 0) || (macBytes[4] != 0) || (macBytes[5] != 0))) {
                    worker.ReportProgress(-1, new ImportProgressState("Last found: " + iEntryAddress.ToString()));
                    if (string.IsNullOrEmpty(iEntryTitle)) {
                        try {
                            var hostEntry = System.Net.Dns.GetHostEntry(iEntryAddress);
                            var hostName = hostEntry.HostName.Split(new char[] { '.' }, 2)[0];
                            if (string.IsNullOrEmpty(hostName)) {
                                iEntryTitle = iEntryAddress.ToString();
                            } else {
                                iEntryTitle = hostName + " at " + iEntryAddress.ToString();
                                worker.ReportProgress(-1, new ImportProgressState("Last found: " + hostName + " at " + iEntryAddress.ToString()));
                            }
                        } catch (SocketException) { //NoSuchHostIsKnown
                            iEntryTitle = iEntryAddress.ToString();
                        }
                    }

                    var macText = (macBytes[0].ToString("x2") + ":" + macBytes[1].ToString("x2") + ":" + macBytes[2].ToString("x2") + ":" + macBytes[3].ToString("x2") + ":" + macBytes[4].ToString("x2") + ":" + macBytes[5].ToString("x2")).ToUpper();
                    var iAddress = new MagiWolDocument.Address(iEntryTitle, macText, "", "", null, null, false, false);

                    lock (_syncRootAsyncMac) {
                        if (!this._asyncMacsList.Contains(iAddress)) {
                            this._asyncMacsList.Add(iAddress);
                        }
                    }
                }
            }

            Interlocked.Increment(ref this._asyncMacTotalCounter);
        }


        private static class NativeMethods {

            [DllImport("iphlpapi.dll", ExactSpelling = true)]
            internal static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        }


    }
}
