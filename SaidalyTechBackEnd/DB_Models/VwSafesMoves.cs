﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class VwSafesMoves
    {
        public int SafeId { get; set; }
        public int Id { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string Type { get; set; }
        [Column(TypeName = "decimal(20, 2)")]
        public decimal? Value { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MoveDate { get; set; }
    }
}