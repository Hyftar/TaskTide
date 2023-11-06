using NodaTime;

namespace TaskTideAPI.Models
{
    public class TaskEvent : ITransactionItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Calendar Parent { get; set; }

        public ZonedDateTime CreatedAt { get; set; }

        public ICollection<Recurrence> Recurrences { get; set; }

        public ICollection<LunarCalendarRecurrence> LunarCalendarRecurrences { get; set; }

        public ICollection<TaskEventInstance> Instances { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
