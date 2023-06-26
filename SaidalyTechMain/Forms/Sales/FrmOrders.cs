using DevExpress.XtraBars;
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
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.Sales
{
    public partial class FrmOrders : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        IService<VwRoshetaOrders> _RoshetaOrders = StartUp<IService<VwRoshetaOrders>>.Services();
        IService<VwRoshetaOrdersItems> _RoshetaOrdersItems = StartUp<IService<VwRoshetaOrdersItems>>.Services();
        IService<TbItemUnits> _itemUnits = StartUp<IService<TbItemUnits>>.Services();

        List<VwRoshetaOrdersItems> lstRoshetaOrdersDetails = new List<VwRoshetaOrdersItems>();
        List<VwRoshetaOrders> lstRoshetaOrders = new List<VwRoshetaOrders>();

        bool IsOrder = false;
        SharedFunctions shared = new SharedFunctions();
        public FrmOrders(bool isOrder)
        {
            InitializeComponent();
            IsOrder = isOrder;
            if (!isOrder)
                lpTitle.Text = "الروشتات";
        }

        private void FrmOrders_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private async void LoadData()
        {
            GcData.DataSource = lstRoshetaOrders.Where(o => o.OrderType == "1").Select(s => new
            {
                s.Id,
                اسم_العميل = s.CustomerName,
                التاريخ = s.Date,
                اسم_المستخدم = s.UserName,
                اصناف_عرض_الاسعار = lstRoshetaOrdersDetails.Where(d => d.OrderId == s.Id).Select(d => new { d.Id, الصنف = d.ItemName, السعر = d.Price, الكميه = d.Qty, الاجمالي = d.Total }).ToList(),
            }).ToList();

            lstRoshetaOrders = await _RoshetaOrders.GetAll();
            lstRoshetaOrdersDetails = await _RoshetaOrdersItems.GetAll();

        }


        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsOrder)
            {
                lstRoshetaOrders = await _RoshetaOrders.GetAll();
                lstRoshetaOrdersDetails = await _RoshetaOrdersItems.GetAll();
                GcData.DataSource = lstRoshetaOrders.Where(o => o.OrderType == "2").Select(s => new
                {
                    s.Id,
                    اسم_العميل = s.CustomerName,
                    التاريخ = s.Date,
                    اسم_المستخدم = s.UserName,
                    اصناف_عرض_الاسعار = lstRoshetaOrdersDetails.Where(d => d.OrderId == s.Id).Select(d => new { d.Id, الصنف = d.ItemName, السعر = d.OrderId, الكميه = d.Qty, الاجمالي = d.Total }).ToList(),
                }).ToList();
            }
            else
            {
                lstRoshetaOrders = await _RoshetaOrders.GetAll();
                lstRoshetaOrdersDetails = await _RoshetaOrdersItems.GetAll();
                GcData.DataSource = lstRoshetaOrders.Where(o => o.OrderType == "1").Select(s => new
                {
                    s.Id,
                    اسم_العميل = s.CustomerName,
                    التاريخ = s.Date,
                    اسم_المستخدم = s.UserName,
                    اصناف_عرض_الاسعار = lstRoshetaOrdersDetails.Where(d => d.OrderId == s.Id).Select(d => new { d.Id, الصنف = d.ItemName, السعر = d.OrderId, الكميه = d.Qty, الاجمالي = d.Total }).ToList(),
                }).ToList();
            }
        }

        private async void makeSaleInvoice()
        {
            if (GvData.FocusedRowHandle >= 0)
            {
                if (IsOrder)
                {
                    int invoiceId = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName));
                    List<VwRoshetaOrdersItems> items = lstRoshetaOrdersDetails.Where(p => p.OrderId == invoiceId).ToList();
                    int customerId = (int)lstRoshetaOrders.Where(p => p.Id == invoiceId).Select(p => p.CustomerId).FirstOrDefault();
                    shared.OpenForm(new FrmSalesInvoice(items, customerId));
                }
                else
                {
                    int invoiceId = Convert.ToInt32(GvData.GetRowCellValue(GvData.FocusedRowHandle, GvData.Columns[0].FieldName));
                    List<VwRoshetaOrdersItems> items = lstRoshetaOrdersDetails.Where(p => p.OrderId == invoiceId).ToList();
                    var itemUnits = await _itemUnits.GetAll();

                    foreach (var item in items)
                    {
                        item.Price = itemUnits.Where(i => i.Id == item.ItemUnitId).Select(i => i.SalesPrice).FirstOrDefault();
                    }


                    shared.OpenForm(new FrmSalesInvoice(items, 1/*عميل نقدي*/));

                }

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            makeSaleInvoice();
        }
    }
}
