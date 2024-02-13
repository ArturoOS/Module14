using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DBOrder
    {
        public List<Order> GetOrders() 
        {
            using (var db = new ORMEntities()) 
            {
                return db.Orders.ToList();
            }
        }
        public void CreateOrder(string orderStatus, int productId)
        {
            using (var db = new ORMEntities())
            {
                Order order = new Order();
                order.OrderStatus = orderStatus;
                order.CreatedDate = DateTime.Now;
                order.UpdatedDate = DateTime.Now;
                order.ProductId = productId;
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }
        public void DeleteOrder(int orderId)
        {
            using (var db = new ORMEntities())
            {
                Order order = new Order { OrderId = orderId };
                db.Entry(order).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public void UpdateOrder(int orderId, string orderStatus)
        {
            using (var db = new ORMEntities())
            {
                var order = db.Orders.Where(x => x.OrderId == orderId).First();
                order.OrderStatus = orderStatus;
                order.UpdatedDate = DateTime.Now;
                db.SaveChanges();
            }
        }
        public List<Order> GetOrdersBy(string orderStatus, DateTime orderDate) 
        {
            List<Order> orders = new List<Order>(); 
            using (var db = new ORMEntities())
            {
                var orderList = db.GetOrders(orderStatus, orderDate);
                foreach (var order in orderList) 
                {
                    Order ord = new Order 
                    {
                        OrderId = order.OrderId, 
                        OrderStatus = order.OrderStatus,
                        CreatedDate = order.CreatedDate,
                        UpdatedDate = order.UpdatedDate,
                    };
                    orders.Add(ord);
                }    
            }
            return orders;
        }
        public void DeleteOrders()
        {
            using (var db = new ORMEntities())
            {
                var rows = db.Orders;
                foreach (var row in rows)
                {
                    db.Orders.Remove(row);
                }
                db.SaveChanges();
            }
        }
    }
}
