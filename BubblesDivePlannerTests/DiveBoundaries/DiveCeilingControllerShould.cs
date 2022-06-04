using BubblesDivePlanner.DiveBoundaries;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveBoundaries
{
    public class DiveCeilingControllerShould
    {
        private DiveStagesTextFixture _diveStagesTestFixtures = new();
        private DiveCeilingController _diveCeilingController = new();

        [Fact]
        public void CalculateDiveCeiling()
        {
            //Arrange
            var toleratedAmbientPressures = _diveStagesTestFixtures.GetDiveProfileResultFromFirstRun.ToleratedAmbientPressures;

            //Act
            double diveCeiling = _diveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures);

            //Assert
            Assert.Equal(4.07, diveCeiling);
        }
    }
}