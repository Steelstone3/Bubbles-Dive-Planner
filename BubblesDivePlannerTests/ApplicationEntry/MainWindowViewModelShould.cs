using System.Collections.Generic;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.GasManagement;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationEntry
{
    public class MainWindowViewModelShould
    {
        private MainWindowViewModel _mainWindowViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Assert
            Assert.NotNull(_mainWindowViewModel.DiveStep);
            Assert.NotNull(_mainWindowViewModel.GasManagement);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveStepModel> diveStepModelDummy = new();
            Mock<IGasManagementModel> gasManagementModelDummy = new();
            var viewModelEvents = new List<string>();
            _mainWindowViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _mainWindowViewModel.DiveStep = diveStepModelDummy.Object;
            _mainWindowViewModel.GasManagement = gasManagementModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_mainWindowViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.GasManagement), viewModelEvents);
        }
    }
}