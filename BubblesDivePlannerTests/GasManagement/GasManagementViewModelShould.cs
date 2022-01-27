using BubblesDivePlanner.GasManagement.Cylinder;
using BubblesDivePlanner.GasManagement.GasMixture;
using BubblesDivePlanner.GasManagement;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.GasManagement
{
    public class GasManagementViewModelShould
    {
        private GasManagementViewModel _gasManagementViewModel;
        private Mock<ICylinderModel> _cylinderModelDummy = new();

        public GasManagementViewModelShould()
        {
            Mock<IGasMixtureController> gasMixtureControllerDummy = new();
            _gasManagementViewModel = new(gasMixtureControllerDummy.Object);
        }

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
            Assert.Contains(nameof(_gasManagementViewModel.SelectedCylinder), viewModelEvents);
        }
    }
}