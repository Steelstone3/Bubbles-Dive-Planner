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
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();

        [Fact]
        public void RunAbValuesStage()
        {
            //Arrange
            var expectedDiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var tissuePressureNitrogen = expectedDiveProfile.TissuePressuresNitrogen;;
            var tissuePressureHelium = expectedDiveProfile.TissuePressuresHelium;
            var tissuePressureTotal = expectedDiveProfile.TissuePressuresTotal;
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile.TissuePressuresNitrogen = new List<double>(tissuePressureNitrogen);
            diveModel.DiveProfile.TissuePressuresHelium = new List<double>(tissuePressureHelium);
            diveModel.DiveProfile.TissuePressuresTotal = new List<double>(tissuePressureTotal);

            IDiveStageCommand diveStage = new AbValuesCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.AValues, diveModel.DiveProfile.AValues);
            Assert.Equal(expectedDiveProfile.BValues, diveModel.DiveProfile.BValues);
        }
    }
}