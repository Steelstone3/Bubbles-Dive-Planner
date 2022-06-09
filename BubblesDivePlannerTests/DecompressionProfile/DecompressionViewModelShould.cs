using System.Collections.Generic;
using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Cylinders.CylinderSelector;
using BubblesDivePlanner.Cylinders.CylinderSetup;
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
        public void RaisePropertyChanged()
        {
            //Arrange
            var diveSteps = new List<IDiveStepModel>();
            var viewModelEvents = new List<string>();
            _decompressionProfileViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _decompressionProfileViewModel.DecompressionDiveSteps = diveSteps;

            //Assert
            Assert.Contains(nameof(_decompressionProfileViewModel.DecompressionDiveSteps), viewModelEvents);
        }

        [Fact]
        public void RecalculateDecompressionStepsOnSelectedCylinder()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            MainWindowViewModel mainWindowViewModel = new();
            mainWindowViewModel.DiveModelSelector.SelectedDiveModel = _divePlannerApplicationTestFixture.GetDiveModel;
            mainWindowViewModel.DiveModelSelector.SelectedDiveModel.DiveProfile = _divePlannerApplicationTestFixture.GetDiveProfileResultFromFirstRun;
            mainWindowViewModel.DecompressionProfile = _decompressionProfileViewModel;
            _decompressionProfileViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            mainWindowViewModel.CylinderSelector.SelectedCylinder = _divePlannerApplicationTestFixture.GetSelectedCylinder;

            //Assert
            Assert.Contains(nameof(_decompressionProfileViewModel.DecompressionDiveSteps), viewModelEvents);
            Assert.NotEmpty(mainWindowViewModel.DecompressionProfile.DecompressionDiveSteps);
        }
    }
}