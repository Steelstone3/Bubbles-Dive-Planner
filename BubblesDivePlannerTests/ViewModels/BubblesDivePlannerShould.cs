using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.ViewModels;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class BubblesDivePlannerShould
    {
        private readonly DivePlanViewModel divePlanViewModel = new();
        private readonly Mock<IDivePlan> divePlan = new();

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            divePlanViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            divePlanViewModel.DivePlan = divePlan.Object;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(divePlanViewModel.DivePlan), viewModelEvents);
        }
    }
}