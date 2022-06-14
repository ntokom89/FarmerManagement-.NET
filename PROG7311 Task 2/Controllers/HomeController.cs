using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using PROG7311_Task_2.Models;

namespace PROG7311_Task_2.Controllers
{
    public class HomeController : Controller
    {
        public static bool bool1 = false;

        public static User user1 = new User();

        public static Farmer farmer = new Farmer();

        public static Employee employee1 = new Employee();


        public ActionResult Index()
        {
            return View();
        }
        //ActionReulst to get the input from the login
        [HttpGet]
        public ActionResult Login()
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
            if (bool1 == true)
            {
                //this.AddNotification("Registration Successful", NotificationType.SUCCESS);
            }
            return View();
        }
        //A method to take the input of the UserObj to validate login.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind] User userObj)
        {
            if (ModelState.IsValid)
            {
                bool boolLogin = DALClass.checkPassword(userObj.userId, userObj.password);
                if (boolLogin == true)
                {
                    user1 = DALClass.selectUser(userObj);

                    if(user1.userType == "Farmer")
                    {
                        
                        farmer=DALClass.selectFarmer(user1);
                        return View("~/Views/Farmer/FarmerMainPage.cshtml");
                    }
                    else if(user1.userType == "Employee")
                    {
                        employee1= DALClass.selectEmployee(user1);
                        ThreadStart Startthread = new ThreadStart(callThread);
                        Thread thread = new Thread(callThread);
                        thread.Start();
                        return View("~/Views/Employee/EmployeeMainPage.cshtml");
                    }
                    else
                    {
                        return View();
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Invalid password or userID entered");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Please enter your userID and password");
            }
            return View();
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
                    bool1 = true;
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

        public void callThread()
        {
            try
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
            }
            catch(ThreadAbortException e)
            {

            }
        }
    }
}