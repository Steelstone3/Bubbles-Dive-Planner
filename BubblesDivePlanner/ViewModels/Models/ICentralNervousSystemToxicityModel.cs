namespace BubblesDivePlanner.ViewModels.Models
{
    public interface ICentralNervousSystemToxicityModel
    {
        double[] OxygenPartialPressureConstant { get; }
        ushort[] MaximumSingleDiveDuration { get; }
        ushort[] TotalDailyDuration { get; }
    }
}