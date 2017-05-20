using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public decimal Cost { get; set; }
        public int StockAvailable { get; set; }
    }
}