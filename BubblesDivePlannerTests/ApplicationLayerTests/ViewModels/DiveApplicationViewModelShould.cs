using System.Collections.Generic;
using System.Reactive.Linq;
using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
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

        //TODO AH Refactor mock up dependencies and trim down this class by using a helper class
        //TODO AH do this one last due to the size of it!
        //TODO AH Migrate these tests to DiveApplicationViewModelShould
        //TODO AH check visibility and enabled cover in tests
        
      

        [Fact]
        public async void NotAllowDivesProfilesToBeRunWithoutASelectedDiveModel()
        {
            //A
            EverythingIsOk();
            SetupDiveModelSelector();

            //A
            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.False(canExecute);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(101, false)]
        [InlineData(0, true)]
        [InlineData(55, true)]
        public async void NotAllowDiveStepExecutionIfADepthIsOutOfRange(int depth, bool expectedResult)
        {
            EverythingIsOk();
            _diveStepViewModel.Depth = depth;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(0, false)]
        [InlineData(101, false)]
        [InlineData(1, true)]
        [InlineData(100, true)]
        public async void NotAllowDiveStepExecutionIfTimeIsOutOfRange(int time, bool expectedResult)
        {
            EverythingIsOk();
            _diveStepViewModel.Time = time;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(57, false)]
        [InlineData(70, false)]
        [InlineData(10, true)]
        [InlineData(55, true)]
        public async void NotAllowDiveStepExecutionIfADepthIsGreaterThanMaximumOperatingDepth(int depth, bool expectedResult)
        {
            EverythingIsOk();
            _diveStepViewModel.Depth = depth;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(2, false)]
        [InlineData(31, false)]
        [InlineData(3, true)]
        [InlineData(30, true)]
        public async void NotAllowDiveStepExecutionIfCylinderVolumeIsOutOfRange(int cylinderVolume, bool expectedResult)
        {
            EverythingIsOk();
            _gasManagementViewModel.CylinderPressure = 200;
            _gasManagementViewModel.CylinderVolume = cylinderVolume;
            _gasManagementViewModel.SacRate = 12;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(49, false)]
        [InlineData(301, false)]
        [InlineData(300, true)]
        [InlineData(50, true)]
        public async void NotAllowDiveStepExecutionIfCylinderPressureIsOutOfRange(int cylinderPressure, bool expectedResult)
        {
            EverythingIsOk();
            _gasManagementViewModel.CylinderPressure = cylinderPressure;
            _gasManagementViewModel.CylinderVolume = 12;
            _gasManagementViewModel.SacRate = 12;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(31, false)]
        [InlineData(5, true)]
        [InlineData(30, true)]
        public async void NotAllowDiveStepExecutionIfSurfaceAirConsumptionRateIsOutOfRange(int surfaceAirConsumptionRate, bool expectedResult)
        {
            EverythingIsOk();
            _gasManagementViewModel.CylinderPressure = 200;
            _gasManagementViewModel.CylinderVolume = 12;
            _gasManagementViewModel.SacRate = surfaceAirConsumptionRate;

            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Fact]
        public async void NotAllowDiveStepExecutionIfNoGasMixtureIsSelected()
        {
            //A
            EverythingIsOk();
            _gasSelectorMixtureViewModel.SelectedGasMixture = null;

            //A
            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.False(canExecute);
        }

        [Fact]
        public async void AllowDiveStepExecutionWhenAllConditionsMet()
        {
            //A
            EverythingIsOk();

            //A
            var canExecute = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.True(canExecute);
        }

        [Fact]
        public async void AllowDiveStepExecutionWhenAllConditionsMetHavingPreviouslyNotBeenMet()
        {
            //AA
            EverythingIsNotOk();
            var canExecuteNotOk = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //AA
            EverythingIsOk();
            var canExecuteOk = await _diveApplicationViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.False(canExecuteNotOk);
            Assert.True(canExecuteOk);
        }

        private void SetupDiveModelSelector()
        {
            _diveModelSelectorViewModel = new DiveModelSelectorViewModel(_diveProfileService);
            _diveApplicationViewModel.DivePlanSetup = _divePlanSetup;
            _diveApplicationViewModel.DivePlanSetup.DiveModelSelector = _diveModelSelectorViewModel;
        }

        private void EverythingIsOk()
        {
            SetupDiveModelSelector();
            _diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();

            _gasSelectorMixtureViewModel.SelectedGasMixture = new GasMixtureModel()
            {
                GasName = "Air",
                Oxygen = 21,
                Helium = 0,
            };

            _gasManagementViewModel = new GasManagementViewModel()
            {
                SacRate = 12,
                CylinderPressure = 200,
                CylinderVolume = 12,
            };

            _diveStepViewModel = new DiveStepViewModel()
            {
                Depth = 10,
                Time = 15,
            };

            SetupMainViewModel();
        }

        private void EverythingIsNotOk()
        {
            SetupDiveModelSelector();
            _diveModelSelectorViewModel.SelectedDiveModel = null;

            _gasSelectorMixtureViewModel.SelectedGasMixture = new GasMixtureModel()
            {
                GasName = "Air",
                Oxygen = 21,
                Helium = 0,
                Nitrogen = 100 - 21,
            };

            _gasManagementViewModel = new GasManagementViewModel()
            {
                SacRate = 0,
                CylinderPressure = 0,
                CylinderVolume = 0,
            };

            _diveStepViewModel = new DiveStepViewModel()
            {
                Depth = 0,
                Time = 0,
            };

            _divePlanSetup = new DivePlanSetupViewModel(new Mock<IDiveProfileService>().Object)
            {
                DiveStep = _diveStepViewModel,
                GasManagement = _gasManagementViewModel,
                GasMixture = _gasSelectorMixtureViewModel,
            };

            _diveApplicationViewModel = new DiveApplicationViewModel(new Mock<IDiveProfileService>().Object)
            {
                DivePlanSetup = _divePlanSetup,
            };

            SetupMainViewModel();
        }

        private void SetupMainViewModel()
        {
            _diveApplicationViewModel.DivePlanSetup.DiveModelSelector = _diveModelSelectorViewModel;
            _diveApplicationViewModel.DivePlanSetup.GasMixture = _gasSelectorMixtureViewModel;
            _diveApplicationViewModel.DivePlanSetup.GasManagement = _gasManagementViewModel;
            _diveApplicationViewModel.DivePlanSetup.DiveStep = _diveStepViewModel;
        }
       
    }
}