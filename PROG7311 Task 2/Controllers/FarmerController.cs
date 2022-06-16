using PROG7311_Task_2.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

            return View();
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
                if (product.ImageToUpload != null)
                { 
                        Byte[] bytes;
                    bytes = new byte[product.ImageToUpload.ContentLength];
                    product.ImageToUpload.InputStream.Read(bytes, 0, (int)product.ImageToUpload.ContentLength);
                    DateTime date = DateTime.Now;
                    DALClass.addProduct(HomeController.farmer, product.ProductName, product.ProductDescription, product.ProductType,
                       date, product.ProductPrice, bytes, product.ProductAmount);
                    ViewBag.result = "Product Inserted Successfully!";
                    return View();
                }
                else
                {
                    ViewBag.result = "Product image name not found!";
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid entry to add farmer");
                return View();
            }

            
        }


    }
}