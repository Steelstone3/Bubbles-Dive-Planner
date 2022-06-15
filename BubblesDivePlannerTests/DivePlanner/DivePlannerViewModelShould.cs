using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveInformation
{
    public class DivePlannerViewModelShould
    {
        private readonly DivePlannerViewModel _divePlannerViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _divePlannerViewModel.DiveModelSelector.SelectedDiveModel = DivePlannerApplicationTestFixture.GetDiveModel;
            _divePlannerViewModel.CylinderSelector.SelectedCylinder = DivePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotNull(_divePlannerViewModel.DiveModelSelector);
            Assert.NotNull(_divePlannerViewModel.CylinderSelector);
            Assert.NotNull(_divePlannerViewModel.DiveModel);
            Assert.NotNull(_divePlannerViewModel.DiveStep);
            Assert.NotNull(_divePlannerViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            var viewModelEvents = new List<string>();
            _divePlannerViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _divePlannerViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _divePlannerViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_divePlannerViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlannerViewModel.CylinderSelector), viewModelEvents);
        }
    }
}