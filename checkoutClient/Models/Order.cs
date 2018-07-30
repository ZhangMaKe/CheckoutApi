using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace checkoutClient.Models
{
    public class Order : BaseEntity
    {
        public List<Item> Items { get; set; }

        public Order()
        {
            Items = new List<Item>();
        }

        [JsonConstructor]
        public Order(Guid id) : base(id)
        {
            
        }
    }
}
