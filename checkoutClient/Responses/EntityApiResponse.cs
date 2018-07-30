using checkoutClient.Models;

namespace checkoutClient.Responses
{
    public class EntityApiResponse<T> : BaseApiResponse where T : BaseEntity
    {
        public T Entity { get; set; }
        public EntityApiResponse(T entity, string message) : base(message)
        {
            Entity = entity;
        }
    }
}
