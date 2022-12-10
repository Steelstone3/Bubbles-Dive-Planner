using BubblesDivePlanner.Models;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class DiveStepViewModelShould
    {
        private readonly DiveStepViewModel diveStepViewModel = new();
        private readonly IDiveStep diveStep = new DiveStep(50, 10);

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            diveStepViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveStepViewModel.DiveStep = diveStep;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(diveStepViewModel.DiveStep), viewModelEvents);
        }

        // [Fact]
        // public void Clone()
        // {
        //     //Arrange
        //     diveStepViewModel.Depth = _depth;
        //     diveStepViewModel.Time = _time;

        //     //Act
        //     var newDiveStep = diveStepViewModel.DeepClone();

        //     //Assert
        //     Assert.NotSame(diveStepViewModel, newDiveStep);
        // }
    }
}