using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models
{
    public class Product
    {
        private int productId;

        private String productName;

        private String productDescription;

        private String productType;

        private Double productPrice;

        public static List<Product> products = new List<Product>();
        public Product()
        {
        }

        public Product(int productId, string productName, string productDescription, string productType, double productPrice)
        {
            this.productId = productId;
            this.productName = productName;
            this.productDescription = productDescription;
            this.productType = productType;
            this.productPrice = productPrice;
        }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductType { get; set; }

        public double ProductPrice { get; set; }





    }
}