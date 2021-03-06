﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GeneralStoreAPI.Models
{
    public class GeneralStoreDBContext : DbContext
    {
        public GeneralStoreDBContext() : base("DefaultConnection") { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; } 
        public DbSet<Transaction> Transactions { get; set; }
    }
}