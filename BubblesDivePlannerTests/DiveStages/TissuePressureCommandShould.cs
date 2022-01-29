using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.DiveStep;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Commands.DiveStages
{
    public class TissuePressureShould
    {
        private IDiveModel _diveModel = new Zhl16BuhlmannModel();

        [Theory]
        [InlineData(
            new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            0,
            new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 },
            new double[16] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
            new double[16] { 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79, 0.79 })]
        public void RunTissuePressureStage(double[] tissuePressuresNitrogen, double[] tissuePressuresHelium,
            double[] tissuePressuresTotal, int bottomTime, double[] tissuePressureNitrogenResult,
            double[] tissuePressureHeliumResult, double[] tissuePressureTotalResult)
        {
            //Arrange
            _diveModel.DiveProfile = SetupDiveProfileModel(_diveModel.DiveProfile, tissuePressuresNitrogen, tissuePressuresHelium, tissuePressuresTotal);
            var diveStepModelStub = SetupDiveStepModelStub(bottomTime);

            var diveStage = new TissuePressureCommand(_diveModel, diveStepModelStub.Object);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(tissuePressureNitrogenResult, _diveModel.DiveProfile.TissuePressuresNitrogen);
            Assert.Equal(tissuePressureHeliumResult, _diveModel.DiveProfile.TissuePressuresHelium);
            Assert.Equal(tissuePressureTotalResult, _diveModel.DiveProfile.TissuePressuresTotal);
        }

        private IDiveProfileModel SetupDiveProfileModel(IDiveProfileModel diveProfileModel, double[] tissuePressuresNitrogen, double[] tissuePressuresHelium, double[] tissuePressuresTotal)
        {
            diveProfileModel.TissuePressuresNitrogen = new List<double>(tissuePressuresNitrogen);
            diveProfileModel.TissuePressuresHelium = new List<double>(tissuePressuresHelium);
            diveProfileModel.TissuePressuresTotal = new List<double>(tissuePressuresTotal);
            
            return diveProfileModel;
        }

        private Mock<IDiveStepModel> SetupDiveStepModelStub(int bottomTime)
        {
            Mock<IDiveStepModel> diveStepModelStub = new();
            diveStepModelStub.Setup(x => x.Time).Returns(bottomTime);

            return diveStepModelStub;
        }
    }
}