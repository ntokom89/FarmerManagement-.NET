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

        //Method to get the ProductsListFarmer page and load the list of farmers belong to specified farmer and return it to a view (Miller, 2021) (Saini, 2019).
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
        //Method to get the addProduct page
        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }
        //Method to implement action when the submit button is clicked(Khan, 2019)(getridbug, 2022).
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            //if modelState is valid
            if (ModelState.IsValid)
            {
                //IF the image file is not null
                if (product.ImageToUpload != null)
                { 
                    //Declare the Byte[] variable
                    Byte[] bytes;
                    //get the content length from the image file and assign it to bytes(Khan, 2019) (Miller, 2021) (Saini, 2019)
                    bytes = new byte[product.ImageToUpload.ContentLength];
                    //Read the inputed image file from the location selected(Khan, 2019) (Miller, 2021) (Saini, 2019)
                    product.ImageToUpload.InputStream.Read(bytes, 0, (int)product.ImageToUpload.ContentLength);
                    //Assign the date to add a date for now
                    DateTime date = DateTime.Now;
                    //Implement the addProduct method of the data link class with all required variables
                    DALClass.addProduct(HomeController.farmer, product.ProductName, product.ProductDescription, product.ProductType,
                       date, product.ProductPrice, bytes, product.ProductAmount);
                    //Update the viewbag and inform the user that the product is added
                    ViewBag.result = "Product Inserted Successfully!";
                    return View();
                }
                //Otherwise tell user via viewBag that the image cannot be found
                else
                {
                    ViewBag.result = "Product image name not found!";
                    return View();
                }
            }
            //Otherwise inform user about invalid data entry
            else
            {
                ModelState.AddModelError("", "Invalid entry to add a product");
                return View();
            }

            
        }


    }
}