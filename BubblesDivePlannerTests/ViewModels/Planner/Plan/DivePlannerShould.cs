using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
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
            Assert.NotNull(planner.DiveSetup);
            Assert.NotNull(planner.Information);
            Assert.NotNull(planner.DiveStage);
        }

        [Fact]
        public void DeriveFrom()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(planner);
            Assert.IsAssignableFrom<IDivePlanner>(planner);
            Assert.IsAssignableFrom<IDivePlannerVM>(planner);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DivePlanner plannerVM = (DivePlanner)planner;
            Mock<IDiveSetup> diveSetup = new();
            Mock<IDiveInformation> diveInformation = new();
            Mock<IDiveStage> diveModel = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveSetup = diveSetup.Object;
            plannerVM.Information = diveInformation.Object;
            plannerVM.DiveStage = diveModel.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveSetup), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Information), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStage), viewModelEvents);
        }
    }
}