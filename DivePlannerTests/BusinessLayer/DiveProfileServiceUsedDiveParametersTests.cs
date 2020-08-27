using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers.DiveStages;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class DiveProfileServiceUsedDiveParametersTests
    {
        [Theory]
        [InlineData("Bob", 50, 10, "Fun Gas", 21, 10)]
        [InlineData("Terry", 30, 20, "Evil Gas", 32, 0)]
        public void Test(string diveModelName, int depth, int time, string gasMixName, int oxygen, int helium)
        {
            //Arrange
            //What is used
            var diveModel = new Zhl16Buhlmann()
            {
                DiveModelName = diveModelName,
            };

            var diveStep = new DiveStepModel()
            {
                Depth = depth,
                Time = time,
            };

            var gasMixture = new GasMixtureModel()
            {
                GasName = gasMixName,
                Oxygen = oxygen,
                Helium = helium,
                //Nitrogen is privately set in the view model only
            };

            //Not used in the results view already tested
            var gasManagement = new GasManagementViewModel();

            var diveParametersModel = new DiveParametersOutputModel();

            var diveStage = new PreDiveStageStepInfo(diveParametersModel, diveModel, diveStep, gasMixture, gasManagement);

            //Act
            diveStage.RunStage();

            //Assert
            Assert.Equal(diveModelName, diveParametersModel.DiveModelUsed);
            Assert.Equal(depth, diveParametersModel.Depth);
            Assert.Equal(time, diveParametersModel.Time);
            Assert.Equal(gasMixName, diveParametersModel.GasName);
            Assert.Equal(oxygen, diveParametersModel.Oxygen);
            Assert.Equal(helium, diveParametersModel.Helium);
            Assert.NotNull(diveParametersModel.DiveProfileStepHeader);
        }
    }
}