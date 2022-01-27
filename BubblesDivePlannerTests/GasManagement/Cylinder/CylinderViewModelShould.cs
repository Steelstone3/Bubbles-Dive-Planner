using BubblesDivePlanner.GasManagement.GasMixture;
using BubblesDivePlanner.GasManagement.GasUsage;
using BubblesDivePlanner.GasManagement.Cylinder;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace BubblesDivePlannerTests.GasManagement.Cylinder
{
    public class CylinderViewModelShould
    {
        private CylinderViewModel _cylinderViewModel = new();
        int _cylinderVolume = 12;
        int _cylinderPressure = 200;

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _cylinderViewModel.CylinderVolume = _cylinderVolume;
            _cylinderViewModel.CylinderPressure = _cylinderPressure;

            //Assert
            Assert.Equal(_cylinderVolume, _cylinderViewModel.CylinderVolume);
            Assert.Equal(_cylinderPressure, _cylinderViewModel.CylinderPressure);
            Assert.NotNull(_cylinderViewModel.GasUsage);
            Assert.NotNull(_cylinderViewModel.GasMixture);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IGasMixtureModel> _gasMixtureModelDummy = new();
            Mock<IGasUsageModel> _gasUsageModelDummy = new();
            var viewModelEvents = new List<string>();
            _cylinderViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _cylinderViewModel.CylinderVolume = _cylinderVolume;
            _cylinderViewModel.CylinderPressure = _cylinderPressure;
            _cylinderViewModel.GasMixture = _gasMixtureModelDummy.Object;
            _cylinderViewModel.GasUsage = _gasUsageModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_cylinderViewModel.CylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_cylinderViewModel.CylinderPressure), viewModelEvents);
            Assert.Contains(nameof(_cylinderViewModel.GasMixture), viewModelEvents);
            Assert.Contains(nameof(_cylinderViewModel.GasUsage), viewModelEvents);
            Assert.Equal(4, viewModelEvents.Count);
        }
    }
}