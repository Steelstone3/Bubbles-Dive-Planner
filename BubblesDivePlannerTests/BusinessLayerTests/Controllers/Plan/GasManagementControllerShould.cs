using BubblesDivePlanner.Controllers.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Controllers.Plan
{
    public class GasManagementControllerShould
    {
        private GasManagementViewModel _gasManagementViewModel = new GasManagementViewModel();
        private GasManagementController _gasManagementController = new GasManagementController();

        [Theory]
        [InlineData(2400, 720, 1680)]
        [InlineData(2000, 1000, 1000)]
        public void CalculateTheGasRemainingPerStep(int gasRemaining, int gasUsed, int expectedResult)
        {
            var remainingGas = _gasManagementController.CalculateGasRemaining(gasRemaining, gasUsed);

            Assert.Equal(expectedResult, remainingGas);
        }

        [Theory]
        [InlineData(50, 10, 12, 720)]
        [InlineData(10, 200, 15, 6000)]
        public void CalculateGasUsedPerStep(int depth, int time, int sacRate, int expectedResult)
        {
            var gasUsed = _gasManagementController.CalculateGasUsed(depth, time, sacRate);

            Assert.Equal(expectedResult, gasUsed);
        }

        [Theory]
        [InlineData(12, 200, 2400)]
        [InlineData(24, 300, 7200)]
        public void CalculateInitialGasVolume(int cylinderVolume, int cylinderPressure, int expectedResult)
        {
            var intialGasVolume = _gasManagementController.CalculateInitialGasVolume(cylinderVolume, cylinderPressure);

            Assert.Equal(expectedResult, intialGasVolume);
        }

        [Theory]
        [InlineData(2400, 12, 200)]
        [InlineData(1000, 24, 41)]
        public void ConvertToBar(int cylinderTotalVolume, int cylinderSize, int expectedResult)
        {
            var volumeToBar = _gasManagementController.ConvertToBar(cylinderTotalVolume, cylinderSize);

            Assert.Equal(expectedResult, volumeToBar);
        }

        [Theory]
        [InlineData(200, 12, 2400)]
        [InlineData(300, 30, 9000)]
        public void CalculateTheInitialGasVolumeOnSet(int cylinderPressure, int cylinderVolume, int expectedResult)
        {
            _gasManagementViewModel.CylinderPressure = cylinderPressure;
            _gasManagementViewModel.CylinderVolume = cylinderVolume;

            Assert.Equal(expectedResult, _gasManagementViewModel.InitialCylinderTotalVolume);
        }

        [Theory]
        [InlineData(12, 200, 2400, 2400)]
        [InlineData(10, 100, 1000, 1000)]
        public void ResetTheRemainingCylinderVolumeOnSet(int cylinderVolume, int cylinderPressure, int initialCylinderTotalVolume, int expectedResult)
        {
            _gasManagementViewModel.CylinderVolume = cylinderVolume;
            _gasManagementViewModel.CylinderPressure = cylinderPressure;
            _gasManagementViewModel.InitialCylinderTotalVolume = initialCylinderTotalVolume;

            Assert.Equal(expectedResult, _gasManagementViewModel.GasRemaining);
        }

        [Theory]
        [InlineData(720, 12, 200, 2400, 1680)]
        [InlineData(720, 10, 100, 1000, 280)]
        public void CalculateTheGasRemainingOnSet(int gasUsed, int cylinderVolume, int cylinderPressure, int initialCylinderTotalVolume, int expectedResult)
        {
            _gasManagementViewModel.CylinderVolume = cylinderVolume;
            _gasManagementViewModel.CylinderPressure = cylinderPressure;
            _gasManagementViewModel.InitialCylinderTotalVolume = initialCylinderTotalVolume;
            _gasManagementViewModel.GasUsedForStep = gasUsed;

            Assert.Equal(expectedResult, _gasManagementViewModel.GasRemaining);
        }
    }
}