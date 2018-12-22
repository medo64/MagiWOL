using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Medo.Configuration;

namespace MagiWol {
    internal static class App {

        public static RecentlyUsed Recent;

        [STAThread]
        static void Main() {
            bool createdNew;
            var mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));
            using (var setupMutex = new Mutex(false, @"Global\JosipMedved_MagiWOL", out createdNew, mutexSecurity)) {

                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Config.Read("CultureName", "en-US"));

                Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
                Medo.Application.UnhandledCatch.Attach();

                Medo.Diagnostics.ErrorReport.DisableAutomaticSaveToTemp = !Config.IsAssumedInstalled;

#pragma warning disable CS0618 // Type or member is obsolete
                var recentLegacy = new RecentFiles();
#pragma warning restore CS0618 // Type or member is obsolete
                if (recentLegacy.Count > 0) {
                    var fileList = new List<string>();
                    foreach (var item in recentLegacy.Items) {
                        fileList.Add(item.FileName);
                    }
                    Recent = new RecentlyUsed(fileList);
                    Config.Write("RecentFile", Recent.FileNames);
                    recentLegacy.Clear();
                } else {
                    Recent = new RecentlyUsed(Config.Read("RecentFile"));
                }
                Recent.Changed += (o, i) => {
                    Config.Write("RecentFile", Recent.FileNames);
                };

                if (!Config.IsAssumedInstalled) {
                    Medo.Windows.Forms.State.ReadState += delegate (object sender, Medo.Windows.Forms.StateReadEventArgs e) {
                        e.Value = Config.Read("State!" + e.Name.Replace("Bimil.", ""), e.DefaultValue);
                    };
                    Medo.Windows.Forms.State.WriteState += delegate (object sender, Medo.Windows.Forms.StateWriteEventArgs e) {
                        Config.Write("State!" + e.Name.Replace("Bimil.", ""), e.Value);
                    };
                }

                if (!((Environment.OSVersion.Version.Build < 7000) || (App.IsRunningOnMono))) {
                    var appId = Assembly.GetExecutingAssembly().Location;
                    if (appId.Length > 127) { appId = @"JosipMedved_MagiWOL\" + appId.Substring(appId.Length - 127 - 20); }
                    NativeMethods.SetCurrentProcessExplicitAppUserModelID(appId);
                }

                Application.Run(new MainForm());

            }
        }



        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("https://medo64.com/feedback/"));
#else
            throw e.Exception;
#endif
        }


        private static class NativeMethods {

            [DllImport("Shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern UInt32 SetCurrentProcessExplicitAppUserModelID(String AppID);

        }

        private static bool IsRunningOnMono {
            get {
                return (Type.GetType("Mono.Runtime") != null);
            }
        }

    }
}
