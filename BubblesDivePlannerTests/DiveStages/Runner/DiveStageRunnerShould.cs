using Xunit;
using BubblesDivePlanner.DiveStages.Runner;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using System.Collections.Generic;
using System;
using BubblesDivePlanner.Results;

namespace BubblesDivePlannerTests.DiveStages.Runner
{
    public class DiveStageRunnerShould
    {
        private IDiveModel diveModel;
        private IDiveStepModel diveStepModel;
        private ICylinderSetupModel selectedCylinder;
        private List<double> compartmentLoad = new List<double> { 124.9, 121.46, 110.01, 99.51, 88.78, 82.71, 76.8, 71.87, 68.05, 66.46, 65.63, 65.15, 65.57, 65.05, 65.29, 65.65 };

        [Fact]
        public void RunDiveStages()
        {
            //Arrange
            GivenDiveParameters();
            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            var results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);

            //Assert
            AssertResultsAreCorrect(results);
            Assert.Equal(compartmentLoad, results.DiveProfileModel.CompartmentLoad);
        }

        [Fact]
        public void RunDiveStagesMoreThanOnce()
        {
            //Arrange
            GivenDiveParameters();
            IDiveStageRunner diveStageRunner = new DiveStageRunner();

            //Act
            var results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            Assert.Equal(compartmentLoad, results.DiveProfileModel.CompartmentLoad);
            results = diveStageRunner.RunDiveStages(diveModel, diveStepModel, selectedCylinder);
            Assert.NotEqual(compartmentLoad, results.DiveProfileModel.CompartmentLoad);

            //Assert
            AssertResultsAreCorrect(results);
        }

        private void GivenDiveParameters()
        {
            diveModel = new Zhl16BuhlmannModel();
            diveStepModel = new DiveStepViewModel()
            {
                Depth = 50,
                Time = 10
            };
            selectedCylinder = new CylinderSetupViewModel()
            {
                CylinderName = "Air",
                CylinderPressure = 200,
                CylinderVolume = 12,
                GasMixture = new GasMixtureViewModel()
                {
                    Oxygen = 21,
                    Helium = 0
                },
                GasUsage = new GasUsageViewModel()
                {
                    GasRemaining = 1680,
                    GasUsed = 720,
                    InitialPressurisedCylinderVolume = 2400,
                    SurfaceAirConsumptionRate = 12
                }
            };
        }

        private void AssertResultsAreCorrect(IResultModel results)
        {
            Assert.NotNull(results);
            Assert.NotNull(results.DiveStepModel);
            Assert.Equal(50, results.DiveStepModel.Depth);
            Assert.Equal(10, results.DiveStepModel.Time);
            Assert.NotNull(results.CylinderSetupModel);
            Assert.Equal("Air", results.CylinderSetupModel.CylinderName);
            Assert.Equal(200, results.CylinderSetupModel.CylinderPressure);
            Assert.Equal(12, results.CylinderSetupModel.CylinderVolume);
            Assert.NotNull(results.CylinderSetupModel.GasMixture);
            Assert.Equal(0, results.CylinderSetupModel.GasMixture.Helium);
            Assert.Equal(79, results.CylinderSetupModel.GasMixture.Nitrogen);
            Assert.Equal(21, results.CylinderSetupModel.GasMixture.Oxygen);
            Assert.NotNull(results.CylinderSetupModel.GasUsage);
            Assert.Equal(1680, results.CylinderSetupModel.GasUsage.GasRemaining);
            Assert.Equal(720, results.CylinderSetupModel.GasUsage.GasUsed);
            Assert.Equal(2400, results.CylinderSetupModel.GasUsage.InitialPressurisedCylinderVolume);
            Assert.Equal(12, results.CylinderSetupModel.GasUsage.SurfaceAirConsumptionRate);
        }
    }
}