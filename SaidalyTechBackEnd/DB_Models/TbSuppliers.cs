﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbSuppliers
    {
        public TbSuppliers()
        {
            TbPurchaseInvoices = new HashSet<TbPurchaseInvoices>();
            TbTransaction = new HashSet<TbTransaction>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("Supplier")]
        public virtual ICollection<TbPurchaseInvoices> TbPurchaseInvoices { get; set; }
        [InverseProperty("Supplier")]
        public virtual ICollection<TbTransaction> TbTransaction { get; set; }
    }
}