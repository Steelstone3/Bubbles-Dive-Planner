using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Plan.Cylinders;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;
using BubblesDivePlanner.ViewModels.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Plan
{
    public class DiveStageShould
    {
        private readonly IDiveStage planner = new DiveStage();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.Null(planner.DiveModel);
            Assert.NotNull(planner.DiveStep);
            Assert.Null(planner.Cylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DiveStage plannerVM = (DiveStage)planner;
            Mock<IDiveModel> diveModel = new();
            Mock<IDiveStep> diveStep = new();
            Mock<ICylinder> cylinder = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveModel = diveModel.Object;
            plannerVM.DiveStep = diveStep.Object;
            plannerVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveModel), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStep), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Cylinder), viewModelEvents);
        }
    }
}