using System.Collections.Generic;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class DiveCalculationParametersViewModelShould
    {
        private readonly DiveCalculationParametersViewModel _diveCalculationParametersViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _diveCalculationParametersViewModel.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            _diveCalculationParametersViewModel.CylinderSelector.SelectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotNull(_diveCalculationParametersViewModel.DiveModelSelector);
            Assert.NotNull(_diveCalculationParametersViewModel.CylinderSelector);
            Assert.NotNull(_diveCalculationParametersViewModel.DiveModel);
            Assert.NotNull(_diveCalculationParametersViewModel.DiveStep);
            Assert.NotNull(_diveCalculationParametersViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            Mock<IDiveStepModel> diveStepDummy = new();
            var viewModelEvents = new List<string>();
            _diveCalculationParametersViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveCalculationParametersViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _diveCalculationParametersViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;
            _diveCalculationParametersViewModel.DiveStep = diveStepDummy.Object;

            //Assert
            Assert.Contains(nameof(_diveCalculationParametersViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_diveCalculationParametersViewModel.CylinderSelector), viewModelEvents);
            Assert.Contains(nameof(_diveCalculationParametersViewModel.DiveStep), viewModelEvents);
        }
    }
}