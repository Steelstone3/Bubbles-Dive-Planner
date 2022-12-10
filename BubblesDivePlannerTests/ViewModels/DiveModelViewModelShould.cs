using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class DiveModelViewModelShould
    {
        [Fact]
        public void ContainsDiveModels()
        {
            // Given
            DiveModelViewModel diveModelViewModel = new();
        
            // Then
            Assert.NotEmpty(diveModelViewModel.DiveModels);
        }
    }
}