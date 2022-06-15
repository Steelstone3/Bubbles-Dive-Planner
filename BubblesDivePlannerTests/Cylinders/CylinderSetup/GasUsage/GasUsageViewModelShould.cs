using System.Collections.Generic;
using Xunit;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;

namespace BubblesDivePlannerTests.Cylinders.CylinderSetup.GasUsage
{
    public class GasUsageViewModelShould
    {
        private readonly GasUsageViewModel _gasUsage = new();
        private readonly ushort _gasRemainingPreUsage = 2400;
        private readonly ushort _gasRemaining = 1680;
        private readonly ushort _gasUsed = 720;
        private readonly byte _surfaceAirConsumptionRate = 12;

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _gasUsage.GasUsed = _gasUsed;
            _gasUsage.GasRemaining = _gasRemaining;
            _gasUsage.SurfaceAirConsumptionRate = _surfaceAirConsumptionRate;

            //Assert
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
            _gasUsage.GasRemaining = _gasRemaining;
            _gasUsage.GasUsed = _gasUsed;
            _gasUsage.SurfaceAirConsumptionRate = _surfaceAirConsumptionRate;
            _gasUsage.IsVisible = true;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasRemaining), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.GasUsed), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.SurfaceAirConsumptionRate), viewModelEvents);
            Assert.Contains(nameof(_gasUsage.IsVisible), viewModelEvents);
        }

        [Fact]
        public void UpdateGasRemaining()
        {
            //Assert
            _gasUsage.GasRemaining = _gasRemainingPreUsage;
            _gasUsage.GasUsed = _gasUsed;

            //Act
            _gasUsage.UpdateGasRemaining();

            //Assert
            Assert.Equal(_gasRemaining, _gasUsage.GasRemaining);
        }

        [Fact]
        public void HaveZeroMinimumValueForGasRemaining()
        {
            //Assert
            _gasUsage.GasRemaining = _gasRemaining;
            _gasUsage.GasUsed = 2400;

            //Act
            _gasUsage.UpdateGasRemaining();

            //Assert
            Assert.Equal(0, _gasUsage.GasRemaining);
        }
    }
}