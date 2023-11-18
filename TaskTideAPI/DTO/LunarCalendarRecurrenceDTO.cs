using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideAPI.DTO
{
    public class LunarCalendarRecurrenceDTO
    {
        public LunarCalendarOrdinal Ordinal { get; set; }

        public AnnualDate TargetDate { get; set; }

        public MoonPhase TargetMoonPhase { get; set; }

        public Weekdays TargetWeekdays { get; set; }
    }
}
