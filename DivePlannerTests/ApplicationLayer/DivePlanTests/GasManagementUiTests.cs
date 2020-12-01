using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using System.Collections.Generic;

namespace DivePlannerTests
{
    public class GasManagementUiTests
    {
        private GasManagementViewModel _gasManagement = new GasManagementViewModel();

        [Fact]
        public void RaisePropertyChangedCylinderVolumeTests()
        {
            //Arrange
            var cylinderVolumeEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => cylinderVolumeEvent = e.PropertyName);
            _gasManagement.CylinderVolume = 10;
            Assert.Equal(nameof(_gasManagement.CylinderVolume), cylinderVolumeEvent);
        }

        [Fact]
        public void RaisePropertyChangedCylinderPressureTests()
        {
            string cylinderPressureEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => cylinderPressureEvent = e.PropertyName);
            _gasManagement.CylinderPressure = 200;
            Assert.Equal(nameof(_gasManagement.CylinderPressure), cylinderPressureEvent);
        }

        [Fact]
        public void RaisePropertyGasRemainingVolumeTests()
        {
            string gasRemainingEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasRemainingEvent = e.PropertyName);
            _gasManagement.GasRemaining = 2000;
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasRemainingEvent);
        }

        [Fact]
        public void RaisePropertyGasUsedVolumeTests()
        {
            var gasUsedEvents = new List<string>();

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => gasUsedEvents.Add( e.PropertyName));
            _gasManagement.GasUsedForStep = 200;
            Assert.Equal(nameof(_gasManagement.GasUsedForStep), gasUsedEvents[0]);
            Assert.Equal(nameof(_gasManagement.GasRemaining), gasUsedEvents[1]);
        }

        [Fact]
        public void RaisePropertyChangedSurfaceAirConsumptionRateTests()
        {
            string sacRateEvent = "Not Fired";

            //AAA
            _gasManagement.PropertyChanged += ((sender, e) => sacRateEvent = e.PropertyName);
            _gasManagement.SacRate = 12;
            Assert.Equal(nameof(_gasManagement.SacRate), sacRateEvent);
        }

        [Fact]
        public void RaisePropertyChangedInitialGasVolumeTests()
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
        public void GasManagementModelCanBeSetTest(int cylinderVolume, int sacRate, int cylinderPressure, int gasRemaining, int gasUsedForStep, int initialGasVolume)
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



        [Theory(Skip = "Composite can execute command needs to be created, Can execute command needs to be created")]
        [InlineData(0, 11, 49)]
        [InlineData(16, 31, 301)]
        public void GasManagementModelLimitsTests(int cylinderVolume, int sacRate, int cylinderPressure)
        {
            //TODO AH may need to look into a composite command for CanExecute using reactiveUI i.e can execute create combined() and https://www.reactiveui.net/docs/handbook/commands/
            //TODO AH Range Validations for Gas Management View Model and CanExecute for the calculate
            //TODO AH Consider a combo box of 3, 5, 7, 10, 12, 15, 20, 24, 30
            //TODO AH Consider a list of gas supplies that you can add and take away from each of which you can assign a gas and supply to and switch out dyamically on the dive with each gas showing remaining and a history of gas usage

            //TODO AH use range attribute on sac rate

            //TODO AH provide a range between 50 and 300 for Cylinder Pressure

            //Arrange
            _gasManagement.CylinderPressure = cylinderPressure;
            _gasManagement.CylinderVolume = cylinderVolume;
            _gasManagement.SacRate = sacRate;

            //Act
            //var canExecute = await = _gasManagementSetup.CanUseGasManagementSetup().FirstAsync();

            //Assert can calculate command as expected false
            //Assert
            //Assert.Equal(false, canExecute);
        }

        [Fact]
        public void GasManagementVisiblityTest()
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