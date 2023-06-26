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

namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmAddStore : DevExpress.XtraEditors.XtraForm
    {

        IService<TbStores> _Stores = StartUp<IService<TbStores>>.Services();
        List<TbStores> allStores = new List<TbStores>();
        TbStores curentSafe = new TbStores();
        int Id;
        public FrmAddStore()
        {
            InitializeComponent();
        }
        private void FrmAddStore_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private async void RefreshData()
        {
            lpeStores.Enabled = true;
            allStores = await _Stores.GetAll();
            lpeStores.Properties.DataSource = allStores.Select(a => new { a.Id, a.Name });
            lpeStores.Properties.DisplayMember = "Name";
            lpeStores.Properties.ValueMember = "Id";
            lpeStores.Text = "";
            textName.Text = "";
            lpeStores.EditValue = null;
            lpeStores.Enabled = true;
            curentSafe = new TbStores();
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

            if (curentSafe.Id != 0)
            {
                curentSafe.Name = name;

                isSuccess = await _Stores.Edit(curentSafe);
            }
            else
                isSuccess = await _Stores.Add(new TbStores() { Name = name });



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
                switch (curentSafe.Id)
                {
                    case 0:
                        MessageBox.Show("! برجاء تحديد تصنيف", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        break;
                    default:
                        if (await _Stores.Delete(curentSafe))
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

        private void lpeStores_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeStores.EditValue != null && !string.IsNullOrEmpty(lpeStores.EditValue.ToString()))
            {
                Id = Convert.ToInt32(lpeStores.EditValue);
                curentSafe = allStores.FirstOrDefault(a => a.Id == Id);
                textName.Text = curentSafe.Name;

            }
        }
    }
}