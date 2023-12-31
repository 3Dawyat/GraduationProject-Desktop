﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbExpenses
    {
        public TbExpenses()
        {
            TbExpensesPayment = new HashSet<TbExpensesPayment>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Expense")]
        public virtual ICollection<TbExpensesPayment> TbExpensesPayment { get; set; }
    }
}