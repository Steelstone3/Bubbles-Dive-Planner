using BubblesDivePlanner.DiveStep;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.DiveStep
{
    public class DiveStepViewModelShould
    {
        private DiveStepViewModel _diveStep = new();
        private int _depth = 50;
        private int _time = 10;

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _diveStep.Depth = _depth;
            _diveStep.Time = _time;

            //Assert
            Assert.Equal(_depth, _diveStep.Depth);
            Assert.Equal(_time, _diveStep.Time);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _diveStep.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveStep.Depth = _depth;
            _diveStep.Time = _time;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_diveStep.Depth), viewModelEvents);
            Assert.Contains(nameof(_diveStep.Time), viewModelEvents);
        }
    }
}