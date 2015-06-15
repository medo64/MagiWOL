using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MagiWol {
    internal partial class WakeProgressForm : Form {

        private IList<MagiWolDocument.Address> _addresses;
        private readonly int PauseInSeconds;

        public WakeProgressForm(IList<MagiWolDocument.Address> addresses, int pauseInSeconds) {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            this._addresses = addresses;
            this.PauseInSeconds = pauseInSeconds;

            Medo.Windows.Forms.TaskbarProgress.SetState(Medo.Windows.Forms.TaskbarProgressState.Normal);
            worker.RunWorkerAsync();
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) {
            var swPause = new Stopwatch();
            var decSeconds = new Medo.Localization.Croatia.NumberDeclination("second", "seconds", "seconds");
            for (int i = 0; i < this._addresses.Count; ++i) {
                var iAddress = this._addresses[i];
                for (int j = 0; j < Settings.WolCount; ++j) {
                    if (worker.CancellationPending) {
                        e.Cancel = true;
                        return;
                    }

                    if (j != 0) {
                        System.Threading.Thread.Sleep(Settings.WolSleepInterval);
                    }
                    Magic.SendMagicPacket(iAddress);

                    int percent = (i * Settings.WolCount + j + 1) * 100 / (this._addresses.Count * Settings.WolCount);
                    worker.ReportProgress(percent, "Waking " + iAddress.Title + "...");
                }

                if ((this.PauseInSeconds > 0) && (i < this._addresses.Count - 1)) {
                    swPause.Reset();
                    swPause.Start();
                    int lastSecondsRemaining = -1;
                    while (swPause.Elapsed.TotalSeconds < this.PauseInSeconds) {
                        int secondsRemaining = this.PauseInSeconds - (int)Math.Floor(swPause.Elapsed.TotalSeconds);
                        if (lastSecondsRemaining != secondsRemaining) {
                            worker.ReportProgress(-1, "Pausing " + decSeconds.GetText(secondsRemaining) + " after " + iAddress.Title + "...");
                            lastSecondsRemaining = secondsRemaining;
                        }
                        if (worker.CancellationPending) {
                            e.Cancel = true;
                            return;
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (e.ProgressPercentage >= 0) {
                progress.Value = e.ProgressPercentage;
                Medo.Windows.Forms.TaskbarProgress.SetPercentage(e.ProgressPercentage);
            }
            labelCurrent.Text = string.Format("{0}", e.UserState);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (!e.Cancelled) {
                if (e.Error != null) {
                    Medo.MessageBox.ShowError(this, e.Error.Message);
                    this.DialogResult = DialogResult.Cancel;
                } else {
                    this.DialogResult = DialogResult.OK;
                }
            }
            Medo.Windows.Forms.TaskbarProgress.SetState(Medo.Windows.Forms.TaskbarProgressState.NoProgress);
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                if (worker.IsBusy) { worker.CancelAsync(); }
            } catch (InvalidOperationException) { }
        }

    }
}
