using DevExpress.Data;
using DevExpress.XtraEditors;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.CustomersAndSuppliers
{
    public partial class FrmSupplierForward : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbSuppliers> _Suppliers = StartUp<IService<TbSuppliers>>.Services();
        IService<VwSalesInvoiceHeds> _SalesInvoice = StartUp<IService<VwSalesInvoiceHeds>>.Services();
        IService<VwSalesInvoiceItems> _SalesInvoiceItems = StartUp<IService<VwSalesInvoiceItems>>.Services();
        IService<VwSupplierssAccountStatement> _SuppliersAccountStatement = StartUp<IService<VwSupplierssAccountStatement>>.Services();
        IService<TbCompanyInformation> _Company = StartUp<IService<TbCompanyInformation>>.Services();
        IService<TbTransaction> _Transaction = StartUp<IService<TbTransaction>>.Services();

        List<VwSupplierssAccountStatement> SupplierAccount = new List<VwSupplierssAccountStatement>();
        List<TbSuppliers> lstSuppliers = new List<TbSuppliers>();
        int id = 0;

        public FrmSupplierForward()
        {
            InitializeComponent();
        }
        public FrmSupplierForward(int Id)
        {
            InitializeComponent();
            id = Id;
        }

        private  void FrmSupplierForward_Load(object sender, EventArgs e)
        {
             FillData();
        }
        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDate.Checked == true)
            {
                dtpDateTo.Enabled = true;
                dtpDateFrom.Enabled = true;
            }
            else
            {
                dtpDateTo.Enabled = false;
                dtpDateFrom.Enabled = false;
            }
        }
        private async void FillData()
        {
            await ReloadData();



            GridColumnSummaryItem siDebt = new GridColumnSummaryItem();
            siDebt.SummaryType = SummaryItemType.Sum;
            siDebt.DisplayFormat = "{0}:مدين";
            GvData.Columns[2].Summary.Add(siDebt);



            GridColumnSummaryItem siCredit = new GridColumnSummaryItem();
            siCredit.SummaryType = SummaryItemType.Sum;
            siCredit.DisplayFormat = "{0}:دائن";
            GvData.Columns[3].Summary.Add(siCredit);


            //GridColumnSummaryItem siBalance = new GridColumnSummaryItem();

            //siBalance.DisplayFormat = "{0}:الرصيد";
            //GvData.Columns[4].Summary.Add();

            if (id > 0)
            {
                lpeSuppliers.EditValue = id;
                btnSerch_Click(null, null);
            }
        }
        private async Task ReloadData()
        {
            lstSuppliers = await _Suppliers.GetAll();
            lpeSuppliers.Properties.DataSource = lstSuppliers.Where(c => c.IsActive == true).Select(c => new { c.Id, c.Name });
            lpeSuppliers.Properties.ValueMember = "Id";
            lpeSuppliers.Properties.DisplayMember = "Name";

            lpeSuppliers.EditValue = null;
            SupplierAccount = new List<VwSupplierssAccountStatement>();

            GcData.DataSource = SupplierAccount;

            GvData.Columns[0].Caption = "ID";
            GvData.Columns[1].Caption = "التاريخ";
            GvData.Columns[2].Caption = "مدين";
            GvData.Columns[3].Caption = "دائن";
            GvData.Columns[4].Caption = "الرصيد";
            GvData.Columns[5].Caption = "النوع";
            GvData.Columns[6].Caption = "العميل";

            GvData.Columns[7].Visible = false;
            GvData.Columns[8].Visible = false;
        }

        private async void btnSerch_Click(object sender, EventArgs e)
        {
            if (lpeSuppliers.EditValue != null)
            {
                SupplierAccount = await _SuppliersAccountStatement.CallStoredProcedure($@"EXEC SpGetSupplierBalance '{Convert.ToInt32(lpeSuppliers.EditValue)}' ");

                if (checkDate.Checked == true)
                {
                    SupplierAccount = SupplierAccount.Where(a => a.InvoiceDate > dtpDateFrom.DateTime && a.InvoiceDate < dtpDateTo.DateTime).ToList();
                }
                GcData.DataSource = SupplierAccount;

            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await ReloadData();
        }

        private void GvData_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (GvData.Columns.Count > 6)
            {
                if (!Convert.ToBoolean(GvData.GetRowCellValue(e.RowHandle, GvData.Columns[8].FieldName)))
                {
                    e.Appearance.BackColor = Color.LightGreen;
                }
                //Override any other formatting  
                e.HighPriority = true;
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            FrmPayMony frm = new FrmPayMony(false);
            frm.ShowDialog();
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[8].FieldName)))
            {

                int invoiceId = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName));

                //  FrmItemAnalysis frm = new FrmItemAnalysis(invoiceId, false);
                //frm.ShowDialog();
            }
            else
            {
                FrmPayMony frm = new FrmPayMony(false, Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName)));
                frm.ShowDialog();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            //string path = $"D/كشف حساب عميل.xlsx";
            //GcData.ExportToXlsx(path);
            //try
            //{
            //    Process.Start(path);
            //}
            //catch
            //{
            //    MessageBox.Show("برجاء اغلاق الملف واعادة التصدير", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (lpeSuppliers.EditValue != null)
            {
                SupplierAccount = await _SuppliersAccountStatement.CallStoredProcedure($@"EXEC SpGetSupplierBalance '{Convert.ToInt32(lpeSuppliers.EditValue)}' ");

                if (checkDate.Checked == true)
                {
                    SupplierAccount = SupplierAccount.Where(a => a.InvoiceDate > dtpDateFrom.DateTime && a.InvoiceDate < dtpDateTo.DateTime).ToList();
                }
                GcData.DataSource = SupplierAccount;

            }
        }


        private void lpeSuppliers_EditValueChanged(object sender, EventArgs e)
        {

        }

    }
}