using System.Collections.Generic;
using System.Reactive.Linq;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Contracts.ViewModels.Results;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.Plan;
using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels.DiveApplication;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels
{
    public class DiveApplicationViewModelShould
    {
        private DiveApplicationViewModel _diveApplicationViewModel;

        private DiveProfileService _diveProfileService = new();
        private DivePlanSetupViewModel _divePlanSetup;
        private DiveModelSelectorViewModel _diveModelSelectorViewModel;

        //TODO AH may be able to mock these...
        private DiveStepViewModel _diveStepViewModel = new();
        private GasManagementViewModel _gasManagementViewModel = new();
        private GasMixtureSelectorViewModel _gasSelectorMixtureViewModel = new();

        public DiveApplicationViewModelShould()
        {
            _diveApplicationViewModel = new(_diveProfileService);
            _divePlanSetup = new DivePlanSetupViewModel(_diveProfileService);
            _diveModelSelectorViewModel = new DiveModelSelectorViewModel(_diveProfileService);
        }

        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            var viewModelEvents = new List<string>();
            _diveApplicationViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveApplicationViewModel.DivePlanSetup = _divePlanSetup;
            _diveApplicationViewModel.DiveInformation = new Mock<IDiveInformationViewModel>().Object;
            _diveApplicationViewModel.DiveResults = new Mock<IDiveResultsViewModel>().Object;

            //Assert
            Assert.Contains(nameof(_diveApplicationViewModel.DivePlanSetup), viewModelEvents);
            Assert.Contains(nameof(_diveApplicationViewModel.DiveInformation), viewModelEvents);
            Assert.Contains(nameof(_diveApplicationViewModel.DiveResults), viewModelEvents);
        }

        [Theory]
        //All ok
        [InlineData(true, 50, 10, true, 55, 12, 200, 12, true)]
        //No dive model
        [InlineData(false, 50, 10, true, 55, 12, 200, 12, false)]
        //Out of bounds depth
        [InlineData(true, 101, 10, true, 500, 12, 200, 12, false)]
        [InlineData(true, -1, 10, true, 55, 12, 200, 12, false)]
        [InlineData(true, 0, 10, true, 55, 12, 200, 12, true)]
        [InlineData(true, 55, 10, true, 55, 12, 200, 12, true)]
        //Depth deeper than max operating depth
        [InlineData(true, 56, 10, true, 55, 12, 200, 12, false)]
        //Out of bounds time
        [InlineData(true, 50, 0, true, 55, 12, 200, 12, false)]
        [InlineData(true, 50, 101, true, 55, 12, 200, 12, false)]
        [InlineData(true, 50, 1, true, 55, 12, 200, 12, true)]
        [InlineData(true, 50, 100, true, 55, 12, 200, 12, true)]
        //No selected gas mixture
        [InlineData(true, 50, 10, false, 55, 12, 200, 12, false)]
        //Out of bounds cylinder volume
        [InlineData(true, 50, 10, true, 55, 2, 200, 12, false)]
        [InlineData(true, 50, 10, true, 55, 31, 200, 12, false)]
        [InlineData(true, 50, 10, true, 55, 3, 200, 12, true)]
        [InlineData(true, 50, 10, true, 55, 30, 200, 12, true)]
        //Out of bounds cylinder pressure
        [InlineData(true, 50, 10, true, 55, 12, 49, 12, false)]
        [InlineData(true, 50, 10, true, 55, 12, 301, 12, false)]
        [InlineData(true, 50, 10, true, 55, 12, 50, 12, true)]
        [InlineData(true, 50, 10, true, 55, 12, 300, 12, true)]
        //Out of bounds sac rate
        [InlineData(true, 50, 10, true, 55, 12, 200, 4, false)]
        [InlineData(true, 50, 10, true, 55, 12, 200, 31, false)]
        [InlineData(true, 50, 10, true, 55, 12, 200, 5, true)]
        [InlineData(true, 50, 10, true, 55, 12, 200, 30, true)]
        public async void CanExecuteCalculateDiveStep(bool useDiveModel, int depth, int time,
            bool useSelectedGasMixture, int maxOperatingDepth, int cylinderVolume, int cylinderPressure, int sacRate,
            bool expectedResult)
        {
            //Arrange
            SetupDiveModelSelector(useDiveModel);
            SetupDiveStep(depth, time);
            SetupGasMixtureSelector(useSelectedGasMixture, maxOperatingDepth);
            SetupGasManagement(sacRate, cylinderPressure, cylinderVolume);

            //Act
            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //Assert
            Assert.Equal(expectedResult, canExecute);
        }

        private void SetupDiveModelSelector(bool useDiveModel = true)
        {
            if (useDiveModel)
            {
                _diveModelSelectorViewModel = new DiveModelSelectorViewModel(_diveProfileService)
                {
                    SelectedDiveModel = new Zhl16Buhlmann(),
                };
            }
            else
            {
                _diveModelSelectorViewModel = new DiveModelSelectorViewModel(_diveProfileService)
                {
                    SelectedDiveModel = null,
                };
            }

            SetupDivePlanSetup();
        }

        private void SetupDiveStep(int depth, int time)
        {
            _diveStepViewModel = new DiveStepViewModel()
            {
                Depth = depth,
                Time = time,
            };

            SetupDivePlanSetup();
        }

        private void SetupGasManagement(int sacRate, int cylinderPressure, int cylinderVolume)
        {
            _gasManagementViewModel = new GasManagementViewModel()
            {
                SacRate = sacRate,
                CylinderPressure = cylinderPressure,
                CylinderVolume = cylinderVolume
            };

            SetupDivePlanSetup();
        }

        private void SetupGasMixtureSelector(bool useGasMixture, int maxOperatingDepth)
        {
            _gasSelectorMixtureViewModel = new GasMixtureSelectorViewModel()
            {
                SelectedGasMixture = new GasMixtureModel()
                {
                    GasName = "Air",
                    Oxygen = 21,
                    Helium = 0,
                    Nitrogen = 100 - 0 - 21,
                },

                MaximumOperatingDepth = maxOperatingDepth,
            };

            if (!useGasMixture)
            {
                _gasSelectorMixtureViewModel.SelectedGasMixture = null;
            }

            SetupDivePlanSetup();
        }

        private void SetupDivePlanSetup()
        {
            _divePlanSetup.DiveModelSelector = _diveModelSelectorViewModel;
            _divePlanSetup.DiveStep = _diveStepViewModel;
            _divePlanSetup.GasMixture = _gasSelectorMixtureViewModel;
            _divePlanSetup.GasManagement = _gasManagementViewModel;

            SetupDiveApplication();
        }

        private void SetupDiveApplication()
        {
            _diveApplicationViewModel.DivePlanSetup = _divePlanSetup;
        }
    }
}