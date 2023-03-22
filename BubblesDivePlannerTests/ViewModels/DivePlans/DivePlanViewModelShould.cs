using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DivePlans;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlans
{
    public class DivePlanViewModelShould
    {
        private readonly DivePlanViewModel _divePlanViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _divePlanViewModel.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            _divePlanViewModel.CylinderSelector.SelectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotNull(_divePlanViewModel.DiveModelSelector);
            Assert.NotNull(_divePlanViewModel.CylinderSelector);
            Assert.NotNull(_divePlanViewModel.DiveModel);
            Assert.NotNull(_divePlanViewModel.DiveStep);
            Assert.NotNull(_divePlanViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            Mock<IDiveStepModel> diveStepDummy = new();
            var viewModelEvents = new List<string>();
            _divePlanViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _divePlanViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _divePlanViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;
            _divePlanViewModel.DiveStep = diveStepDummy.Object;

            //Assert
            Assert.Contains(nameof(_divePlanViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlanViewModel.CylinderSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlanViewModel.DiveStep), viewModelEvents);
        }
    }
}