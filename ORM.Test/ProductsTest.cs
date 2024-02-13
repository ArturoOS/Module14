using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ORM.Test
{
    [TestClass]
    public class ProductsTest
    {
        [TestMethod]
        public void CreateProduct()
        {
            DBProduct db = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(1, 101).ToString();
            db.CreateProduct("100", productName, "100","Product1Description");

            var products = db.GetProducts();
            Assert.IsTrue(products.Where(x=>x.ProductName== productName).Count()==1);
        }
        
        [TestMethod]
        public void UpdateProduct()
        {
            DBProduct db = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(1, 101).ToString();
            string productName2 = "Product" + rand.Next(101, 201).ToString();
            db.CreateProduct("100", productName, "100", "Product2Description");
            int id = db.GetProducts().Where(x => x.ProductName == productName).First().ProductId;
            
            db.UpdateProduct(id,"50","DescriptionPorduct2", productName2, "50","50");

            var products = db.GetProducts();
            Assert.IsTrue(products.Where(x => x.ProductName == productName2).Count() == 1);
        }

        [TestMethod]
        public void DeleteProduct()
        {
            DBProduct db = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(1, 101).ToString();
            db.CreateProduct("100", productName, "100", "Product3Description");
            int id = db.GetProducts().Where(x => x.ProductName == productName).First().ProductId;
            
            db.DeleteProduct(id);

            var products = db.GetProducts();
            Assert.IsTrue(products.Where(x => x.ProductName == productName).Count() == 0);
        }
        [TestMethod]
        public void DeleteBulkProducts()
        {
            DBProduct db = new DBProduct();
            db.DeleteProducts();

            var products = db.GetProducts();
            Assert.IsTrue(products.Count() == 0);
        }
    }
}
