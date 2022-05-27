using PROG7311_Task_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult ProductListByFarmers()
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


                return View(Product.products);
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
                DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail, modelView.farmer.FarmerID,
                    modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);
             
                return View();
            }
            else
            {
                ModelState.AddModelError("", "Invalid entry to add farmer");
                return View();
            }

            
        }

    }
}