﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class VwSupplierssAccountStatement
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Debt { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Credit { get; set; }
        [Column(TypeName = "numeric(2, 1)")]
        public decimal Balance { get; set; }
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(200)]
        public string SupplierName { get; set; }
        public int? SupplierId { get; set; }
        public bool? isInvoice { get; set; }
    }
}