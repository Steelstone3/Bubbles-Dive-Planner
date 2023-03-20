using BubblesDivePlanner.DiveStep;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.DiveStep
{
    public class DiveStepViewModelShould
    {
        private readonly DiveStepViewModel _diveStep = new();
        private readonly byte _depth = 50;
        private readonly byte _time = 10;

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

        [Theory]
        [InlineData(0, 1, true)]
        [InlineData(100, 60, true)]
        [InlineData(101, 61, false)]
        [InlineData(255, 255, false)]
        public void ValidateModelAtTheBounds(byte depth, byte time, bool expectedValidity)
        {
            //Arrange
            _diveStep.Depth = depth;
            _diveStep.Time = time;

            //Act
            var isValid = _diveStep.ValidateDiveStep(_diveStep);

            //Assert
            Assert.Equal(expectedValidity, isValid);
        }

        [Fact]
        public void Clone()
        {
            //Arrange
            _diveStep.Depth = _depth;
            _diveStep.Time = _time;

            //Act
            var newDiveStep = _diveStep.DeepClone();

            //Assert
            Assert.NotSame(_diveStep, newDiveStep);
        }
    }
}