namespace Eshop.Shared.Models.Messages
{
    public abstract class BaseMessage
    {
        public Guid Id { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public string? Source { get; set; }

        public BaseMessage()
        {
            Id = Guid.NewGuid();
            CreatedOnUtc = DateTime.UtcNow;
        }
    }
}
