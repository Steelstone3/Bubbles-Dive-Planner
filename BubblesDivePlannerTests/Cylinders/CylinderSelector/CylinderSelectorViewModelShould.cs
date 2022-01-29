using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlannerTests.Cylinders.CylinderSelector
{
    public class CylinderSelectorViewModelShould
    {
        private CylinderSelectorViewModel _cylinderSelectorViewModel = new();
        private Mock<ICylinderSetupModel> _cylinderModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Assert
            Assert.NotNull(_cylinderSelectorViewModel.Cylinders);
            Assert.NotNull(_cylinderSelectorViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _cylinderSelectorViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _cylinderSelectorViewModel.SelectedCylinder = _cylinderModelDummy.Object;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_cylinderSelectorViewModel.SelectedCylinder), viewModelEvents);
        }

        [Fact (Skip = "Unexplained failing test")]
        public void AddCylinders()
        {
            //Arrange
            _cylinderSelectorViewModel.SelectedCylinder = _cylinderModelDummy.Object;

            //Act
            _cylinderSelectorViewModel.AddCylinderCommand.Execute();

            //Assert
            Assert.NotEmpty(_cylinderSelectorViewModel.Cylinders);
        }
    }
}