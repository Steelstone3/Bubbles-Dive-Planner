using System.Collections.Generic;
using System.Collections.Specialized;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Results
{
    public class ResultsHistoryViewModelShould
    {
        private readonly ResultsOverviewViewModel _resultsHistoryViewModel = new();
        private readonly Mock<IResultModel> _resultModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _resultsHistoryViewModel.Results.Add(_resultModelDummy.Object);
            _resultsHistoryViewModel.LatestResult = _resultModelDummy.Object;

            //Assert
            Assert.NotEmpty(_resultsHistoryViewModel.Results);
            Assert.NotNull(_resultsHistoryViewModel.LatestResult);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _resultsHistoryViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _resultsHistoryViewModel.LatestResult = _resultModelDummy.Object;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_resultsHistoryViewModel.LatestResult), viewModelEvents);
        }

        [Fact]
        public void RaiseCollectionChanged()
        {
            //Arrange
            var viewModelEvents = new List<NotifyCollectionChangedAction>();
            _resultsHistoryViewModel.Results.CollectionChanged += (sender, e) => viewModelEvents.Add(e.Action);

            //Act
            _resultsHistoryViewModel.Results.Add(_resultModelDummy.Object);

            //Assert
            Assert.Contains(NotifyCollectionChangedAction.Add, viewModelEvents);
            Assert.Contains(_resultModelDummy.Object, _resultsHistoryViewModel.Results);
        }

        [Fact]
        public void AddLatestResultToResultsHistory()
        {
            //Act
            _resultsHistoryViewModel.LatestResult = _resultModelDummy.Object;

            //Assert
            Assert.NotEmpty(_resultsHistoryViewModel.Results);
        }
    }
}