using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.DiveApplication;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.Result;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.DiveApplication.DivePlanSetup
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

        [Fact(Skip = "This test is not implemented correctly")]
        public void LockTheCurrentDiveModelWhenTheFirstDiveProfileIsRun()
        {
            //A
            var diveProfileService = new Mock<DiveProfileService>();

            var diveApplication = new DiveApplicationViewModel(diveProfileService.Object)
            {
                DivePlanSetup = new DivePlanSetupViewModel(diveProfileService.Object)
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
                }
            };

            var diveResultsMock = new Mock<DiveResultsViewModel>();

            //A
            diveApplication.CalculateDiveStepCommand.Execute();

            //Assert
            Assert.False(diveApplication.DivePlanSetup.DiveModelSelector.IsUiEnabled);
            Assert.False(diveApplication.DivePlanSetup.DiveModelSelector.IsUiVisible);
            Assert.True(_diveModelSelectorViewModel.IsReadOnlyUiVisible);
        }

        [Fact(Skip = "This test is not implemented correctly")]
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