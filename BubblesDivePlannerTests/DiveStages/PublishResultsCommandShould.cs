using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasMixture;
using BubblesDivePlanner.Cylinders.CylinderSetup.GasUsage;
using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStages;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Xunit;

namespace BubblesDivePlannerTests.DiveStages
{
    public class PublishResultsCommandShould
    {
        private IDiveProfileModel _diveProfile = new DiveProfileViewModel();
        private ICylinderSetupModel _selectedCylinder = new CylinderSetupViewModel();
        private IDiveStepModel _diveStep = new DiveStepViewModel();
        private IResultModel _resultViewModel = new ResultViewModel();

        [Fact]
        public void PopulateDiveResultsModelOutputStage()
        {
            //Arrange
          GivenDiveParameters();

            var diveStage = new PublishResultsCommand(_diveProfile, _diveStep, _selectedCylinder, _resultViewModel);

            //Act
            diveStage.RunDiveStage();

            //Assert
            Assert.Equal(_diveStep.Depth, _resultViewModel.DiveStepModel.Depth);
            Assert.Equal(_diveProfile.MaxSurfacePressures, _resultViewModel.DiveProfileModel.MaxSurfacePressures);
            Assert.Equal(_selectedCylinder.CylinderName, _resultViewModel.CylinderSetupModel.CylinderName);
        }

       private void GivenDiveParameters()
        {
            
            _diveStep = new DiveStepViewModel()
            {
                Depth = 50,
                Time = 10
            };
            _selectedCylinder = new CylinderSetupViewModel()
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
    }
}