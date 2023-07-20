using System.Collections.Specialized;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result;
using BubblesDivePlanner.ViewModels.Planner.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Planner.Plan.Result
{
    public class ResultsShould
    {
        private readonly IResults results = new Results();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.Null(results.Result);
            Assert.Empty(results.HistoricResults);
        }

        [Fact]
        public void DeriveFrom()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(results);
            Assert.IsAssignableFrom<IResults>(results);
        }

         [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Results resultsVM = (Results)results;
            Mock<IDiveStage> diveStage = new();
            List<string> viewModelEvents = new();
            resultsVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            resultsVM.Result = diveStage.Object;

            //Assert
            Assert.Contains(nameof(resultsVM.Result), viewModelEvents);
        }

        [Fact]
        public void RaiseCollectionChanged()
        {
            // Given
            Results resultsVM = (Results)results;
            Mock<IDiveStage> diveStage = new();
            List<NotifyCollectionChangedAction> viewModelEvents = new();
            resultsVM.HistoricResults.CollectionChanged += (sender, e) => viewModelEvents.Add(e.Action);

            // When
            resultsVM.HistoricResults.Add(diveStage.Object);

            // Then
            Assert.Contains(NotifyCollectionChangedAction.Add, viewModelEvents);
            Assert.Contains(diveStage.Object, resultsVM.HistoricResults);
        }
    }
}