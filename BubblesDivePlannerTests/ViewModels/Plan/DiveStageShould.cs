using BubblesDivePlanner.ViewModels;
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
        private readonly IDiveStage diveStage = new DiveStage();

        [Fact]
        public void Construct()
        {
            // Then
            Assert.IsAssignableFrom<ViewModelBase>(diveStage);
            Assert.Null(diveStage.DiveModel);
            Assert.NotNull(diveStage.DiveStep);
            Assert.Null(diveStage.Cylinder);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            DiveStage diveStageVM = (DiveStage)diveStage;
            Mock<IDiveModel> diveModel = new();
            Mock<IDiveStep> diveStep = new();
            Mock<ICylinder> cylinder = new();
            List<string> viewModelEvents = new();
            diveStageVM.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            diveStageVM.DiveModel = diveModel.Object;
            diveStageVM.DiveStep = diveStep.Object;
            diveStageVM.Cylinder = cylinder.Object;

            //Assert
            Assert.Contains(nameof(diveStageVM.DiveModel), viewModelEvents);
            Assert.Contains(nameof(diveStageVM.DiveStep), viewModelEvents);
            Assert.Contains(nameof(diveStageVM.Cylinder), viewModelEvents);
        }
    }
}