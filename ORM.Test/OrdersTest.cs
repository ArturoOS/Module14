using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ORM.Test
{
    [TestClass]
    public class OrdersTest
    {
        [TestMethod]
        public void CreateOrder()
        {
            DBProduct dbProduct = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(201, 301).ToString();
            dbProduct.CreateProduct("100", productName, "100", "ProductADescription");
            int id = dbProduct.GetProducts().Where(x => x.ProductName == productName).First().ProductId;

            DBOrder db = new DBOrder();
            db.CreateOrder("Not Started",id);
            var orders = db.GetOrders();

            Assert.IsTrue(orders.Where(x=>x.ProductId==id).Count()==1);
        }
        [TestMethod]
        public void UpdateOrder()
        {
            DBProduct dbProduct = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(201, 301).ToString();
            dbProduct.CreateProduct("100", productName, "100", "ProductADescription");
            int id = dbProduct.GetProducts().Where(x => x.ProductName == productName).First().ProductId;

            DBOrder db = new DBOrder();
            db.CreateOrder("Not Started", id);
            int idOrder = db.GetOrders().Where(x => x.ProductId == id).First().OrderId;

            db.UpdateOrder(idOrder,"Loading");
            var orders = db.GetOrders();

            Assert.IsTrue(orders.Where(x => x.ProductId == id && x.OrderStatus== "Loading").Count() == 1);
        }
        [TestMethod]
        public void DeleteOrder()
        {
            DBProduct dbProduct = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(201, 301).ToString();
            dbProduct.CreateProduct("100", productName, "100", "ProductADescription");
            int id = dbProduct.GetProducts().Where(x => x.ProductName == productName).First().ProductId;

            DBOrder db = new DBOrder();
            db.CreateOrder("Not Started", id);
            var orders = db.GetOrders();
            int idOrder = orders.Where(x=>x.ProductId==id).FirstOrDefault().OrderId;
            db.DeleteOrder(idOrder);

            orders = db.GetOrders();
            Assert.IsTrue(orders.Where(x => x.ProductId == id).Count() == 0);
        }
        [TestMethod]
        public void GetOrdersBy()
        {
            DBProduct dbProduct = new DBProduct();
            Random rand = new Random();
            string productName = "Product" + rand.Next(201, 301).ToString();
            dbProduct.CreateProduct("100", productName, "100", "ProductADescription");
            int id = dbProduct.GetProducts().Where(x => x.ProductName == productName).First().ProductId;

            DBOrder db = new DBOrder();
            db.CreateOrder("InProgress", id);
            var orders = db.GetOrdersBy("InProgress",DateTime.Now);
            Assert.IsTrue(orders.Count()==1);
        }
        [TestMethod]
        public void DeleteBulkOrderss()
        {
            DBOrder db = new DBOrder();
            db.DeleteOrders();

            var products = db.GetOrders();
            Assert.IsTrue(products.Count() == 0);
        }
    }
}
