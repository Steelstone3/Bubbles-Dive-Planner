using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.GasManagement.GasUsage;

namespace BubblesDivePlannerTests.GasManagement.GasUsage
{
    public class GasUsageViewModelShould
    {
        private GasUsageViewModel _gasUsage;
        private int _initialPressurisedCylinderVolume;
        private int _gasRemaining;
        private int _gasUsed;
        private int _surfaceAirConsumptionRate;

        public GasUsageViewModelShould()
        {
            _initialPressurisedCylinderVolume = 2400;
            _gasRemaining = 1680;
            _gasUsed = 720;
            _surfaceAirConsumptionRate = 12;
            _gasUsage = new();
        }

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

            //Assert
            Assert.Contains(nameof(_gasUsage.InitialPressurisedCylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasRemaining), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasUsed), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.SurfaceAirConsumptionRate), viewModelEvents);
        }
    }
}