using DevExpress.XtraEditors;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.MainForms
{
    public partial class FrmStartInterface : DevExpress.XtraEditors.XtraForm
    {

        SharedFunctions shared = new SharedFunctions();

        public FrmStartInterface()
        {
            InitializeComponent();
        }


        private void FrmStartInterface_Load(object sender, EventArgs e)
        {
            //ConnectionString connection = new ConnectionString(
            //                                               Settings.Default.Authentication,
            //                                               Settings.Default.DataSource,
            //                                               Settings.Default.DatabaseName,
            //                                               Settings.Default.SQLUser,
            //                                               Settings.Default.SQLPassword);
            //using (SaidalyTechMainContext db = new SaidalyTechMainContext())
            //{
            //    try
            //    {
            //        if (db.Database.CanConnect())
                        timer1.Enabled = true;
            //    }
            //    catch
            //    {
            //        timer1.Enabled = false;
            //        shared.OpenFormDialog(new FrmDatabaseConnection("Restart"));
            //        //البرنامج بيرستر

            //    }
            //}
        }
        async Task IsActivated()
        {
           
                shared.OpenForm(new FrmLogin());
           
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            await IsActivated();
            this.Hide();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

    }
}