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

            //Assert
            Assert.NotEmpty(_resultsHistoryViewModel.Results);
            Assert.NotNull(_resultsHistoryViewModel.LatestResult);
        }

        [Fact]
        public void GetLastResult() {
            //Arrange
            Mock<IResultModel> secondResultModelDummy = new();
            _resultsHistoryViewModel.Results.Add(_resultModelDummy.Object);
            _resultsHistoryViewModel.Results.Add(secondResultModelDummy.Object);

            //Assert
            Assert.NotNull(_resultsHistoryViewModel.LatestResult);
            Assert.Same(secondResultModelDummy.Object, _resultsHistoryViewModel.LatestResult);
        }
    }
}