using NodaTime;

namespace TaskTideAPI.DateExtensions
{
    public static class DateTimeExtensions
    {
        public static LocalDate GetFirstDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth;
            }

            return startOfMonth.Next(dayOfWeek);
        }

        public static LocalDate GetSecondDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth.PlusWeeks(1);
            }

            return startOfMonth.Next(dayOfWeek).PlusWeeks(1);
        }

        public static LocalDate GetThirdDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth.PlusWeeks(2);
            }

            return startOfMonth.Next(dayOfWeek).PlusWeeks(2);
        }

        public static LocalDate GetFourthDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.StartOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth.PlusWeeks(3);
            }

            return startOfMonth.Next(dayOfWeek).PlusWeeks(3);
        }

        public static LocalDate GetLastDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.EndOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth;
            }

            return startOfMonth.Previous(dayOfWeek);
        }

        public static LocalDate GetSecondLastDayOfMonth(this LocalDate date, IsoDayOfWeek dayOfWeek)
        {
            var startOfMonth = DateAdjusters.EndOfMonth(date);

            if (startOfMonth.DayOfWeek == dayOfWeek)
            {
                return startOfMonth.Minus(Period.FromWeeks(1));
            }

            return startOfMonth.Previous(dayOfWeek).Minus(Period.FromWeeks(1));
        }
    }
}
