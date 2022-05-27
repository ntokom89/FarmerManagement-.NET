using PROG7311_Task_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROG7311_Task_2.Controllers
{
    public class FarmerController : Controller
    {
        public static Farmer farmer = new Farmer();
        // GET: Farmer
        public ActionResult FarmerMainPage()
        {
            if ((Product.products != null) && (!Product.products.Any()))
            {
                DALClass.displayProductByFarmer(HomeController.farmer.FarmerID,HomeController.farmer.FarmerName);
            }
            else
            {
                Product.products.Clear();
                DALClass.displayProductByFarmer(HomeController.farmer.FarmerID, HomeController.farmer.FarmerName);
            }

            return View(Product.products);
            //return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        public ActionResult ProductsListFarmer()
        {
            if ((Product.products != null) && (!Product.products.Any()))
            {
                DALClass.displayProductByFarmer(HomeController.farmer.FarmerID, HomeController.farmer.FarmerName);
            }
            else
            {
                Product.products.Clear();
                DALClass.displayProductByFarmer(HomeController.farmer.FarmerID, HomeController.farmer.FarmerName);
            }

            return View(Product.products);
        }
        [HttpPost]

        public ActionResult AddProduct(Product product)
       {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                DALClass.addProduct(HomeController.farmer,101, product.ProductName, product.ProductDescription, product.ProductType,
                   date, product.ProductPrice);

                return View("~/Views/Farmer/ProductsListFarmer.cshtml");
            }
            else
            {
                ModelState.AddModelError("", "Invalid entry to add farmer");
                return View();
            }

            
        }


    }
}