using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Results
{
    public class ResultsHistoryViewModelShould
    {
        private ResultsHistoryViewModel _resultsHistoryViewModel = new();
        private Mock<IResultModel> _resultModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _resultsHistoryViewModel.Results.Add(_resultModelDummy.Object);

            //Assert
            Assert.NotEmpty(_resultsHistoryViewModel.Results);
        }
    }
}