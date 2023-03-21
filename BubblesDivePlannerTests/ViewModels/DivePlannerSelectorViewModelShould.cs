using System.Collections.Generic;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlanner.ViewModels.Models;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class DivePlannerSelectorViewModelShould
    {
        private readonly DivePlannerSelectorViewModel _divePlannerSelectorViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _divePlannerSelectorViewModel.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            _divePlannerSelectorViewModel.CylinderSelector.SelectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotNull(_divePlannerSelectorViewModel.DiveModelSelector);
            Assert.NotNull(_divePlannerSelectorViewModel.CylinderSelector);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            var viewModelEvents = new List<string>();
            _divePlannerSelectorViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _divePlannerSelectorViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _divePlannerSelectorViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_divePlannerSelectorViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlannerSelectorViewModel.CylinderSelector), viewModelEvents);
        }
    }
}