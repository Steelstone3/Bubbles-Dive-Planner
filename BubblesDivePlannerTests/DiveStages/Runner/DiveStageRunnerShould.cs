using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using Moq;
using System.Collections.Generic;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.Results;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveStep;

namespace BubblesDivePlannerTests.DiveStages.Runner
{
    public class DiveStageRunnerShould
    {
        //TODO AH need to check the results make sense
        [Fact]
        public void RunDiveStages()
        {
            //Arrange
            IDiveModel diveModel = new Zhl16BuhlmannModel();
            IDiveStepModel diveStepModel = new DiveStepViewModel();
            ICylinderSetupModel selectedCylinder = new CylinderSetupViewModel();
            IResultModel resultModel = new ResultViewModel();
            var diveStageCommandFactory = new DiveStageCommandFactory(diveModel, diveStepModel, selectedCylinder, resultModel);
            IDiveStageRunner diveStageRunner = new DiveStageRunner(resultModel, diveStageCommandFactory);

            //Act
            var results = diveStageRunner.RunDiveStages();

            //Assert
            Assert.NotNull(results);
        }
    }
}