using DevExpress.XtraEditors;
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

namespace SaidalyTechMain.Forms.MainForms
{
    public partial class FrmAddNewForms : DevExpress.XtraEditors.XtraForm
    {
        IService<TbForms> _Forms = StartUp<IService<TbForms>>.Services();
        List<TbForms> FormsList = new List<TbForms>();

        public FrmAddNewForms()
        {
            InitializeComponent();
        }

        private async void FrmAddNewForms_Load(object sender, EventArgs e)
        {
            FormsList = await _Forms.GetAll();
            lpeFather.Properties.DataSource = FormsList.Select(f => new { f.Id, f.Name, f.ButtonType });
            lpeFather.Properties.ValueMember = "Id";
            lpeFather.Properties.DisplayMember = "Name";

            List<string> types = new List<string>()
            {
                "ribbonPage","ribbonPageGroup","barButtonItem","tileElement"
            };
            lpeType.Properties.DataSource = types;
            TextText.Text = "";
            textName.Text = "";
            lpeType.EditValue = null;
            lpeFather.EditValue = null;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            TbForms form = new TbForms();
            form.Name = TextText.Text;
            form.ButtonType = lpeType.Text;
            form.ButtonName = textName.Text;
            form.ParentId = Convert.ToInt32(lpeFather.EditValue);

            TextText.Text = "";
            textName.Text = "";
            lpeType.EditValue = null;
            lpeFather.EditValue = null;

            if (await _Forms.Add(form))
            { MessageBox.Show("تمت الاضافه بنجاح"); }
            else
            {
                MessageBox.Show("حدث خطأ اثناء الاضافه");
            }

            FrmAddNewForms_Load(null, null);
        }
    }
}