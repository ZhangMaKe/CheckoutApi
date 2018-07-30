namespace checkout.Responses
{
    public class EntityResponse<T> : BaseResponse
    {
        public T Entity { get; set; }

        public EntityResponse(T entity, string message) : base(message)
        {
            Entity = entity;
        }
    }
}
