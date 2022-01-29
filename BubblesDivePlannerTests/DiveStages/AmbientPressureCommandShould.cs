using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.DiveStep;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class AmbientPressureShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();

        [Theory]
        [InlineData(0, 100, 0, 0, 0, 0, 1)]
        [InlineData(21, 79, 0, 0, 0.21, 0, 0.79)]
        [InlineData(32, 18, 50, 0, 0.32, 0.5, 0.18)]
        [InlineData(32, 18, 50, 10, 0.64, 1, 0.36)]
        [InlineData(32, 18, 50, 30, 1.28, 2, 0.72)]
        public void RunAmbientPressurePreStage(double oxygenPercentage, double nitrogenPercentage, double heliumPercentage, int depth,
            double resultOxygen, double resultHelium, double resultNitrogen)
        {
            //Arrange
            var gasMixtureModelStub = SetupGasMixtureStub(oxygenPercentage, nitrogenPercentage, heliumPercentage);
            var diveStepModelStub = SetupDiveStepStub(depth);

            var diveStage = new AmbientPressureCommand(_diveModel.DiveProfile, gasMixtureModelStub.Object, diveStepModelStub.Object);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(resultOxygen, _diveModel.DiveProfile.PressureOxygen, 2);
            Assert.Equal(resultHelium, _diveModel.DiveProfile.PressureHelium, 2);
            Assert.Equal(resultNitrogen, _diveModel.DiveProfile.PressureNitrogen, 2);
        }

        private IMock<IGasMixtureModel> SetupGasMixtureStub(double oxygenPercentage, double nitrogenPercentage, double heliumPercentage) {
            var gasMixtureModelStub = new Mock<IGasMixtureModel>();

            gasMixtureModelStub.Setup(x => x.Oxygen).Returns(oxygenPercentage);
            gasMixtureModelStub.Setup(x => x.Nitrogen).Returns(nitrogenPercentage);
            gasMixtureModelStub.Setup(x => x.Helium).Returns(heliumPercentage);

            return gasMixtureModelStub;
        }

        private IMock<IDiveStepModel> SetupDiveStepStub(int depth) {
            var diveStepModelStub = new Mock<IDiveStepModel>();

            diveStepModelStub.Setup(x => x.Depth).Returns(depth);

            return diveStepModelStub;
        }
    }
}