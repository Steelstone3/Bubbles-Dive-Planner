using BubblesDivePlanner.DiveBoundaries;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveBoundaries
{
    public class DiveCeilingControllerShould
    {
        private readonly DivePlannerApplicationTestFixture _diveStagesTestFixtures = new();

        [Fact]
        public void CalculateDiveCeiling()
        {
            //Arrange
            var toleratedAmbientPressures = _diveStagesTestFixtures.GetDiveProfileResultFromFirstRun.ToleratedAmbientPressures;

            //Act
            double diveCeiling = DiveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures);

            //Assert
            Assert.Equal(4.07, diveCeiling);
        }
    }
}