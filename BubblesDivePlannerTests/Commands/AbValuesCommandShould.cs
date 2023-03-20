using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlannerTests.TestFixtures;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class AbValuesCommandShould
    {
        [Fact]
        public void RunAbValuesStage()
        {
            //Arrange
            var expectedDiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            diveModel.DiveProfile.TissuePressuresNitrogen = DivePlannerApplicationTestFixture.GetDiveProfile.TissuePressuresNitrogen;
            diveModel.DiveProfile.TissuePressuresHelium = DivePlannerApplicationTestFixture.GetDiveProfile.TissuePressuresHelium;
            diveModel.DiveProfile.TissuePressuresTotal = DivePlannerApplicationTestFixture.GetDiveProfile.TissuePressuresTotal;

            IDiveStageCommand diveStage = new AbValuesCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.AValues, diveModel.DiveProfile.AValues);
            Assert.Equal(expectedDiveProfile.BValues, diveModel.DiveProfile.BValues);
        }
    }
}