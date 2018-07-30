using System;

namespace checkoutClient.Models
{
    public class BaseEntity
    {
        public Guid Id { get; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        protected BaseEntity(Guid id)
        {
            Id = id;
        }
    }
}
