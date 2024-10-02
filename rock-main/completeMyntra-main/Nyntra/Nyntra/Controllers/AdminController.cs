using Nyntra.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Text;
using Rotativa;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Web.UI.WebControls;



namespace Nyntra.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyntraEntities _context;

     
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyProfile()
        {
            return View();
        }

       
        //display
        [HttpGet]
        public ActionResult ProductList()
        {
            try
            {
                using (var context = new MyntraEntities())
                {
                    int activeProductsCount = GetActiveProductsCount(); // Get count of active products
                    int inactiveProductsCount = GetInactiveProductsCount();
                    decimal totalPrice = GetTotalPrice();
                    int totalProducts = GetTotalProducts();
                    ViewBag.ActiveProductsCount = activeProductsCount; // Store active products count in ViewBag
                    ViewBag.InactiveProductsCount = inactiveProductsCount;
                    ViewBag.TotalProducts = totalProducts;
                    ViewBag.TotalPrice = totalPrice;
                    var products = context.Products.ToList();
                    return View(products);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Response.Write(ex.Message);
                return View(new List<Product>()); // Return an empty list in case of error
            }
        }
        public int GetTotalProducts()
        {
            using (var context = new MyntraEntities())
            {
                // Count the total number of products
                int totalProducts = context.Products.Count();
                return totalProducts;
            }
        }
        //total price
        public decimal GetTotalPrice()
        {
            using (var context = new MyntraEntities())
            {
                // Sum the ProductPrice from the Products table
                decimal totalPrice = context.Products.Sum(p => p.ProductPrice) ?? 0; // Use null coalescing operator to handle null values
                return totalPrice;
            }
        }
        public int GetActiveProductsCount()
        {
            using (var context = new MyntraEntities())
            {
                // Count the number of products where status is 'Active'
                int activeProductsCount = context.Products.Count(p => p.Status == "Active");
                return activeProductsCount;
            }
        }
        public int GetInactiveProductsCount()
        {
            using (var context = new MyntraEntities())
            {
                // Count the number of products where status is 'Inactive'
                int inactiveProductsCount = context.Products.Count(p => p.Status == "Inactive");
                return inactiveProductsCount;
            }
        }

        private MyntraEntities db = new MyntraEntities();

        // GET: Product/Create
        [HttpGet]
        public ActionResult AddProducts()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult AddProducts(Product productData, HttpPostedFileBase ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    // Ensure the directory exists
                    string directoryPath = Server.MapPath("~/Images/");
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Save the file to the server
                    string path = Path.Combine(directoryPath, Path.GetFileName(ImageFile.FileName));
                    ImageFile.SaveAs(path);

                    // Set the ImagePath for storing in the database
                    productData.ProductImage = "~/Images/" + ImageFile.FileName;
                }

                // Save the product data to the database
                db.Products.Add(productData);
                int resultValue = db.SaveChanges();

                if (resultValue > 0)
                {
                    ViewBag.Message = "Product added successfully";
                }
                else
                {
                    ViewBag.Message = "Product not inserted";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }
            return View(productData);
        }


        //edit
        [HttpGet]
        public ActionResult EditProducts(int id)
        {
            using (var context = new MyntraEntities())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound(); // Return 404 if the product is not found
                }
                return View(product);
            }
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult EditProducts(Product model, HttpPostedFileBase ProductImage)
        {
            try
            {
                using (var context = new MyntraEntities())
                {
                    var existingProduct = context.Products.Find(model.ProductID);
                    if (existingProduct != null)
                    {
                        existingProduct.ProductName = model.ProductName;
                        existingProduct.ProductPrice = model.ProductPrice;
                        existingProduct.ProductDescription = model.ProductDescription;
                        existingProduct.Category = model.Category;
                        existingProduct.Color = model.Color;
                        existingProduct.Brand = model.Brand;

                        // Check if a new image is uploaded
                        if (ProductImage != null && ProductImage.ContentLength > 0)
                        {
                            // Save the new image to a folder and get the path
                            var fileName = Path.GetFileName(ProductImage.FileName);
                            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            ProductImage.SaveAs(path);
                            existingProduct.ProductImage = "~/Images/" + fileName; // Save the relative path to the database
                        }

                        context.SaveChanges(); // Save changes to the database
                        return RedirectToAction("ProductList"); // Redirect after successful edit
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message); // Add the error message to the model state
            }

            return View(model); // Return the view with the model in case of error
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (var context = new MyntraEntities())
            {
                var product = context.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound(); // Return 404 if the product is not found
                }
                return View(product);
            }
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var context = new MyntraEntities())
            {
                var product = context.Products.Find(id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
                return RedirectToAction("ProductList"); // Redirect to the product list after deletion
            }
        }

       

        // Display Addresses
        [HttpGet]
        public ActionResult Addresses()
        {
            try
            {
                using (var context = new MyntraEntities())
                {
                    var addresses = context.Addresses.ToList(); // Assuming you have an Addresses DbSet
                    return View(addresses);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it)
                Response.Write(ex.Message);
                return View(new List<Address>()); // Return an empty list in case of error
            }
        }
        // GET: Admin/DeleteAddress/5
        [HttpGet]
        public ActionResult DeleteAddress(int id)
        {
            using (var context = new MyntraEntities())
            {
                var address = context.Addresses.Find(id);
                if (address == null)
                {
                    return HttpNotFound(); // Return 404 if the address is not found
                }
                return View(address); // Pass the address model to the view
            }
        }

        // POST: Admin/DeleteAddress/5
        [HttpPost, ActionName("DeleteAddress")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAddressConfirmed(int id)
        {
            using (var context = new MyntraEntities())
            {
                var address = context.Addresses.Find(id);
                if (address != null)
                {
                    context.Addresses.Remove(address); // Remove the address from the database
                    context.SaveChanges(); // Save the changes
                }
                return RedirectToAction("Addresses"); // Redirect to the list of addresses after deletion
            }
        }

        //using System.IO;

        // GET: Export CSV
        public ActionResult ExportToCsv()
        {
            try
            {
                // Fetch the list from your database
                using (var context = new MyntraEntities())
                {
                    var addresses = context.Addresses.ToList();

                    // Create a StringBuilder to hold CSV data
                    var csv = new StringBuilder();

                    // Add the CSV header
                    csv.AppendLine("AddressID,ProductName,FullName,Mobile,PinCode,Address1,Locality,CityOrDistrict,State");

                    // Loop through the data and add rows to the CSV
                    foreach (var address in addresses)
                    {
                        csv.AppendLine($"{address.AddressID},{address.ProductName},{address.FullName},{address.Mobile},{address.PinCode},{address.Address1},{address.Locality},{address.CityOrDistrict},{address.State}");
                    }

                    // Return the CSV file as a download
                    return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "Addresses.csv");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new HttpStatusCodeResult(500, ex.Message);
            }

        }
        public ActionResult ProductExportToCsv()
        {
            try
            {
                // Fetch the list of products from your database
                using (var context = new MyntraEntities())
                {
                    var products = context.Products.ToList(); // Assuming you have a Products DbSet

                    // Create a StringBuilder to hold CSV data
                    var csv = new StringBuilder();

                    // Add the CSV header
                    csv.AppendLine("ProductID,ProductName,ProductPrice,ProductDescription,Category,Color,Brand,ProductImage,Status");

                    // Loop through the data and add rows to the CSV
                    foreach (var product in products)
                    {
                        // Handle nullable decimal for ProductPrice
                        var productPrice = product.ProductPrice.HasValue ? product.ProductPrice.Value.ToString("F2") : "N/A";

                        csv.AppendLine($"{product.ProductID},{product.ProductName},{productPrice},{product.ProductDescription},{product.Category},{product.Color},{product.Brand},{product.ProductImage},{product.Status}");
                    }

                    // Return the CSV file as a download
                    return File(Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "Products.csv");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                return new HttpStatusCodeResult(500, ex.Message);
            }
        }

        // GET: Export PDF
        public ActionResult ProductExportToPdf
        {
            get
            {
                using (var context = new MyntraEntities())
                {
                    // Fetch the list from your database
                    var addresses = context.Products.ToList();

                    // Return the PDF view with the list of addresses
                    return new ViewAsPdf("ProductList", addresses)
                    {
                        FileName = "Products.pdf"
                    };
                }
            }
        }
    }

    }








    
