using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        public List<Order> Orders { get; set; }

        public OrderRepository()
        {
            Orders = new List<Order>();
        }

        public Order Get(Guid id)
        {
            return Orders.FirstOrDefault(x => x.Id == id);
        }
        public bool Add(Order order)
        {
            try
            {
                Orders.Add(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Order order)
        {
            try
            {
                var index = Orders.FindIndex(o => o.Id == order.Id);
                Orders[index] = order;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Remove(Order order)
        {
            try
            {
                return Orders.Remove(order);
            }
            catch
            {
                return false;
            }
        }


        public bool AddItem(Guid orderId, Item item)
        {
            try
            {
                var order = Orders.First(o => o.Id == orderId);
                order.Items.Add(item);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveItem(Guid orderId, Item item)
        {
            try
            {
                var order = Orders.First(o => o.Id == orderId);
                return order.Items.Remove(item);
            }
            catch
            {
                return false;
            }
        }

    }
}
