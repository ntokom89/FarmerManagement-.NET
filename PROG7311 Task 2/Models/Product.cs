using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PROG7311_Task_2.Models
{
    public class Product
    {
        //product attributes
        private int productId;

        private String productName;

        private String productDescription;

        private String productType;

        private Decimal productPrice;

        private int productAmount;

        private DateTime dateAddedProduct;

        private String farmername;

        private byte[] productImage;

       
        //A list of products
        public static List<Product> products = new List<Product>();
        public Product()
        {

        }

        public Product(int productId, string productName, string productDescription, string productType, decimal productPrice)
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


        public decimal ProductPrice { get; set; }

        public int ProductAmount { get; set; }

        public DateTime DateAddedProduct { get; set; }

        public string FarmerName { get; set; }

        public byte[] ProductImage { get; set; }

        //A File that will be stored in the ProductImage 
        public HttpPostedFileBase ImageToUpload { get; set; }

    }
}