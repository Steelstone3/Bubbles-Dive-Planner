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
        [Fact]
        public void RunCompartmentLoadStage()
        {
            //Arrange
            var expectedDiveProfile = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            var diveModel = DivePlannerApplicationTestFixture.GetDiveModel;
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