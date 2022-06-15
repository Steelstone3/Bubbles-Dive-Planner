using System.Collections.Generic;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.DivePlanner;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationEntry
{
    public class MainWindowViewModelShould
    {
        private readonly MainWindowViewModel _mainWindowViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Assert
            Assert.NotNull(_mainWindowViewModel.DiveInformation);
            Assert.NotNull(_mainWindowViewModel.HeaderModel);
            Assert.NotNull(_mainWindowViewModel.DivePlanner);
            Assert.NotNull(_mainWindowViewModel.DiveInformation);
            Assert.NotNull(_mainWindowViewModel.ResultsOverview);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DivePlannerViewModel divePlannerModelDummy = new();
            Mock<IResultsOverviewModel> resultsOverviewModelDummy = new();
            Mock<IDiveInformationModel> diveInformationModelDummy = new();
            var viewModelEvents = new List<string>();
            _mainWindowViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _mainWindowViewModel.DivePlanner = divePlannerModelDummy;
            _mainWindowViewModel.DiveInformation = diveInformationModelDummy.Object;
            _mainWindowViewModel.ResultsOverview = resultsOverviewModelDummy.Object;

            //Assert
            Assert.Contains(nameof(_mainWindowViewModel.DivePlanner), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.DiveInformation), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.ResultsOverview), viewModelEvents);
        }
    }
}