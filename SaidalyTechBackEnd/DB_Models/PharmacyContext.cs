﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class PharmacyContext : DbContext
    {
        public PharmacyContext()
        {
        }

        public PharmacyContext(DbContextOptions<PharmacyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAuthorization> TbAuthorization { get; set; }
        public virtual DbSet<TbCategories> TbCategories { get; set; }
        public virtual DbSet<TbCompanyInformation> TbCompanyInformation { get; set; }
        public virtual DbSet<TbCustomers> TbCustomers { get; set; }
        public virtual DbSet<TbDelivryMen> TbDelivryMen { get; set; }
        public virtual DbSet<TbExpenses> TbExpenses { get; set; }
        public virtual DbSet<TbExpensesPayment> TbExpensesPayment { get; set; }
        public virtual DbSet<TbForms> TbForms { get; set; }
        public virtual DbSet<TbGroups> TbGroups { get; set; }
        public virtual DbSet<TbItemProperties> TbItemProperties { get; set; }
        public virtual DbSet<TbItemUnits> TbItemUnits { get; set; }
        public virtual DbSet<TbItems> TbItems { get; set; }
        public virtual DbSet<TbPrinterSettings> TbPrinterSettings { get; set; }
        public virtual DbSet<TbPurchaseInvoiceItems> TbPurchaseInvoiceItems { get; set; }
        public virtual DbSet<TbPurchaseInvoices> TbPurchaseInvoices { get; set; }
        public virtual DbSet<TbPurchaseReturnsDitails> TbPurchaseReturnsDitails { get; set; }
        public virtual DbSet<TbPurchasesReturns> TbPurchasesReturns { get; set; }
        public virtual DbSet<TbSafes> TbSafes { get; set; }
        public virtual DbSet<TbSalesInvoiceItems> TbSalesInvoiceItems { get; set; }
        public virtual DbSet<TbSalesInvoices> TbSalesInvoices { get; set; }
        public virtual DbSet<TbSalesReturns> TbSalesReturns { get; set; }
        public virtual DbSet<TbSalesReturnsDitails> TbSalesReturnsDitails { get; set; }
        public virtual DbSet<TbShifts> TbShifts { get; set; }
        public virtual DbSet<TbStockTransactions> TbStockTransactions { get; set; }
        public virtual DbSet<TbStockWithdrawAndDeposit> TbStockWithdrawAndDeposit { get; set; }
        public virtual DbSet<TbStoreTransactionDetails> TbStoreTransactionDetails { get; set; }
        public virtual DbSet<TbStores> TbStores { get; set; }
        public virtual DbSet<TbStoresTransaction> TbStoresTransaction { get; set; }
        public virtual DbSet<TbSuppliers> TbSuppliers { get; set; }
        public virtual DbSet<TbTransaction> TbTransaction { get; set; }
        public virtual DbSet<TbTransactionTypes> TbTransactionTypes { get; set; }
        public virtual DbSet<TbUnits> TbUnits { get; set; }
        public virtual DbSet<VwAuthorizationWithFormName> VwAuthorizationWithFormName { get; set; }
        public virtual DbSet<VwAuthorizationWithNames> VwAuthorizationWithNames { get; set; }
        public virtual DbSet<VwCasherItemsWithUnits> VwCasherItemsWithUnits { get; set; }
        public virtual DbSet<VwCustomerLaterInShift> VwCustomerLaterInShift { get; set; }
        public virtual DbSet<VwCustomersAccountStatement> VwCustomersAccountStatement { get; set; }
        public virtual DbSet<VwCustomersBalance> VwCustomersBalance { get; set; }
        public virtual DbSet<VwCustomersTransactions> VwCustomersTransactions { get; set; }
        public virtual DbSet<VwExpensesReport> VwExpensesReport { get; set; }
        public virtual DbSet<VwItemsWithUnits> VwItemsWithUnits { get; set; }
        public virtual DbSet<VwSafesBalance> VwSafesBalance { get; set; }
        public virtual DbSet<VwSafesMoves> VwSafesMoves { get; set; }
        public virtual DbSet<VwStockTransactionsReport> VwStockTransactionsReport { get; set; }
        public virtual DbSet<VwStockWithdrawAndDeposit> VwStockWithdrawAndDeposit { get; set; }
        public virtual DbSet<VwStoresTransactionHeads> VwStoresTransactionHeads { get; set; }
        public virtual DbSet<VwSupplierInvoices> VwSupplierInvoices { get; set; }
        public virtual DbSet<VwSuppliersTransactions> VwSuppliersTransactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SRV-SQLDATA;Initial Catalog=PharmacySystem;Persist Security Info=True;User ID=sa;Password=ITS@2019TeCh!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAuthorization>(entity =>
            {
                entity.HasOne(d => d.Form)
                    .WithMany(p => p.TbAuthorization)
                    .HasForeignKey(d => d.FormId)
                    .HasConstraintName("FK_TbAuthorization_TbForms");
            });

            modelBuilder.Entity<TbCustomers>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreditLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TbExpenses>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TbExpensesPayment>(entity =>
            {
                entity.HasOne(d => d.Expense)
                    .WithMany(p => p.TbExpensesPayment)
                    .HasForeignKey(d => d.ExpenseId)
                    .HasConstraintName("FK_TbExpensesPayment_TbExpenses");

                entity.HasOne(d => d.Safe)
                    .WithMany(p => p.TbExpensesPayment)
                    .HasForeignKey(d => d.SafeId)
                    .HasConstraintName("FK_TbExpensesPayment_TbSafes");

                entity.HasOne(d => d.Sheft)
                    .WithMany(p => p.TbExpensesPayment)
                    .HasForeignKey(d => d.SheftId)
                    .HasConstraintName("FK_TbExpensesPayment_TbShifts");
            });

            modelBuilder.Entity<TbForms>(entity =>
            {
                entity.Property(e => e.Authorized).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TbGroups>(entity =>
            {
                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbGroups)
                    .HasForeignKey(d => d.ItemId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TbGroups_TbItems");
            });

            modelBuilder.Entity<TbItemProperties>(entity =>
            {
                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TbItemProperties)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_TbProperties_TbGroups");
            });

            modelBuilder.Entity<TbItemUnits>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.PuchasePrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.SalesPrice).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitsNumber).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.TbItemUnits)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("FK_TbProductUnits_TbProducts");

                entity.HasOne(d => d.Unit)
                    .WithMany(p => p.TbItemUnits)
                    .HasForeignKey(d => d.UnitId)
                    .HasConstraintName("FK_TbProductUnits_TbUnits");
            });

            modelBuilder.Entity<TbItems>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_TbProducts_TbCategories");
            });

            modelBuilder.Entity<TbPrinterSettings>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TbPrinterSettings)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_TbPrinterSettings_TbCategories");
            });

            modelBuilder.Entity<TbPurchaseInvoiceItems>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasComputedColumnSql("([Qty]*[PurchasePrice]-[Discount])");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbPurchaseInvoices");

                entity.HasOne(d => d.ItemUnit)
                    .WithMany(p => p.TbPurchaseInvoiceItems)
                    .HasForeignKey(d => d.ItemUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoiceItems_TbItemUnits");
            });

            modelBuilder.Entity<TbPurchaseInvoices>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Later).HasComputedColumnSql("([Total]-[Cash])");

                entity.HasOne(d => d.Sheft)
                    .WithMany(p => p.TbPurchaseInvoices)
                    .HasForeignKey(d => d.SheftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoices_TbShifts");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TbPurchaseInvoices)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_TbPurchaseInvoices_TbStores");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TbPurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbPurchaseInvoices_TbSuppliers");
            });

            modelBuilder.Entity<TbPurchaseReturnsDitails>(entity =>
            {
                entity.Property(e => e.Total).HasComputedColumnSql("([Qty]*[SalesPrice])");

                entity.HasOne(d => d.ItemUnit)
                    .WithMany(p => p.TbPurchaseReturnsDitails)
                    .HasForeignKey(d => d.ItemUnitId)
                    .HasConstraintName("FK_TbPurchaseReturnsDitails_TbItemUnits");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.TbPurchaseReturnsDitails)
                    .HasForeignKey(d => d.ReturnId)
                    .HasConstraintName("FK_TbPurchaseReturnsDitails_TbPurchasesReturns");
            });

            modelBuilder.Entity<TbPurchasesReturns>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.StoreId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Total).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Inv)
                    .WithMany(p => p.TbPurchasesReturns)
                    .HasForeignKey(d => d.InvId)
                    .HasConstraintName("FK_TbPurchasesReturns_TbPurchaseInvoices");
            });

            modelBuilder.Entity<TbSafes>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TbSalesInvoiceItems>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Total).HasComputedColumnSql("([Qty]*[SalesPrice]-[Discount])");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbSalesInvoices");

                entity.HasOne(d => d.ItemUnit)
                    .WithMany(p => p.TbSalesInvoiceItems)
                    .HasForeignKey(d => d.ItemUnitId)
                    .HasConstraintName("FK_TbSalesInvoiceItems_TbItemUnits");
            });

            modelBuilder.Entity<TbSalesInvoices>(entity =>
            {
                entity.Property(e => e.Cash).HasDefaultValueSql("((0))");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Later).HasComputedColumnSql("([Total]-([Cash]+[Discount]))");

                entity.Property(e => e.StoreId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Total).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Sheft)
                    .WithMany(p => p.TbSalesInvoices)
                    .HasForeignKey(d => d.SheftId)
                    .HasConstraintName("FK_TbSalesInvoices_TbShifts");
            });

            modelBuilder.Entity<TbSalesReturns>(entity =>
            {
                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.InvType).HasDefaultValueSql("((1))");

                entity.Property(e => e.StoreId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Total).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Inv)
                    .WithMany(p => p.TbSalesReturns)
                    .HasForeignKey(d => d.InvId)
                    .HasConstraintName("FK_TbSalesReturns_TbSalesInvoices");
            });

            modelBuilder.Entity<TbSalesReturnsDitails>(entity =>
            {
                entity.Property(e => e.Total).HasComputedColumnSql("([Qty]*[SalesPrice])");

                entity.HasOne(d => d.ItemUnit)
                    .WithMany(p => p.TbSalesReturnsDitails)
                    .HasForeignKey(d => d.ItemUnitId)
                    .HasConstraintName("FK_TbSalesReturnsDitails_TbItemUnits");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.TbSalesReturnsDitails)
                    .HasForeignKey(d => d.ReturnId)
                    .HasConstraintName("FK_TbSalesReturnsDitails_TbSalesReturns");
            });

            modelBuilder.Entity<TbShifts>(entity =>
            {
                entity.Property(e => e.OpeningBalance).HasDefaultValueSql("((0))");

                entity.Property(e => e.SafeBalance).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Safe)
                    .WithMany(p => p.TbShifts)
                    .HasForeignKey(d => d.SafeId)
                    .HasConstraintName("FK_TbShifts_TbSafes");
            });

            modelBuilder.Entity<TbStockTransactions>(entity =>
            {
                entity.HasOne(d => d.SafeFrom)
                    .WithMany(p => p.TbStockTransactionsSafeFrom)
                    .HasForeignKey(d => d.SafeFromId)
                    .HasConstraintName("FK_TbStockTransactions_TbSafes");

                entity.HasOne(d => d.SafeTo)
                    .WithMany(p => p.TbStockTransactionsSafeTo)
                    .HasForeignKey(d => d.SafeToId)
                    .HasConstraintName("FK_TbStockTransactions_TbSafes1");
            });

            modelBuilder.Entity<TbStockWithdrawAndDeposit>(entity =>
            {
                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.TbStockWithdrawAndDeposit)
                    .HasForeignKey(d => d.StockId)
                    .HasConstraintName("FK_TbStockWithdrawAndDeposit_TbSafes");
            });

            modelBuilder.Entity<TbStoreTransactionDetails>(entity =>
            {
                entity.HasOne(d => d.ItemUnit)
                    .WithMany(p => p.TbStoreTransactionDetails)
                    .HasForeignKey(d => d.ItemUnitId)
                    .HasConstraintName("FK_TbStoreTransactionDetails_TbItemUnits");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TbStoreTransactionDetails)
                    .HasForeignKey(d => d.TransactionId)
                    .HasConstraintName("FK_TbStoreTransactionDetails_TbStoresTransaction");
            });

            modelBuilder.Entity<TbStoresTransaction>(entity =>
            {
                entity.HasOne(d => d.IdStoreFromNavigation)
                    .WithMany(p => p.TbStoresTransactionIdStoreFromNavigation)
                    .HasForeignKey(d => d.IdStoreFrom)
                    .HasConstraintName("FK_TbStoresTransaction_TbStores");

                entity.HasOne(d => d.IdStoreToNavigation)
                    .WithMany(p => p.TbStoresTransactionIdStoreToNavigation)
                    .HasForeignKey(d => d.IdStoreTo)
                    .HasConstraintName("FK_TbStoresTransaction_TbStores1");
            });

            modelBuilder.Entity<TbTransaction>(entity =>
            {
                entity.Property(e => e.SheftId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Cust)
                    .WithMany(p => p.TbTransaction)
                    .HasForeignKey(d => d.CustId)
                    .HasConstraintName("FK_TbTransaction_TbCustomers");

                entity.HasOne(d => d.Safe)
                    .WithMany(p => p.TbTransaction)
                    .HasForeignKey(d => d.SafeId)
                    .HasConstraintName("FK_TbTransaction_TbSafes");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.TbTransaction)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_TbTransaction_TbSuppliers");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.TbTransaction)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .HasConstraintName("FK_TbTransaction_TbTransactionTypes");
            });

            modelBuilder.Entity<VwAuthorizationWithFormName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAuthorizationWithFormName");
            });

            modelBuilder.Entity<VwAuthorizationWithNames>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwAuthorizationWithNames");
            });

            modelBuilder.Entity<VwCasherItemsWithUnits>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCasherItemsWithUnits");
            });

            modelBuilder.Entity<VwCustomerLaterInShift>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomerLaterInShift");
            });

            modelBuilder.Entity<VwCustomersAccountStatement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomersAccountStatement");
            });

            modelBuilder.Entity<VwCustomersBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomersBalance");
            });

            modelBuilder.Entity<VwCustomersTransactions>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwCustomersTransactions");
            });

            modelBuilder.Entity<VwExpensesReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwExpensesReport");
            });

            modelBuilder.Entity<VwItemsWithUnits>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwItemsWithUnits");
            });

            modelBuilder.Entity<VwSafesBalance>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSafesBalance");
            });

            modelBuilder.Entity<VwSafesMoves>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSafesMoves");

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<VwStockTransactionsReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStockTransactionsReport");
            });

            modelBuilder.Entity<VwStockWithdrawAndDeposit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStockWithdrawAndDeposit");
            });

            modelBuilder.Entity<VwStoresTransactionHeads>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwStoresTransactionHeads");
            });

            modelBuilder.Entity<VwSupplierInvoices>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSupplierInvoices");
            });

            modelBuilder.Entity<VwSuppliersTransactions>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VwSuppliersTransactions");
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}