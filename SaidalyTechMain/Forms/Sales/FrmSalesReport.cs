using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.MainForms;
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

namespace SaidalyTechMain.Forms.Sales
{
    public partial class FrmSalesReport : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        SharedFunctions shared;
        IService<TbSalesInvoices> _TbSelesInvs = StartUp<IService<TbSalesInvoices>>.Services();
        IService<TbSalesInvoiceItems> _TbSelesInvItems = StartUp<IService<TbSalesInvoiceItems>>.Services();
        IService<VwSalesInvoiceHeds> _SelesInvs = StartUp<IService<VwSalesInvoiceHeds>>.Services();
        List<VwSalesInvoiceHeds> Invoices = new List<VwSalesInvoiceHeds>();
        IService<VwSalesInvoiceItems> _SelesItems = StartUp<IService<VwSalesInvoiceItems>>.Services();
        List<VwSalesInvoiceItems> Items = new List<VwSalesInvoiceItems>();

        public FrmSalesReport()
        {
            InitializeComponent();
        }
        private  void FrmSalesReport_Load(object sender, EventArgs e)
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

            Invoices.ForEach(a =>
            {
                switch (a.InvoiceType)
                {
                    case "1":
                        a.InvoiceType = "تيك اواي";
                        break;
                    case "2":
                        a.InvoiceType = "صاله";
                        break;
                    case "3":
                        a.InvoiceType = "دليفري";
                        break;
                    case "4":
                        a.InvoiceType = "فاتورة مبيعات";
                        break;
                    case "5":
                        a.InvoiceType = "حجز";
                        break;
                    case "6":
                        a.InvoiceType = "مسلمه";
                        break;
                }

            });
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
                الدليفري = a.Deleviry,
                الفتره = a.Shift,
                تاريخ_الأنشاء = a.RealeDate,
                النوع = a.InvoiceType,
                ملاحظات = a.Notes,
                الأصناف = Items.Where(i => i.InvoiceId == a.Id).Select(x => new
                {
                    الصنف = x.ItemArName + " " + x.unitName,
                    الكميه = x.Qty,
                    سعر_البيع = x.SalesPrice,
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
                FrmSalesInvoice frm = new FrmSalesInvoice(invoiceId);
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

                    await _TbSelesInvItems.DeleteListBy(i => i.InvoiceId == invoiceId);
                    await _TbSelesInvs.DeleteListBy(i => i.Id == invoiceId);
                }
            }
            GC.Collect();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            this.Enabled = false;
            if (radioShowAll.Checked)
            {
                switch (true)
                {
                    case var n when (!string.IsNullOrEmpty(textDateFrom.Text) && !string.IsNullOrEmpty(textDateTo.Text)):
                        Invoices = await _SelesInvs.GetListBy(a => a.Date >= textDateFrom.DateTime.Date && a.Date <= textDateTo.DateTime.Date.AddDays(1));
                        Items = await _SelesItems.GetListBy(a => a.InvoiceDate >= textDateFrom.DateTime.Date && a.InvoiceDate <= textDateTo.DateTime.Date.AddDays(1));
                         AddDataToGridView();
                        break;
                    case var n when (!string.IsNullOrEmpty(textDateFrom.Text) && string.IsNullOrEmpty(textDateTo.Text)):
                        MessageBox.Show("! أدخل تاريخ نهايه", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    case var n when (string.IsNullOrEmpty(textDateFrom.Text) && !string.IsNullOrEmpty(textDateTo.Text)):
                        MessageBox.Show("! أدخل تاريخ بداية", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    case var n when (string.IsNullOrEmpty(textDateFrom.Text) && string.IsNullOrEmpty(textDateTo.Text)):
                        Items = await _SelesItems.GetAll();
                        Invoices = await _SelesInvs.GetAll();
                         AddDataToGridView();
                        break;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(textCode.Text.Trim()))
                {
                    Invoices = await _SelesInvs.GetListBy(a => a.Id == Convert.ToInt32(textCode.Text));
                    Items = await _SelesItems.GetListBy(a => a.InvoiceId == Convert.ToInt32(textCode.Text));
                     AddDataToGridView();
                }
            }
            splashScreenManager1.CloseWaitForm();
            this.Enabled = true;
            GC.Collect();
        }

        private void radioShowAll_CheckedChanged(object sender, EventArgs e)
        {
            textCode.Enabled = !radioShowAll.Checked;
            textDateFrom.Enabled = radioShowAll.Checked;
            textDateTo.Enabled = radioShowAll.Checked;


            
        }
    }
}