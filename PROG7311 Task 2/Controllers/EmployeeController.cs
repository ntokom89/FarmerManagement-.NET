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
        public ActionResult Index()
        {
            return View();
        }
    }
}