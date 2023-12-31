﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbPurchaseReturnsDitails
    {
        [Key]
        public int Id { get; set; }
        public int? ReturnId { get; set; }
        public int? ItemUnitId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? SalesPrice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "decimal(37, 4)")]
        public decimal? Total { get; set; }

        [ForeignKey(nameof(ItemUnitId))]
        [InverseProperty(nameof(TbItemUnits.TbPurchaseReturnsDitails))]
        public virtual TbItemUnits ItemUnit { get; set; }
        [ForeignKey(nameof(ReturnId))]
        [InverseProperty(nameof(TbPurchasesReturns.TbPurchaseReturnsDitails))]
        public virtual TbPurchasesReturns Return { get; set; }
    }
}