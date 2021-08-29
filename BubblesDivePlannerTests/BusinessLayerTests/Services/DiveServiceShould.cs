using System.Collections.Generic;
using BubblesDivePlanner.Commands.DiveStages;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.Plan;
using BubblesDivePlanner.Models.Results;
using Xunit;

namespace BubblesDivePlannerTests.BusinessLayerTests.Services
{
    public class DiveServiceShould
    {
        //Using the buhlmann model for tests
        private Zhl16Buhlmann _diveModel = new();
        private DiveProfile _diveProfile = new();

        [Theory]
        [InlineData("Bob", 50, 10, "Fun Gas", 21, 10, 4.5)]
        [InlineData("Terry", 30, 20, "Evil Gas", 32, 0, 4.5)]
        public void PopulateTheDiveServiceParameters(string diveModelName, int depth, int time, string gasMixName,
            int oxygen, int helium, double diveCeiling)
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
            var gasManagement = new GasManagementModel();

            var diveParametersModel = new DiveParametersResultModel();
            
            var diveStage = new PostDiveStageStepInfo(diveParametersModel, diveModel, diveStep, gasMixture,
                gasManagement,
                new List<double>() { 1.1, 1.2, 1.3, 1.45, 1.2 });

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
            Assert.NotEmpty(diveParametersModel.DiveProfileStepHeader);
            Assert.Equal(diveCeiling, diveParametersModel.DiveCeiling);
        }
    }
}