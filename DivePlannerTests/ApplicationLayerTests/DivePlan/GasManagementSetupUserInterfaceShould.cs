using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class GasManagementSetupUserInterfaceShould
    {
        private GasManagementViewModel _gasManagement = new GasManagementViewModel();

        [Theory]
        [InlineData(12, 12, 50, 200, 400, 600)]
        [InlineData(1, 12, 200, -800, 1000, 200)]
        [InlineData(15, 30, 300, 4200, 300, 4500)]
        public void AllowGasManagementModelToBeSet(int cylinderVolume, int sacRate, int cylinderPressure, int gasRemaining, int gasUsedForStep, int initialGasVolume)
        {
            //Arrange
            _gasManagement.CylinderVolume = cylinderVolume;
            _gasManagement.CylinderPressure = cylinderPressure;
            _gasManagement.SacRate = sacRate;

            _gasManagement.GasUsedForStep = gasUsedForStep;

            //Assert
            Assert.Equal(cylinderVolume, _gasManagement.CylinderVolume);
            Assert.Equal(cylinderPressure, _gasManagement.CylinderPressure);
            Assert.Equal(sacRate, _gasManagement.SacRate);
            Assert.Equal(initialGasVolume, _gasManagement.InitialCylinderTotalVolume);
            Assert.Equal(gasRemaining, _gasManagement.GasRemaining);
            Assert.Equal(gasUsedForStep, _gasManagement.GasUsedForStep);
        }

        [Fact]
        public void RaisePropertyChangedWhenCylinderVolumeIsSet()
        {
            //Arrange
            var cylinderVolumeEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => cylinderVolumeEvent = e.PropertyName);
            _gasManagement.CylinderVolume = 10;
            Assert.Equal(nameof(_gasManagement.CylinderVolume), cylinderVolumeEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenCylinderPressureIsSet()
        {
            string cylinderPressureEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => cylinderPressureEvent = e.PropertyName);
            _gasManagement.CylinderPressure = 200;
            Assert.Equal(nameof(_gasManagement.CylinderPressure), cylinderPressureEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenSurfaceAirConsumptionRateIsSet()
        {
            string sacRateEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => sacRateEvent = e.PropertyName);
            _gasManagement.SacRate = 12;
            Assert.Equal(nameof(_gasManagement.SacRate), sacRateEvent);
        }

        [Fact]
        public void HaveADefaultVisibiltyState()
        {
            //Arrange

            //Act

            //Assert
            Assert.False(_gasManagement.IsGasUsageVisible);
            Assert.True(_gasManagement.IsUiVisible);
            Assert.True(_gasManagement.IsUiEnabled);
        }
    }
}