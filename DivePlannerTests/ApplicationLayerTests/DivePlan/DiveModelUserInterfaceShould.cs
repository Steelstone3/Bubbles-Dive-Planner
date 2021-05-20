using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using Moq;
using Xunit;

namespace DivePlannerTests
{
    public class DiveModelUserInterfaceShould
    {
        private IDiveProfileService _diveProfileController = new DiveProfileService();
        private DiveModelSelectorViewModel _diveModelSelectorViewModel;
        private MainWindowViewModel _mainWindowViewModel = new MainWindowViewModel();

        private void DiveModelSetup()
        {
            _diveModelSelectorViewModel = new DiveModelSelectorViewModel(_diveProfileController);
        }

        [Fact]
        public void AllowSelectionOfTheCurrentDiveModel()
        {
            //Arrange
            DiveModelSetup();

            //Act
            for (int i = 0; i < _diveModelSelectorViewModel.DiveModels.Count; i++)
            {
                _diveModelSelectorViewModel.SelectedDiveModel = _diveModelSelectorViewModel.DiveModels[i];

                //Assert
                Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel);
                Assert.NotNull(_diveProfileController.TheDiveModel);
            }
        }

        [Fact]
        public void HasADefaultUiState()
        {
            //Arrange
            DiveModelSetup();

            //Assert
            Assert.True(_diveModelSelectorViewModel.IsUiEnabled);
            Assert.True(_diveModelSelectorViewModel.IsUiVisible);
            Assert.False(_diveModelSelectorViewModel.IsReadOnlyUiVisible);
        }

        [Fact(Skip="This test is not implemented correctly")]
        public void LockTheCurrentDiveModelWhenTheFirstDiveProfileIsRun()
        {
            //A
            var diveProfileService = new DiveProfileService()
            {
                TheDiveModel = new Zhl16Buhlmann(),
            };

            var divePlan = new DivePlanViewModel(diveProfileService)
            {
                DiveStep = new DiveStepViewModel()
                {
                    Depth = 50,
                    Time = 10,
                },

                GasMixture = new GasMixtureSelectorViewModel(),

                GasManagement = new GasManagementViewModel()
                {
                    CylinderVolume = 12,
                    CylinderPressure = 200,
                    SacRate = 12,
                },
            };
            
            var diveResultsMock = new Mock<DiveResultsViewModel>();

            //A
            divePlan.CalculateDiveStep(diveResultsMock.Object);

            //Assert
            Assert.False(divePlan.DiveModelSelector.IsUiEnabled);
            Assert.False(divePlan.DiveModelSelector.IsUiVisible);
            Assert.True(_diveModelSelectorViewModel.IsReadOnlyUiVisible);
        }

        [Fact(Skip="This test is not implemented correctly")]
        public void UnlockTheCurrentDiveModelSelectionWhenANewDiveIsStarted()
        {
            //Arrange
            DiveModelSetup();
            var diveModel = _diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();
            //_diveModelSelectorViewModel.IsUiEnabled = false;
            //_diveModelSelectorViewModel.IsUiVisible = false;

            //Act
            _mainWindowViewModel.DiveHeader.File.NewCommand.Execute();

            //Assert
            Assert.True(_diveModelSelectorViewModel.IsUiEnabled);
            Assert.True(_diveModelSelectorViewModel.IsUiVisible);
            Assert.False(_diveModelSelectorViewModel.IsReadOnlyUiVisible);
        }

        [Fact]
        public void ValidateInvalidDiveModelSelectorParameters()
        {
            DiveModelSetup();

            Assert.False(_diveModelSelectorViewModel.ValidateSelectedDiveModel(null));
        }

        [Fact]
        public void ValidateValidDiveModelSelectorParameters()
        {
            DiveModelSetup();

            var diveModel = _diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();

            Assert.True(_diveModelSelectorViewModel.ValidateSelectedDiveModel(diveModel));
        }
    }
}