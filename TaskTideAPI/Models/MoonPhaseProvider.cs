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
                    > 0.90  or <= 0.10 => MoonPhase.NewMoon,
                    > 0.10 and <= 0.15 => MoonPhase.WaxingCrescent,
                    > 0.15 and <= 0.35 => MoonPhase.FirstQuarter,
                    > 0.35 and <= 0.40 => MoonPhase.WaxingGibbous,
                    > 0.40 and <= 0.60 => MoonPhase.FullMoon,
                    > 0.60 and <= 0.65 => MoonPhase.WaningGibbous,
                    > 0.65 and <= 0.85 => MoonPhase.ThirdQuarter,
                    > 0.85 and <= 0.90 => MoonPhase.WaningCrescent,

                    _ => throw new NotImplementedException(),
                };

            return new(moonPhase, illumination.Phase, illumination.Angle, illumination.Fraction);
        }
    }
}
