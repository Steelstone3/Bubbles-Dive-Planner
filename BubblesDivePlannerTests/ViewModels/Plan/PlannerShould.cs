using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
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
            Assert.NotNull(planner.DiveModelSelection);
            Assert.NotNull(planner.CylinderSelection);
            Assert.Null(planner.DiveModel);
            Assert.NotNull(planner.DiveStep);
            // TODO may change to null set from the selector...
            Assert.NotNull(planner.Cylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Planner plannerVM = (Planner)planner;
            Mock<IDiveModelSelection> diveModelSelection = new();
            Mock<ICylinderSelection> cylinderSelection = new();
            Mock<IDiveModel> diveModel = new();
            Mock<IDiveStep> diveStep = new();
            Mock<ICylinder> cylinder = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveModelSelection = diveModelSelection.Object;
            plannerVM.CylinderSelection = cylinderSelection.Object;
            plannerVM.DiveModel = diveModel.Object;
            plannerVM.DiveStep = diveStep.Object;
            plannerVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveModelSelection), viewModelEvents);
            Assert.Contains(nameof(plannerVM.CylinderSelection), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveModel), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStep), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Cylinder), viewModelEvents);
        }
    }
}