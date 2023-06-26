using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting.Shape.Native;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.CustomersAndSuppliers;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.SuppliersAndSuppliers
{
    public partial class FrmSuppliers : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbSuppliers> _Supplier = StartUp<IService<TbSuppliers>>.Services();
        IService<VwSuppliersBalance> _SupplierBalance = StartUp<IService<VwSuppliersBalance>>.Services();

        List<VwSuppliersBalance> lstSupplierBalance = new List<VwSuppliersBalance>();
        List<VwSuppliersBalance> lstSupplierBalanceShown = new List<VwSuppliersBalance>();
        List<TbSuppliers> lstSuppliers = new List<TbSuppliers>();
        SharedFunctions shared = new SharedFunctions();
        List<LambdaExpression> expressions;
        public FrmSuppliers()
        {
            InitializeComponent();
        }
        private async void FrmSuppliers_Load(object sender, EventArgs e)
        {
            await FillGridView();
            GridColumnSummaryItem siDebt = new GridColumnSummaryItem();
            siDebt.SummaryType = SummaryItemType.Sum;
            siDebt.DisplayFormat = "{0}:مدين";
            GvData.Columns[5].Summary.Add(siDebt);
            GridColumnSummaryItem siCredit = new GridColumnSummaryItem();
            siCredit.SummaryType = SummaryItemType.Sum;
            siCredit.DisplayFormat = "{0}:دائن";
            GvData.Columns[6].Summary.Add(siCredit);
            GridColumnSummaryItem siBalance = new GridColumnSummaryItem();
            siBalance.SummaryType = SummaryItemType.Sum;
            siBalance.DisplayFormat = "{0}:الرصيد";
            GvData.Columns[7].Summary.Add(siBalance);
        }
       



        private void btnExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private async Task FillGridView()
        {
            GC.Collect();

            lstSuppliers = await _Supplier.GetAll();
            lstSupplierBalance = await _SupplierBalance.GetAll();
            lstSupplierBalanceShown = lstSuppliers.Where(c => c.IsActive == true)
            .Select(c => new VwSuppliersBalance()
            {
                SupplierId = c.Id,
                SupplierName = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Note = c.Note,
                balance = lstSupplierBalance.FirstOrDefault(b => b.SupplierId == c.Id)?.balance ?? 0,
                Debt = lstSupplierBalance.FirstOrDefault(b => b.SupplierId == c.Id)?.Debt ?? 0,
                Credit = lstSupplierBalance.FirstOrDefault(b => b.SupplierId == c.Id)?.Credit ?? 0,
            })
            .ToList();
            GcData.DataSource = lstSupplierBalanceShown;
            GvData.Columns[0].Caption = "الكود";
            GvData.Columns[1].Caption = "الاسم";
            GvData.Columns[2].Caption = "الهاتف";
            GvData.Columns[3].Caption = "العنوان";
            GvData.Columns[4].Caption = "الملاحظات";
            GvData.Columns[5].Caption = "مدين";
            GvData.Columns[6].Caption = "دائن";
            GvData.Columns[7].Caption = "الرصيد";

            GvData.Columns[8].Visible = false;
            GvData.BestFitColumns();
            //GvData.Columns[4].Visible = false;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(true));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName)), true));
        }
        void AddToExprition(Expression<Func<TbSuppliers, object>> Expression)
        {
            expressions.Add(Expression);
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل تريد مسح المورد ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                expressions = new List<LambdaExpression>();
                AddToExprition(c => c.IsActive);

                await _Supplier.EditProperties(new TbSuppliers() { Id = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName)), IsActive = false }, expressions);

                await FillGridView();
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await FillGridView();
        }

        private void btnAccountStatement_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmSupplierForward(Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName))));

        }
    }
}
