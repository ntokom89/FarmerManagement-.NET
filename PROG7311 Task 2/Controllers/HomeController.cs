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
        //Public static variables
        public static bool bool1 = false;

        public static User user1 = new User();

        public static Farmer farmer = new Farmer();

        public static Employee employee1 = new Employee();

        //Get index page
        public ActionResult Index()
        {
            return View();
        }
        //ActionReulst to get the input from the login
        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }
        //A method to take the input of the UserObj to validate login (Zeeshan, 2015) (getridbug, 2022).
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind] User userObj)
        {
            //If modelState is valid
            if (ModelState.IsValid)
            {
                //Check password with userID and password
                bool boolLogin = DALClass.checkPassword(userObj.userId, userObj.password);
                if (boolLogin == true)
                {
                    //select the user
                    user1 = DALClass.selectUser(userObj);
                    //If the user type is farmer then select the farmer using the user object and go to the farmerMainPage
                    if(user1.userType == "Farmer")
                    {
                        
                        farmer=DALClass.selectFarmer(user1);
                        return View("~/Views/Farmer/FarmerMainPage.cshtml");
                    }
                    //If the user type is employee then select the employee using the user object and go to the employeeMainPage
                    else if (user1.userType == "Employee")
                    {
                        employee1= DALClass.selectEmployee(user1);
                        //call thread and start the thread
                        ThreadStart Startthread = new ThreadStart(callThread);
                        Thread thread = new Thread(callThread);
                        thread.Start();
                        return View("~/Views/Employee/EmployeeMainPage.cshtml");
                    }
                    else
                    {
                        //do not change page
                        return View();
                    }

                }
                else
                {
                    //Notify user that the passowrd or userID is invalid
                    ModelState.AddModelError("", "Invalid password or userID entered");
                    return View();
                }
            }
            else
            {
                //Notify user to enter password and useraID
                ModelState.AddModelError("", "Please enter your userID and password");
            }
            return View();
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