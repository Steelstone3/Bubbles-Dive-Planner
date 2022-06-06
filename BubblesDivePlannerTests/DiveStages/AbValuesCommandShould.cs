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
        private DivePlannerApplicationTestFixture diveStagesTextFixture = new DivePlannerApplicationTestFixture();

        [Fact]
        public void RunAbValuesStage()
        {
            //Arrange
            var expectedDiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var tissuePressureNitrogen = expectedDiveProfile.TissuePressuresNitrogen;;
            var tissuePressureHelium = expectedDiveProfile.TissuePressuresHelium;
            var tissuePressureTotal = expectedDiveProfile.TissuePressuresTotal;
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile.TissuePressuresNitrogen = diveStagesTextFixture.GetDiveProfile.TissuePressuresNitrogen;
            diveModel.DiveProfile.TissuePressuresHelium = diveStagesTextFixture.GetDiveProfile.TissuePressuresHelium;
            diveModel.DiveProfile.TissuePressuresTotal = diveStagesTextFixture.GetDiveProfile.TissuePressuresTotal;

            IDiveStageCommand diveStage = new AbValuesCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.AValues, diveModel.DiveProfile.AValues);
            Assert.Equal(expectedDiveProfile.BValues, diveModel.DiveProfile.BValues);
        }
    }
}