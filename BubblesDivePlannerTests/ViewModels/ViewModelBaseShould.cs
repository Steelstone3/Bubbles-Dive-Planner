using BubblesDivePlanner.ViewModels;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class ViewModelBaseShould
    {
        [Fact]
        public void IsVisible()
        {
            // Given
            ViewModelBase viewModelBase = new();

            // Then
            Assert.True(viewModelBase.IsVisible);
        }
    }
}