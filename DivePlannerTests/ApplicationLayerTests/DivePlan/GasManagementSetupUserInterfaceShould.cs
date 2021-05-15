using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class GasManagementSetupUserInterfaceShould
    {
        private GasManagementViewModel _gasManagement = new GasManagementViewModel();

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
        public void RaisePropertyChangedWhenGasRemainingVolumeIsSet()
        {
            string gasRemainingEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasRemainingEvent = e.PropertyName);
            _gasManagement.GasRemaining = 2000;
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasRemainingEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenGasUsedVolumeIsSet()
        {
            var gasUsedEvents = new List<string>();

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasUsedEvents.Add(e.PropertyName));
            _gasManagement.GasUsedForStep = 200;
            Assert.Equal(nameof(_gasManagement.GasUsedForStep), gasUsedEvents[0]);
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasUsedEvents[1]);
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
        public void RaisePropertyChangedWhenInitialGasVolumeIsSet()
        {
            string initialGasEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => initialGasEvent = e.PropertyName);
            _gasManagement.InitialCylinderTotalVolume = 4000;
            Assert.Equal(nameof(_gasManagement.InitialCylinderTotalVolume), initialGasEvent);
        }

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

        [Theory(Skip = "Cylinder Volume Should Be Between 3 and 30")]
        [InlineData(2, false)]
        [InlineData(31, false)]
        [InlineData(3, true)]
        [InlineData(30, true)]
        public void NotAllowDiveStepExecutionIfCylinderVolumeIsOutOfRange(int cylinderVolume, bool expectedResult)
        {

        }

        [Theory(Skip = "Cylinder Pressure Should Be Between 50 and 300")]
        [InlineData(49, false)]
        [InlineData(301, false)]
        [InlineData(300, true)]
        [InlineData(50, true)]
        public void NotAllowDiveStepExecutionIfCylinderPressureIsOutOfRange(int cylinderPressure, bool expectedResult)
        {

        }

        [Theory(Skip = "SAC Rate Should Be Between 5 and 30")]
        [InlineData(4, false)]
        [InlineData(31, false)]
        [InlineData(5, true)]
        [InlineData(30, true)]
        public void NotAllowDiveStepExecutionIfSurfaceAirConsumptionRateIsOutOfRange(int surfaceAirComsumptionRate, bool expectedResult)
        {

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

        [Fact(Skip = "Test needs Implementing")]
        public void HaveAPostDiveStepVisibiltyState()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_gasManagement.IsGasUsageVisible);
            Assert.False(_gasManagement.IsUiVisible);
            Assert.False(_gasManagement.IsUiEnabled);
        }
    }
}