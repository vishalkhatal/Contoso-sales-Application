using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestApplication.DAL;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class InvoicesController : Controller
    {
        private ContosoContext db = new ContosoContext();

        // GET: Invoices
        public ActionResult Index()
        {
            var allInvoices = db.Invoices.ToList();
            var DailySales = allInvoices.Where(x=>x.SaleDate.Month==DateTime.Today.Month && x.SaleDate.Year == DateTime.Today.Year && x.SaleDate.Day == DateTime.Today.Day).ToList();
            if(allInvoices!=null)
            ViewBag.OverallSale = allInvoices.Sum(x => x.InvoiceTotal);
            if (DailySales != null)
                ViewBag.DailySale = DailySales.Sum(x => x.InvoiceTotal);

            return View(DailySales);
        }

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            var allProductList = db.Products.Where(x=>x.StockAvailable>0).ToList();
            List<SelectListItem> ListOfItems = new List<SelectListItem>();
            foreach(var product in allProductList)
            {
                ListOfItems.Add(new SelectListItem { Text = product.ProductName, Value = product.ID.ToString() });
            }
            Invoices invoiceObj = new Invoices();
            invoiceObj.ProductList = ListOfItems;
            return View(invoiceObj);
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                bool isStockAvailable = false;
                var product = db.Products.Where(s => s.ID.ToString() == invoices.ProductName).FirstOrDefault<Product>();
                if(product.StockAvailable>invoices.Quantity)
                {
                    product.StockAvailable = product.StockAvailable - invoices.Quantity;
                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    isStockAvailable = true;
                }
                else
                {
                    ViewBag.Message = "Ooops..Insufficient stock for selected product";
                }
                if (isStockAvailable)
                {
                    invoices.SaleDate = DateTime.Now;
                    invoices.InvoiceTotal = invoices.CostPerUnit * invoices.Quantity;
                    db.Invoices.Add(invoices);
                    db.SaveChanges();

                    // decrement product stock
                    ViewBag.Message = "Invoice Added Successfully.";
                }

                return RedirectToAction("Create");
            }
            ViewBag.Message = "Error while saving record in Database.";

            return View(invoices);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Invoices invoices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoices).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(invoices);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoices invoices = db.Invoices.Find(id);
            if (invoices == null)
            {
                return HttpNotFound();
            }
            return View(invoices);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoices invoices = db.Invoices.Find(id);
            db.Invoices.Remove(invoices);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
