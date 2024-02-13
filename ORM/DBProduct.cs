using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ORM
{
    public class DBProduct
    {
        public List<Product> GetProducts() 
        {
            using (var db = new ORMEntities()) 
            {
                return db.Products.ToList();
            }
        }
        public void CreateProduct(string length, string name, string width, string description)
        {
            using (var db = new ORMEntities())
            {
                Product product = new Product();
                product.ProductLenght = length;
                product.ProductName = name;
                product.ProductWidth = width;
                product.ProductDescription = description;
                db.Products.Add(product);
                db.SaveChanges();
            }
        }
        public void DeleteProduct(int productId)
        {
            using (var db = new ORMEntities())
            {
                Product product = new Product { ProductId = productId };
                db.Entry(product).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public void UpdateProduct(int productId, string weight, string description, string name, string lenght, string width)
        {
            using (var db = new ORMEntities())
            {
                var product = db.Products.Where(x => x.ProductId == productId).First();
                product.ProductWeight = weight;
                product.ProductDescription = description;
                product.ProductName = name;
                product.ProductLenght = lenght;
                product.ProductWidth = width;
                db.SaveChanges();
            }
        }
        public void DeleteProducts()
        {
            using (var db = new ORMEntities())
            {
                var rows = db.Products;
                foreach (var row in rows)
                {
                    db.Products.Remove(row);
                }
                db.SaveChanges();
            }
        }
    }
}
