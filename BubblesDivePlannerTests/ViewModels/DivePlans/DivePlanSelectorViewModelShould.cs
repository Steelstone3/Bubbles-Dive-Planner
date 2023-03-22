using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DivePlans;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlans
{
    public class DivePlanSelectorViewModelShould
    {
        private readonly DivePlanSelectorViewModel _divePlanSelectorViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _divePlanSelectorViewModel.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            _divePlanSelectorViewModel.CylinderSelector.SelectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotNull(_divePlanSelectorViewModel.DiveModelSelector);
            Assert.NotNull(_divePlanSelectorViewModel.CylinderSelector);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            var viewModelEvents = new List<string>();
            _divePlanSelectorViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _divePlanSelectorViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _divePlanSelectorViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_divePlanSelectorViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlanSelectorViewModel.CylinderSelector), viewModelEvents);
        }
    }
}