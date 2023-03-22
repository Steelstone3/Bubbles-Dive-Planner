using System.Collections.Generic;
using BubblesDivePlanner.ViewModels.DivePlans;
using BubblesDivePlanner.ViewModels.Models;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlans
{
    public class DiveModelSelectorViewModelShould
    {
        private readonly DiveModelSelectorViewModel _diveModelSelectorViewModel = new();
        private readonly Mock<IDiveModel> _diveModelDummy = new();

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

        [Fact]
        public void ValidateModelAtTheBoundsInvalid()
        {
            //Act
            var isValid = _diveModelSelectorViewModel.ValidateSelectedDiveModel(null);

            //Assert
            Assert.False(isValid);
        }

        [Fact]
        public void ValidateModelAtTheBoundsValid()
        {
            //Act
            var isValid = _diveModelSelectorViewModel.ValidateSelectedDiveModel(_diveModelDummy.Object);

            //Assert
            Assert.True(isValid);
        }
    }
}