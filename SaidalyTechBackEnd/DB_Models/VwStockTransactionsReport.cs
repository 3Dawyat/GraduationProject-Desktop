﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class VwStockTransactionsReport
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string UserName { get; set; }
        [StringLength(200)]
        public string From { get; set; }
        [StringLength(200)]
        public string To { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Qty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
    }
}