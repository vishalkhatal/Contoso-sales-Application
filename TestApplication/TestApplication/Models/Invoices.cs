using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestApplication.Models
{
    public class Invoices
    {
        [DisplayName("Invoice Id")]
        public int ID { get; set; }

        [DisplayName("Date Of Sale")]
        public DateTime SaleDate { get; set; }

        [DisplayName("Total Amount")]
        public double InvoiceTotal { get; set; }

        [Required(ErrorMessage = "Please Enter Customer Name")]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please Enter Cost Per Unit")]
        [DisplayName("Cost Per Unit")]
        [Range(1, 1000,
            ErrorMessage = "Cost Per Unit must be between 1 and 1000")]
        public int CostPerUnit { get; set; }

        [Required(ErrorMessage = "Please Enter Product Quantity")]
        [Range(1, 100,
            ErrorMessage = "Quantity must be between 1 and 100")]
        public int Quantity { get; set; }

        [NotMapped]
        public int Discount { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> ProductList { get; set; }

    }
}