using BubblesDivePlanner.ViewModels.Models.Plan;
using BubblesDivePlanner.ViewModels.Models.Plans;
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
            Assert.NotNull(planner.DiveStep);
            Assert.NotNull(planner.Cylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Planner plannerVM = (Planner)planner;
            Mock<IDiveStep> diveStep = new();
            Mock<ICylinder> cylinder = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveStep = diveStep.Object;
            plannerVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveStep), viewModelEvents);
        }
    }
}