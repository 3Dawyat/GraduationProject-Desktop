﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class VwSalesInvoiceItems
    {
        public int? InvoiceId { get; set; }
        [StringLength(250)]
        public string ItemArName { get; set; }
        [Required]
        [StringLength(200)]
        public string unitName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalesPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        public int? UnitId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(38, 4)")]
        public decimal? Total { get; set; }
        public int RowId { get; set; }
        public int CategoryId { get; set; }
        public int Id { get; set; }
        [StringLength(200)]
        public string Barcode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvoiceDate { get; set; }
        public int? StoreId { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
    }
}