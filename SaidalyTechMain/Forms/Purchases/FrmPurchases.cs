using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraSplashScreen;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.Purchases;
using SaidalyTechMain.Forms.Sales;
using SaidalyTechMain.Reports;
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

namespace SaidalyTechMain.Forms.Purchases
{
    public partial class FrmPurchases : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbSuppliers> _Suppliers = StartUp<IService<TbSuppliers>>.Services();
        IService<TbSafes> _Safes = StartUp<IService<TbSafes>>.Services();
        IService<TbStores> _Stores = StartUp<IService<TbStores>>.Services();
        IService<VwItemsWithUnits> _Items = StartUp<IService<VwItemsWithUnits>>.Services();
        IService<TbPurchaseInvoiceItems> _PurchaseInvoiceItems = StartUp<IService<TbPurchaseInvoiceItems>>.Services();
        IService<TbPurchaseInvoices> _PurchaseInvoices = StartUp<IService<TbPurchaseInvoices>>.Services();
        IService<VwStoreInventory> _storeInventory = StartUp<IService<VwStoreInventory>>.Services();
        IService<VwSuppliersBalance> _SupplierBalance = StartUp<IService<VwSuppliersBalance>>.Services();
        IService<VwPurchaseInvoiceitems> _PurchaseInvoiceItemsWithNames = StartUp<IService<VwPurchaseInvoiceitems>>.Services();
        List<VwPurchaseInvoiceitems> invoiceItems = new List<VwPurchaseInvoiceitems>();
        TbPurchaseInvoices EditingInvoice;
        public static ZiadDataSet data = new ZiadDataSet();
        List<TbSuppliers> lstSuppliers;
        List<TbSafes> lstSafes;
        List<TbStores> lstStores;
        List<VwItemsWithUnits> lstItems;
        VwItemsWithUnits item;
        RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
        decimal tax = 0;
        List<TbPurchaseInvoiceItems> lstInvoiceItems = new List<TbPurchaseInvoiceItems>();
        List<VwStoreInventory> lstStoreInventory = new List<VwStoreInventory>();
        private List<TbPurchaseInvoices> lstInvoices = new List<TbPurchaseInvoices>();
        private List<TbPurchaseInvoiceItems> lstOldInvoiceItems = new List<TbPurchaseInvoiceItems>();
        SharedFunctions shared = new SharedFunctions();
        SharedGridControlFunctions sharedGrid = new SharedGridControlFunctions();
        int invoiceId = 0;
        List<int> lstDeletedItemsIds;


        public FrmPurchases()
        {
            InitializeComponent();
        }
        public FrmPurchases(int InvoiceId)
        {
            InitializeComponent();
            invoiceId = InvoiceId;
            lstDeletedItemsIds = new List<int>();
        }

        private async void FrmPurchases_Load(object sender, EventArgs e)
        {
            await ReLoadData();

            SetRepoMemoEvents();
            #region gridControl Settings 
            GCData.ForceInitialize();
            GVData.PopulateColumns();
            sharedGrid.SetColumnEditsARepoMemo(GVData, repoMemo, new int[] { 0, 1, 5 });
            GVData.BestFitColumns();
            sharedGrid.HideGridColumns(GVData, new int[] { 7, 4 });
            #region Grid Captions
            GVData.Columns[0].Caption = "Id";
            GVData.Columns[1].Caption = "الصنف";
            GVData.Columns[SalesAndPurchasesConsts.DtQty].Caption = "الكميه";
            GVData.Columns[SalesAndPurchasesConsts.DtPrice].Caption = "السعر";
            GVData.Columns[5].Caption = "الاجمالي";
            GVData.Columns[6].Caption = "كميه المخزن";
            GVData.Columns[8].Caption = "اخر سعر";
            GVData.Columns[9].Caption = "تاريخ الصلاحيه";
            #endregion

            #endregion
            #region gridControl Footer 

            GridColumnSummaryItem siTotal = new GridColumnSummaryItem();
            siTotal.SummaryType = SummaryItemType.Sum;
            siTotal.FieldName = "Total";
            siTotal.DisplayFormat = "{0}:الاجمالي";
            GVData.Columns[5].Summary.Add(siTotal);

            GridColumnSummaryItem siCount = new GridColumnSummaryItem();
            siCount.SummaryType = SummaryItemType.Count;
            siCount.DisplayFormat = "{0}:عدد الاصناف";
            GVData.Columns[1].Summary.Add(siCount);

            GridColumnSummaryItem siDiscount = new GridColumnSummaryItem();
            siDiscount.SummaryType = SummaryItemType.Sum;
            siDiscount.DisplayFormat = "{0}:الخصومات";
            GVData.Columns[4].Summary.Add(siDiscount);

            #endregion
            if (invoiceId > 0)
                EditInvoice(invoiceId);
        }

