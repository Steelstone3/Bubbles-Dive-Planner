using System.Collections.Generic;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveModels.Selector;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
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
            Assert.NotNull(_mainWindowViewModel.DecompressionProfile);
            Assert.NotNull(_mainWindowViewModel.HeaderModel);
            Assert.NotNull(_mainWindowViewModel.DiveModelSelector);
            Assert.NotNull(_mainWindowViewModel.DiveStep);
            Assert.NotNull(_mainWindowViewModel.CylinderSelector);
            Assert.NotNull(_mainWindowViewModel.CentralNervousSystemToxicity);
            Assert.NotNull(_mainWindowViewModel.ResultsOverviewModel);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<IDiveStepModel> diveStepModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            Mock<IResultsOverviewModel> resultsOverviewModelDummy = new();
            Mock<IDecompressionProfileModel> decompressionProfileModelDummy = new();
            var viewModelEvents = new List<string>();
            _mainWindowViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _mainWindowViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _mainWindowViewModel.DiveStep = diveStepModelDummy.Object;
            _mainWindowViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;
            _mainWindowViewModel.DecompressionProfile = decompressionProfileModelDummy.Object;
            _mainWindowViewModel.ResultsOverviewModel = resultsOverviewModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_mainWindowViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.CylinderSelector), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.DecompressionProfile), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.ResultsOverviewModel), viewModelEvents);
        }
    }
}