using BubblesDivePlanner.DiveStep;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class DiveStepViewModelShould
    {
        private DiveStepViewModel _diveStep = new();
        private int depth;
        private int time;

        public DiveStepViewModelShould()
        {
            depth = 50;
            time = 10;
        }

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _diveStep.Depth = depth;
            _diveStep.Time = time;

            //Assert
            Assert.Equal(depth, _diveStep.Depth);
            Assert.Equal(time, _diveStep.Time);
        }

        [Fact]
        public void RaisePropertyChanged() {
            //Arrange
            var viewModelEvents = new List<string>();
            _diveStep.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveStep.Depth = depth;
            _diveStep.Time = time;

            //Assert
            Assert.Contains(nameof(_diveStep.Depth), viewModelEvents);
            Assert.Contains(nameof(_diveStep.Time), viewModelEvents);
        }
    }
}