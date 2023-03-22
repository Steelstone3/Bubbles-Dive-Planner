using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.ViewModels.DiveInformation
{
    public class CentralNervousSystemToxicityViewModel : ICentralNervousSystemToxicityModel
    {
        private const int ROWS = 11;
        public double[] OxygenPartialPressureConstant => new double[ROWS] { 1.6, 1.5, 1.4, 1.3, 1.2, 1.1, 1.0, 0.9, 0.8, 0.7, 0.6 };
        public ushort[] MaximumSingleDiveDuration => new ushort[ROWS] { 45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720 };
        public ushort[] TotalDailyDuration => new ushort[ROWS] { 150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720 };
    }
}