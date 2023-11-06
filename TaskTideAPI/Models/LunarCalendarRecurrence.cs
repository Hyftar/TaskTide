using NodaTime;

namespace TaskTideAPI.Models
{
    public class LunarCalendarRecurrence : ITransactionItem
    {
        public int Id { get; set; }

        public TaskEvent Parent { get; set; }

        public LunarCalendarOrdinal Ordinal { get; set; }

        public AnnualDate TargetDate { get; set; }

        public MoonPhase TargetMoonPhase { get; set; }

        public Weekdays TargetWeekdays { get; set; }
    }
}
