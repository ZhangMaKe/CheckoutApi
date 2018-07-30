using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Models
{
    public class Order : BaseEntity
    {
        public List<Item> Items { get; set; }

        public Order(Guid id, List<Item> items) : base(id)
        {
            Items = items;
        }
    }
}
