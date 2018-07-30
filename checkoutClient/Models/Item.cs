using System;
using Newtonsoft.Json;

namespace checkoutClient.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Item(string name, int quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        [JsonConstructor]
        public Item(string name, int quantity, Guid id) : base(id)
        {
            Name = name;
            Quantity = quantity;
        }
    }
}
