using System.Collections.Generic;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.DivePlan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class BubblesDivePlannerViewModelShould
    {
        private readonly BubblesDivePlannerViewModel bubblesDivePlannerViewModel = new();
        private readonly DivePlanViewModel divePlan = new();

        [Fact]
        public void Initialise()
        {
            // Then
            Assert.NotNull(bubblesDivePlannerViewModel.DivePlan);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            bubblesDivePlannerViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            bubblesDivePlannerViewModel.DivePlan = divePlan;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(bubblesDivePlannerViewModel.DivePlan), viewModelEvents);
        }
    }
}