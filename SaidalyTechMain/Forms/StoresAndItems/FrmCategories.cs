
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Office.Services;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Shared;
using SaidalyTechMain.DB_Models;

namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmCategories : DevExpress.XtraEditors.XtraForm
    {
        IService<TbCategories> _category = StartUp<IService<TbCategories>>.Services();
        List<TbCategories> allCategories = new List<TbCategories>();
        TbCategories curentCategory = new TbCategories();
        int Id;

        public FrmCategories()
        {
            InitializeComponent();
        }

        private void FrmCategories_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private async void RefreshData()
        {
            lpeCategory.Enabled = true;
            allCategories = await _category.GetAll();
            lpeCategory.Properties.DataSource = allCategories.Select(a => new { a.Id, a.Name });
            lpeCategory.Properties.DisplayMember = "Name";
            lpeCategory.Properties.ValueMember = "Id";
            lpeCategory.Text = "";
            textName.Text = "";
            lpeCategory.EditValue = null;
            lpeCategory.Enabled = true;
            curentCategory = new TbCategories();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async Task<bool> SaveButtonClickAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            bool isSuccess;

            if (curentCategory.Id != 0)
            {
                curentCategory.Name = name;

                isSuccess = await _category.Edit(curentCategory);
            }
            else
                isSuccess = await _category.Add(new TbCategories() { Name = name });



            if (isSuccess)
            {
                RefreshData();
                return true;
            }

            return false;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {

            bool result = await SaveButtonClickAsync(textName.Text);
            if (result)
                MessageBox.Show("تمت العمليه بنجاح");
            else
                MessageBox.Show("حدث خطا اثناء العمليه");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحذف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                switch (curentCategory.Id)
                {
                    case 0:
                        MessageBox.Show("! برجاء تحديد تصنيف", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    default:
                        if (await _category.Delete(curentCategory))
                        {
                            MessageBox.Show("! تم الحذف بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                            MessageBox.Show("! حدث خطأ أثناء العمليه او هذا التصنيف مرتبط بتصنيفات أخري", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void lpeCategory_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeCategory.EditValue != null && !string.IsNullOrEmpty(lpeCategory.EditValue.ToString()))
            {
                Id = Convert.ToInt32(lpeCategory.EditValue);
                curentCategory = allCategories.FirstOrDefault(a => a.Id == Id);
                textName.Text = curentCategory.Name;

            }
        }
    }
}