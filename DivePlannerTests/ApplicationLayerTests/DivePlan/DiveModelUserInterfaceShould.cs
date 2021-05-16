using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DivePlan;
using Moq;
using Xunit;

namespace DivePlannerTests
{
    public class DiveModelUserInterfaceShould
    {
        private IDiveProfileService _diveProfileController = new DiveProfileService();
        private PlanDiveModelSelectorViewModel _diveModelSelectorViewModel;

        private void DiveModelSetup()
        {
            _diveModelSelectorViewModel = new PlanDiveModelSelectorViewModel(_diveProfileController);
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

        [Fact(Skip = "Needs implementation")]
        public void LockTheCurrentDiveModelWhenTheFirstDiveProfileIsRun()
        {
            //Arrange

            //Act

            //Assert
        }

        [Fact(Skip = "Needs implementation")]
        public void UnlockTheCurrentDiveModelSelectionWhenANewDiveIsStarted()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}