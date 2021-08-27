using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class DiveModelSelectorViewModelShould
    {
        [Fact]
        public void PopulateTheDiveSelectorViewModel()
        {
            var diveProfileService = new Mock<DiveProfileService>();
            var diveModelSelectorViewModel = new DiveModelSelectorViewModel(diveProfileService.Object);

            diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();

            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.DiveModelName);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.DiveModelName);
            
            Assert.Equal(16, diveModelSelectorViewModel.SelectedDiveModel.CompartmentCount);
            
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.AValuesHelium);
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.BValuesHelium);
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.AValuesNitrogen);
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.BValuesNitrogen);
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.HeliumHalfTime);
            Assert.NotNull(diveModelSelectorViewModel.SelectedDiveModel.NitrogenHalfTime);
           
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.AValuesHelium);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.BValuesHelium);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.AValuesNitrogen);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.BValuesNitrogen);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.HeliumHalfTime);
            Assert.NotEmpty(diveModelSelectorViewModel.SelectedDiveModel.NitrogenHalfTime);
        }
    }
}