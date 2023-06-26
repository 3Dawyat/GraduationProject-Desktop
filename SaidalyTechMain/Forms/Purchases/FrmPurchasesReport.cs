using DevExpress.XtraEditors;
using System;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Shared;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using SaidalyTechMain.Forms.Purchases;
using DevExpress.Data;

namespace SaidalyTechMain.Forms.Purchases
{
    public partial class FrmPurchasesReport : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        SharedFunctions shared;
        IService<TbPurchaseInvoices> _TbPurchasesInvs = StartUp<IService<TbPurchaseInvoices>>.Services();
        IService<TbPurchaseInvoiceItems> _TbPurchasesInvItems = StartUp<IService<TbPurchaseInvoiceItems>>.Services();
        IService<VwPurchaseInvoiceHeds> _PurchasesInvs = StartUp<IService<VwPurchaseInvoiceHeds>>.Services();
        List<VwPurchaseInvoiceHeds> Invoices = new List<VwPurchaseInvoiceHeds>();
        IService<VwPurchaseInvoiceitems> _PurchasesItems = StartUp<IService<VwPurchaseInvoiceitems>>.Services();
        List<VwPurchaseInvoiceitems> Items = new List<VwPurchaseInvoiceitems>();
        public FrmPurchasesReport()
        {
            InitializeComponent();
        }
        private void FrmPurchasesReport_Load(object sender, EventArgs e)
        {
            AddDataToGridView();
            textCode.Enabled = false;
            #region grid footer 
            GridColumnSummaryItem siCount = new GridColumnSummaryItem();
            siCount.SummaryType = SummaryItemType.Count;
            siCount.DisplayFormat = "عدد الفواتير :  {0}";
            GvData.Columns[0].Summary.Add(siCount);


            GridColumnSummaryItem siCash = new GridColumnSummaryItem();
            siCash.SummaryType = SummaryItemType.Sum;
            siCash.DisplayFormat = "نقدي :  {0}";
            GvData.Columns[3].Summary.Add(siCash);


            GridColumnSummaryItem siLater = new GridColumnSummaryItem();
            siLater.SummaryType = SummaryItemType.Sum;
            siLater.DisplayFormat = "المتبقي :  {0}";
            GvData.Columns[4].Summary.Add(siLater);

            
            GridColumnSummaryItem siDiscount = new GridColumnSummaryItem();
            siDiscount.SummaryType = SummaryItemType.Sum;
            siDiscount.DisplayFormat = "الخصم :  {0}";
            GvData.Columns[5].Summary.Add(siDiscount);

            GridColumnSummaryItem siTotal = new GridColumnSummaryItem();
            siTotal.SummaryType = SummaryItemType.Sum;
            siTotal.DisplayFormat = "اجمالي :  {0}";
            GvData.Columns[6].Summary.Add(siTotal);

            #endregion


        }
        private void FormatDateTime(int[] indexs)
        {
            for (int i = 0; i < indexs.Length; i++)
            {
                GvData.Columns[indexs[i]].DisplayFormat.FormatString = "dd-MM-yyyy hh:mm tt";
            }
            GvData.BestFitColumns();
        }
        private void AddDataToGridView()
        {

            
            GcData.DataSource = Invoices.Select(a => new
            {
                الكود = a.Id,
                تاريخ_الفاتوره = a.Date,
                العميل = a.Customer,
                نقدي = a.Cash,
                المتبقي = a.Later,
                الخصم = a.Discount,
                الأجمالي = a.invoiceTotal,
                المستخدم = a.UserName,
                الفتره = a.Shift,
                تاريخ_الأنشاء = a.RealDate,
                ملاحظات = a.Notes,
                الأصناف = Items.Where(i => i.InvoiceId == a.Id).Select(x => new
                {
                    الصنف = x.ItemArName + " " + x.unitName,
                    الكميه = x.Qty,
                    سعر_البيع = x.PurchasePrice,
                    الأجمالي = x.Total,
                }).ToList()
            });



            GvData.BestFitColumns();
            FormatDateTime(new int[] { 1, 10 });
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            int invoiceId; // initializing invoiceId as an integer

            if (int.TryParse(Convert.ToString(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName)), out invoiceId))
            {
                FrmPurchases frm = new FrmPurchases(invoiceId);
                frm.Show();
            }

        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل حقا تريد مسح الفاتوره", "تحذير", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                if (GvData.FocusedRowHandle >= 0)
                {
                    int invoiceId = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName));

                    await _TbPurchasesInvItems.DeleteListBy(i => i.InvoiceId == invoiceId);
                    await _TbPurchasesInvs.DeleteListBy(i => i.Id == invoiceId);
                }
            }
            GC.Collect();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            this.Enabled = false;
            if (radioAllInvoices.Checked)
            {
                switch (true)
                {
                    case var n when (!string.IsNullOrEmpty(textDateFrom.Text) && !string.IsNullOrEmpty(textDateTo.Text)):
                        Invoices = await _PurchasesInvs.GetListBy(a => a.Date >= textDateFrom.DateTime.Date && a.Date <= textDateTo.DateTime.Date.AddDays(1));
                        Items = await _PurchasesItems.GetListBy(a => a.Date >= textDateFrom.DateTime.Date && a.Date <= textDateTo.DateTime.Date.AddDays(1));
                        AddDataToGridView();
                        break;
                    case var n when (!string.IsNullOrEmpty(textDateFrom.Text) && string.IsNullOrEmpty(textDateTo.Text)):
                        MessageBox.Show("! أدخل تاريخ نهايه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    case var n when (string.IsNullOrEmpty(textDateFrom.Text) && !string.IsNullOrEmpty(textDateTo.Text)):
                        MessageBox.Show("! أدخل تاريخ بداية", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    case var n when (string.IsNullOrEmpty(textDateFrom.Text) && string.IsNullOrEmpty(textDateTo.Text)):
                        Items = await _PurchasesItems.GetAll();
                        Invoices = await _PurchasesInvs.GetAll();
                        AddDataToGridView();
                        break;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(textCode.Text.Trim()))
                {
                    Invoices = await _PurchasesInvs.GetListBy(a => a.Id == Convert.ToInt32(textCode.Text));
                    Items = await _PurchasesItems.GetListBy(a => a.InvoiceId == Convert.ToInt32(textCode.Text));
                    AddDataToGridView();
                }
            }
            splashScreenManager1.CloseWaitForm();
            this.Enabled = true;
            GC.Collect();
        }
        private void radioAllInvoices_CheckedChanged(object sender, EventArgs e)
        {
            textCode.Enabled = !radioAllInvoices.Checked;
            textDateFrom.Enabled = radioAllInvoices.Checked;
            textDateTo.Enabled = radioAllInvoices.Checked;
        }
    }
}