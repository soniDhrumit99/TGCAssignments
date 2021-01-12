using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ProductManagement.Models;

namespace ProductManagement.Context
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
    }
}