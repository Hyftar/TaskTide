using NodaTime;

namespace TaskTideAPI.Models
{
    public interface ILunarCalendarRecurrenceCalculator
    {
        bool ShouldOccurOn(LunarCalendarRecurrence recurrence, LocalDate target);
    }

    public class LunarCalendarRecurrenceCalculator : ILunarCalendarRecurrenceCalculator
    {
        private readonly IMoonPhaseProvider MoonPhaseProvider;

        public LunarCalendarRecurrenceCalculator(
            IMoonPhaseProvider moonPhaseProvider
        )
        {
            this.MoonPhaseProvider = moonPhaseProvider;
        }

        public bool ShouldOccurOn(LunarCalendarRecurrence recurrence, LocalDate target)
        {
            var targetDateInYear = recurrence.TargetDate.InYear(target.Year);

            var weekdayFlag = (Weekdays) (1 << ((int) target.DayOfWeek - 1));

            if ((recurrence.TargetWeekdays & weekdayFlag) == 0)
            {
                return false;
            }

            return recurrence.Ordinal switch
            {
                LunarCalendarOrdinal.FirstWeekdayAfterMoonEvent => this.HandleAfterMoonEven(recurrence, targetDateInYear, target),
                LunarCalendarOrdinal.FirstWeekdayBeforeMoonEvent => this.HandleBeforeMoonEvent(recurrence, targetDateInYear, target),
                LunarCalendarOrdinal.FirstWeekdayOnMoonEvent => this.HandleOnMoonEvent(recurrence, targetDateInYear, target),
                _ => throw new NotImplementedException(),
            };
        }

        private bool HandleAfterMoonEven(LunarCalendarRecurrence recurrence, LocalDate targetDateInYear, LocalDate target)
        {
            if (targetDateInYear > target)
            {
                return false;
            }

            var daysBetween = new DateInterval(targetDateInYear, target);

            var lunarEventHappened = false;
            foreach (var day in daysBetween)
            {
                var weekdayFlag = (Weekdays) (1 << ((int) day.DayOfWeek - 1));
                var moonPhase = this.MoonPhaseProvider.GetMoonPhase(day.AtMidnight());
                var isOnMoonPhase = moonPhase.MoonPhase == recurrence.TargetMoonPhase;

                // Check if conditions are met before reaching target day
                if (day != target
                    && lunarEventHappened
                    && (weekdayFlag & recurrence.TargetWeekdays) != 0)
                {
                    return false;
                }

                if (isOnMoonPhase)
                {
                    lunarEventHappened = true;
                }

                if (lunarEventHappened && day == target)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HandleBeforeMoonEvent(LunarCalendarRecurrence recurrence, LocalDate targetDateInYear, LocalDate target)
        {
            if (targetDateInYear < target)
            {
                return false;
            }

            var daysBetween = new DateInterval(target, targetDateInYear).Reverse();

            var lunarEventHappened = false;
            foreach (var day in daysBetween)
            {
                var weekdayFlag = (Weekdays) (1 << ((int) day.DayOfWeek - 1));
                var moonPhase = this.MoonPhaseProvider.GetMoonPhase(day.AtMidnight());
                var isOnMoonPhase = moonPhase.MoonPhase == recurrence.TargetMoonPhase;

                // Check if conditions are met before reaching target day
                if (day != target
                    && lunarEventHappened
                    && (weekdayFlag & recurrence.TargetWeekdays) != 0)
                {
                    return false;
                }

                if (isOnMoonPhase)
                {
                    lunarEventHappened = true;
                }

                if (lunarEventHappened && day == target)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HandleOnMoonEvent(
            LunarCalendarRecurrence recurrence,
            LocalDate targetDateInYear,
            LocalDate target)
        {
            if (targetDateInYear > target)
            {
                return false;
            }

            var daysBetween = new DateInterval(targetDateInYear, target);

            var firstWeekDayAfterMoonEvent =
                daysBetween
                    .Where(day => ((Weekdays) (1 << ((int) day.DayOfWeek - 1)) & recurrence.TargetWeekdays) != 0)
                    .Where(day => this.MoonPhaseProvider.GetMoonPhase(day.AtMidnight()).MoonPhase == recurrence.TargetMoonPhase)
                    .FirstOrDefault();

            return firstWeekDayAfterMoonEvent == target;
        }
    }

}
