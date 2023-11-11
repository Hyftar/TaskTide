using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideTests
{
    public class MoonPhasesTests
    {
        [TestCase(2023, 4, 5, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2023, 4, 6, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2023, 4, 7, ExpectedResult = MoonPhase.FullMoon)]
        
        [TestCase(2023, 4, 9, ExpectedResult = MoonPhase.WaningGibbous)]
        [TestCase(2023, 4, 10, ExpectedResult = MoonPhase.WaningGibbous)]

        [TestCase(2023, 4, 12, ExpectedResult = MoonPhase.ThirdQuarter)]
        [TestCase(2023, 4, 13, ExpectedResult = MoonPhase.ThirdQuarter)]
        [TestCase(2023, 4, 14, ExpectedResult = MoonPhase.ThirdQuarter)]

        [TestCase(2023, 4, 16, ExpectedResult = MoonPhase.WaningCrescent)]
        [TestCase(2023, 4, 17, ExpectedResult = MoonPhase.WaningCrescent)]

        [TestCase(2023, 4, 19, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2023, 4, 20, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2023, 4, 21, ExpectedResult = MoonPhase.NewMoon)]

        [TestCase(2023, 4, 23, ExpectedResult = MoonPhase.WaxingCrescent)]
        [TestCase(2023, 4, 24, ExpectedResult = MoonPhase.WaxingCrescent)]

        [TestCase(2023, 4, 26, ExpectedResult = MoonPhase.FirstQuarter)]
        [TestCase(2023, 4, 27, ExpectedResult = MoonPhase.FirstQuarter)]
        [TestCase(2023, 4, 28, ExpectedResult = MoonPhase.FirstQuarter)]

        [TestCase(2023, 5, 1, ExpectedResult = MoonPhase.WaxingGibbous)]
        [TestCase(2023, 5, 2, ExpectedResult = MoonPhase.WaxingGibbous)]

        [TestCase(2023, 5, 4, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2023, 5, 5, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2023, 5, 6, ExpectedResult = MoonPhase.FullMoon)]

        [TestCase(2024, 3, 9, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2024, 3, 10, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2024, 3, 11, ExpectedResult = MoonPhase.NewMoon)]

        [TestCase(2024, 3, 13, ExpectedResult = MoonPhase.WaxingCrescent)]
        [TestCase(2024, 3, 14, ExpectedResult = MoonPhase.WaxingCrescent)]

        [TestCase(2024, 3, 16, ExpectedResult = MoonPhase.FirstQuarter)]
        [TestCase(2024, 3, 17, ExpectedResult = MoonPhase.FirstQuarter)]

        [TestCase(2024, 3, 20, ExpectedResult = MoonPhase.WaxingGibbous)]
        [TestCase(2024, 3, 21, ExpectedResult = MoonPhase.WaxingGibbous)]

        [TestCase(2024, 3, 24, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2024, 3, 25, ExpectedResult = MoonPhase.FullMoon)]
        [TestCase(2024, 3, 26, ExpectedResult = MoonPhase.FullMoon)]

        [TestCase(2024, 3, 28, ExpectedResult = MoonPhase.WaningGibbous)]
        [TestCase(2024, 3, 29, ExpectedResult = MoonPhase.WaningGibbous)]

        [TestCase(2024, 4, 1, ExpectedResult = MoonPhase.ThirdQuarter)]
        [TestCase(2024, 4, 2, ExpectedResult = MoonPhase.ThirdQuarter)]
        [TestCase(2024, 4, 3, ExpectedResult = MoonPhase.ThirdQuarter)]

        [TestCase(2024, 4, 5, ExpectedResult = MoonPhase.WaningCrescent)]
        [TestCase(2024, 4, 6, ExpectedResult = MoonPhase.WaningCrescent)]

        [TestCase(2024, 4, 7, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2024, 4, 8, ExpectedResult = MoonPhase.NewMoon)]
        [TestCase(2024, 4, 9, ExpectedResult = MoonPhase.NewMoon)]

        public MoonPhase CheckIfCorrectMoonPhase(int year, int month, int day)
        {
            return
                new MoonPhaseProvider()
                    .GetMoonPhase(new LocalDate(year, month, day).AtMidnight())
                    .MoonPhase;
        }
    }
}
