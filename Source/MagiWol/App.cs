using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace MagiWol {
    internal static class App {

        [STAThread]
        static void Main() {
            bool createdNew;
            var mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));
            using (var setupMutex = new Mutex(false, @"Global\JosipMedved_MagiWOL", out createdNew, mutexSecurity)) {

                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Medo.Configuration.Settings.Read("CultureName", "en-US"));

                Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
                Medo.Application.UnhandledCatch.Attach();

                Medo.Configuration.Settings.NoRegistryWrites = !Settings.IsInstalled;
                Medo.Configuration.RecentFiles.NoRegistryWrites = !Settings.IsInstalled;
                Medo.Windows.Forms.State.NoRegistryWrites = !Settings.IsInstalled;
                Medo.Diagnostics.ErrorReport.DisableAutomaticSaveToTemp = !Settings.IsInstalled;

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
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("http://jmedved.com/feedback/"));
#else
            throw e.Exception;
#endif
        }


        private static class NativeMethods {

            [DllImport("Shell32.dll", SetLastError = true)]
            public static extern UInt32 SetCurrentProcessExplicitAppUserModelID(String AppID);

        }

        private static bool IsRunningOnMono {
            get {
                return (Type.GetType("Mono.Runtime") != null);
            }
        }

    }
}
