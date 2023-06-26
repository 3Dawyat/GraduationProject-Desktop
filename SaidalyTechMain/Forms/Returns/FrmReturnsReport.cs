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

namespace SaidalyTechMain.Forms.Returns
{
    public partial class FrmReturnsReport : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<VwPurchasesReturnsHeads> _VwPurchasesReturns = StartUp<IService<VwPurchasesReturnsHeads>>.Services();
        IService<VwPurchaseReturnsDetails> _VwPurchaseReturnsDetails = StartUp<IService<VwPurchaseReturnsDetails>>.Services();
        IService<VwSalesReturnsHeads> _VwSalesReturns = StartUp<IService<VwSalesReturnsHeads>>.Services();
        IService<VwSalesReturnsDetails> _VwSalesReturnsDetails = StartUp<IService<VwSalesReturnsDetails>>.Services();

        List<VwSalesReturnsHeads> LstSalesReturns = new List<VwSalesReturnsHeads>();
        List<VwPurchasesReturnsHeads> LstPurchasesReturns;
        List<VwPurchaseReturnsDetails> LstPurchaseReturnsDetails;
        List<VwSalesReturnsDetails> LstSalesReturnsDetails;

        public FrmReturnsReport()
        {
            InitializeComponent();
        }

        private void FrmReturnsReport_Load(object sender, EventArgs e)
        {

        }
        private void GridViewProp()
        {
            GvData.Columns[0].BestFit();
            GvData.Columns[1].BestFit();
            GvData.Columns[2].UnboundDataType = typeof(DateTime);
            GvData.Columns[2].DisplayFormat.FormatString = "dd-MM-yyyy hh:mm tt";
            GvData.Columns[2].BestFit();
            GvData.Columns[3].BestFit();
            GvData.Columns[4].BestFit();
            GvData.Columns[5].BestFit();
            GvData.Columns[5].UnboundDataType = typeof(string);
            GvData.Columns[6].BestFit();
            GvData.Columns[7].BestFit();
        }

        private async void LoadPurchases()
        {
            LstPurchasesReturns = new List<VwPurchasesReturnsHeads>();
            LstPurchaseReturnsDetails = new List<VwPurchaseReturnsDetails>();


            GvData.Columns[5].Caption = "المورد";

            LstPurchasesReturns = await _VwPurchasesReturns.GetAll();
            LstPurchaseReturnsDetails = await _VwPurchaseReturnsDetails.GetAll();
            GridViewProp();
        }
        private async void LoadSales()
        {
            LstSalesReturns = new List<VwSalesReturnsHeads>();
            LstSalesReturnsDetails = new List<VwSalesReturnsDetails>();
            GcData.DataSource = LstSalesReturns.Select(r => new
            {
                r.Id,
                التاريخ = r.InvoiceDate,
                رقم_الفاتوره = r.InvId,
                الفتره = r.SheftId,
                الاجمالي = r.Total,
                العميل = r.customerName,
                المستخدم = r.userName,
                الخصم = r.Discount,
                اصناف_المرتجع = LstSalesReturnsDetails.Where(it => it.ReturnId == 0).Select(i => new { الاسم = i.Name, الكميه = i.Qty, السعر = i.SalesPrice,  الاجمالي = i.Total  }).ToList(),

            });
            GvData.Columns[5].Caption = "العميل";
            LstSalesReturns = await _VwSalesReturns.GetAll();

            LstSalesReturnsDetails = await _VwSalesReturnsDetails.GetAll();
            GridViewProp();
        }


        private void LoadMainData()
        {
            textDateFrom.DateTime = DateTime.UtcNow.ToLocalTime();
            textDateFrom.Enabled = false;
            textDateTo.Enabled = false;
            textDateTo.DateTime = DateTime.UtcNow.ToLocalTime();
            textInvId.Enabled = false;
        }

