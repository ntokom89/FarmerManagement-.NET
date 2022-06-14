using Microsoft.AspNetCore.Mvc;
using PROG7311_Task_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PROG7311_Task_2.Controllers
{
    public class EmployeeController : Controller

         
    {

        public static Employee employee1 = new Employee();
        // GET: Employee
        public ActionResult EmployeeMainPage()
        {
            if ((Product.products != null) && (!Product.products.Any()))
            {
                DALClass.displayProducts();
            }
            else
            {
                Product.products.Clear();
                DALClass.displayProducts();
            }

            return View();
        }

        public ActionResult ProductListByFarmers(String orderBy)
        {
            ViewBag.CurrentSort = orderBy;
            ViewBag.ProductTypeSortParm = String.IsNullOrEmpty(orderBy) ? "ProductType_desc" : "";
            ViewBag.DateSortParm = orderBy == "Date" ? "date_desc" : "Date";

            if ((Product.products != null) && (!Product.products.Any()))
                {
                    DALClass.productFarmers();
                }
                else
                {
                    Product.products.Clear();
                    DALClass.productFarmers();
                }

            var products = from p in Product.products
                           select p;
            switch (orderBy)
            {
                case "ProductType_desc":
                    products = products.OrderByDescending(p => p.ProductType);
                    break;
                case "Date":
                    products = products.OrderBy(p => p.DateAddedProduct);
                    break;
                case "date_desc":
                    products = products.OrderByDescending(p => p.DateAddedProduct);
                    break;
                default:
                    products = products.OrderBy(p => p.ProductType);
                    break;
            }

            //return View(Product.products);

            return View(products);
        }

        [HttpGet]
        public ActionResult AddFarmer()
        {
            return View();
        }

        [HttpPost]

        public ActionResult AddFarmer(AddFarmerrModelView modelView) 
        {
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                List<User> users = DALClass.checkUsers(modelView.user.userId);
                if((users != null) && (!users.Any()))
                {
                    DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail,
                                       modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);

                    ViewBag.result = "Farmer Inserted Successfully!";
                    return View();
                }
                else
                {
                    ViewBag.result = "Farmer Insertion unsuccessfull! Change your userID.";
                    return View();

                }
                    //DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail,
                    //modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);


            }
            else
            {
                ModelState.AddModelError("", "Invalid entry to add farmer");
                return View();
            }
            
            
        }

    }
}