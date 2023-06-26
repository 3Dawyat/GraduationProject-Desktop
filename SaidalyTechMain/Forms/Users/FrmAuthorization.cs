using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.MainForms;
using SaidalyTechMain.Properties;
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

namespace SaidalyTechMain.Forms.Users
{
    public partial class FrmAuthorization : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        #region objects
        SharedFunctions shared = new SharedFunctions();

        IService<AspNetRoles> _jobs = StartUp<IService<AspNetRoles>>.Services();
        IService<VwFormsData> _forms = StartUp<IService<VwFormsData>>.Services();
        IService<VwAuthorizationWithFormName> _authorizations = StartUp<IService<VwAuthorizationWithFormName>>.Services();
        IService<VwAuthorizationWithNames> _AuthorizationWithName = StartUp<IService<VwAuthorizationWithNames>>.Services();
        IService<TbAuthorization> _tbAuthorizations = StartUp<IService<TbAuthorization>>.Services();

        List<AspNetRoles> jobs = new List<AspNetRoles>();
        List<VwFormsData> FormsData = new List<VwFormsData>();
        List<VwAuthorizationWithFormName> ClearedAuthorization = new List<VwAuthorizationWithFormName>();
        List<VwAuthorizationWithFormName> AuthorizationNames = new List<VwAuthorizationWithFormName>();
        List<TbAuthorization> LstAuthorization = new List<TbAuthorization>();
        TbAuthorization authorization = new TbAuthorization();

        RepositoryItemCheckEdit RpoCex = new RepositoryItemCheckEdit();
        RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
        #endregion
        public FrmAuthorization()
        {
            InitializeComponent();
        }

        private async void FrmAuthorization_Load(object sender, EventArgs e)
        {
            await FillLpeData();

            btnSave.Enabled = false;

        }
        async Task FillLpeData()
        {
            jobs = await _jobs.GetListBy(a=>!WebApp.AspRoles.Contains(a.Name));
            lpeJobs.Properties.DataSource = jobs.Select(a => new
            {
                الكود = a.Id,
                الأسم = a.Name
            });
            lpeJobs.Properties.DisplayMember = "الأسم";
            lpeJobs.Properties.ValueMember = "الكود";
        }
        async Task FillTreeList(string jobId)
        {
            AuthorizationNames = await _authorizations.GetListBy(a => a.JopId == jobId);

            if (Properties.Settings.Default.UserId == "1")
            {
                FormsData = await _forms.GetAll();
                foreach (VwFormsData form in FormsData)
                {
                    ClearedAuthorization.Add(new VwAuthorizationWithFormName
                    {
                        Id = 0,
                        JopId = "0",
                        FormId = form.Id,
                        ParentId = form.ParentId,
                        Authorized = (bool)form.Authorized,
                        Name = form.Name,


                    });
                }
                foreach (var item in ClearedAuthorization)
                {


                    if (!AuthorizationNames.Select(auName => auName.FormId).ToList().Contains(item.FormId))
                        AuthorizationNames.Add(item);
                }
            }



            treeList1.DataSource = AuthorizationNames;
            treeList1.KeyFieldName = "FormId";
            treeList1.ParentFieldName = "ParentId";
            RpoCex.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            repoMemo.ReadOnly = true;

            treeList1.ForceInitialize();
            treeList1.PopulateColumns();

            treeList1.Columns[0].Caption = "الاسم";
            treeList1.Columns[1].Caption = "اظهار";

            treeList1.Columns[0].ColumnEdit = repoMemo;
            treeList1.Columns[1].ColumnEdit = RpoCex;

            treeList1.Columns[2].Visible = false;
            treeList1.Columns[3].Visible = false;

            treeList1.ExpandAll();
            treeList1.BestFitColumns();
        }

