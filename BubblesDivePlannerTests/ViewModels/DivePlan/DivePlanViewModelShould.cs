using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DivePlan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlan
{
    public class DivePlanViewModelShould
    {
        private readonly DivePlanViewModel divePlanViewModel = new();
        private readonly DiveModelViewModel diveModelViewModel = new();
        private readonly DiveStepViewModel diveStepViewModel = new();
        private readonly CylinderViewModel cylinderViewModel = new();

        [Fact]
        public void Initialise()
        {
            // Then
            Assert.NotNull(divePlanViewModel.DiveModel);
            Assert.NotNull(divePlanViewModel.DiveStep);
            Assert.NotNull(divePlanViewModel.Cylinders);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            divePlanViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            divePlanViewModel.DiveModel = diveModelViewModel;
            divePlanViewModel.DiveStep = diveStepViewModel;
            divePlanViewModel.Cylinders = cylinderViewModel;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(divePlanViewModel.DiveModel), viewModelEvents);
            Assert.Contains(nameof(divePlanViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(divePlanViewModel.Cylinders), viewModelEvents);
        }
    }
}