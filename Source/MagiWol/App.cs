using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MagiWol {
    internal static class App {

        private static Mutex _setupMutex;


        [STAThread]
        static void Main() {
            _setupMutex = new Mutex(false, @"Global\JosipMedved_MagiWOL");

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Medo.Configuration.Settings.Read("CultureName", "en-US"));

            Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
            Medo.Application.UnhandledCatch.Attach();

            Medo.Configuration.Settings.NoRegistryWrites = !Medo.Configuration.Settings.Read("Installed", false);
            Medo.Configuration.RecentFiles.NoRegistryWrites = !Medo.Configuration.Settings.Read("Installed", false);
            Medo.Windows.Forms.State.NoRegistryWrites = !Medo.Configuration.Settings.Read("Installed", false);
            Medo.Diagnostics.ErrorReport.DisableAutomaticSaveToTemp = !Medo.Configuration.Settings.Read("Installed", false);

            if (!((Environment.OSVersion.Version.Build < 7000) || (App.IsRunningOnMono))) {
                var appId = Assembly.GetExecutingAssembly().Location;
                if (appId.Length > 127) { appId = @"JosipMedved_MagiWOL\" + appId.Substring(appId.Length - 127 - 20); }
                NativeMethods.SetCurrentProcessExplicitAppUserModelID(appId);
            }

            Application.Run(new MainForm());

            _setupMutex.Close();
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
