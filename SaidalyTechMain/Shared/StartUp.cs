using DevExpress.Xpo.DB;
using Microsoft.Extensions.DependencyInjection;
using SaidalyTechMain.BL.ClsServices;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaidalyTechMain.Shared
{
    public class StartUp<T>
    {

        private static IServiceProvider CongigrtionServices()
        {
            var services = new ServiceCollection()
                .AddScoped<IService<TbItems>, ClsService<TbItems>>()
                .AddScoped<IService<TbCategories>, ClsService<TbCategories>>()
                .AddScoped<IService<TbCompanyInformation>, ClsService<TbCompanyInformation>>()
                .AddScoped<IService<TbCustomers>, ClsService<TbCustomers>>()
                .AddScoped<IService<TbForms>, ClsService<TbForms>>()
                .AddScoped<IService<TbGroups>, ClsService<TbGroups>>()
                .AddScoped<IService<TbItemProperties>, ClsService<TbItemProperties>>()
                .AddScoped<IService<TbItemUnits>, ClsService<TbItemUnits>>()
                .AddScoped<IService<TbSalesInvoiceItems>, ClsService<TbSalesInvoiceItems>>()
                .AddScoped<IService<TbShifts>, ClsService<TbShifts>>()
                .AddScoped<IService<TbUnits>, ClsService<TbUnits>>()
                .AddScoped<IService<TbSalesInvoices>, ClsService<TbSalesInvoices>>()
                .AddScoped<IService<TbAuthorization>, ClsService<TbAuthorization>>()
                .AddScoped<IService<TbDelivryMen>, ClsService<TbDelivryMen>>()
                .AddScoped<IService<TbSafes>, ClsService<TbSafes>>()
                .AddScoped<IService<TbStores>, ClsService<TbStores>>()
                .AddScoped<IService<TbPrinterSettings>, ClsService<TbPrinterSettings>>()
                .AddScoped<IService<TbSuppliers>, ClsService<TbSuppliers>>()
                .AddScoped<IService<TbPurchaseInvoiceItems>, ClsService<TbPurchaseInvoiceItems>>()
                .AddScoped<IService<TbPurchaseInvoices>, ClsService<TbPurchaseInvoices>>()
                .AddScoped<IService<TbExpensesPayment>, ClsService<TbExpensesPayment>>()
                .AddScoped<IService<TbExpenses>, ClsService<TbExpenses>>()
                .AddScoped<IService<TbTransactionTypes>, ClsService<TbTransactionTypes>>()
                .AddScoped<IService<TbTransaction>, ClsService<TbTransaction>>()
                .AddScoped<IService<TbPurchasesReturns>, ClsService<TbPurchasesReturns>>()
                .AddScoped<IService<TbSalesReturns>, ClsService<TbSalesReturns>>()
                .AddScoped<IService<TbSalesReturnsDitails>, ClsService<TbSalesReturnsDitails>>()
                .AddScoped<IService<TbPurchaseReturnsDitails>, ClsService<TbPurchaseReturnsDitails>>()
                .AddScoped<IService<TbStockTransactions>, ClsService<TbStockTransactions>>()
                .AddScoped<IService<TbStockWithdrawAndDeposit>, ClsService<TbStockWithdrawAndDeposit>>()
                .AddScoped<IService<TbStoresTransaction>, ClsService<TbStoresTransaction>>()
                .AddScoped<IService<TbStoreTransactionDetails>, ClsService<TbStoreTransactionDetails>>()
                .AddScoped<IService<TbStoreTransactionDetails>, ClsService<TbStoreTransactionDetails>>()
                .AddScoped<IService<VwItemsWithUnits>, ClsService<VwItemsWithUnits>>()
                .AddScoped<IService<VwAuthorizationWithNames>, ClsService<VwAuthorizationWithNames>>()
                .AddScoped<IService<VwCustomersBalance>, ClsService<VwCustomersBalance>>()
                .AddScoped<IService<VwCustomerLaterInShift>, ClsService<VwCustomerLaterInShift>>()
                .AddScoped<IService<VwCustomersAccountStatement>, ClsService<VwCustomersAccountStatement>>()
                .AddScoped<IService<VwAuthorizationWithFormName>, ClsService<VwAuthorizationWithFormName>>()
                .AddScoped<IService<VwExpensesReport>, ClsService<VwExpensesReport>>()
                .AddScoped<IService<VwCustomersTransactions>, ClsService<VwCustomersTransactions>>()
                .AddScoped<IService<VwSuppliersTransactions>, ClsService<VwSuppliersTransactions>>()
                .AddScoped<IService<VwSupplierInvoices>, ClsService<VwSupplierInvoices>>()
                .AddScoped<IService<VwSafesBalance>, ClsService<VwSafesBalance>>()
                .AddScoped<IService<VwStockTransactionsReport>, ClsService<VwStockTransactionsReport>>()
                .AddScoped<IService<VwStockWithdrawAndDeposit>, ClsService<VwStockWithdrawAndDeposit>>()
                .AddScoped<IService<VwSafesMoves>, ClsService<VwSafesMoves>>()
                .AddScoped<IService<VwStoresTransactionHeads>, ClsService<VwStoresTransactionHeads>>()
                .AddScoped<IService<VwSalesInvoiceHeds>, ClsService<VwSalesInvoiceHeds>>()
                .AddScoped<IService<VwSalesInvoiceItems>, ClsService<VwSalesInvoiceItems>>()
                .AddScoped<IService<VwSuppliersBalance>, ClsService<VwSuppliersBalance>>()
                .AddScoped<IService<VwSupplierssAccountStatement>, ClsService<VwSupplierssAccountStatement>>()
                .AddScoped<IService<VwEachStoreInventory>, ClsService<VwEachStoreInventory>>()
                .AddScoped<IService<VwStoreInventory >, ClsService<VwStoreInventory >> ()
                .AddScoped<IService<VwPurchaseInvoiceitems>, ClsService<VwPurchaseInvoiceitems>> ()
                .AddScoped<IService<VwPurchaseInvoiceHeds>, ClsService<VwPurchaseInvoiceHeds>> ()
                .AddScoped<IService<VwPurchasesReturnsHeads>, ClsService<VwPurchasesReturnsHeads>> ()
                .AddScoped<IService<VwSalesReturnsDetails>, ClsService<VwSalesReturnsDetails>> ()
                .AddScoped<IService<VwPurchaseReturnsDetails>, ClsService<VwPurchaseReturnsDetails>> ()
                .AddScoped<IService<VwSalesReturnsHeads>, ClsService<VwSalesReturnsHeads>> ()
                .AddScoped<IService<AspNetRoles>, ClsService<AspNetRoles>> ()
                .AddScoped<IService<VwRoshetaOrdersItems>, ClsService<VwRoshetaOrdersItems>> ()
                .AddScoped<IService<VwRoshetaOrders>, ClsService<VwRoshetaOrders>> ()
                .AddScoped<IService<VwDesktopUsers>, ClsService<VwDesktopUsers>> ()
                .AddScoped<IService<VwFormsData>, ClsService<VwFormsData>> ()
                .AddScoped<IService<AspNetUsers>, ClsService<AspNetUsers>> ()
                .AddScoped<IService<AspNetUserRoles>, ClsService<AspNetUserRoles>> ()

                .BuildServiceProvider();
            return services;
        }
        public static T Services()
        {
            return CongigrtionServices().GetRequiredService<T>();
        }
    }
}