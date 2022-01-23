using System.Collections.Generic;
using Moq;
using Xunit;
using BubblesDivePlanner.GasManagement.GasUsage;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class GasUsageViewModelShould
    {
        private GasUsageViewModel _gasUsage;
        private int initialPressurisedCylinderVolume;
        private int gasRemaining;
        private int gasUsed;
        private  int surfaceAirConsumptionRate;

        public GasUsageViewModelShould()
        {
            initialPressurisedCylinderVolume = 2400;
            gasRemaining = 1680;
            gasUsed = 720;
            surfaceAirConsumptionRate = 12;
            _gasUsage = new();
        }

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _gasUsage.InitialPressurisedCylinderVolume = initialPressurisedCylinderVolume;
            _gasUsage.GasUsed = gasUsed;
            _gasUsage.GasRemaining = gasRemaining;
            _gasUsage.SurfaceAirConsumptionRate = surfaceAirConsumptionRate;

            //Assert
            Assert.Equal(initialPressurisedCylinderVolume, _gasUsage.InitialPressurisedCylinderVolume);
            Assert.Equal(gasRemaining, _gasUsage.GasRemaining);
            Assert.Equal(gasUsed, _gasUsage.GasUsed);
            Assert.Equal(surfaceAirConsumptionRate, _gasUsage.SurfaceAirConsumptionRate);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _gasUsage.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _gasUsage.InitialPressurisedCylinderVolume = initialPressurisedCylinderVolume;
            _gasUsage.GasRemaining = gasRemaining;
            _gasUsage.GasUsed = gasUsed;
            _gasUsage.SurfaceAirConsumptionRate = surfaceAirConsumptionRate;

            //Assert
            Assert.Contains(nameof(_gasUsage.InitialPressurisedCylinderVolume), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasRemaining), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasUsed), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.SurfaceAirConsumptionRate), viewModelEvents);
        }
    }
}