using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Item(Guid id, string name, int quantity) : base(id)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
