using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.ViewModels.DivePlan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlan
{
    public class CylinderViewModelShould
    {
        private readonly CylinderViewModel cylinderViewModel = new();
        private readonly Mock<ICylinder> cylinder = new();

        [Fact]
        public void Initialise()
        {
            // Then
            Assert.NotNull(cylinderViewModel.Cylinders);
            Assert.Null(cylinderViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            cylinderViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            cylinderViewModel.SelectedCylinder = cylinder.Object;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.NotNull(cylinderViewModel.Cylinders);
            Assert.Contains(nameof(cylinderViewModel.SelectedCylinder), viewModelEvents);
        }
    }
}