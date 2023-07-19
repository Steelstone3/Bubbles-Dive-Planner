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
            Assert.IsAssignableFrom<ViewModelBase>(diveModelSelection);
            Assert.NotEmpty(diveModelSelection.DiveModels);
        }
    }
}