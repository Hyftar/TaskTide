using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideLibTests
{
    public class IntervalRecurrenceTests
    {
        [Test]
        public void DailyRecurrence_When1DayInterval_ShouldOccurNextDay()
        {
            var targetDate = new LocalDate(2023, 10, 19);
            var nextDay = new LocalDate(2023, 10, 20);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.Daily,
                    StartDate = targetDate,
                    EndType = EndType.Unending,
                    Interval = 1,
                };

            Assert.Multiple(
                () =>
                {
                    Assert.That(sut.ShouldOccurOn(nextDay));
                    Assert.That(sut.ShouldOccurOn(nextDay));

                    Assert.That(sut.ShouldOccurOn(targetDate.PlusDays(-1)), Is.False);
                }
            );
        }

        [Test]
        public void DailyRecurrence_When2DayInterval_ShouldOccurEvery2Days()
        {
            var targetDate = new LocalDate(2023, 10, 19);
            var nextDay = new LocalDate(2023, 10, 21);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.Daily,
                    StartDate = targetDate,
                    EndType = EndType.Unending,
                    Interval = 2,
                };

            Assert.Multiple(
                () =>
                {
                    Assert.That(sut.ShouldOccurOn(nextDay));

                    Assert.That(sut.ShouldOccurOn(targetDate.PlusDays(1)), Is.False);

                    Assert.That(sut.ShouldOccurOn(nextDay));
                }
            );
        }

        [Test]
        public void DailyRecurrence_When1DayInterval_ShouldOccurAtSameDateNextMonth()
        {
            var targetDate = new LocalDate(2023, 10, 19);
            var sameDateNextMonth = new LocalDate(2023, 11, 19);

            var sut = new Recurrence()
            {
                Type = RecurrenceType.Daily,
                StartDate = targetDate,
                EndType = EndType.Unending,
                Interval = 1,
            };

            Assert.Multiple(
                () =>
                {
                    Assert.That(sut.ShouldOccurOn(targetDate));

                    Assert.That(sut.ShouldOccurOn(sameDateNextMonth));
                }
            );
        }

        [Test]
        public void DailyRecurrence_When3DayInterval_ShouldNotOccur31DaysLater()
        {
            var targetDate = new LocalDate(2023, 10, 1);
            var sameDateNextMonth = new LocalDate(2023, 11, 1);

            var sut = new Recurrence()
            {
                Type = RecurrenceType.Daily,
                StartDate = targetDate,
                EndType = EndType.Unending,
                Interval = 3,
            };

            Assert.Multiple(
                () =>
                {
                    Assert.That(sut.ShouldOccurOn(targetDate));

                    Assert.That(sut.ShouldOccurOn(sameDateNextMonth), Is.False);
                }
            );
        }
    }
}
