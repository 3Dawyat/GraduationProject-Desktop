﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class VwAuthorizationWithNames
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public bool Authorized { get; set; }
        [Required]
        [StringLength(200)]
        public string JopId { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string ButtonName { get; set; }
        [StringLength(200)]
        public string ButtonType { get; set; }
        public int? ParentId { get; set; }
    }
}