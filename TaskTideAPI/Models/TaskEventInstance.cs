using NodaTime;

namespace TaskTideAPI.Models
{
    public class TaskEventInstance : ITransactionItem
    {
        public int Id { get; set; }

        public TaskEvent Parent { get; set; }

        public LocalDate StartDate { get; set; }

        public LocalTime? StartTime { get; set; }

        public Duration? Duration { get; set; }

        public ZonedDateTime CreatedAt { get; set; }

        public bool AllDay { get; set; }

        public bool IsCompleted { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
