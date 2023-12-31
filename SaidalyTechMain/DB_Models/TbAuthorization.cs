﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechMain.DB_Models
{
    public partial class TbAuthorization
    {
        [Key]
        public int Id { get; set; }
        public int FormId { get; set; }
        public bool Authorized { get; set; }
        [Required]
        [StringLength(450)]
        public string JopId { get; set; }

        [ForeignKey(nameof(FormId))]
        [InverseProperty(nameof(TbForms.TbAuthorization))]
        public virtual TbForms Form { get; set; }
        [ForeignKey(nameof(JopId))]
        [InverseProperty(nameof(AspNetRoles.TbAuthorization))]
        public virtual AspNetRoles Jop { get; set; }
    }
}