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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmShowItems : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<VwItemsWithUnits> _ItemWithUnits = StartUp<IService<VwItemsWithUnits>>.Services();
        IService<TbItems> _items = StartUp<IService<TbItems>>.Services();
        SharedFunctions shared = new SharedFunctions();
        List<VwItemsWithUnits> ItemWithUnits = new List<VwItemsWithUnits>();
        RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();

        public FrmShowItems()
        {
            InitializeComponent();
        }

        private async void FrmShowItems_Load(object sender, EventArgs e)
        {
            await FillData();
        }
        public async Task FillData()
        {
            ItemWithUnits = await _ItemWithUnits.GetAll();

            GCData.DataSource = ItemWithUnits.OrderBy(i => i.Name).ToList();

            GCData.ForceInitialize();
            GVData.PopulateColumns();
            GVData.Columns[10].Visible = false;
            GVData.Columns[11].Visible = false;
            GVData.Columns[12].Visible = false;
            GVData.Columns[13].Visible = false;
            repoMemo.ReadOnly = true;
            string[] columnCaptions = {
                "Id",
                "الاسم",
                "الماده الفعاله",
                "الشركه",
                "دواعي الاستعمال",
                "الجرعه",
                "المكونات",
                "سعر البيع",
                "سعر الشراء",
                "بار كود" };
            for (int i = 0; i <=9; i++)
            {
                GVData.Columns[i].Caption = columnCaptions[i]; 
                GVData.Columns[i].MaxWidth = 250; 
              // GVData.Columns[i]. = 250; 
            }

            for (int i = 0; i <= 10; i++)
            {
                GVData.Columns[i].ColumnEdit = repoMemo;
            }
        GVData.BestFitColumns();
          

        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmItem());
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            var itemId = Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[10].FieldName));
            shared.OpenForm(new FrmItem(itemId));

        }

        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل حقا تريد حذف الصنف؟ ", "تحذير", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var itemId = Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[10].FieldName));
                var deletedItem = await _items.GetObjectBy(i => i.Id == itemId);
                deletedItem.IsDeleted = true;
                if (await _items.Edit(deletedItem))
                {
                    MessageBox.Show("تم الحذف بنجاح ");
                }
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await FillData();
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    string keyValue = gridView1.FocusedNode[treeList1.KeyFieldName].ToString();
            //    var item = ItemWithUnits.FirstOrDefault(a => a.Id == keyValue);
            //    if (item != null && !string.IsNullOrEmpty(item.Barcode) && item.Barcode != "0")
            //    {
            //        FrmBarcodePrint frm = new FrmBarcodePrint(item.Barcode);
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("برجاء اختيار وحده");
            //}
        }
    }
}
