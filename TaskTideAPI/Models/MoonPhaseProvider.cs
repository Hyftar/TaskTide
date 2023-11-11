using NodaTime;
using SunCalcNet;

namespace TaskTideAPI.Models
{
    public interface IMoonPhaseProvider
    {
        MoonIlluminationResult GetMoonPhase(LocalDateTime dateTime);
    }

    public class MoonPhaseProvider : IMoonPhaseProvider
    {
        public MoonIlluminationResult GetMoonPhase(LocalDateTime dateTime)
        {
            var illumination = MoonCalc.GetMoonIllumination(dateTime.ToDateTimeUnspecified());

            var moonPhase =
                illumination.Phase switch
                {
                    > 0.94  or <= 0.05 => MoonPhase.NewMoon,
                    > 0.05 and <= 0.15 => MoonPhase.WaxingCrescent,
                    > 0.15 and <= 0.275 => MoonPhase.FirstQuarter,
                    > 0.275 and <= 0.44 => MoonPhase.WaxingGibbous,
                    > 0.44 and <= 0.55 => MoonPhase.FullMoon,
                    > 0.55 and <= 0.705 => MoonPhase.WaningGibbous,
                    > 0.705 and <= 0.85 => MoonPhase.ThirdQuarter,
                    > 0.85 and <= 0.94 => MoonPhase.WaningCrescent,

                    _ => throw new NotImplementedException(),
                };

            return new(moonPhase, illumination.Phase, illumination.Angle, illumination.Fraction);
        }
    }
}
