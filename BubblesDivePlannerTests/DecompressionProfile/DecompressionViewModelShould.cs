using System.Collections.Generic;
using System.Collections.Specialized;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DecompressionProfile
{
    public class DecompressionViewModelShould
    {
        private DivePlannerApplicationTestFixture _divePlannerApplicationTestFixture = new();
        private DecompressionProfileViewModel _decompressionProfileViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Assert
            Assert.NotNull(_decompressionProfileViewModel.DecompressionDiveSteps);
        }

        [Fact]
        public void RaiseCollectionChanged()
        {
            //Arrange
            var diveStepDummy = new Mock<IDiveStepModel>();
            var viewModelEvents = new List<NotifyCollectionChangedAction>();
            _decompressionProfileViewModel.DecompressionDiveSteps.CollectionChanged += (sender, e) => viewModelEvents.Add(e.Action);

            //Act
            _decompressionProfileViewModel.DecompressionDiveSteps.Add(diveStepDummy.Object);

            //Assert
            Assert.Contains(NotifyCollectionChangedAction.Add, viewModelEvents);
            Assert.Contains(diveStepDummy.Object, _decompressionProfileViewModel.DecompressionDiveSteps);
        }

        [Fact]
        public void RecalculateDecompressionStepsOnSelectedCylinder()
        {
            //Arrange
            MainWindowViewModel mainWindowViewModel = new();
            mainWindowViewModel.DivePlanner.DiveModelSelector.SelectedDiveModel = _divePlannerApplicationTestFixture.GetDiveModel;
            mainWindowViewModel.DivePlanner.DiveModelSelector.SelectedDiveModel.DiveProfile = _divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            mainWindowViewModel.DiveInformation.DecompressionProfile = _decompressionProfileViewModel;

            //Act
            mainWindowViewModel.DivePlanner.CylinderSelector.SelectedCylinder = _divePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.NotEmpty(mainWindowViewModel.DiveInformation.DecompressionProfile.DecompressionDiveSteps);
        }
    }
}