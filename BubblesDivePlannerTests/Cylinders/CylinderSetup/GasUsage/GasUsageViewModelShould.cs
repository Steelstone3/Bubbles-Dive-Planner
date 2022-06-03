using System.Collections.Generic;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageViewModelShould
    {
        private GasUsageViewModel _gasUsage = new();
        private ushort _initialPressurisedCylinderVolume = 2400;
        private ushort _gasRemaining = 1680;
        private ushort _gasUsed = 720;
        private byte _surfaceAirConsumptionRate = 12;

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _gasUsage.InitialPressurisedCylinderVolume = _initialPressurisedCylinderVolume;
            _gasUsage.GasUsed = _gasUsed;
            _gasUsage.GasRemaining = _gasRemaining;
            _gasUsage.SurfaceAirConsumptionRate = _surfaceAirConsumptionRate;

            //Assert
            Assert.Equal(_initialPressurisedCylinderVolume, _gasUsage.InitialPressurisedCylinderVolume);
            Assert.Equal(_gasRemaining, _gasUsage.GasRemaining);
            Assert.Equal(_gasUsed, _gasUsage.GasUsed);
            Assert.Equal(_surfaceAirConsumptionRate, _gasUsage.SurfaceAirConsumptionRate);
            Assert.False(_gasUsage.IsVisible);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _gasUsage.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _gasUsage.InitialPressurisedCylinderVolume = _initialPressurisedCylinderVolume;
            _gasUsage.GasRemaining = _gasRemaining;
            _gasUsage.GasUsed = _gasUsed;
            _gasUsage.SurfaceAirConsumptionRate = _surfaceAirConsumptionRate;
            _gasUsage.IsVisible = true;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_gasUsage.InitialPressurisedCylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasRemaining), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasUsed), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.SurfaceAirConsumptionRate), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.IsVisible), viewModelEvents);
        }
    }
}