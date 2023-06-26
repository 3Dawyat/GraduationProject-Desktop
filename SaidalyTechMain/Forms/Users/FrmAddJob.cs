using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.Users
{
    public partial class FrmAddJob : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        #region objects
        List<TbAuthorization> LstAuthorization = new List<TbAuthorization>();
        TbAuthorization authorization = new TbAuthorization();
        List<VwFormsData> forms;
        SharedFunctions shared = new SharedFunctions();
        IService<AspNetRoles> _jobs = StartUp<IService<AspNetRoles>>.Services();
        IService<TbAuthorization> _authorizations = StartUp<IService<TbAuthorization>>.Services();
        IService<VwFormsData> _forms = StartUp<IService<VwFormsData>>.Services();
        RepositoryItemCheckEdit RpoCex = new RepositoryItemCheckEdit();
        RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
        #endregion
        public FrmAddJob()
        {
            InitializeComponent();
        }

        private async void FrmAddJob_Load(object sender, EventArgs e)
        {

            await FillData();
        }
        private async Task FillData()
        {
            forms = await _forms.GetAll();

            //if (Properties.Settings.Default.UserId != "1")
            //{
            //    var UserForms = await _authorizations.GetListBy(a => a.JopId == Properties.Settings.Default.JopId);
            //    var formsId = UserForms.Select(f => f.FormId).ToList();
            //    forms = forms.Where(FI => formsId.Contains(FI.Id)).ToList();
            //}
            #region treelist
            //var formsData = forms.Select(x =>new { x.Id,x.ParentId,x.Name,x.Authorized}).ToList();
            treeList1.DataSource = forms;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "ParentId";
            RpoCex.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            repoMemo.ReadOnly = true;


            treeList1.ForceInitialize();
            treeList1.PopulateColumns();

            treeList1.Columns[0].Caption = "الاسم";
            treeList1.Columns[1].Caption = "اظهار";

            RpoCex.ReadOnly = false;
            var f = RpoCex.Editable;

            treeList1.Columns[0].ColumnEdit = repoMemo;
            treeList1.Columns[1].ColumnEdit = RpoCex;
            treeList1.ExpandAll();
            treeList1.BestFitColumns();
            #endregion
        }
        private void GetDataFromtreeList(string jobId)
        {
            for (int i = 0; i < treeList1.Nodes.Count; i++)
            {

                authorization = new TbAuthorization()
                {
                    JopId = jobId,
                    FormId = Convert.ToInt32(treeList1.Nodes[i].GetValue(treeList1.KeyFieldName)),
                    Authorized = Convert.ToBoolean(treeList1.Nodes[i].GetValue(treeList1.Columns[1].FieldName)),
                   
                };
                LstAuthorization.Add(authorization);
                for (int i2 = 0; i2 < treeList1.Nodes[i].Nodes.Count; i2++)
                {
                    authorization = new TbAuthorization()
                    {
                        JopId = jobId,
                        FormId = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].GetValue(treeList1.KeyFieldName)),
                        Authorized = Convert.ToBoolean(treeList1.Nodes[i].Nodes[i2].GetValue(treeList1.Columns[1].FieldName).ToString()),
                       
                    };
                    LstAuthorization.Add(authorization);


                    for (int i3 = 0; i3 < treeList1.Nodes[i].Nodes[i2].Nodes.Count; i3++)
                    {
                        authorization = new TbAuthorization()
                        {
                            JopId = jobId,
                            FormId = Convert.ToInt32(treeList1.Nodes[i].Nodes[i2].Nodes[i3].GetValue(treeList1.KeyFieldName)),
                            Authorized = Convert.ToBoolean(treeList1.Nodes[i].Nodes[i2].Nodes[i3].GetValue(treeList1.Columns[1].FieldName).ToString()),
                           
                        };
                        LstAuthorization.Add(authorization);
                    }
                }
            }
        }
        private async void ClearData()
        {
            textName.Text = "";
            authorization = new TbAuthorization();
            LstAuthorization.Clear();
            for (int i = 0; i < forms.Count; i++)
            {
                forms[i].Authorized = false;
            }
            await FillData();
        }
        private bool checkInputs()
        {
            if (string.IsNullOrEmpty(textName.Text.Trim()))
            {
                MessageBox.Show("تاكد من ادخال اسم وظيفه");
                return false;
            }
            else
            {
                return true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (checkInputs())
            {
                string jobId = Convert.ToString(Guid.NewGuid());
                GetDataFromtreeList(jobId);

                if (await _jobs.Add(new AspNetRoles { Id = jobId, Name = textName.Text ,NormalizedName= textName.Text ,ConcurrencyStamp= textName.Text }))
                {
                    if (await _authorizations.AddRange(LstAuthorization))
                    {
                        MessageBox.Show("تم اضافة وظيفه بنجاح");
                    }
                    else
                    {
                        MessageBox.Show("حدث مشكله اثناء اضافة صلاحيات");
                    }
                }
                else
                {
                    MessageBox.Show("حدث مشكله اثناء اضافة وظيفه");
                }
                ClearData();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            shared.CloseForm(new FrmAddJob());

        }
    }
}
