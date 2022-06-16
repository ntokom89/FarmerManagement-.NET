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
                    if (modelView.user.userId.Contains("fam"))
                    {
                        DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail,
                                           modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);

                        ViewBag.result = "Farmer Inserted Successfully!";
                        return View();
                    }
                    else
                    {
                        ViewBag.result = "Invalid userID. please make sure the userID looks like fam1001 for example!";
                        return View();
                    }
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


        //A method that returns a register view along with allowing to get input values.
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        //A method to take the userObj and use it 
        [HttpPost]
        public ActionResult Register(RegisterModelView vmodelViewRegister)
        {

            if (ModelState.IsValid)
            {
                List<User> users = DALClass.checkUsers(vmodelViewRegister.user.userId);


                if ((users != null) && (!users.Any()))
                {

                    DALClass.RegisterEmployee(vmodelViewRegister.user.userId, vmodelViewRegister.user.password, vmodelViewRegister.user.userEmail, vmodelViewRegister.employee.EmployeeName, vmodelViewRegister.employee.EmployeeSurname);
                    return View("Login");
                }
                else
                {
                    ModelState.AddModelError("", "UserID already exists");
                    return View();
                }
                //    //DALClass.RegisterEmployee(vmodelViewRegister.user.userId, vmodelViewRegister.user.password, vmodelViewRegister.user.userEmail, vmodelViewRegister.employee.EmployeeName, vmodelViewRegister.employee.EmployeeSurname);
                //bool1 = true;
                //return View("Login");
            }
            else
            {

                ModelState.AddModelError("", "Invalid entry for Registration");
                return View();
            }


            // ModelState.AddModelError("", "Invalid entry for Registration");
            // return View();

        }


    }
}