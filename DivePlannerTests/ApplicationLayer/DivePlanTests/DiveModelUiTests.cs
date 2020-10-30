using DivePlannerMk3.Contracts;
using DivePlannerMk3.Controllers;
using DivePlannerMk3.ViewModels;
using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class DiveModelUiTests
    {
        //Dive model selection + no selection = no execute
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
            }
        }

        [Fact(Skip="Test needs work and possibly logic implementing")]
        public void CanNotChangeDiveModelOnceDivePlanStartedTest()
        {
            //Arrange
            DiveModelSetup();

            //Act
            for (int i = 0; i < _diveModelSelectorViewModel.DiveModels.Count; i++)
            {
                _diveModelSelectorViewModel.SelectedDiveModel = _diveModelSelectorViewModel.DiveModels[i];
                //run a calculate
                
                //check disabled bool for model selector

                //Assert
                //Assuming the first dive model index does not change
                Assert.Equal( _diveModelSelectorViewModel.DiveModels[0], _diveModelSelectorViewModel.SelectedDiveModel);
            }
        }

        [Fact(Skip = "Test needs implementing")]
        public void DiveModelNotSelectedCanNotExcuteDiveStepTest()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}