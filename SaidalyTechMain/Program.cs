using DevExpress.Skins;
using DevExpress.UserSkins;
using SaidalyTechMain.Forms.MainForms;
using SaidalyTechMain.Forms.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace SaidalyTechMain
{
    internal static class Program
    {

      
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Disable DWM composition


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new FrmStartInterface());
         //   Application.Run(new FrmLogin());
        }
    }
}