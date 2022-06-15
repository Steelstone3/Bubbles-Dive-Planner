using BubblesDivePlanner.DiveModels.DiveProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlanner.Results;
using Moq;
using Xunit;
using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup;

namespace BubblesDivePlannerTests.Results
{
    public class ResultViewModelShould
    {
        private readonly ResultViewModel _resultViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            Mock<IDiveStepModel> diveStepModelDummy = new();
            Mock<ICylinderSetupModel> cylinderSetupModelDummy = new();
            Mock<IDiveProfileModel> diveProfileModelDummy = new();

            //Act
            _resultViewModel.DiveProfileModel = diveProfileModelDummy.Object;
            _resultViewModel.DiveStepModel = diveStepModelDummy.Object;
            _resultViewModel.CylinderSetupModel = cylinderSetupModelDummy.Object;

            //Assert
            Assert.Equal(diveStepModelDummy.Object, _resultViewModel.DiveStepModel);
            Assert.Equal(diveProfileModelDummy.Object, _resultViewModel.DiveProfileModel);
            Assert.Equal(cylinderSetupModelDummy.Object, _resultViewModel.CylinderSetupModel);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _resultViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _resultViewModel.DiveStepModel = new DiveStepViewModel();
            _resultViewModel.CylinderSetupModel = new CylinderSetupViewModel();
            _resultViewModel.DiveProfileModel = new DiveProfileViewModel(16);

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveStepModel), viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.CylinderSetupModel), viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveProfileModel), viewModelEvents);
        }
    }
}