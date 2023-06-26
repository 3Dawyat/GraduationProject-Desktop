﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbSalesInvoiceItems
    {
        [Key]
        public int Id { get; set; }
        public int? ItemUnitId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalesPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        public int? InvoiceId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Total { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }

        [ForeignKey(nameof(InvoiceId))]
        [InverseProperty(nameof(TbSalesInvoices.TbSalesInvoiceItems))]
        public virtual TbSalesInvoices Invoice { get; set; }
        [ForeignKey(nameof(ItemUnitId))]
        [InverseProperty(nameof(TbItemUnits.TbSalesInvoiceItems))]
        public virtual TbItemUnits ItemUnit { get; set; }
    }
}