using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideTests
{
    public class LunarCalendarRecurrenceCalculatorTests
    {
        [Test]
        public void LunarRecurrenceOn_FirstNewMoonOnSaturday_ShouldOccurOn()
        {
            var recurrence =
                new LunarCalendarRecurrence()
                {
                    Id = 1,
                    Ordinal = LunarCalendarOrdinal.FirstWeekdayOnMoonEvent,
                    TargetDate = new AnnualDate(6, 1),
                    TargetMoonPhase = MoonPhase.NewMoon,
                    TargetWeekdays = Weekdays.Sunday,
                };

            var sut = new LunarCalendarRecurrenceCalculator(new MoonPhaseProvider());

            foreach (var day in new YearMonth(2023, 6).ToDateInterval())
            {
                var result = sut.ShouldOccurOn(recurrence, day);

                if (day == new LocalDate(2023, 6, 18))
                {
                    Assert.That(result, Is.True);
                }
                else
                {
                    Assert.That(result, Is.False);
                }
            }
        }

        [Test]
        public void LunarRecurrenceOn_FirstMondayAfterFullMoon_ShouldOccurOn()
        {
            var recurrence =
                new LunarCalendarRecurrence()
                {
                    Id = 1,
                    Ordinal = LunarCalendarOrdinal.FirstWeekdayAfterMoonEvent,
                    TargetDate = new AnnualDate(3, 21),
                    TargetMoonPhase = MoonPhase.FullMoon,
                    TargetWeekdays = Weekdays.Monday,
                };

            var sut = new LunarCalendarRecurrenceCalculator(new MoonPhaseProvider());

            foreach (var day in new YearMonth(2023, 4).ToDateInterval())
            {
                var result = sut.ShouldOccurOn(recurrence, day);

                if (day == new LocalDate(2023, 4, 10))
                {
                    Assert.That(result, Is.True);
                }
                else
                {
                    Assert.That(result, Is.False);
                }
            }
        }

        [Test]
        public void LunarRecurrenceOn_FirstMondayBeforeFullMoon_ShouldOccurOn()
        {
            var recurrence =
                new LunarCalendarRecurrence()
                {
                    Id = 1,
                    Ordinal = LunarCalendarOrdinal.FirstWeekdayBeforeMoonEvent,
                    TargetDate = new AnnualDate(4, 4),
                    TargetMoonPhase = MoonPhase.FullMoon,
                    TargetWeekdays = Weekdays.Monday,
                };

            var sut = new LunarCalendarRecurrenceCalculator(new MoonPhaseProvider());

            foreach (var day in new YearMonth(2023, 4).ToDateInterval())
            {
                var result = sut.ShouldOccurOn(recurrence, day);

                if (day == new LocalDate(2023, 4, 3))
                {
                    Assert.That(result, Is.True);
                }
                else
                {
                    Assert.That(result, Is.False);
                }
            }
        }
    }
}
