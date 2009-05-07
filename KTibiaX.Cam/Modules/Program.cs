using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KTibiaX.Cam.Dialogs;
using System.Diagnostics;

namespace KTibiaX.Cam {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.UserSkins.OfficeSkins.Register();

            var clientsform = new frm_Clients();
            Application.Run(clientsform);
            if (clientsform.SelectedProcess != null) {
                AppClientProcess = clientsform.SelectedProcess;
                Application.Run(new frm_Main());
            }
        }

        /// <summary>
        /// Gets or sets the app client process.
        /// </summary>
        /// <value>The app client process.</value>
        public static Process AppClientProcess { get; set; }
    }
}
