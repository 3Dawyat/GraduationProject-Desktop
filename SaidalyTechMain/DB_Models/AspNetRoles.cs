﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            TbAuthorization = new HashSet<TbAuthorization>();
        }

        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
        [StringLength(256)]
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        [InverseProperty("Jop")]
        public virtual ICollection<TbAuthorization> TbAuthorization { get; set; }
    }
}