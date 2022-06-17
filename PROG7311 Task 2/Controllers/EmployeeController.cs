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

        //public static Employee employee1 = new Employee();
        // GET: EmployeeMainPage 
        public ActionResult EmployeeMainPage()
        {

            return View();
        }
        //A method to return a list of products that can be ordered by date or product type (Microsoft, 2022)
        public ActionResult ProductListByFarmers(String orderBy)
        {
            //A viewBags that are used to get a type of the ordering of a attribute (Microsoft, 2022)
            ViewBag.CurrentSort = orderBy;
            ViewBag.ProductTypeSortParm = String.IsNullOrEmpty(orderBy) ? "ProductType_desc" : "";
            ViewBag.DateSortParm = orderBy == "Date" ? "date_desc" : "Date";

            //Implement the method to produce a list of products by each farmer (Miller, 2021) (Saini, 2019)
            if ((Product.products != null) && (!Product.products.Any()))
            {
                DALClass.productFarmers();
            }
            else
            {
                Product.products.Clear();
                DALClass.productFarmers();
            }
            //A linq statement to get a list 
            var products = from p in Product.products
                           select p;
            //Case statements for the product type and date ordering by (Microsoft, 2022) (Miller, 2021) (Saini, 2019)
            switch (orderBy)
            {
                case "ProductType_desc":
                    //Order the product type by descending
                    products = products.OrderByDescending(p => p.ProductType);
                    break;
                case "Date":
                    //Orderby the date when product is added
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
            //return a view with the list of products
            return View(products);
        }
        //GET the AddFarmer page
        [HttpGet]
        public ActionResult AddFarmer()
        {
            return View();
        }
        //Method that will take the input from the view and add the farmer to the database(Microsoft, 2022)
        [HttpPost]
        public ActionResult AddFarmer(AddFarmerrModelView modelView) 
        {
            //If modelState is valid
            if (ModelState.IsValid)
            {
                DateTime date = DateTime.Now;
                //check userID if it already exists
                List<User> users = DALClass.checkUsers(modelView.user.userId);
                //If there is nothing in the users list then
                if((users != null) && (!users.Any()))
                {
                    //if the userID contains the word fam then implement the addFarmer method to add the farmer
                    if (modelView.user.userId.Contains("fam"))
                    {
                        DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail,
                                           modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);

                        //Notify user that the farmer is added to the database
                        ViewBag.result = "Farmer Inserted Successfully!";
                        return View();
                    }
                    else
                    {
                        //Notify user that the userID is invalid
                        ViewBag.result = "Invalid userID. please make sure the userID looks like fam1001 for example";
                        return View();
                    }
                }
                else
                {
                    //Notify user that the userID entered already existed
                    ViewBag.result = "Farmer Insertion unsuccessfull! Change your userID.";
                    return View();

                }
                    //DALClass.addFarmer(HomeController.employee1, modelView.user.userId, modelView.user.password, modelView.user.userEmail,
                    //modelView.farmer.FarmerName, modelView.farmer.FarmerSurname, date);


            }
            else
            {
                //Notify user that the entry of a farmer is invalid
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
        //A method to take the register the employee
        [HttpPost]
        public ActionResult Register(RegisterModelView vmodelViewRegister)
        {
            //if the ModelState is valid
            if (ModelState.IsValid)
            {
                //check if the userID already exists
                List<User> users = DALClass.checkUsers(vmodelViewRegister.user.userId);

                //if the users list is empty
                if ((users != null) && (!users.Any()))
                {

                    //Add a employee method
                    DALClass.RegisterEmployee(vmodelViewRegister.user.userId, vmodelViewRegister.user.password, vmodelViewRegister.user.userEmail, vmodelViewRegister.employee.EmployeeName, vmodelViewRegister.employee.EmployeeSurname);
                    return View();
                }
                else
                {
                    //Add a Model error that the userID typed already exists
                    ModelState.AddModelError("", "UserID already exists");
                    return View();
                }
                //    //DALClass.RegisterEmployee(vmodelViewRegister.user.userId, vmodelViewRegister.user.password, vmodelViewRegister.user.userEmail, vmodelViewRegister.employee.EmployeeName, vmodelViewRegister.employee.EmployeeSurname);
                //bool1 = true;
                //return View("Login");
            }
            else
            {
                //Add a Model error that entry for registration is invalid
                ModelState.AddModelError("", "Invalid entry for Registration");
                return View();
            }


            // ModelState.AddModelError("", "Invalid entry for Registration");
            // return View();

        }


    }
}