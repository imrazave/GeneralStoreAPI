﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class Transaction
    {
        // Primary Key
        [Key]
        public int Id { get; set; }

        // Foreign Key
        [ForeignKey(nameof(Customer))]
        public int CustomerID { get; set; }

        // Navigation Property
        public virtual Customer Customer { get; set; }

        // Foreign Key
        [ForeignKey(nameof(Product))]
        public string ProductSKU { get; set; }

        // Navigation Property
        public virtual Product Product { get; set; }

        public int ItemCount { get; set; }

        public DateTime DateOfTransaction { get; set; }
    }
}