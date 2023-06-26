using CrystalDecisions.CrystalReports.Engine;
using DevExpress.ClipboardSource.SpreadsheetML;
using DevExpress.XtraEditors;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.MainForms;
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
using System.Web.Services.Description;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SaidalyTechMain.Forms.Returns
{
    public partial class FrmReturns : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        SharedFunctions shared = new SharedFunctions();
        ZiadDataSet data = new ZiadDataSet();
        IService<TbSalesReturns> _SalesReturns = StartUp<IService<TbSalesReturns>>.Services();
        IService<TbSalesReturnsDitails> _SalesReturnsDitails = StartUp<IService<TbSalesReturnsDitails>>.Services();
        IService<VwSalesReturnsDetails> _VwSalesReturnsDetails = StartUp<IService<VwSalesReturnsDetails>>.Services();
        IService<VwPurchasesReturnsHeads> _PurchasesReturnsHeads = StartUp<IService<VwPurchasesReturnsHeads>>.Services();

        IService<TbSalesInvoices> _SalesInvoice = StartUp<IService<TbSalesInvoices>>.Services();
        IService<VwSalesInvoiceItems> _SalesItems = StartUp<IService<VwSalesInvoiceItems>>.Services();
        IService<VwSalesReturnsHeads> _SalesReturnsHeads = StartUp<IService<VwSalesReturnsHeads>>.Services();
        TbSalesInvoices SalesInvoice = new TbSalesInvoices();

        IService<TbPurchasesReturns> _PurchasesReturns = StartUp<IService<TbPurchasesReturns>>.Services();
        IService<TbPurchaseReturnsDitails> _PurchaseReturnsDitails = StartUp<IService<TbPurchaseReturnsDitails>>.Services();
        IService<VwPurchaseReturnsDetails> _VwPurchasesReturnsDetails = StartUp<IService<VwPurchaseReturnsDetails>>.Services();

        IService<TbPurchaseInvoices> _PurchaseInvoice = StartUp<IService<TbPurchaseInvoices>>.Services();
        IService<VwPurchaseInvoiceitems> _PurchaseItems = StartUp<IService<VwPurchaseInvoiceitems>>.Services();
        TbPurchaseInvoices PurchaseInvoice = new TbPurchaseInvoices();


        IService<TbSafes> _Safes = StartUp<IService<TbSafes>>.Services();
        List<TbSafes> LstSafes = new List<TbSafes>();

        IService<TbStores> _Stores = StartUp<IService<TbStores>>.Services();
        List<TbStores> LstStores = new List<TbStores>();
        public FrmReturns()
        {
            InitializeComponent();
        }


        private async void FrmReturns_Load(object sender, EventArgs e)
        {

            GCData.DataSource = data.Tables["Returns"];
            LstSafes = await _Safes.GetListBy(a => a.IsActive == true);
            LstStores = await _Stores.GetAll();


            LpeStores.Properties.DataSource = LstStores.Select(x => new { x.Id, x.Name }).ToList();
            LpeStores.Properties.ValueMember = "Id";
            LpeStores.Properties.DisplayMember = "Name";
            LpeStores.EditValue = 1;

            LpeSafes.Properties.DataSource = LstSafes.Select(x => new { x.Id, x.Name }).ToList();
            LpeSafes.Properties.ValueMember = "Id";
            LpeSafes.Properties.DisplayMember = "Name";
            LpeSafes.EditValue = 1;



        }

        private void txtInvNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtInvNumber.Text))
            {
                splashScreenManager2.ShowWaitForm();
                Enabled = false;
                data.Clear();
                if (chekSales.Checked)
                {
                    var items = await _SalesItems.GetListBy(a => a.InvoiceId == Convert.ToInt32(txtInvNumber.Text));
                    var oldReturns = await _VwSalesReturnsDetails.GetListBy(a => a.InvId == Convert.ToInt32(txtInvNumber.Text));
                    var oldReturn = oldReturns.GroupBy(aa => aa.ItemUnitId)
                    .Select(a => new
                    {
                        ItemUnitId = a.First().ItemUnitId,
                        Qty = a.Sum(c => c.Qty),
                    }).ToList();

                    if (items.Count > 0)
                        SalesInvoice = await _SalesInvoice.GetObjectBy(a => a.Id == items[0].InvoiceId);
                    foreach (var item in items)
                    {
                        var oldItem = oldReturn.FirstOrDefault(a => a.ItemUnitId == item.UnitId);

                        if (oldItem != null)
                        {
                            if (item.Qty - oldItem.Qty > 0)
                                data.Tables["Returns"].Rows.Add(
                                                            false,
                                                            item.ItemArName + " " + item.unitName,
                                                            item.Qty - oldItem.Qty,
                                                            item.SalesPrice,
                                                        Math.Round((double)((item.Qty - oldItem.Qty) * (item.SalesPrice )), 2),
                                                            item.InvoiceId,
                                                            item.UnitId);
                        }
                        else
                        {
                            data.Tables["Returns"].Rows.Add(
                                                       false,
                                                       item.ItemArName + " " + item.unitName,
                                                       item.Qty,
                                                       item.SalesPrice,
                                                         Math.Round((double)(item.Qty * (item.SalesPrice)), 2),
                                                       item.InvoiceId,
                                                       item.UnitId
                                                       );
                        }
                    }
                }
                else
                {
                    var itemms = await _PurchaseItems.GetListBy(a => a.InvoiceId == Convert.ToInt32(txtInvNumber.Text));
                    var oldReturns = await _VwPurchasesReturnsDetails.GetListBy(a => a.InvId == Convert.ToInt32(txtInvNumber.Text));
                    var oldReturn = oldReturns.GroupBy(aa => aa.ItemUnitId)
                    .Select(a => new
                    {
                        ItemUnitId = a.First().ItemUnitId,
                        Qty = a.Sum(c => c.Qty),
                    }).ToList();

                    if (itemms.Count > 0)
                        PurchaseInvoice = await _PurchaseInvoice.GetObjectBy(a => a.Id == itemms[0].InvoiceId);
                    foreach (var itemm in itemms)
                    {
                        var oldItem = oldReturn.FirstOrDefault(a => a.ItemUnitId == itemm.ItemUnitId);
                       
                        if (oldItem != null)
                        {

                            if (itemm.Qty - oldItem.Qty > 0)
                            {

                                data.Tables["Returns"].Rows.Add(false,
                                                                        itemm.ItemArName,
                                                                        itemm.Qty - oldItem.Qty,
                                                                        itemm.PurchasePrice,
                                                                     Math.Round((double)((itemm.Qty - oldItem.Qty) * (itemm.PurchasePrice )), 2),
                                                                        itemm.InvoiceId,
                                                                        itemm.ItemUnitId
                                                                        );
                            }

                        }
                        else
                        {
                            data.Tables["Returns"].Rows.Add(
                                                            false,
                                                            itemm.ItemArName ,
                                                            itemm.Qty,
                                                            itemm.PurchasePrice,
                                                             Math.Round((double)(itemm.Qty * (itemm.PurchasePrice )), 2),
                                                            itemm.InvoiceId,
                                                            itemm.ItemUnitId
                                                            );
                        }
                    }
                }
                GVData.BestFitColumns();
                splashScreenManager2.CloseWaitForm();
                Enabled = true;
            }
        }
        private void GVData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Subtract)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[2].FieldName))) && Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[2].FieldName)) > 1)
                {
                    GVData.SetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[2].FieldName, Convert.ToInt32(GVData.GetRowCellValue(GVData.FocusedRowHandle, GVData.Columns[2].FieldName)) - 1);
                }
            }
        }

        private void txtInvNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void chekSales_CheckedChanged(object sender, EventArgs e)
        {
            data.Clear();
        }

        private void GVData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //qty 2
            //price 3
            //total 5
            if (e.Column.FieldName == "Qty")
            {
                GVData.SetRowCellValue(e.RowHandle, GVData.Columns[6].FieldName, Math.Round(
                    Convert.ToDecimal(e.Value) * Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[3].FieldName)) + Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[4].FieldName)) + Convert.ToDecimal(GVData.GetRowCellValue(e.RowHandle, GVData.Columns[5].FieldName))
                    , 2));
            }


        }

        private async void btnReturnSelected_Click(object sender, EventArgs e)
        {

            if (LpeSafes.EditValue != null && LpeStores.EditValue != null)
            {
                if (data.Tables["Returns"].Rows.Count > 0)
                {
                    splashScreenManager2.ShowWaitForm();
                    Enabled = false;
                    ZiadDataSet newData = new ZiadDataSet();
                    if (chekSales.Checked)
                    {
                        List<TbSalesReturnsDitails> returnsItem = new List<TbSalesReturnsDitails>();
                        for (int i = 0; i < data.Tables["Returns"].Rows.Count; i++)
                        {
                            if ((bool)data.Tables["Returns"].Rows[i][0])
                            {
                                returnsItem.Add(new TbSalesReturnsDitails
                                {
                                    ItemUnitId = Convert.ToInt32(data.Tables["Returns"].Rows[i][6]),
                                    Qty = Convert.ToDecimal(data.Tables["Returns"].Rows[i][2]),
                                    SalesPrice = Convert.ToDecimal(data.Tables["Returns"].Rows[i][3]),
                                    Total = Convert.ToDecimal(data.Tables["Returns"].Rows[i][4]),
                                });
                                



                            }
                        }
                        if (returnsItem.Count > 0)
                        {
                            TbSalesReturns returnInv = new TbSalesReturns
                            {
                                CustomerId = SalesInvoice.CustomerId,
                                InvoiceDate = DateTime.UtcNow.ToLocalTime(),
                                InvType = SalesInvoice.InvoiceType,
                                InvId = SalesInvoice.Id,
                                SafeId = Convert.ToInt32(LpeSafes.EditValue),
                                StoreId = Convert.ToInt32(LpeStores.EditValue),
                                SheftId = Settings.Default.ShiftId,
                                UserId = Settings.Default.UserId,
                                Total = returnsItem.Sum(a => a.Total),
                            };
                            if (await _SalesReturns.Add(returnInv))
                            {

                                returnsItem.ForEach(a => a.ReturnId = returnInv.Id);
                                if (await _SalesReturnsDitails.AddRange(returnsItem))
                                {
                                    MessageBox.Show("تمت العمليه بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Question);

                                    SalesInvoice.Notes = "يوجد مرتجعات لهذة الفاتوره";
                                    if (await _SalesInvoice.Edit(SalesInvoice))
                                    {
                                    }
                                    else
                                        MessageBox.Show("حدث خطأ أثناء تعديل  فاتورة المشتريات الأصليه", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show("حدث خطأ أثناء اضافة اصناف فاتورة المرتجعات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("حدث خطأ أثناء اضافة فاتورة المرتجعات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        List<TbPurchaseReturnsDitails> returnsItem = new List<TbPurchaseReturnsDitails>();
                        for (int i = 0; i < data.Tables["Returns"].Rows.Count; i++)
                        {
                            if ((bool)data.Tables["Returns"].Rows[i][0])
                            {
                                returnsItem.Add(new TbPurchaseReturnsDitails
                                {
                                    ItemUnitId = Convert.ToInt32(data.Tables["Returns"].Rows[i][6]),
                                    Qty = Convert.ToDecimal(data.Tables["Returns"].Rows[i][2]),
                                    SalesPrice = Convert.ToDecimal(data.Tables["Returns"].Rows[i][3]),
                                    Total = Convert.ToDecimal(data.Tables["Returns"].Rows[i][4]),
                                });
                              
                            }
                        }
                        if (returnsItem.Count > 0)
                        {
                            TbPurchasesReturns returnInv = new TbPurchasesReturns
                            {
                                SupplierId = PurchaseInvoice.SupplierId,
                                InvoiceDate = DateTime.UtcNow.ToLocalTime(),
                                InvId = PurchaseInvoice.Id,
                                SafeId = Convert.ToInt32(LpeSafes.EditValue),
                                StoreId = Convert.ToInt32(LpeStores.EditValue),
                                SheftId = Settings.Default.ShiftId,
                                UserId = Settings.Default.UserId,
                                Total = returnsItem.Sum(a => a.Total),
                            };
                            if (await _PurchasesReturns.Add(returnInv))
                            {

                                returnsItem.ForEach(a => a.ReturnId = returnInv.Id);
                                if (await _PurchaseReturnsDitails.AddRange(returnsItem))
                                {
                                    MessageBox.Show("تمت العمليه بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Question);


                                    PurchaseInvoice.Notes = "يوجد مرتجعات لهذة الفاتوره";
                                    if (await _PurchaseInvoice.Edit(PurchaseInvoice))
                                    {
                                        MessageBox.Show("حدث خطأ أثناءاضافه عمليات المستخدم", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("حدث خطأ أثناء تعديل  فاتورة المشتريات الأصليه", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    MessageBox.Show("حدث خطأ أثناء اضافة اصناف فاتورة المرتجعات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("حدث خطأ أثناء اضافة فاتورة المرتجعات", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    txtInvNumber.Text = "";
                    data.Clear();
                    splashScreenManager2.CloseWaitForm();
                    Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("تاكد من اختيار مخزن و خزنه");
            }
        }
       

    }
}