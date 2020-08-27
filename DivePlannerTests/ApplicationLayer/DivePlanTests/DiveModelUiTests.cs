using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class DiveModelUiTests
    {
        //TODO Dive model selection + no selection = no execute
        private IDiveProfileService _diveProfileController = new DiveProfileService();
        private PlanDiveModelSelectorViewModel _diveModelSelectorViewModel;

        private void DiveModelSetup()
        {
            _diveModelSelectorViewModel = new PlanDiveModelSelectorViewModel(_diveProfileController);
        }

        [Fact]
        public void DiveModelCanBeSelectedTest()
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

        [Fact(Skip = "Test needs implementing and a second dive model")]
        public void CanNotChangeDiveModelOnceDivePlanStartedTest()
        {
            //Arrange

            //Act

            //Assert
        }

        //TODO Setup cannot execute in the view model command and test that
        [Fact(Skip = "Test needs implementing - is can execute dive step false when selected dive model or gas mixture is null")]
        public void DiveModelNotSelectedCanNotExcuteDiveStepTest()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}