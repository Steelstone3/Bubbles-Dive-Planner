using Xunit;
using Moq;
using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup
{
    public class CylinderSetupViewModelShould
    {
        private CylinderSetupViewModel _cylinderSetupViewModel = new();
        private string _cylinderName = "EAN32";
        private byte _cylinderVolume = 12;
        private ushort _cylinderPressure = 200;
        private ushort _initialPressurisedCylinderVolume = 2400;

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _cylinderSetupViewModel.CylinderName = _cylinderName;
            _cylinderSetupViewModel.CylinderVolume = _cylinderVolume;
            _cylinderSetupViewModel.CylinderPressure = _cylinderPressure;
            _cylinderSetupViewModel.InitialPressurisedCylinderVolume = _initialPressurisedCylinderVolume;


            //Assert
            Assert.Equal(_cylinderName, _cylinderSetupViewModel.CylinderName);
            Assert.Equal(_cylinderVolume, _cylinderSetupViewModel.CylinderVolume);
            Assert.Equal(_cylinderPressure, _cylinderSetupViewModel.CylinderPressure);
            Assert.Equal(_initialPressurisedCylinderVolume, _cylinderSetupViewModel.InitialPressurisedCylinderVolume);
            Assert.True(_cylinderSetupViewModel.IsVisible);
            Assert.NotNull(_cylinderSetupViewModel.GasUsage);
            Assert.NotNull(_cylinderSetupViewModel.GasMixture);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IGasMixtureModel> _gasMixtureModelDummy = new();
            Mock<IGasUsageModel> _gasUsageModelDummy = new();
            var viewModelEvents = new List<string>();
            _cylinderSetupViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _cylinderSetupViewModel.CylinderName = _cylinderName;
            _cylinderSetupViewModel.CylinderVolume = _cylinderVolume;
            _cylinderSetupViewModel.CylinderPressure = _cylinderPressure;
            _cylinderSetupViewModel.InitialPressurisedCylinderVolume = _initialPressurisedCylinderVolume;
            _cylinderSetupViewModel.IsVisible = false;
            _cylinderSetupViewModel.GasMixture = _gasMixtureModelDummy.Object;
            _cylinderSetupViewModel.GasUsage = _gasUsageModelDummy.Object;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.CylinderName), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.CylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.CylinderPressure), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.InitialPressurisedCylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.IsVisible), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.GasMixture), viewModelEvents);
            Assert.Contains(nameof(_cylinderSetupViewModel.GasUsage), viewModelEvents);
        }
    }
}