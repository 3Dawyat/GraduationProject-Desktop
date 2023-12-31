﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class VwPurchaseInvoiceHeds
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RealDate { get; set; }
        [StringLength(200)]
        public string Safe { get; set; }
        [StringLength(200)]
        public string Customer { get; set; }
        public int SupplierId { get; set; }
        [StringLength(256)]
        public string UserName { get; set; }
        [Required]
        [StringLength(450)]
        public string UserId { get; set; }
        public int Shift { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Cash { get; set; }
        [Column(TypeName = "decimal(19, 0)")]
        public decimal? Later { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? Discount { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? invoiceTotal { get; set; }
        public string Notes { get; set; }
        public int? SafeId { get; set; }
        public int? StoreId { get; set; }
    }
}