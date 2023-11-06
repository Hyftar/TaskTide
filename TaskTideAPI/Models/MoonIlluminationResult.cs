namespace TaskTideAPI.Models
{
    public record MoonIlluminationResult(
        MoonPhase MoonPhase,
        double IlluminationValue,
        double Angle,
        double Fraction
    );
}
