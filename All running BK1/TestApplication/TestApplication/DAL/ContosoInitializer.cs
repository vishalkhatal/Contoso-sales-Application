using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestApplication.Models;

namespace TestApplication.DAL
{

    public class ContosoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ContosoContext>
    {
        protected override void Seed(ContosoContext contosoContext)
        {
            var listOfProduct = new List<Product>
            {
            new Product{ProductName="Product1",Cost=100,StockAvailable=50},
            new Product{ProductName="Product2",Cost=200,StockAvailable=50},
            new Product{ProductName="Product3",Cost=300,StockAvailable=50}

             };

            listOfProduct.ForEach(item => contosoContext.Products.Add(item));
            //contosoContext.SaveChanges();

            base.Seed(contosoContext);
        }
    }
}