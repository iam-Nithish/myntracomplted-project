using Nyntra.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nyntra.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BeautylandingPage()
        {
            return View();
        }
        public ActionResult MenlandingPage()
        {
            return View();
        }
        public ActionResult WomanlandingPage()
        {
            return View();
        }
        public ActionResult KidslandingPage()
        {
            return View();
        }
        public ActionResult Studio()
        {
            return View();
        }
        public ActionResult HomeLandingPage()
        {
            return View();
        }
        public ActionResult Bag()
        {
            return View();
        }
       //
       
      

        // GET: Address/Create
        public ActionResult DeliveryAdress()
        {
            return View();
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeliveryAdress(Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();
                // Redirect to another action or back to Create after success
                return RedirectToAction("selectadress"); // You can change this to any other action if needed
            }

            return View(address); // Return the same view with the model if there are validation errors
        }
        public ActionResult Payment()
        {
            return View();
        }
        public ActionResult selectadress()
        {
            return View();
        }
        public ActionResult successPage()
        {
            return View();
        }
        public ActionResult wishlist()
        {
            return View();
        }
        //admin page
        public ActionResult AdminPage()
        {
            return View();
        }
        //pages
        public ActionResult tshirtsmen()
        {
            return View();
        }
        public ActionResult casualshirtsmens()
        {
            return View();
        }
        public ActionResult Jeans()
        {
            return View();
        }
        public ActionResult casualtousers()
        {
            return View();
        }
        public ActionResult casualshoesmens()
        {
            return View();
        }
        public ActionResult sportsShoesMens()
        {
            return View();
        }

        public ActionResult kurtaSuitsWomen()
        {
            return View();
        }
        public ActionResult kurtisTunicWomen()
        {
            return View();
        }
        public ActionResult dressesWomen()
        {
            return View();
        }
        public ActionResult topWomen()
        {
            return View();
        }
        public ActionResult tShirtsKids()
        {
            return View();
        }

        public ActionResult shirtsKids()
        {
            return View();
        }
        public ActionResult dresesKids()
        {
            return View();
        }
        public ActionResult bedRunnerHome()
        {
            return View();
        }
        public ActionResult mattressHome()
        {
            return View();
        }
        public ActionResult lipsticBeauty()
        {
            return View();
        }























        //login page controller
        private MyntraEntities db = new MyntraEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]



        public ActionResult Login(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var loggedInUser = db.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);


                    if (loggedInUser != null)
                    {
                        // Redirect based on user role
                        if (loggedInUser.Role == "Admin")
                        {
                            return RedirectToAction("AdminPage");
                        }
                        else if (loggedInUser.Role == "User")
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login credentials.");
                    }
                }


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View(user);

        }

        //register page controller

        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if the email already exists
                    var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("", "Email already exists. Please choose another one.");
                        return View(user);
                    }

                    // Add new user and save changes
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


            return View(user);
        }

        //landing pages for mens ,womens and kids

        //here is code for get  deatils from database for user 
        [HttpGet]
        public ActionResult UserPortal()
        {
            try
            {
                using (var context = new MyntraEntities())
                {
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
        //order details
        [HttpGet]
        public ActionResult orderdetails()
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
    }

}
