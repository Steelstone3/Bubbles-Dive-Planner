using System.Reactive;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Information;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
using BubblesDivePlanner.ViewModels.Planner.Plan;
using BubblesDivePlanner.ViewModels.Planner.Plan.Stage;
using BubblesDivePlannerTests.TestFixtures;
using Moq;
using ReactiveUI;
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
            Assert.NotNull(planner.Results);
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
            Mock<IResults> results = new();
            List<string> viewModelEvents = new();
            plannerVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            plannerVM.DiveSetup = diveSetup.Object;
            plannerVM.Information = diveInformation.Object;
            plannerVM.DiveStage = diveModel.Object;
            plannerVM.Results = results.Object;

            //Assert
            Assert.Contains(nameof(plannerVM.DiveSetup), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Information), viewModelEvents);
            Assert.Contains(nameof(plannerVM.DiveStage), viewModelEvents);
            Assert.Contains(nameof(plannerVM.Results), viewModelEvents);
        }

        [Fact]
        public void CalculateDiveProfile()
        {
            // Given
            IDiveStage expectedDiveStage = new DiveStage
            {
                DiveModel = PlannerTestFixture.GetDiveModel,
                DiveStep = PlannerTestFixture.GetDiveStep,
                Cylinder = PlannerTestFixture.GetCylinder
            };
            expectedDiveStage.DiveModel.DiveProfile.NitrogenTissuePressures = PlannerTestFixture.GetDiveProfileResult.NitrogenTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.HeliumTissuePressures = PlannerTestFixture.GetDiveProfileResult.HeliumTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.TotalTissuePressures = PlannerTestFixture.GetDiveProfileResult.TotalTissuePressures;
            expectedDiveStage.DiveModel.DiveProfile.AValues = PlannerTestFixture.GetDiveProfileResult.AValues;
            expectedDiveStage.DiveModel.DiveProfile.BValues = PlannerTestFixture.GetDiveProfileResult.BValues;
            expectedDiveStage.DiveModel.DiveProfile.MaxSurfacePressures = PlannerTestFixture.GetDiveProfileResult.MaxSurfacePressures;
            expectedDiveStage.DiveModel.DiveProfile.ToleratedAmbientPressures = PlannerTestFixture.GetDiveProfileResult.ToleratedAmbientPressures;
            expectedDiveStage.DiveModel.DiveProfile.CompartmentLoads = PlannerTestFixture.GetDiveProfileResult.CompartmentLoads;
            expectedDiveStage.DiveModel.DiveProfile.OxygenAtPressure = PlannerTestFixture.GetDiveProfileResult.OxygenAtPressure;
            expectedDiveStage.DiveModel.DiveProfile.HeliumAtPressure = PlannerTestFixture.GetDiveProfileResult.HeliumAtPressure;
            expectedDiveStage.DiveModel.DiveProfile.NitrogenAtPressure = PlannerTestFixture.GetDiveProfileResult.NitrogenAtPressure;
            DivePlanner plannerVM = (DivePlanner)planner;
            plannerVM.DiveSetup.CylinderSelection.Cylinder = PlannerTestFixture.GetCylinder;
            // TODO may need to do this via a selectable dive model like cylinder selection...
            plannerVM.DiveStage.DiveModel = PlannerTestFixture.GetDiveModel;
            plannerVM.DiveStage.DiveStep = PlannerTestFixture.GetDiveStep;
            ReactiveCommand<Unit, Unit> calculateDiveProfileCommand = ReactiveCommand.Create(plannerVM.CalculateDiveProfile);

            // When
            calculateDiveProfileCommand.Execute().Subscribe();

            // Then
            Assert.Equivalent(expectedDiveStage, plannerVM.Results.Result);
            Assert.NotEmpty(plannerVM.Results.HistoricResults);
            // Assert.NotSame(plannerVM.DiveStage, plannerVM.Results.Result);
            // Assert.NotSame(plannerVM.DiveStage, plannerVM.Results.HistoricResults[0]);
        }
    }
}