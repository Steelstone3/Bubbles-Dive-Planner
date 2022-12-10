using BubblesDivePlanner.ViewModels.DivePlan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlan
{
    public class DiveModelViewModelShould
    {
        [Fact]
        public void Initialise()
        {
            // Given
            DiveModelViewModel diveModelViewModel = new();

            // Then
            Assert.NotEmpty(diveModelViewModel.DiveModels);
        }
    }
}