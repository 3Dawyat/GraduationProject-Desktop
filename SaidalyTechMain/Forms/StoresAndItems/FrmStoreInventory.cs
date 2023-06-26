using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
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

namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmStoreInventory : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<VwEachStoreInventory> _EachStoreInventory = StartUp<IService<VwEachStoreInventory>>.Services();
        List<VwEachStoreInventory> lstEachStoreInventory = new List<VwEachStoreInventory>();

        public FrmStoreInventory()
        {
            InitializeComponent();
        }

        private async void FrmStoreInventory_Load(object sender, EventArgs e)
        {
            await ReLoadData();


            GvData.Columns[1].Caption = "المخزن";
            GvData.Columns[2].Caption = "الصنف";
            GvData.Columns[4].Caption = "المشتريات";
            GvData.Columns[5].Caption = "مرتجع مبيعات";
            GvData.Columns[6].Caption = "وارد مخزن";
            GvData.Columns[7].Caption = "مبيعات";
            GvData.Columns[8].Caption = "مرتجع مشتريات";
            GvData.Columns[9].Caption = "صادر من المخزن";
            GvData.Columns[10].Caption = "الكميه";

            GridColumnSummaryItem siDiscount = new GridColumnSummaryItem();
            siDiscount.SummaryType = SummaryItemType.Sum;
            siDiscount.DisplayFormat = "{0}:مجموع";
            GvData.Columns[2].Summary.Add(siDiscount);

            GridColumnSummaryItem siCount = new GridColumnSummaryItem();
            siCount.SummaryType = SummaryItemType.Count;
            siCount.DisplayFormat = "{0}:عدد الاصناف";
            GvData.Columns[1].Summary.Add(siCount);

            GridColumnSummaryItem siCapital = new GridColumnSummaryItem();
            siCapital.SummaryType = SummaryItemType.Count;
            siCapital.DisplayFormat = "{0}:راس المال";
            GvData.Columns[5].Summary.Add(siCapital);

            GvData.Columns[0].Visible = false;
            GvData.Columns[3].Visible = false;
        }
        private async Task ReLoadData()
        {
            lstEachStoreInventory = await _EachStoreInventory.GetAll();
            GcData.DataSource = new List<VwEachStoreInventory>();




        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            GcData.DataSource = lstEachStoreInventory;
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await ReLoadData();
        }
    }
}