        private async Task ReLoadData()
        {
            GCData.DataSource = data.Tables["PurchaseItemsDT"];
            textDate.DateTime = DateTime.Now;
            //companyInformation = await _company.GetFirst();
            lstSuppliers = await _Suppliers.GetAll();
            lstSafes = await _Safes.GetListBy(s => s.IsActive == true);
            lstStores = await _Stores.GetAll();
            lstItems = await _Items.GetAll();
            lstStoreInventory = await _storeInventory.GetAll();
            lstInvoices = await _PurchaseInvoices.GetAll();
            lstOldInvoiceItems = await _PurchaseInvoiceItems.GetAll();
            data.Tables["PurchaseItemsDT"].Clear();

            data.Tables["PurchaseItemsDT"].Rows.Add();
            data.Tables["PurchaseItemsDT"].Rows.RemoveAt(0);

            lpeSupplier.Properties.DataSource = lstSuppliers.Select(c => new { c.Id, c.Name }).ToList();
            lpeStok.Properties.DataSource = lstSafes.Select(c => new { c.Id, c.Name }).ToList();
            lpeStore.Properties.DataSource = lstStores.Select(c => new { c.Id, c.Name }).ToList();
            lpeProduct.Properties.DataSource = lstItems.Select(p => new { Id = p.ItemUnitId, Name = p.Name, p.PuchasePrice, p.Barcode }).ToList();


            lpeSupplier.Properties.ValueMember = "Id";
            lpeSupplier.Properties.DisplayMember = "Name";
            lpeSupplier.EditValue = 1;

            lpeStok.Properties.ValueMember = "Id";
            lpeStok.Properties.DisplayMember = "Name";
            lpeStok.EditValue = 1;

            lpeStore.Properties.ValueMember = "Id";
            lpeStore.Properties.DisplayMember = "Name";
            lpeStore.EditValue = 1;

            lpeProduct.Properties.ValueMember = "Id";
            lpeProduct.Properties.DisplayMember = "Name";
            lpeProduct.EditValue = null;

            textBarcode.Focus();

            GC.Collect();

        }


