using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.Models;

namespace DivePlannerTests
{
    public class GasManagementSetupUiTests
    {
        private PlanGasManagementViewModel gasManagementSetup = new PlanGasManagementViewModel()
        {
            GasManagementModel = new GasManagementSetupModel(),
        };

        [Theory]
        [InlineData(12)]
        [InlineData(1)]
        [InlineData(15)]
        public void GasManagementCylinderVolumeCanBeSetTest(int cylinderVolume)
        {
            //Arrange

            //Act
            gasManagementSetup.GasManagementModel.CylinderVolume = cylinderVolume;

            //Assert
            Assert.Equal(cylinderVolume, gasManagementSetup.GasManagementModel.CylinderVolume);
        }

        [Theory(Skip="Need to check validation in this test")]
        [InlineData(0)]
        [InlineData(16)]
        public void GasManagementCylinderVolumeLimitsTest(int cylinderVolume)
        {
            //TODO AH Consider a combo box of 3, 5, 7, 10, 12, 15, 20, 24, 30
            //TODO AH Consider a list of gas supplies that you can add and take away from each of which you can assign a gas and supply to and switch out dyamically on the dive with each gas showing remaining and a history of gas usage

            //Arrange

            //Act
            //Invalid values need to be handled
            gasManagementSetup.GasManagementModel.CylinderVolume = cylinderVolume;

            //Assert
            Assert.Equal(cylinderVolume, gasManagementSetup.GasManagementModel.CylinderVolume);

            //TODO AH Assert cannot calculate
        }

        [Theory]
        [InlineData(12)]
        public void GasManagementSacRateCanBeSetTest(int sacRate)
        {
            //Arrange

            //Act
            gasManagementSetup.GasManagementModel.SacRate = sacRate;
            //Assert
            Assert.Equal(sacRate, gasManagementSetup.GasManagementModel.SacRate);
        }

        [Theory(Skip="Need to check validation in this test")]
        //May use these values as warnings in version 2
        //[InlineData(5)]
        //[InlineData(91)]
        //Where these error
        [InlineData(11)]
        [InlineData(31)]
        public void GasManagementSacRateLimitsTest(int sacRate)
        {
            //TODO AH use range attribute

            //Arrange

            //Act
            gasManagementSetup.GasManagementModel.SacRate = sacRate;
            //Assert
            Assert.Equal(sacRate, gasManagementSetup.GasManagementModel.SacRate);

            //TODO AH check validation this test isn't valid
        }

        [Theory]
        [InlineData(50)]
        [InlineData(200)]
        [InlineData(300)]
        public void GasManagementCylinderPressureCanBeSetTest(int cylinderPressure)
        {
            //Arrange

           //Act
            gasManagementSetup.GasManagementModel.CylinderPressure = cylinderPressure;
            //Assert
            Assert.Equal(cylinderPressure, gasManagementSetup.GasManagementModel.CylinderPressure);
        }

         [Theory(Skip="Need to check validation in this test")]
         [InlineData(49)]
         [InlineData(301)]
        public void GasManagementCylinderPressureLimitsTest(int cylinderPressure)
        {
            //TODO AH Consider a combo box 50, 100, 150, 200, 250, 300
            //Alternatively provide a range between 50 and 300

            //Arrange

            //Act
            gasManagementSetup.GasManagementModel.CylinderPressure = cylinderPressure;
            //Assert
            Assert.Equal(cylinderPressure, gasManagementSetup.GasManagementModel.CylinderPressure);

            //TODO check can execute
        }
    }
}