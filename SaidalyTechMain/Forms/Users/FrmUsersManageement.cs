using DevExpress.XtraBars;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
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
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace SaidalyTechMain.Forms.Users
{
    public partial class FrmUsersManageement : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        SharedFunctions shared = new SharedFunctions();
        IService<VwDesktopUsers> _usersWithJob = StartUp<IService<VwDesktopUsers>>.Services();
        IService<AspNetUsers> _users = StartUp<IService<AspNetUsers>>.Services();

        public FrmUsersManageement()
        {
            InitializeComponent();
        }
        private async void FrmUsersManageement_Load(object sender, EventArgs e)
        {
            await fillData();
        }


        private async Task fillData()
        {
            var data = await _usersWithJob.GetAll();
            GcData.DataSource = data;

            GcData.ForceInitialize();
            GvData.PopulateColumns();

            #region gridControl Settings 

            GvData.Columns[0].Caption = "كود";
            GvData.Columns[1].Caption = "الاسم";
            GvData.Columns[3].Caption = "الايميل";
            GvData.Columns[4].Caption = "الوظيفه";
             
             
            GvData.Columns[2].Visible = false;
            GvData.Columns[5].Visible = false;

            GvData.BestFitColumns();

            #endregion
        }
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddUser());

        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            var UserId = Convert.ToString(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName));

            shared.OpenForm(new FrmAddUser(UserId));

        }

        private  void btnDelete_Click(object sender, EventArgs e)
        {
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
           await fillData();
        }
    }
}
