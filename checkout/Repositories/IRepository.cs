using System;
using checkout.Models;

namespace checkout.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Get(Guid id);
        bool Add(T item);
        bool Update(T item);
        bool Remove(T item);
    }
}
