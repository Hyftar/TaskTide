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

            var firstDayOfWeekBeforeMoonEventAfterTarget =
                new DateInterval(targetDateInYear, targetDateInYear.PlusDays(30))
                    .First(x => this.MoonPhaseProvider.GetMoonPhase(x.At(new LocalTime(12, 0))).MoonPhase == recurrence.TargetMoonPhase)
                    .Next(target.DayOfWeek);

            return firstDayOfWeekBeforeMoonEventAfterTarget == target;
        }

        private bool HandleBeforeMoonEvent(LunarCalendarRecurrence recurrence, LocalDate targetDateInYear, LocalDate target)
        {
            if (targetDateInYear > target)
            {
                return false;
            }

            var firstDayOfWeekBeforeMoonEventAfterTarget =
                new DateInterval(targetDateInYear, targetDateInYear.PlusDays(30))
                    .First(x => this.MoonPhaseProvider.GetMoonPhase(x.AtMidnight()).MoonPhase == recurrence.TargetMoonPhase)
                    .Previous(target.DayOfWeek);

            return firstDayOfWeekBeforeMoonEventAfterTarget == target;
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
