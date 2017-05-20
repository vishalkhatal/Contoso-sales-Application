using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TestApplication.Models;

namespace TestApplication.DAL
{
    
    public class ContosoContext : DbContext
    {

        public ContosoContext()
            : base("ContosoContext")
        {
            Database.SetInitializer(new ContosoInitializer());

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Invoices> Invoices { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}