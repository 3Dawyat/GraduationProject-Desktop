﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class VwStoresTransactionHeads
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string From { get; set; }
        [StringLength(100)]
        public string To { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(250)]
        public string Note { get; set; }
    }
}