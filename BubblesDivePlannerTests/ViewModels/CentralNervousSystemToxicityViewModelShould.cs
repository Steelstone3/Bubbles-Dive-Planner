using BubblesDivePlanner.CentralNervousSystemToxicity;
using Xunit;

namespace BubblesDivePlannerTests.CentralNervousSystemToxicity
{
    public class CentralNervousSystemToxicityViewModelShould
    {
        private const int ROWS = 11;
        private readonly double[] _oxygenPartialPressureConstant = new double[ROWS] { 1.6, 1.5, 1.4, 1.3, 1.2, 1.1, 1.0, 0.9, 0.8, 0.7, 0.6 };
        private readonly ushort[] _maximumSingleDiveDuration = new ushort[ROWS] { 45, 120, 150, 180, 210, 240, 300, 360, 450, 570, 720 };
        private readonly ushort[] _totalDailyDuration = new ushort[ROWS] { 150, 180, 180, 210, 240, 270, 300, 360, 450, 570, 720 };
        private readonly CentralNervousSystemToxicityViewModel _centralNervousSystemToxicityViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            Assert.Equal(_oxygenPartialPressureConstant, _centralNervousSystemToxicityViewModel.OxygenPartialPressureConstant);
            Assert.Equal(_maximumSingleDiveDuration, _centralNervousSystemToxicityViewModel.MaximumSingleDiveDuration);
            Assert.Equal(_totalDailyDuration, _centralNervousSystemToxicityViewModel.TotalDailyDuration);
        }
    }
}