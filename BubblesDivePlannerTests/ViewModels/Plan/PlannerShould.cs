using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
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
            Assert.NotNull(planner.CylinderSelection);
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
            Mock<ICylinderSelection> cylinderSelection = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.CylinderSelection = cylinderSelection.Object;
            plannerVM.DiveStep = diveStep.Object;
            plannerVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.CylinderSelection), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStep), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Cylinder), viewModelEvents);
        }
    }
}