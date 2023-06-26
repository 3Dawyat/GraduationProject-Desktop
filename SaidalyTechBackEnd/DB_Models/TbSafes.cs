﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbSafes
    {
        public TbSafes()
        {
            TbExpensesPayment = new HashSet<TbExpensesPayment>();
            TbShifts = new HashSet<TbShifts>();
            TbStockTransactionsSafeFrom = new HashSet<TbStockTransactions>();
            TbStockTransactionsSafeTo = new HashSet<TbStockTransactions>();
            TbStockWithdrawAndDeposit = new HashSet<TbStockWithdrawAndDeposit>();
            TbTransaction = new HashSet<TbTransaction>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Safe")]
        public virtual ICollection<TbExpensesPayment> TbExpensesPayment { get; set; }
        [InverseProperty("Safe")]
        public virtual ICollection<TbShifts> TbShifts { get; set; }
        [InverseProperty(nameof(TbStockTransactions.SafeFrom))]
        public virtual ICollection<TbStockTransactions> TbStockTransactionsSafeFrom { get; set; }
        [InverseProperty(nameof(TbStockTransactions.SafeTo))]
        public virtual ICollection<TbStockTransactions> TbStockTransactionsSafeTo { get; set; }
        [InverseProperty("Stock")]
        public virtual ICollection<TbStockWithdrawAndDeposit> TbStockWithdrawAndDeposit { get; set; }
        [InverseProperty("Safe")]
        public virtual ICollection<TbTransaction> TbTransaction { get; set; }
    }
}