using BubblesDivePlanner.Models;
using BubblesDivePlanner.ViewModels.DivePlan;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.DivePlan
{
    public class DiveStepViewModelShould
    {
        private readonly DiveStepViewModel diveStepViewModel = new();

        [Fact]
        public void RaisePropertyChanged()
        {
            // Given
            var viewModelEvents = new List<string>();
            diveStepViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            // When
            diveStepViewModel.Depth = TestFixture.FixtureDiveStep.Depth;
            diveStepViewModel.Time = TestFixture.FixtureDiveStep.Time;

            // Then
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(diveStepViewModel.Depth), viewModelEvents);
            Assert.Contains(nameof(diveStepViewModel.Time), viewModelEvents);
        }

        // [Fact]
        // public void Clone()
        // {
        //     // Given
        //     diveStepViewModel.Depth = _depth;
        //     diveStepViewModel.Time = _time;

        //     // When
        //     var newDiveStep = diveStepViewModel.DeepClone();

        //     // Then
        //     Assert.NotSame(diveStepViewModel, newDiveStep);
        // }
    }
}