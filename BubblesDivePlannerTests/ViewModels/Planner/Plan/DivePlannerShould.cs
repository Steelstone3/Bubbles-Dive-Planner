using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Planner.Plan
{
    public class DivePlannerShould
    {
        private readonly IDivePlanner planner = new DivePlanner();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(planner);
            Assert.NotNull(planner.DiveModelSelection);
            Assert.NotNull(planner.CylinderSelection);
            Assert.NotNull(planner.Information);
            Assert.NotNull(planner.DiveStage);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DivePlanner plannerVM = (DivePlanner)planner;
            Mock<IDiveModelSelection> diveModelSelection = new();
            Mock<ICylinderSelectionVM> cylinderSelection = new();
            Mock<IDiveInformation> diveInformation = new();
            Mock<IDiveStage> diveModel = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveModelSelection = diveModelSelection.Object;
            plannerVM.CylinderSelection = cylinderSelection.Object;
            plannerVM.Information = diveInformation.Object;
            plannerVM.DiveStage = diveModel.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveModelSelection), viewModelEvents);
            Assert.Contains(nameof(plannerVM.CylinderSelection), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Information), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStage), viewModelEvents);
        }
    }
}