        private void radioSalesReturns_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSalesReturns.Checked)
            {
                LoadSales();
            }
            else
            {
                LoadPurchases();
            }
        }

        private void radioWithCode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioInvByCode.Checked)
            {
                textInvId.Enabled = true;
                textDateFrom.Enabled = false;
                textDateTo.Enabled = false;
                checkDate.Enabled = false;
                checkDate.Checked = false;
            }
            else if (!radioInvByCode.Checked)
            {
                textInvId.Enabled = false;
                checkDate.Enabled = true;
                checkDate.Checked = false;
            }
        }

        private void checkDate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkDate.Checked)
            {
                textDateFrom.Enabled = true;
                textDateTo.Enabled = true;
            }
            else
            {
                textDateFrom.Enabled = false;
                textDateTo.Enabled = false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (radioSalesReturns.Checked)
            {
                if (radioInvByCode.Checked && !string.IsNullOrEmpty(textInvId.Text))
                {
                    var data = LstSalesReturns.Where(r => r.Id == Convert.ToInt32(textInvId.Text)).Select(r => new
                    {
                        r.Id,
                        التاريخ = r.InvoiceDate,
                        رقم_الفاتوره = r.InvId,
                        الفتره = r.SheftId,
                        الاجمالي = r.Total,
                        العميل = r.customerName,
                        المستخدم = r.userName,
                        الخصم = r.Discount,
                        اصناف_المرتجع = LstSalesReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.Name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total  }).ToList(),
                    }).ToList();
                    GcData.DataSource = data;
                }
                else if (radioAllInv.Checked)
                {
                    if (checkDate.Checked && !string.IsNullOrEmpty(textDateTo.Text) && !string.IsNullOrEmpty(textDateFrom.Text))
                    {
                        var data = LstSalesReturns.Where(r => r.InvoiceDate > textDateFrom.DateTime && r.InvoiceDate < textDateTo.DateTime).Select(r => new
                        {
                            r.Id,
                            التاريخ = r.InvoiceDate,
                            رقم_الفاتوره = r.InvId,
                            الفتره = r.SheftId,
                            الاجمالي = r.Total,
                            العميل = r.customerName,
                            المستخدم = r.userName,
                            الخصم = r.Discount,
                            اصناف_المرتجع = LstSalesReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.Name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total  }).ToList(),
                        }).ToList();
                        GcData.DataSource = data;
                    }
                    else
                    {
                        var data = LstSalesReturns.Select(r => new
                        {
                            r.Id,
                            التاريخ = r.InvoiceDate,
                            رقم_الفاتوره = r.InvId,
                            الفتره = r.SheftId,
                            الاجمالي = r.Total,
                            العميل = r.customerName,
                            المستخدم = r.userName,
                            الخصم = r.Discount,
                            اصناف_المرتجع = LstSalesReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.Name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total  }).ToList(),
                        }).ToList();
                        GcData.DataSource = data;
                    }
                }
            }
            else
            {
                if (radioInvByCode.Checked && !string.IsNullOrEmpty(textInvId.Text))
                {
                    var data = LstPurchasesReturns.Where(r => r.Id == Convert.ToInt32(textInvId.Text)).Select(r => new
                    {
                        r.Id,
                        التاريخ = r.InvoiceDate,
                        رقم_الفاتوره = r.InvId,
                        الفتره = r.SheftId,
                        الاجمالي = r.Total,
                        العميل = r.SupplierName,
                        المستخدم = r.UserName,
                        الخصم = r.Discount,
                        اصناف_المرتجع = LstPurchaseReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total }).ToList(),
                    }).ToList();
                    GcData.DataSource = data;
                }
                else if (radioAllInv.Checked)
                {
                    if (checkDate.Checked && !string.IsNullOrEmpty(textDateTo.Text) && !string.IsNullOrEmpty(textDateFrom.Text))
                    {
                        var data = LstPurchasesReturns.Where(r => r.InvoiceDate > textDateFrom.DateTime && r.InvoiceDate < textDateTo.DateTime).Select(r => new
                        {
                            r.Id,
                            التاريخ = r.InvoiceDate,
                            رقم_الفاتوره = r.InvId,
                            الفتره = r.SheftId,
                            الاجمالي = r.Total,
                            العميل = r.SupplierName,
                            المستخدم = r.UserName,
                            الخصم = r.Discount,
                            اصناف_المرتجع = LstPurchaseReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total }).ToList(),
                        }).ToList();
                        GcData.DataSource = data;
                    }
                    else
                    {
                        var data = LstPurchasesReturns.Select(r => new
                        {
                            r.Id,
                            التاريخ = r.InvoiceDate,
                            رقم_الفاتوره = r.InvId,
                            الفتره = r.SheftId,
                            الاجمالي = r.Total,
                            العميل = r.SupplierName,
                            المستخدم = r.UserName,
                            الخصم = r.Discount,
                            اصناف_المرتجع = LstPurchaseReturnsDetails.Where(it => it.ReturnId == r.Id).Select(i => new { الاسم = i.name, الكميه = i.Qty, السعر = i.SalesPrice, الاجمالي = i.Total  }).ToList(),
                        }).ToList();
                        GcData.DataSource = data;
                    }
                }
                GvData.Columns[5].Caption = "المورد";

            }
            GridViewProp();
        }

        private void textInvId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }

        private void textInvId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(null, null);
            }
        }
    }
}