using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Plan;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class DiveStepShould
    {
        private readonly IDiveStep diveStep = new DiveStep();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(diveStep);
            Assert.Equal(0, diveStep.Depth);
            Assert.Equal(0, diveStep.Time);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DiveStep diveStepVM = (DiveStep)diveStep;
            List<string> viewModelEvents = new();
            diveStepVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveStepVM.Depth = 50;
            diveStepVM.Time = 10;

            //Assert
            Assert.Contains(nameof(diveStepVM.Depth), viewModelEvents);
            Assert.Contains(nameof(diveStepVM.Time), viewModelEvents);
        }
    }
}