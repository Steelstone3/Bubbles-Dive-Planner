using System.Collections.Generic;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
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
            Assert.NotNull(_mainWindowViewModel.DiveModelSelector);
            Assert.NotNull(_mainWindowViewModel.DiveStep);
            Assert.NotNull(_mainWindowViewModel.CylinderSelector);
            Assert.NotNull(_mainWindowViewModel.ResultModel);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDiveModelSelectorModel> diveModelSelectorModelDummy = new();
            Mock<IDiveStepModel> diveStepModelDummy = new();
            Mock<ICylinderSelectorModel> cylinderSelectorModelDummy = new();
            Mock<IResultModel> resultModelDummy = new();
            var viewModelEvents = new List<string>();
            _mainWindowViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _mainWindowViewModel.DiveModelSelector = diveModelSelectorModelDummy.Object;
            _mainWindowViewModel.DiveStep = diveStepModelDummy.Object;
            _mainWindowViewModel.CylinderSelector = cylinderSelectorModelDummy.Object;
            _mainWindowViewModel.ResultModel = resultModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_mainWindowViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.CylinderSelector), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.ResultModel), viewModelEvents);
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CalculateDiveStep()
        {
            //Arrange
            //Stubs of requirements

            //Act
            _mainWindowViewModel.CalculateDiveStepCommand.Execute();

            //Assert
            //TODO AH something results populated
        }

        [Fact(Skip = "Need to work out how to get this type of test working")]
        public void CanCalculateDiveStep()
        {
            //Arrange
            //Stubs of requirements

            //Act
            _mainWindowViewModel.CalculateDiveStepCommand.Execute();

            //Assert
            //TODO AH something results populated
        }
    }
}