using MollahThaiGlassHouse.Models;
using MollahThaiGlassHouse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MollahThaiGlassHouse.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly MollahThaiGlassHouseDbContext db = new MollahThaiGlassHouseDbContext();
        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(x => x.TransactionDetails.Select(y => y.Product)).OrderByDescending(x => x.CustomerId).ToList();

            return View(customers);
        }
    
        public ActionResult AddNewProduct(int? id)
        {
            ViewBag.Products = new SelectList(db.Products.ToList(), "ProductId", "ProductName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewProduct");
        
        }
       
        public ActionResult Create ()
        {
            return View();
        }

      
        [HttpPost]
        public ActionResult Create(ClientVM clientVM, int[] productId) 
        {
            if (ModelState.IsValid)
            {
                Customer cutomer = new Customer()
                {
                    CustomerName = clientVM.CustomerName,
                    Address = clientVM.Address,
                    Phone = clientVM.Phone,
                    PurchaseDate = clientVM.PurchaseDate,
                    IsPaid = clientVM.IsPaid

                };

                //image passing
                HttpPostedFileBase file = clientVM.PicturFile;
                if (file != null)
                {

                    string fileName = Path.Combine("/Images/", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    //string fileName = Path.Combine("/Images/", Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

                    file.SaveAs(Server.MapPath(fileName));
                    cutomer.Picture = fileName;
                }
                foreach (var item in productId)
                {
                    TransactionDetail transactionDetail = new TransactionDetail()
                    {
                        Customer = cutomer,
                        CustomerId = cutomer.CustomerId,
                        ProductId = item
                    };
                    db.TransactionDetails.Add(transactionDetail);
                }
                db.SaveChanges();
                return PartialView("_success");


            }
            return PartialView("_error");


        }
       
        public ActionResult Edit(int? id) 
        {
            Customer customer = db.Customers.First(x => x.CustomerId == id);
            var customerProduct = db.TransactionDetails.Where(x => x.CustomerId == id).ToList();
            ClientVM clientVM = new ClientVM()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Address = customer.Address,
                Phone = customer.Phone,
                PurchaseDate = customer.PurchaseDate,
                Picture=customer.Picture,
                IsPaid = customer.IsPaid
            };
            if (customerProduct.Count()>0)
            {
                foreach (var item in customerProduct)
                {
                    clientVM.ProductList.Add(item.ProductId);
                }

            }
            return View(clientVM);

        }
      
        [HttpPost]
        public ActionResult Edit(ClientVM clientVM, int[] productId)
        {
            if (ModelState.IsValid)
            {
                //  Create the object and MANUALLY set the ID
                Customer cutomer = new Customer()
                {
                    CustomerId = clientVM.CustomerId, // This allows EF to find the record
                    CustomerName = clientVM.CustomerName,
                    Address = clientVM.Address,
                    Phone = clientVM.Phone,
                    PurchaseDate = clientVM.PurchaseDate,
                    IsPaid = clientVM.IsPaid
                };

                //  Handle the Picture
                if (clientVM.PicturFile != null)
                {
                    string fileName = "/Images/" + DateTime.Now.Ticks.ToString() + Path.GetExtension(clientVM.PicturFile.FileName);
                    clientVM.PicturFile.SaveAs(Server.MapPath(fileName));
                    cutomer.Picture = fileName;
                }
                else
                {
                    cutomer.Picture = clientVM.Picture; // Keep old picture if new one isn't uploaded
                }

                //  Remove old Details first
                var oldDetails = db.TransactionDetails.Where(x => x.CustomerId == clientVM.CustomerId).ToList();
                db.TransactionDetails.RemoveRange(oldDetails);

                //  Add new Details
                
                foreach (var item in productId)
                {
                     TransactionDetail transactionDetail = new TransactionDetail()
                    {
                        CustomerId = clientVM.CustomerId,
                        ProductId = item
                    };
                    db.TransactionDetails.Add(transactionDetail);
                }

                //  Tell EF this is an existing record and SAVE
                db.Entry(cutomer).State = EntityState.Modified;
                db.SaveChanges();

                return PartialView("_success");
            }
            return PartialView("_error");
        }
   
        public ActionResult Delete(int? id)
        {
            Customer customer = db.Customers.First(x => x.CustomerId == id);
            var customerProduct = db.TransactionDetails.Where(x => x.CustomerId == id).ToList();
            ClientVM clientVM = new ClientVM()
            {
                CustomerId = customer.CustomerId,
                CustomerName = customer.CustomerName,
                Address = customer.Address,
                Phone = customer.Phone,
                Picture = customer.Picture,
                PurchaseDate = customer.PurchaseDate,
                IsPaid = customer.IsPaid
            };
            if (customerProduct.Count() > 0)
            {
                foreach (var item in customerProduct)
                {
                    clientVM.ProductList.Add(item.ProductId);
                }

            }
            return View(clientVM);

        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Customer cutomer = db.Customers.Find(id);
            if (cutomer==null)
            {
                return HttpNotFound();  
            }
            var oldDetails = db.TransactionDetails.Where(x => x.CustomerId == cutomer.CustomerId).ToList();
            db.TransactionDetails.RemoveRange(oldDetails);
            db.Entry(cutomer).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}