        private async void EditInvoice(int InvoiceId)
        {
            invoiceItems = await _PurchaseInvoiceItemsWithNames.GetListBy(invItem => invItem.InvoiceId == InvoiceId);

            foreach (var item in invoiceItems)
                data.Tables["PurchaseItemsDT"].Rows.Add(item.ItemUnitId, item.ItemArName + " " + item.unitName, item.Qty, item.PurchasePrice, item.Discount, item.Total, lstStoreInventory.Where(n => n.ItemUnitId == item.ItemUnitId).Select(c => c.Qty).FirstOrDefault(), item.RowId, item.ExpiryDate);

            EditingInvoice = await _PurchaseInvoices.GetObjectBy(inv => inv.Id == InvoiceId);
            lpeSupplier.EditValue = EditingInvoice.SupplierId;
            textDate.DateTime = (DateTime)EditingInvoice.InvoiceDate;
            lpeStok.EditValue = EditingInvoice.SafeId;
            lpeStore.EditValue = EditingInvoice.SafeId;
        }
        private void SetRepoMemoEvents()
        {
            repoMemo.ReadOnly = true;

            repoMemo.KeyDown += (object sender2, KeyEventArgs e2) =>
            {
                // GCData_KeyDown(sender2, e2);
                if (e2.KeyCode == Keys.Delete)
                {
                    int rowId = Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[7].FieldName));
                    if (rowId != 0)
                    {
                        lstDeletedItemsIds.Add(rowId);
                        GVData.DeleteRow(GVData.FocusedRowHandle);
                    }
                    else
                        GVData.DeleteRow(GVData.FocusedRowHandle);
                }
                if (e2.KeyCode == Keys.Subtract)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName))) && Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName)) > 1)
                    {
                        GVData.SetRowCellValue(GVData.FocusedRowHandle, GVData.Columns["Qty"], Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName)) - 1);
                    }
                }
                else if (e2.KeyCode == Keys.Add)
                {
                    GVData.SetRowCellValue(GVData.FocusedRowHandle, GVData.Columns["Qty"], Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName)) + 1);
                }
                else if (e2.KeyCode == Keys.F1)
                    btnSave_Click(null, null);
            };

        }
        private void fillGridWithItem(VwItemsWithUnits item)
        {
            bool add = true;
            if (GVData.RowCount != 0)
            {
                for (int i = 0; i < GVData.RowCount; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(GVData.GetRowCellValue(i, GVData.Columns[0].FieldName))) && Convert.ToInt32(GVData.GetRowCellValue(i, GVData.Columns[0].FieldName)) == item.ItemUnitId)
                    {
                        add = false;
                        GVData.SetRowCellValue(i, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName, Convert.ToInt32(GVData.GetRowCellValue(i, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName)) + 1);
                        break;
                    }
                }
            }
            if (add)
            {
                List<TbPurchaseInvoices> ThisSupplierInvoices = lstInvoices.Where(s => s.SupplierId == Convert.ToInt32(lpeSupplier.EditValue)).ToList();
                List<TbPurchaseInvoiceItems> listLastInvoices = lstOldInvoiceItems.Where(items => ThisSupplierInvoices.Select(invoices => invoices.Id).ToList().Contains((int)items.InvoiceId)).ToList();
                List<TbPurchaseInvoiceItems> lstlistLastItemsInvoices = listLastInvoices.Where(i => i.ItemUnitId == Convert.ToInt32(item.ItemUnitId)).ToList();
                if (lstlistLastItemsInvoices.Count > 0)
                {
                    data.Tables["PurchaseItemsDT"].Rows.Add(item.ItemUnitId, item.Name, 1, item.PuchasePrice, 0, item.PuchasePrice, lstStoreInventory.Where(n => n.ItemUnitId == item.ItemUnitId).Select(c => c.Qty).FirstOrDefault(), 0, lstlistLastItemsInvoices[lstlistLastItemsInvoices.Count - 1].PurchasePrice, DateTime.UtcNow.ToLocalTime());

                }
                else
                {
                    data.Tables["PurchaseItemsDT"].Rows.Add(item.ItemUnitId, item.Name, 1, item.PuchasePrice, 0, item.PuchasePrice, lstStoreInventory.Where(n => n.ItemUnitId == item.ItemUnitId).Select(c => c.Qty).FirstOrDefault(), 0, 0, DateTime.UtcNow.ToLocalTime());

                }
            }
        }
        private void InsertPurchaseInvItemsIntoList(int invoiceId)
        {

            for (int i = 0; i < GVData.RowCount; i++)
            {
                decimal qty = Convert.ToDecimal(GVData.GetRowCellValue(i, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName));
                decimal PurchasePrice = Convert.ToDecimal(GVData.GetRowCellValue(i, GVData.Columns[SalesAndPurchasesConsts.DtPrice].FieldName));
                decimal discount = Convert.ToDecimal(GVData.GetRowCellValue(i, GVData.Columns[SalesAndPurchasesConsts.DtDiscount].FieldName));
                decimal total = qty * PurchasePrice;
                TbPurchaseInvoiceItems invoiceItem = new TbPurchaseInvoiceItems
                {
                    Id = Convert.ToInt32(GVData.GetRowCellValue(i, GVData.Columns[7].FieldName)),
                    ItemUnitId = Convert.ToInt32(GVData.GetRowCellValue(i, GVData.Columns[0].FieldName)),
                    InvoiceId = invoiceId,
                    Qty = qty,
                    PurchasePrice = PurchasePrice,
                    Total = total,
                    Discount = discount
                };

                lstInvoiceItems.Add(invoiceItem);
            }
        }
        private decimal ReturnInvoiceTotal()
        {
            decimal total = 0;
            for (int i = 0; i < data.Tables["PurchaseItemsDT"].Rows.Count; i++)
                total += Convert.ToDecimal(data.Tables["PurchaseItemsDT"].Rows[i][5]);

            return total;
        }

        private void textBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(textBarcode.Text))
                {
                    item = lstItems.Where(i => i.Barcode == textBarcode.Text || i.ItemUnitId == Convert.ToInt32(textBarcode.Text)).FirstOrDefault();
                    if (item != null)
                    {
                        item = lstItems.Where(i => i.Barcode == textBarcode.Text || i.ItemUnitId == Convert.ToInt32(textBarcode.Text)).FirstOrDefault();
                        fillGridWithItem(item);
                    }
                    else
                        MessageBox.Show("الباركود غير موجود");
                    textBarcode.Text = "";
                }
            }
            else if (e.KeyCode == Keys.F1)
                btnSave_Click(null, null);

        }

        private void lpeProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lpeProduct.EditValue != null)
                {
                    item = lstItems.Where(i => i.ItemUnitId == Convert.ToInt32(lpeProduct.EditValue)).FirstOrDefault();
                    fillGridWithItem(item);
                }
            }
            else if (e.KeyCode == Keys.F1)
                btnSave_Click(null, null);
        }

        private void GVData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if ((e.Column.ColumnHandle == 2 || e.Column.ColumnHandle == 3 | e.Column.ColumnHandle == 4) && !string.IsNullOrEmpty(Convert.ToString(e.Value)))
            {
                if (!(string.IsNullOrEmpty(Convert.ToString(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtPrice]))) || string.IsNullOrEmpty(Convert.ToString(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtPrice])))))
                {
                    var qty = Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty]));
                    var price = Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtPrice]));
                    var discount = Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtDiscount]));
                    GVData.SetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtTotal], qty * price - discount);
                }

                if (Convert.ToInt32(e.Value) == 0)
                {
                    GVData.SetRowCellValue(e.RowHandle, GVData.Columns[SalesAndPurchasesConsts.DtQty].FieldName, 1);
                    return;
                }


            }
        }

        private void Print(TbPurchaseInvoices invoice)
        {
            #region الطباعه ب كريستال ريبورت
            //ReportDocument recept = new ReportDocument();
            //FileServices fileServices = new FileServices();
            //recept.Load(fileServices.GetReportFile("CryPurchaseInvoice.rpt"));
            CryPurchaseInvoice recept = new CryPurchaseInvoice();
            FrmCryPreview frm = new FrmCryPreview();
            //add first row then delete it cause of a run time problem 
            DataRow newRow = data.Tables["PurchaseItemsDT"].NewRow();

            for (int i = 0; i < data.Tables["PurchaseItemsDT"].Columns.Count; i++)
            {
                newRow[i] = data.Tables["PurchaseItemsDT"].Rows[0][i];
            }
            data.Tables["PurchaseItemsDT"].Rows.Add(newRow);
            data.Tables["PurchaseItemsDT"].Rows.RemoveAt(0);
            //end of 
            recept.SetDataSource(data.Tables["PurchaseItemsDT"]);
            splashScreenManager2.CloseWaitForm();
            this.Enabled = true;
            recept.SetParameterValue("Date", invoice.InvoiceDate);
            recept.SetParameterValue("UserName", Properties.Settings.Default.UserName);
            recept.SetParameterValue("InvId", invoice.Id);
            recept.SetParameterValue("SupplierName", lpeSupplier.Text);
            recept.SetParameterValue("Cash", invoice.Cash);
            recept.SetParameterValue("Discount", invoice.Discount);

            if (shared.Question(MessageBox.Show("معاينه قبل الطباعه ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
            {
                recept.PrintToPrinter(1, true, 1, 0);
                frm.crystalReportViewer1.ReportSource = recept;
                frm.Show();
            }
            else
            {
                recept.PrintToPrinter(1, true, 1, 0);
            }

            #region PDF
            //CrystalDecisions.Shared.ExportOptions CrExportOptions;
            //DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            ////CrDiskFileDestinationOptions.DiskFileName = $"{FrmLogin.ProgramSettings.ReportsPath}{invoice.Id}فاتورة مبيعات رقم  .pdf";
            //CrExportOptions = recept.ExportOptions;
            //{
            //    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            //    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            //    CrExportOptions.FormatOptions = new PdfRtfWordFormatOptions();
            //}
            //recept.Export();
            //Process.Start(CrDiskFileDestinationOptions.DiskFileName);
            #endregion
            recept.Close();
            recept.Dispose();
            invoice.Id = 0;
            data.Tables["PurchaseItemsDT"].Clear();
            #endregion
            #region الطباعه ب اكسترا ريبورت
            //receipt.DataSource = data;
            //receipt.RequestParameters = false;
            //receipt.ShowPrintMarginsWarning = false;
            //receipt.ShowPreviewMarginLines = false;
            //receipt.ShowPrintStatusDialog = false;
            //previewReport.documentViewer1.DocumentSource = receipt;

            //receipt.Parameters["Date"].Value = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")).ToString("dd-MM-yyyy hh:mm tt");

            //receipt.Parameters["UserName"].Value = Properties.Settings.Default.UserName;
            //receipt.Parameters["InvId"].Value = invoice.Id;
            //receipt.Parameters["Visa"].Value = invoice.Visa;
            //receipt.Parameters["Cash"].Value = invoice.Cash;
            //receipt.Parameters["SupplierName"].Value = lpeSupplier.Text;
            //receipt.Parameters["Tax"].Value = invoice.Tax;

            //receipt.PrintingSystem.ShowMarginsWarning = false;
            //receipt.Bands[BandKind.ReportHeader].Controls.Add(new XRPictureBox
            //{
            //    Sizing = ImageSizeMode.ZoomImage,
            //    Size = new Size(100, 100),
            //    LocationF = new PointF(100, 0)
            //});

            //splashScreenManager2.CloseWaitForm();
            //previewReport.ShowDialog();
            //this.Enabled = true;
            //await ReLoadData();

            //receipt.CreateDocument();
            //PrintToolBase tool = new PrintToolBase(receipt.PrintingSystem);
            //tool.Print();
            #endregion

        }

        private void GoWayOfPayment()
        {
            if (EditingInvoice != null && EditingInvoice.Id > 0)
            {
                var total = ReturnInvoiceTotal() - EditingInvoice.Discount;
                FrmWayOfPayment.total = (decimal)total;
                FrmWayOfPayment frm = new FrmWayOfPayment();
                frm.ShowDialog();
            }
            else
            {
                FrmWayOfPayment.total = ReturnInvoiceTotal();
                FrmWayOfPayment frm = new FrmWayOfPayment();
                frm.ShowDialog();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (GVData.RowCount > 0)
            {
                GoWayOfPayment();
                if (FrmWayOfPayment.Chek)
                {
                    if (!splashScreenManager2.IsSplashFormVisible)
                    {
                        splashScreenManager2.ShowWaitForm();
                        this.Enabled = false;
                        // add new invoice
                        TbPurchaseInvoices invoice = new TbPurchaseInvoices
                        {
                            Id = invoiceId,
                            InvoiceDate = Convert.ToDateTime(textDate.Text),
                            CollectionDate = DateTime.UtcNow.ToLocalTime(),
                            SupplierId = Convert.ToInt32(lpeSupplier.EditValue),
                            Discount = FrmWayOfPayment.discount,
                            Cash = FrmWayOfPayment.cash,
                            SheftId = Properties.Settings.Default.ShiftId,
                            UserId = Properties.Settings.Default.UserId,
                            StoreId = Convert.ToInt32(lpeStore.EditValue),
                            RealDate = Convert.ToDateTime(TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")).ToString("dd-MM-yyyy hh:mm tt")),
                            SafeId = Convert.ToInt32(lpeStok.EditValue),

                            Total = ReturnInvoiceTotal(),
                        };

                        if (invoiceId > 0)
                        {
                            invoice.Discount = EditingInvoice.Discount;
                            invoice.SheftId = EditingInvoice.SheftId;
                            invoice.InvoiceDate = EditingInvoice.InvoiceDate;
                            invoice.Total = invoice.Total - invoice.Discount;

                            if (await _PurchaseInvoices.Edit(invoice))
                            {
                                var SupplierAccount = await _SupplierBalance.GetObjectBy(a => a.SupplierId == invoice.SupplierId);
                                var currentDebt = SupplierAccount?.balance ?? 0m;

                                // add  invoice item
                                InsertPurchaseInvItemsIntoList(invoice.Id);


                                await _PurchaseInvoiceItems.DeleteListBy(i => lstDeletedItemsIds.Contains(i.Id));

                                if (await _PurchaseInvoiceItems.EditRange(lstInvoiceItems))
                                {

                                    Print(invoice);
                                }
                                else
                                    MessageBox.Show("حدث مشكله اثناء تعديل اصناف الفاتوره");
                            }
                            else
                            {
                                splashScreenManager2.CloseWaitForm();
                                this.Enabled = true;
                                MessageBox.Show("حدث مشكله اثناء تعديل الفاتوره");
                            }
                            EditingInvoice = new TbPurchaseInvoices();
                        }
                        else
                        {
                            if (await _PurchaseInvoices.Add(invoice))
                            {

                                var SupplierAccount = await _SupplierBalance.GetObjectBy(a => a.SupplierId == invoice.SupplierId);
                                var currentDebt = SupplierAccount?.balance ?? 0m;


                                InsertPurchaseInvItemsIntoList(invoice.Id);


                                if (await _PurchaseInvoiceItems.AddRange(lstInvoiceItems))
                                {

                                    Print(invoice);
                                }
                                else
                                    MessageBox.Show("حدث مشكله اثناء اضافة اصناف الفاتوره");
                            }
                            else
                            {
                                splashScreenManager2.CloseWaitForm();
                                this.Enabled = true;
                                MessageBox.Show("حدث مشكله اثناء اضافة الفاتوره");
                            }
                        }


                        invoiceId = 0;
                        await ReLoadData();
                        lstInvoiceItems.Clear();
                        textBarcode.Focus();

                        //splashScreenManager2.CloseWaitForm();

                    }

                }
                else
                    MessageBox.Show("لا يمكن ان تكون الفاتوره فارغه");
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            await ReLoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}