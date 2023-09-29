namespace Radiao.Domain.Entities
{
    abstract public class Entity
    {
        public Guid Id { get; protected set; }

        public void NewId()
        {
            Id = Guid.NewGuid();
        }
    }
}