        private void GetDataFromtreeList(string jobId)
        {
            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {
                authorization = new TbAuthorization()
                {
                    Id = Convert.ToInt32(treeList1.Nodes[i].GetValue(treeList1.Columns[3].FieldName).ToString()),
                    JopId = jobId,
                    FormId = Convert.ToInt32(treeList1.Nodes[i].GetValue(treeList1.KeyFieldName)),
                    Authorized = Convert.ToBoolean(treeList1.Nodes[i].GetValue(treeList1.Columns["Authorized"])),
                };
                LstAuthorization.Add(authorization);
                for (int i2 = 0; i2 < treeList1.Nodes[i].Nodes.Count; i2++)
                {
                    authorization = new TbAuthorization()
                    {
                        Id = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].GetValue(treeList1.Columns[3].FieldName).ToString()),
                        JopId = jobId,
                        FormId = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].GetValue(treeList1.KeyFieldName)),
                        Authorized = Convert.ToBoolean(treeList1.Nodes[i].Nodes[i2].GetValue(treeList1.Columns["Authorized"]).ToString()),
                    };
                    LstAuthorization.Add(authorization);


                    for (int i3 = 0; i3 < treeList1.Nodes[i].Nodes[i2].Nodes.Count; i3++)
                    {
                        authorization = new TbAuthorization()
                        {
                            Id = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].Nodes[i3].GetValue(treeList1.Columns[3].FieldName).ToString()),
                            JopId = jobId,
                            FormId = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].Nodes[i3].GetValue(treeList1.KeyFieldName)),
                            Authorized = Convert.ToBoolean(treeList1.Nodes[i].Nodes[i2].Nodes[i3].GetValue(treeList1.Columns["Authorized"]).ToString()),//
                            };
                        LstAuthorization.Add(authorization);


                        for (int i4 = 0; i4 < treeList1.Nodes[i].Nodes[i2].Nodes[i3].Nodes.Count; i4++)
                        {
                            authorization = new TbAuthorization()
                            {
                                Id = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].Nodes[i3].Nodes[i4].GetValue(treeList1.Columns[3].FieldName).ToString()),
                                JopId = jobId,
                                FormId = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].Nodes[i3].Nodes[i4].GetValue(treeList1.KeyFieldName)),
                                Authorized = Convert.ToBoolean(treeList1.Nodes[i].Nodes[i2].Nodes[i3].Nodes[i4].GetValue(treeList1.Columns["Authorized"]).ToString()),//
                               };
                            LstAuthorization.Add(authorization);


                        }

                    }
                }
            }
        }
        private  void ClearData()
        {
            int m = treeList1.Nodes.Count;
            for (int i = 0; i < m; i++)
            {
                treeList1.Nodes[0].Remove();
            }
            LstAuthorization = new List<TbAuthorization>();
            //FrmLogin.Authorizations = await _AuthorizationWithName.GetListBy(a => a.JopId == Settings.Default.JopId);
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {

            if (Convert.ToString(lpeJobs.EditValue) != "" && Convert.ToString(lpeJobs.EditValue) != null)
            {
                await FillTreeList(Convert.ToString(lpeJobs.EditValue));
                btnSave.Enabled = true;
            }
            else
                MessageBox.Show("اختر الوظيفه اولا");
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await FillLpeData();
            ClearData();
            btnSave.Enabled = false;
            lpeJobs.EditValue = "";
        }

        private void btnAddJob_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddJob());

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            shared.CloseForm(new FrmAuthorization());
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (treeList1.Nodes.Count > 0)
            {
                GetDataFromtreeList(Convert.ToString(lpeJobs.EditValue));
                if (await _tbAuthorizations.EditRange(LstAuthorization.Where(a => !(a.Authorized == false && a.Id == 0)).ToList()))
                {
                    MessageBox.Show("تم تعديل الوظيفه بنجاح");

                    ClearData();

                }
                else
                {
                    MessageBox.Show("حدث مشكله اثناء التعديل");
                    ClearData();
                }
            }
            else
                MessageBox.Show("اختر الوظيفه و ابحث عن صلاحياتها اولا");
        }
    }
}
