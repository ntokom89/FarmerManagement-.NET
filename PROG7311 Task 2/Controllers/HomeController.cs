﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PROG7311_Task_2.Models;

namespace PROG7311_Task_2.Controllers
{
    public class HomeController : Controller
    {
        public static bool bool1 = false;

        public static User user1 = new User();
        public ActionResult Index()
        {
            return View();
        }
        //ActionReulst to get the input from the login
        [HttpGet]
        public ActionResult Login()
        {
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
                        DALClass.selectFarmer(user1);
                        return View("~/Views/Farmer/FarmerMainPage.cshtml");
                    }
                    else if(user1.userType == "Employee")
                    {
                        DALClass.selectEmployee(user1);
                        return View("~/Views/Farmer/EmployeeMainPage.cshtml");
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
        public ActionResult Register(User userObj, Employee employeeObj)
        {
            if (ModelState.IsValid)
            {

                DALClass.RegisterEmployee(userObj.userId, userObj.password,userObj.userEmail,employeeObj.EmployeeID, employeeObj.EmployeeName,employeeObj.EmployeeSurname);
                bool1 = true;
                return View("Login");
            }
            else
            {
                ModelState.AddModelError("", "Invalid entry for Registration");
                return View();
            }
        }
    }
}