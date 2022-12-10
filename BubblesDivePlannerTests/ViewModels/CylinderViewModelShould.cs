using System.Collections.Generic;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.ViewModels;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class CylinderViewModelShould
    {
        private readonly CylinderViewModel cylinderViewModel = new();
        private readonly ICylinder cylinder = new Cylinder("", 0, 0, 0, new GasMixture(0, 0), 0, 0);

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            cylinderViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            cylinderViewModel.SelectedCylinder = cylinder;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.NotNull(cylinderViewModel.Cylinders);
            Assert.Contains(nameof(cylinderViewModel.SelectedCylinder), viewModelEvents);
        }
    }
}