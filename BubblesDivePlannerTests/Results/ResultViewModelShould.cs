using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;
using System.Collections.Generic;

namespace BubblesDivePlannerTests.Results
{
    public class ResultViewModelShould
    {
        private ResultViewModel _resultViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            Mock<IDiveStepModel> diveStepModelDummy = new();
            Mock<IDiveProfileModel> diveProfileModelDummy = new();

            //Act
           _resultViewModel.DiveProfileModel = diveProfileModelDummy.Object;
           _resultViewModel.DiveStepModel = diveStepModelDummy.Object;

            //Assert
            Assert.Equal(diveStepModelDummy.Object, _resultViewModel.DiveStepModel);
            Assert.Equal(diveProfileModelDummy.Object, _resultViewModel.DiveProfileModel);
        }
        
        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _resultViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);
        
            //Act
            _resultViewModel.DiveStepModel = new DiveStepViewModel();
            _resultViewModel.DiveProfileModel = new DiveProfileModel();
        
            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveStepModel), viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveProfileModel), viewModelEvents);
        }
    }
}