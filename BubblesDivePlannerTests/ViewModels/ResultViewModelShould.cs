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
            _resultViewModel.DiveProfile = diveProfileModelDummy.Object;
            _resultViewModel.DiveStep = diveStepModelDummy.Object;
            _resultViewModel.SelectedCylinder = cylinderSetupModelDummy.Object;

            //Assert
            Assert.Equal(diveStepModelDummy.Object, _resultViewModel.DiveStep);
            Assert.Equal(diveProfileModelDummy.Object, _resultViewModel.DiveProfile);
            Assert.Equal(cylinderSetupModelDummy.Object, _resultViewModel.SelectedCylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _resultViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _resultViewModel.DiveStep = new DiveStepViewModel();
            _resultViewModel.SelectedCylinder = new CylinderSetupViewModel();
            _resultViewModel.DiveProfile = new DiveProfileViewModel(16);

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.SelectedCylinder), viewModelEvents);
            Assert.Contains(nameof(_resultViewModel.DiveProfile), viewModelEvents);
        }
    }
}