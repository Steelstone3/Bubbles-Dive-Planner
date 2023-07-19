using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan.DiveModels;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan.DiveModels
{
    public class DiveModelSelectionShould
    {
        private readonly IDiveModelSelection diveModelSelection = new DiveModelSelection();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.NotEmpty(diveModelSelection.DiveModels);
        }

        [Fact]
        public void BeAViewModelBase()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(diveModelSelection);
        }
    }
}