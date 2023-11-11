using NodaTime;

namespace TaskTideAPI.DateExtensions
{
    public static class DateTimeExtensions
    {
        public static LocalDate GetFirstDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            return DateAdjusters.NextOrSame(dayOfWeek)(startOfMonth);
        }

        public static LocalDate GetSecondDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            return DateAdjusters.NextOrSame(dayOfWeek)(startOfMonth).PlusWeeks(1);
        }

        public static LocalDate GetThirdDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            return DateAdjusters.NextOrSame(dayOfWeek)(startOfMonth).PlusWeeks(2);
        }

        public static LocalDate GetFourthDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            return DateAdjusters.NextOrSame(dayOfWeek)(startOfMonth).PlusWeeks(3);
        }

        public static LocalDate GetLastDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var endOfMonth = DateAdjusters.EndOfMonth(date);

            return DateAdjusters.PreviousOrSame(dayOfWeek)(endOfMonth);
        }

        public static LocalDate GetSecondLastDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var endOfMonth = DateAdjusters.EndOfMonth(date);

            return DateAdjusters.PreviousOrSame(dayOfWeek)(endOfMonth).PlusWeeks(-1);
        }
    }
}
