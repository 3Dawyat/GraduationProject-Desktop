using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Customization;
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

namespace SaidalyTechMain.Forms.SafesAndMony
{
    public partial class FrmAddSafe : DevExpress.XtraEditors.XtraForm
    {
        IService<TbSafes> _safes = StartUp<IService<TbSafes>>.Services();
        List<TbSafes> allSafes = new List<TbSafes>();
        TbSafes curentSafe = new TbSafes();
        int Id;
        public FrmAddSafe()
        {
            InitializeComponent();
        }
        private void FrmAddSafe_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private async void RefreshData()
        {
            lpeSafes.Enabled = true;
            allSafes = await _safes.GetAll();
            lpeSafes.Properties.DataSource = allSafes.Select(a => new { a.Id, a.Name });
            lpeSafes.Properties.DisplayMember = "Name";
            lpeSafes.Properties.ValueMember = "Id";
            lpeSafes.Text = "";
            textName.Text = "";
            lpeSafes.EditValue = null;
            lpeSafes.Enabled = true;
            curentSafe = new TbSafes();
        }
        private async Task<bool> SaveButtonClickAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            bool isSuccess;

            if (curentSafe.Id != 0)
            {
                curentSafe.Name = name;

                isSuccess = await _safes.Edit(curentSafe);
            }
            else
                isSuccess = await _safes.Add(new TbSafes() { Name = name });



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

        private void btnNew_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل تريد الحذف ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                switch (curentSafe.Id)
                {
                    case 0:
                        MessageBox.Show("! برجاء تحديد تصنيف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        break;
                    default:
                        if (await _safes.Delete(curentSafe))
                        {
                            MessageBox.Show("! تم الحذف بنجاح", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            RefreshData();
                        }
                        else
                            MessageBox.Show("! حدث خطأ أثناء العمليه او هذا التصنيف مرتبط بتصنيفات أخري", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void lpeSafes_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeSafes.EditValue != null && !string.IsNullOrEmpty(lpeSafes.EditValue.ToString()))
            {
                Id = Convert.ToInt32(lpeSafes.EditValue);
                curentSafe = allSafes.FirstOrDefault(a => a.Id == Id);
                textName.Text = curentSafe.Name;

            }
        }
    }
}