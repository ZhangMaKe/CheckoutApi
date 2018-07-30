using System;

namespace checkout.Models
{
    public abstract class BaseEntity
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
