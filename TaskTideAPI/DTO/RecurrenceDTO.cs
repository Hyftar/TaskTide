using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideAPI.DTO
{
    public class RecurrenceDTO
    {
        public RecurrenceType Type { get; set; }

        /// <summary>
        /// Date reference of the first event
        /// </summary>
        public LocalDate StartDate { get; set; }

        /// <summary>
        /// When to stop the recurrence
        /// </summary>
        public EndType EndType { get; set; }

        /// <summary>
        /// For EndType = count, after how many events to stop the recurrence
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// For EndType = Duration, after how long to stop the recurrence
        /// </summary>
        public Duration? Duration { get; set; }

        /// <summary>
        /// Interval of time between two events
        /// </summary>
        public int? Interval { get; set; }

        /// <summary>
        /// For monthly or yearly ordinal events, representing the ordinal of the days it should occur on.
        /// For example, a recurrence with weekdays Friday and Saturday with SecondLast occurence
        /// will occur on the second last Friday and Saturday of a month.
        /// </summary>
        public WeekdayOrdinal Ordinal { get; set; }

        /// <summary>
        /// For weekly or monthly and yearly ordinal events, representing the days of the week it should occur on.
        /// </summary>
        public Weekdays Weekdays { get; set; }

        /// <summary>
        /// For yearly ordinal events, representing the months the ordinal event should occur on.
        /// </summary>
        public Months Months { get; set; }
    }
}
