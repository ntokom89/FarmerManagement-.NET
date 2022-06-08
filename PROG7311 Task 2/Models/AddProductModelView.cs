using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models
{
    public class AddProductModelView
    {
        public Farmer farmer {get; set; }
        public Product product { get; set; }

        public DateTime dateProductAdded { get; set; }

        public static List<AddProductModelView> productsFarmers = new List<AddProductModelView>();

        public AddProductModelView()
        {
        }
    }
}