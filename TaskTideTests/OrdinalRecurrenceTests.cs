using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideLibTests
{
    public class OrdinalRecurrenceTests
    {
        [Test]
        public void LastTuesdayOfMonth_WhenSameDayAsLastDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 31);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 10),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Last,
                    Weekdays = Weekdays.Tuesday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void LastStaturdayOfMonth_WhenDifferentDayAsLastDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 28);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 10),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Last,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(
                () =>
                {
                    foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                    {
                        if (day == targetDate)
                        {
                            Assert.That(sut.ShouldOccurOn(day));
                        }
                        else
                        {
                            Assert.That(sut.ShouldOccurOn(day), Is.False);
                        }
                    }
                }
            );
        }

        [Test]
        public void SecondLastTuesdayOfMonth_WhenSameDayAsLastDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 24);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 10),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.SecondLast,
                    Weekdays = Weekdays.Tuesday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void SecondLastStaturdayOfMonth_WhenDifferentDayAsLastDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 28);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 10),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Last,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FirstStaturdayOfMonth_WhenDifferentDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 7);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.First,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void SecondStaturdayOfMonth_WhenDifferentDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 14);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Second,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void ThirdStaturdayOfMonth_WhenDifferentDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 21);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Third,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FourthStaturdayOfMonth_WhenDifferentDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 28);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Fourth,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FirstSundayOfMonth_WhenSameDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 1);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.First,
                    Weekdays = Weekdays.Sunday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void SecondSundayOfMonth_WhenSameDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 8);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Second,
                    Weekdays = Weekdays.Sunday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void ThirdSundayOfMonth_WhenSameDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 15);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Third,
                    Weekdays = Weekdays.Sunday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FourthSundayOfMonth_WhenSameDayAsFirstDay_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 10, 22);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 10, 1),
                    EndType = EndType.Unending,
                    Months = Months.October,
                    Ordinal = WeekdayOrdinal.Fourth,
                    Weekdays = Weekdays.Sunday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new YearMonth(2023, 10).ToDateInterval())
                {
                    if (day == targetDate)
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void LastTuesdayOfMonth_MultipleMonths_WhenSameDayAsLastDay_ShouldOccur()
        {
            var targetDates = new[] { new LocalDate(2023, 9, 26), new LocalDate(2023, 10, 31) };

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 1, 1),
                    EndType = EndType.Unending,
                    Months = Months.September | Months.October,
                    Ordinal = WeekdayOrdinal.Last,
                    Weekdays = Weekdays.Tuesday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new DateInterval(new LocalDate(2023, 9, 1), new LocalDate(2023, 10, 31)))
                {
                    if (targetDates.Contains(day))
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void LastStaturdayOfMonth_MultipleMonths_WhenDifferentDayAsLastDay_ShouldOccur()
        {
            var targetDates = new[] { new LocalDate(2023, 9, 30), new LocalDate(2023, 10, 28) };

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 1, 1),
                    EndType = EndType.Unending,
                    Months = Months.September | Months.October,
                    Ordinal = WeekdayOrdinal.Last,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new DateInterval(new LocalDate(2023, 9, 1), new LocalDate(2023, 10, 31)))
                {
                    if (targetDates.Contains(day))
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void SecondLastTuesdayOfMonth_MultipleMonths_WhenSameDayAsLastDay_ShouldOccur()
        {
            var targetDates = new[] { new LocalDate(2023, 9, 23), new LocalDate(2023, 10, 21) };

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 1, 1),
                    EndType = EndType.Unending,
                    Months = Months.September | Months.October,
                    Ordinal = WeekdayOrdinal.SecondLast,
                    Weekdays = Weekdays.Saturday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new DateInterval(new LocalDate(2023, 9, 1), new LocalDate(2023, 10, 31)))
                {
                    if (targetDates.Contains(day))
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FirstTuesdayOfMonth_MultipleMonths_WhenSameDayAsLastDay_ShouldOccur()
        {
            var targetDates = new[] { new LocalDate(2023, 9, 5), new LocalDate(2023, 10, 3) };

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 1, 1),
                    EndType = EndType.Unending,
                    Months = Months.September | Months.October,
                    Ordinal = WeekdayOrdinal.First,
                    Weekdays = Weekdays.Tuesday,
                };

            Assert.Multiple(() =>
            {
                foreach (var day in new DateInterval(new LocalDate(2023, 9, 1), new LocalDate(2023, 10, 31)))
                {
                    if (targetDates.Contains(day))
                    {
                        Assert.That(sut.ShouldOccurOn(day));
                    }
                    else
                    {
                        Assert.That(sut.ShouldOccurOn(day), Is.False);
                    }
                }
            });
        }

        [Test]
        public void FirstMondayOfSeptember_ShouldOccur()
        {
            var targetDate = new LocalDate(2023, 9, 4);

            var sut =
                new Recurrence()
                {
                    Type = RecurrenceType.YearlyOrdinal,
                    StartDate = new LocalDate(2023, 1, 1),
                    EndType = EndType.Unending,
                    Weekdays = Weekdays.Monday,
                    Months = Months.September,
                    Ordinal = WeekdayOrdinal.First,
                };

            Assert.Multiple(
                () =>
                {
                    foreach (var day in new YearMonth(2023, 9).ToDateInterval())
                    {
                        if (targetDate == day)
                        {
                            Assert.That(sut.ShouldOccurOn(day), Is.True);
                        }
                        else
                        {
                            Assert.That(sut.ShouldOccurOn(day), Is.False);
                        }
                    }
                }
            );
        }
    }
}
