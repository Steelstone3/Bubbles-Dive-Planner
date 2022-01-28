using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlannerTests.Cylinders.CylinderSelector
{
    public class GasManagementViewModelShould
    {
        private CylinderSelectorViewModel _gasManagementViewModel = new();
        private Mock<ICylinderSetupModel> _cylinderModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _gasManagementViewModel.Cylinders.Add(_cylinderModelDummy.Object);

            //Assert
            Assert.NotEmpty(_gasManagementViewModel.Cylinders);
            Assert.NotNull(_gasManagementViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _gasManagementViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _gasManagementViewModel.SelectedCylinder = _cylinderModelDummy.Object;

            //Assert
            Assert.Single(viewModelEvents);
            Assert.Contains(nameof(_gasManagementViewModel.SelectedCylinder), viewModelEvents);
        }
    }
}