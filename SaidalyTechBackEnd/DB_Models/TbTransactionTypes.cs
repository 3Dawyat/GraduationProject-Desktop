﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaidalyTechBackEnd.DB_Models
{
    public partial class TbTransactionTypes
    {
        public TbTransactionTypes()
        {
            TbTransaction = new HashSet<TbTransaction>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty("TransactionType")]
        public virtual ICollection<TbTransaction> TbTransaction { get; set; }
    }
}