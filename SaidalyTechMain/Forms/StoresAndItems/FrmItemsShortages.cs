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
    public partial class FrmItemsShortages : DevExpress.XtraEditors.XtraForm
    {

        IService<VwStoreInventory> _storeInventory = StartUp<IService<VwStoreInventory>>.Services();
        List<VwStoreInventory> lstStoreInventory = new List<VwStoreInventory>();
        public FrmItemsShortages()
        {
            InitializeComponent();
        }

        private async void FrmItemsShortages_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            lstStoreInventory =await _storeInventory.GetAll();

            var list = lstStoreInventory.OrderBy(i => i.Qty).Select(i => new {i.ItemName , i.Qty}).ToList();
            GcData.DataSource = list;
            GvData.Columns[0].Caption = "الصنف";
            GvData.Columns[1].Caption = "الكميه";
        }
    }
}