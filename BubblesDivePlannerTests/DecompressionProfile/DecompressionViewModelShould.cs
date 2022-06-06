using System.Collections.Generic;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveStep;
using BubblesDivePlannerTests.TestFixtures;
using Xunit;

namespace BubblesDivePlannerTests.DecompressionProfile
{
    public class DecompressionViewModelShould
    {
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
    }
}