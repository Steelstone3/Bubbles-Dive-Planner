using BubblesDivePlanner.DiveBoundaries;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DiveBoundaries
{
    public class DiveCeilingControllerShould
    {
        [Fact]
        public void CalculateDiveCeiling()
        {
            //Arrange
            var toleratedAmbientPressures = DivePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun.ToleratedAmbientPressures;

            //Act
            double diveCeiling = DiveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures);

            //Assert
            Assert.Equal(4.07, diveCeiling);
        }
    }
}