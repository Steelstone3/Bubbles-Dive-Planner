using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Results
{
    public class ResultsHistoryViewModelShould
    {
        private ResultsOverviewViewModel _resultsHistoryViewModel = new();
        private Mock<IResultModel> _resultModelDummy = new();

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
        public void AddLatestResultToResultsHistory() {
            //Act
            _resultsHistoryViewModel.LatestResult = _resultModelDummy.Object;

            //Assert
            Assert.NotEmpty(_resultsHistoryViewModel.Results);
        }
    }
}