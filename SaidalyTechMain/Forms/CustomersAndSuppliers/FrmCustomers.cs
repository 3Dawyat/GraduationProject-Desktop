using DevExpress.Data;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
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

namespace SaidalyTechMain.Forms.CustomersAndSuppliers
{
    public partial class FrmCustomers : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbCustomers> _Customer = StartUp<IService<TbCustomers>>.Services();
        IService<VwCustomersBalance> _CustomerBalance = StartUp<IService<VwCustomersBalance>>.Services();

        List<VwCustomersBalance> lstCustomerBalance = new List<VwCustomersBalance>();
        List<VwCustomersBalance> lstCustomerBalanceShown = new List<VwCustomersBalance>();
        List<TbCustomers> lstCustomers = new List<TbCustomers>();
        SharedFunctions shared = new SharedFunctions();
        List<LambdaExpression> expressions;
        public FrmCustomers()
        {
            InitializeComponent();
        }

        private async void FrmCustomers_Load(object sender, EventArgs e)
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

        private async Task FillGridView()
        {
            GC.Collect();

            lstCustomers = await _Customer.GetAll();
            lstCustomerBalance = await _CustomerBalance.GetAll();
            lstCustomerBalanceShown = lstCustomers.Where(c => c.IsActive == true)
            .Select(c => new VwCustomersBalance()
            {
                CustomerId = c.Id,
                customerName = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Note = c.Note,
                CreditLimit = c.CreditLimit,
                balance = lstCustomerBalance.FirstOrDefault(b => b.CustomerId == c.Id)?.balance ?? 0,
                Debt = lstCustomerBalance.FirstOrDefault(b => b.CustomerId == c.Id)?.Debt ?? 0,
                Credit = lstCustomerBalance.FirstOrDefault(b => b.CustomerId == c.Id)?.Credit ?? 0,
            })
            .ToList();
            GcData.DataSource = lstCustomerBalanceShown;
            GvData.Columns[0].Caption = "الكود";
            GvData.Columns[1].Caption = "الاسم";
            GvData.Columns[2].Caption = "الهاتف";
            GvData.Columns[3].Caption = "العنوان";
            GvData.Columns[4].Caption = "الملاحظات";
            GvData.Columns[5].Caption = "حد الأتمان";
            GvData.Columns[6].Caption = "مدين";
            GvData.Columns[7].Caption = "دائن";
            GvData.Columns[8].Caption = "الرصيد";

            GvData.Columns[9].Visible = false;
            GvData.Columns[5].Visible = false;
            GvData.BestFitColumns();
            //GvData.Columns[3].Visible = false;
            //GvData.Columns[4].Visible = false;
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(false));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle,GvData.Columns[0].FieldName)),false));
        }
        void AddToExprition(Expression<Func<TbCustomers, object>> Expression)
        {
            expressions.Add(Expression);
        }
        private async void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("هل تريد مسح العميل ؟", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                expressions = new List<LambdaExpression>();
                AddToExprition(c => c.IsActive);

                await _Customer.EditProperties(new TbCustomers() { Id = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName)), IsActive = false }, expressions);
               
                await FillGridView();
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await FillGridView();
        }

        private void btnAccountStatement_Click(object sender, EventArgs e)
        {
            shared.OpenForm(new FrmCustomerForward(Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName))));

        }
    }
}
