using System.Collections.Generic;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.ViewModels.DivePlan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlan
{
    public class DivePlanViewModelShould
    {
        private readonly DivePlanViewModel divePlanViewModel = new();
        private readonly Mock<IDivePlan> divePlan = new();

        [Fact]
        public void Initialise()
        {
            // Then
            Assert.Null(divePlanViewModel.DivePlan);
        }

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