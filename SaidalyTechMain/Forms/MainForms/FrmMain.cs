
using SaidalyTechMain.Forms.StoresAndItems;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SaidalyTechMain.Forms.Sales;
using SaidalyTechMain.Forms.Purchases;
using SaidalyTechMain.Forms.Returns;
using SaidalyTechMain.Forms.CustomersAndSuppliers;
using SaidalyTechMain.Shared;
using SaidalyTechMain.Forms.SafesAndMony;
using SaidalyTechMain.Forms.Users;
using SaidalyTechMain.DB_Models;
using Microsoft.EntityFrameworkCore;
using SaidalyTechMain.Forms.SuppliersAndSuppliers;
using System.Diagnostics;
using SaidalyTechMain.Forms.MainForms;
using SaidalyTechMain.Properties;
using System.Net;
using SaidalyTechMain.BL.IServices;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars;

namespace SaidalyTechMain
{
    public partial class FrmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SharedFunctions shared = new SharedFunctions();
        IService<TbForms> _Forms = StartUp<IService<TbForms>>.Services();
        IService<VwAuthorizationWithNames> _Authorization = StartUp<IService<VwAuthorizationWithNames>>.Services();
        List<VwAuthorizationWithNames> Autorize = new List<VwAuthorizationWithNames>();

        public FrmMain(string jopId)
        {
            InitializeComponent();
            Autorization(jopId);

        }
        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private async  void Autorization(string jopId)
        {

            if (Settings.Default.UserId == "1") //ITS
            {
                List<TbForms> forms = await _Forms.GetAll();
                forms.ForEach(form =>
                {
                    switch (form.ButtonType)
                    {
                        case "barButtonItem":
                            RibbonControlMain.Items.ForEach(item => item.Visibility = BarItemVisibility.Always);
                            break;

                        case "ribbonPage":
                            RibbonControlMain.Pages.GetPageByName(form.ButtonName).Visible = true;
                            break;

                        case "ribbonPageGroup":
                            RibbonControlMain.GetGroupByName(form.ButtonName).Visible = true;
                            break;

                        default:
                            break;
                    }
                });
                RibbonControlMain.GetGroupByName("ribbonPageGroupITS").Visible = true;
            }
            else //anyuser
            {
                Autorize = await _Authorization.GetListBy(a => a.JopId == jopId);
                Autorize.ForEach(autho =>
                {
                    switch (autho.ButtonType)
                    {
                        case "barButtonItem":
                            RibbonControlMain.Items
                                .Where(item => item.Name == autho.ButtonName)
                                .ToList()
                                .ForEach(item => item.Visibility = autho.Authorized ? BarItemVisibility.Always : BarItemVisibility.Never);
                            break;
                        case "ribbonPage":
                            RibbonControlMain.Pages
                                .GetPageByName(autho.ButtonName).Visible = autho.Authorized;
                            break;
                        case "ribbonPageGroup":
                            RibbonControlMain
                                .GetGroupByName(autho.ButtonName).Visible = autho.Authorized;
                            break;
                        case "barSubItem":
                            RibbonControlMain
                                .GetGroupByName(autho.ButtonName).Visible = autho.Authorized;
                            break;
                       
                        default:
                            break;
                    }
                });
            }
        }
        private void BarButtonPuchases_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            
        }

        private void MainPageTileControl_Click(object sender, EventArgs e)
        {

        }

        private void barButtonReceipt_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPurchases frm = new FrmPurchases();
            frm.Show();
        }

        private void barButtonPurchasesReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPurchasesReport frm = new FrmPurchasesReport();
            frm.Show();
        }

        private void barButtonSalesReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmSalesReport frm = new FrmSalesReport();
            frm.Show();
        }

        private void barButtonSalesInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmSalesInvoice frm = new FrmSalesInvoice();
            frm.Show();
        }

        private void barButtonSupplierAccount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmSupplierForward());

        }

        private void barButtonInvoiceThrowback_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmReturns frm = new FrmReturns();
            frm.Show();
        }

        private void barButtonThrowbackReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmReturnsReport frm = new FrmReturnsReport();
            frm.Show();
        }

        private void BarButtonWorker_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonStorage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddStore());
        }

        private void barButtonTreatment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmShowItems());

        }

        private void barButtonCustomerAccount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmCustomerForward());

        }

        private void barButtonDepartment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmCategories());

        }

        private void barButtonAnotherTreatment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmItem());

        }

        private void barButtonMin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmItemsShortages());

        }

        private void btnTransferBetweenSafes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmSafesTransaction());
        }

        private void barButtonTransferBetweenStores_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmStoresTransfer());

        }

        private void barButtonItemsShortage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmItemsShortages());

        }

        private void barButtonStoresReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmStoreInventory());

        }

        private void barButtonAddCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(false));

        }

        private void barButtonAddSupplier_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddCustomerOrSupplier(true));

        }

        private void barButtonPayForSupplier_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmPayMony(true));

        }

        private void barButtonGetFromCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmPayMony(false));

        }

        private void barButtonCustomerData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmCustomers());

        }

        private void barButtonCustmer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmSuppliers());

        }

        private void barButtonJobs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddJob());

        }

        private void barButtonAuthorization_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAuthorization());

        }

        private void barButtonUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmUsersManageement());

        }

        private void barButtonUsersGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddUser());

        }

        private void barButtonSafes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddSafe());

        }

        private void barButtonSupport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://wa.me/+201141309185");
            Process.Start(sInfo);
        }

        private void barButtonConnectUs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("https://wa.me/+201141309185");
            Process.Start(sInfo);
        }

        private void barButtonOrders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmOrders(true));

        }

        private void barButtonOldRosheta_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmOrders(false));
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            shared.OpenForm(new FrmAddNewForms());

        }

       
    }

}
