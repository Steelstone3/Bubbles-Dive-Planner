using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlannerTests.Asserters;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class CompartmentLoadCommandShould
    {
        private DiveStagesTextFixture diveStagesTextFixture = new DiveStagesTextFixture();
        private DiveParameterAsserter diveParameterAsserter = new DiveParameterAsserter();

        [Fact]
        public void RunCompartmentLoadStage()
        {
            //Arrange
            var expectedDiveProfile = diveStagesTextFixture.GetDiveProfileResultFromFirstRun;
            var diveModel = diveStagesTextFixture.GetDiveModel;
            diveModel.DiveProfile.TissuePressuresTotal = expectedDiveProfile.TissuePressuresTotal;
            diveModel.DiveProfile.MaxSurfacePressures = expectedDiveProfile.MaxSurfacePressures;

            var diveStage = new CompartmentLoadCommand(diveModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(expectedDiveProfile.CompartmentLoad, diveModel.DiveProfile.CompartmentLoad);
        }
    }
}