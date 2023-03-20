using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;

namespace BubblesDivePlannerTests.Cylinders.CylinderSelector
{
    public class CylinderSelectorViewModelShould
    {
        private readonly CylinderSelectorViewModel _cylinderSelectorViewModel = new();
        private readonly Mock<ICylinderSetupModel> _cylinderModelDummy = new();

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

        [Fact]
        public void ValidateModelAtTheBoundsInvalid()
        {
            //Arrange
            ICylinderSetupModel _cylinderViewModel = new CylinderSetupViewModel();

            //Act
            var isValid = _cylinderSelectorViewModel.ValidateSelectedCylinder(_cylinderViewModel);

            //Assert
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateModelAtTheBoundsValid()
        {
            //Arrange
            ICylinderSetupModel _cylinderViewModel = new CylinderSetupViewModel()
            {
                CylinderName = "Air",
                CylinderVolume = 12,
                CylinderPressure = 200,
                GasMixture = new GasMixtureViewModel()
                {
                    Oxygen = 21,
                    Helium = 0,
                },
                GasUsage = new GasUsageViewModel()
                {
                    SurfaceAirConsumptionRate = 12,
                }
            };

            //Act
            var isValid = _cylinderSelectorViewModel.ValidateSelectedCylinder(_cylinderViewModel);

            //Assert
            Assert.True(isValid);
        }

        [Fact]
        public void RaiseSelectedCylinderChangedEvent()
        {
            //Arrange
            const string eventFired = "event fired";
            var viewModelEvents = new List<string>();
            var selectedCylinder = new Mock<ICylinderSetupModel>();
            _cylinderSelectorViewModel.SelectedCylinderChanged += (sender, e) => viewModelEvents.Add(eventFired);

            //Act
            _cylinderSelectorViewModel.SelectedCylinder = selectedCylinder.Object;

            //Assert
            Assert.Contains(eventFired, viewModelEvents);
        }
    }
}