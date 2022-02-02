using System.Collections.Generic;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveModels.Selector;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveModels.Selector
{
    public class DiveModelSelectorViewModelShould
    {
        private DiveModelSelectorViewModel _diveModelSelectorViewModel = new();
        private Mock<IDiveModel> _diveModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _diveModelSelectorViewModel.SelectedDiveModel = _diveModelDummy.Object;

            //Assert
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel);
            Assert.NotEmpty(_diveModelSelectorViewModel.DiveModels);
            Assert.True(_diveModelSelectorViewModel.IsVisible);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _diveModelSelectorViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveModelSelectorViewModel.SelectedDiveModel = _diveModelDummy.Object;
            _diveModelSelectorViewModel.IsVisible = false;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_diveModelSelectorViewModel.SelectedDiveModel), viewModelEvents);
            Assert.Contains(nameof(_diveModelSelectorViewModel.IsVisible), viewModelEvents);
        }
    }
}