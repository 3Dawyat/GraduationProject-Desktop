﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class VwFormsData
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        public bool? Authorized { get; set; }
    }
}