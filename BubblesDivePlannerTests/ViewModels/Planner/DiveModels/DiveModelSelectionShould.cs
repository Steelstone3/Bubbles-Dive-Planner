using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Planner.DiveModels;
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