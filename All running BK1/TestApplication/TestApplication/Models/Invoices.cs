using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApplication.Models
{
    public class Invoices
    {       
            public int ID { get; set; }
            public DateTime SaleDate { get; set; }
            public decimal InvoiceTotal { get; set; }
            public string CustomerName { get; set; }
            public int ProductId { get; set; }
            public int CostPerUnit { get; set; }
        public int Quantity { get; set; }

    }
}