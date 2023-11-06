using NodaTime;

namespace TaskTideAPI.Models
{
    public class TransactionnalEvents
    {
        public int Id { get; set; }

        public TransactionType Type { get; set; }

        public User? User { get; set; }

        public string Model { get; set; }

        public int ModelId { get; set; }

        public ZonedDateTime Timestamp { get; set; }
    }
}
