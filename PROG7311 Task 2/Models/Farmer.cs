using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models
{
    public class Farmer
    {
        private int farmerID;

        private String userID;

        private String farmerName;

        private String farmerSurname;


        public static List<Farmer> farmerList = new List<Farmer>();

        public Farmer(int farmerID, string userID, string farmerName, string farmerSurname)
        {
            this.farmerID = farmerID;
            this.userID = userID;
            this.farmerName = farmerName;
            this.farmerSurname = farmerSurname;
        }

        public Farmer()
        {
        }

        public int FarmerID { get; set; }

        public String UserID { get; set; } 

        public String FarmerName { get; set; }

        public String FarmerSurname { get; set; }


    }
}