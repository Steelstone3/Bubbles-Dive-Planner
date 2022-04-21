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
            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            var results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder, resultModel);

            //Assert
            Assert.NotNull(results);
        }
    }
}