using System.Reactive.Linq;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class CalculateDiveStepUserInterfaceShould
    {
        private MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel();
        private PlanDiveModelSelectorViewModel _diveModelSelectorViewModel;
        private DiveStepViewModel _diveStepViewModel = new DiveStepViewModel();
        private GasManagementViewModel _gasManagementViewModel = new GasManagementViewModel();
        private GasMixtureSelectorViewModel _gasMixtureViewModel = new GasMixtureSelectorViewModel();

        [Fact]
        public async void NotAllowDivesProfilesToBeRunWithoutASelectedDiveModel()
        {
            //A
            EverythingIsOk();
            SetupDiveModelSelector();

            //A
            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

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

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

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

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

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

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

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

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

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

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Theory]
        [InlineData(4, false)]
        [InlineData(31, false)]
        [InlineData(5, true)]
        [InlineData(30, true)]
        public async void NotAllowDiveStepExecutionIfSurfaceAirConsumptionRateIsOutOfRange(int surfaceAirComsumptionRate, bool expectedResult)
        {
            EverythingIsOk();
            _gasManagementViewModel.CylinderPressure = 200;
            _gasManagementViewModel.CylinderVolume = 12;
            _gasManagementViewModel.SacRate = surfaceAirComsumptionRate;

            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

            Assert.Equal(expectedResult, canExecute);
        }

        [Fact]
        public async void NotAllowDiveStepExecutionIfNoGasMixtureIsSelected()
        {
            //A
            EverythingIsOk();
            _gasMixtureViewModel.SelectedGasMixture = null;

            //A
            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.False(canExecute);
        }

        [Fact]
        public async void AllowDiveStepExecutionWhenAllConditionsMet()
        {
            //A
            EverythingIsOk();

            //A
            var canExecute = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.True(canExecute);
        }

        [Fact]
        public async void AllowDiveStepExecutionWhenAllConditionsMetHavingPreviouslyNotBeenMet()
        {
            //AA
            EverythingIsNotOk();
            var canExecuteNotOk = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();
            
            //AA
            EverythingIsOk();
            var canExecuteOk = await _mainWindowViewModel.CanExecuteDiveStep.FirstAsync();

            //A
            Assert.False(canExecuteNotOk);
            Assert.True(canExecuteOk);
        }

        private void SetupDiveModelSelector()
        {
            var diveProfileService = new DiveProfileService();
            _diveModelSelectorViewModel = new PlanDiveModelSelectorViewModel(diveProfileService);
            _mainWindowViewModel.DivePlan.DiveModelSelector = _diveModelSelectorViewModel;
        }

        private void EverythingIsOk()
        {
            SetupDiveModelSelector();
            _diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();

            _gasMixtureViewModel.SelectedGasMixture = new GasMixtureViewModel()
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

            _gasMixtureViewModel.SelectedGasMixture = new GasMixtureViewModel()
            {
                GasName = "Air",
                Oxygen = 21,
                Helium = 0,
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

            SetupMainViewModel();
        }

        private void SetupMainViewModel()
        {
            _mainWindowViewModel.DivePlan.DiveModelSelector = _diveModelSelectorViewModel;
            _mainWindowViewModel.DivePlan.GasMixture = _gasMixtureViewModel;
            _mainWindowViewModel.DivePlan.GasManagement = _gasManagementViewModel;
            _mainWindowViewModel.DivePlan.DiveStep = _diveStepViewModel;
        }
    }
}