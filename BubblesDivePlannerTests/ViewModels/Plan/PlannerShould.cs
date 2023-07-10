using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class PlannerShould
    {
        private readonly IPlanner planner = new Planner();

        [Fact]
        public void Construct()
        {
            // Then
            // Assert.Null(planner.SelectedDiveModel);
            // Assert.Null(planner.SelectedCylinder);
            Assert.NotNull(planner.DiveStep);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Planner plannerVM = (Planner)planner;
            Mock<IDiveModel> diveModel = new();
            Mock<IDiveStep> diveStep = new();
            Mock<ICylinder> cylinder = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            // plannerVM.SelectedDiveModel = diveModel.Object;
            // plannerVM.SelectedCylinder = cylinder.Object;
            plannerVM.DiveStep = diveStep.Object;

            //Assert
            // Assert.Contains(nameof(plannerVM.SelectedDiveModel), viewModelEvents);
            // Assert.Contains(nameof(plannerVM.SelectedCylinder), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStep), viewModelEvents);
        }
    }
}