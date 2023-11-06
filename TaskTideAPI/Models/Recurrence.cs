using NodaTime;
using TaskTideAPI.DateExtensions;

namespace TaskTideAPI.Models
{
    public class Recurrence : ITransactionItem
    {
        public int Id { get; set; }

        public TaskEvent TaskEvent { get; set; }

        /// <summary>
        /// Type of event recurrence, this affects how other properties are read.
        /// </summary>
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

        public bool Deleted { get; set; } = false;

        public bool ShouldOccurOn(LocalDate target)
        {
            if (target < this.StartDate)
            {
                return false;
            }

            var periodElapsed = Period.Between(this.StartDate, target, this.GetIntervalUnit());
            //var periodElapsed = target - this.StartDate;
            var weekdayFlag = (Weekdays)(1 << ((int) target.DayOfWeek - 1));
            var endOfMonth = DateAdjusters.EndOfMonth(target);

            return this.Type switch
            {
                RecurrenceType.Daily => periodElapsed.Days % this.Interval == 0,

                RecurrenceType.Weekly => periodElapsed.Weeks % this.Interval == 0 && (this.Weekdays & weekdayFlag) == 0,

                RecurrenceType.Monthly =>
                    periodElapsed.Months % this.Interval == 0
                    && (
                        (this.StartDate.Day == target.Day)
                        || (this.StartDate.Day > endOfMonth.Day && target.Day == endOfMonth.Day)
                    ),

                RecurrenceType.YearlyInterval =>
                    periodElapsed.Years % this.Interval == 0
                    && (this.StartDate.Month == target.Month)
                    && (
                        (this.StartDate.Day == target.Day)
                        || (this.StartDate.Day > endOfMonth.Day && target.Day == endOfMonth.Day)
                    ),

                RecurrenceType.YearlyOrdinal => this.HandleYearlyOrdinalOccursOn(target),

                _ => throw new NotSupportedException($"Recurrence type {this.Type} is not supported"),
            };
        }

        private PeriodUnits GetIntervalUnit()
        {
            return this.Type switch
            {
                RecurrenceType.Daily => PeriodUnits.Days,
                RecurrenceType.Weekly => PeriodUnits.Weeks,
                RecurrenceType.Monthly => PeriodUnits.Months,
                RecurrenceType.YearlyInterval => PeriodUnits.Years,
                RecurrenceType.YearlyOrdinal => PeriodUnits.YearMonthDay,
                _ => throw new NotImplementedException(),
            };
        }

        private bool HandleYearlyOrdinalOccursOn(LocalDate target)
        {
            var monthFlag = (Months) (1 << (target.Month - 1));

            if ((this.Months & monthFlag) == 0)
            {
                return false;
            }

            var weekdayFlag = (Weekdays) (1 << ((int) target.DayOfWeek - 1));

            if ((this.Weekdays & weekdayFlag) == 0)
            {
                return false;
            }

            return this.Ordinal switch
            {
                WeekdayOrdinal.First => target.GetFirstDayOfMonth(target.DayOfWeek) == target,
                WeekdayOrdinal.Second => target.GetSecondDayOfMonth(target.DayOfWeek) == target,
                WeekdayOrdinal.Third => target.GetThirdDayOfMonth(target.DayOfWeek) == target,
                WeekdayOrdinal.Fourth => target.GetFourthDayOfMonth(target.DayOfWeek) == target,
                WeekdayOrdinal.SecondLast => target.GetSecondLastDayOfMonth(target.DayOfWeek) == target,
                WeekdayOrdinal.Last => target.GetLastDayOfMonth(target.DayOfWeek) == target,
                _ => throw new NotSupportedException($"Ordinal not supported: {this.Ordinal}"),
            };
        }
    }
}
