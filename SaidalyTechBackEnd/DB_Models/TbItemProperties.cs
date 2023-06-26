﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbItemProperties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty(nameof(TbGroups.TbItemProperties))]
        public virtual TbGroups Group { get; set; }
    }
}