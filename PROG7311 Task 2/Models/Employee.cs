using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models
{
    public class Employee
    {

        private int employeeID;

        private String userID;

        private String employeeName;

        private String employeeSurname;

        public Employee(int employeeID, string userID, string employeeName, string employeeSurname)
        {
            this.employeeID = employeeID;
            this.userID = userID;
            this.employeeName = employeeName;
            this.employeeSurname = employeeSurname;
        }

        public Employee()
        {

        }

        public int EmployeeID { get; set; }

        public String UserID { get; set; }

        public String EmployeeName { get; set; }

        public String EmployeeSurname { get; set; }
    }
}