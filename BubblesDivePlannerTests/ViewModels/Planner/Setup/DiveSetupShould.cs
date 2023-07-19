using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;
using BubblesDivePlanner.ViewModels.Model.Planner.Setup;
using BubblesDivePlanner.ViewModels.Planner.Cylinders;
using BubblesDivePlanner.ViewModels.Planner.Plan.Setup;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Planner.Setup
{
    public class DiveSetupShould
    {
        private readonly IDiveSetup diveSetup = new DiveSetup();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(diveSetup);
            Assert.NotNull(diveSetup.DiveModelSelection);
            Assert.NotNull(diveSetup.CylinderSelection);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DiveSetup diveSetupVM = (DiveSetup)diveSetup;
            Mock<IDiveModelSelection> diveModelSelection = new();
            Mock<ICylinderSelectionVM> cylinderSelection = new();
            List<string> viewModelEvents = new();
            diveSetupVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveSetupVM.DiveModelSelection = diveModelSelection.Object;
            diveSetupVM.CylinderSelection = cylinderSelection.Object;

            //Assert
            Assert.Contains(nameof(diveSetupVM.DiveModelSelection), viewModelEvents);
            Assert.Contains(nameof(diveSetupVM.CylinderSelection), viewModelEvents);
        }
    }
}