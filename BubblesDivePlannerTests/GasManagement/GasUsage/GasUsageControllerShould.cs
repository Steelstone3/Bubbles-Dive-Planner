using Moq;
using Xunit;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement.GasUsage;

namespace BubblesDivePlannerTests.GasManagement.GasUsage
{
    public class GasUsageControllerShould
    {
        private IGasUsageController _gasUsageController;
        private IGasUsageModel _gasUsageModel;
        private Mock<IDiveStepModel> _diveStepModelStub;

        public GasUsageControllerShould()
        {
            _diveStepModelStub = new Mock<IDiveStepModel>();
            _diveStepModelStub = SetupDiveStepModelStub();
            _gasUsageModel = new GasUsageViewModel()
            {
                InitialPressurisedCylinderVolume = 2400,
                GasUsed = 0,
                GasRemaining = 2400,
                SurfaceAirConsumptionRate = 12,
            };

            _gasUsageController = new GasUsageController();
        }

        [Theory]
        [InlineData(12, 200, 2400)]
        [InlineData(10, 200, 2000)]
        [InlineData(12, 100, 1200)]
        public void CalculateInitialPressurisedCylinderVolume(int cylinderVolume, int clyinderPressure, int expectedInitialPressurisedCylinderVolume)
        {
            //Act
            int actualInitialPressurisedCylinderVolume = _gasUsageController.CalculateInitialPressurisedCylinderVolume(cylinderVolume, clyinderPressure);

            //Assert
            Assert.Equal(expectedInitialPressurisedCylinderVolume, actualInitialPressurisedCylinderVolume);
        }

        [Fact]
        public void UpdateGasUsage()
        {
            //Act
            var actualGasUsageModel = _gasUsageController.UpdateGasUsage(_diveStepModelStub.Object, _gasUsageModel);

            //Assert
            Assert.Equal(2400, actualGasUsageModel.InitialPressurisedCylinderVolume);
            Assert.Equal(1680, actualGasUsageModel.GasRemaining);
            Assert.Equal(720, actualGasUsageModel.GasUsed);
            Assert.Equal(12, actualGasUsageModel.SurfaceAirConsumptionRate);
        }

        private Mock<IDiveStepModel> SetupDiveStepModelStub()
        {
            _diveStepModelStub.Setup(x => x.Depth).Returns(50);
            _diveStepModelStub.Setup(x => x.Time).Returns(10);

            return _diveStepModelStub;
        }
    }
}