using Xunit;

namespace BubblesDivePlannerTests.ViewModels
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