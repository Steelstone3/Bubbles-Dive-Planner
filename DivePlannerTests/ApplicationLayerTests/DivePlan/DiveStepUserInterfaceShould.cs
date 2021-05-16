using Xunit;
using DivePlannerMk3.ViewModels.DivePlan;

namespace DivePlannerTests
{
    public class DiveStepUserInterfaceShould
    {
        // depth, time and limits no -depth/time or depth/time over 1000

        private DiveStepViewModel _diveStep = new DiveStepViewModel();

        [Fact]
        public void AllowDiveStepModelToBeSet()
        {
            //Act
            _diveStep.Depth = 10;
            _diveStep.Time = 50;

            //Assert
            Assert.Equal(10, _diveStep.Depth);
            Assert.Equal(50, _diveStep.Time);
        }

        [Fact]
        public void RaisePropertyChangedWhenDepthIsSet()
        {
            //Arrange
            string depthEvent = "Not Fired";
            _diveStep.PropertyChanged += ((sender, e) => depthEvent = e.PropertyName);

            //Act
            _diveStep.Depth = 200;

            //Assert
            Assert.Equal(nameof(_diveStep.Depth), depthEvent);
        }

        [Fact]
        public void RaisePropertyChangedWhenTimeIsSet()
        {
            //Arrange
            string timeEvent = "Not Fired";
            _diveStep.PropertyChanged += ((sender, e) => timeEvent = e.PropertyName);

            //Act
            _diveStep.Time = 200;

            //Assert
            Assert.Equal(nameof(_diveStep.Time), timeEvent);
        }
    }
}