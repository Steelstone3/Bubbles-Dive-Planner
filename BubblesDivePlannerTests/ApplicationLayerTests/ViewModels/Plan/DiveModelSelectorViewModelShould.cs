using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Services;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class DiveModelSelectorViewModelShould
    {
        private Mock<DiveProfileService> _diveProfileService = new();
        private DiveModelSelectorViewModel _diveModelSelectorViewModel;

        public DiveModelSelectorViewModelShould()
        {
            _diveModelSelectorViewModel = new(_diveProfileService.Object);
        }
        
        [Fact]
        public void ContainsAListOfSelectableDiveModels()
        {
            Assert.NotEmpty(_diveModelSelectorViewModel.DiveModels);
            
            foreach (var diveModel in _diveModelSelectorViewModel.DiveModels)
            {
                Assert.NotNull(diveModel);
            }
        }
        
        [Fact]
        public void PopulateTheDiveSelectorViewModel()
        {
            _diveModelSelectorViewModel.SelectedDiveModel = _diveModelSelectorViewModel.DiveModels[0];

            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.DiveModelName);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.DiveModelName);
            
            Assert.Equal(16, _diveModelSelectorViewModel.SelectedDiveModel.CompartmentCount);
            
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.AValuesHelium);
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.BValuesHelium);
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.AValuesNitrogen);
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.BValuesNitrogen);
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.HeliumHalfTime);
            Assert.NotNull(_diveModelSelectorViewModel.SelectedDiveModel.NitrogenHalfTime);
           
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.AValuesHelium);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.BValuesHelium);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.AValuesNitrogen);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.BValuesNitrogen);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.HeliumHalfTime);
            Assert.NotEmpty(_diveModelSelectorViewModel.SelectedDiveModel.NitrogenHalfTime);
        }
        
        [Fact]
        public void ValidateInvalidDiveModelSelectorParameters()
        {
            Assert.False(_diveModelSelectorViewModel.ValidateSelectedDiveModel(null));
        }

        [Fact]
        public void ValidateValidDiveModelSelectorParameters()
        {
            var diveModel = _diveModelSelectorViewModel.SelectedDiveModel = new Zhl16Buhlmann();

            Assert.True(_diveModelSelectorViewModel.ValidateSelectedDiveModel(diveModel));
        }

        [Fact]
        public void HasADefaultUiState()
        {
            //Assert
            Assert.True(_diveModelSelectorViewModel.IsUiEnabled);
            Assert.True(_diveModelSelectorViewModel.IsUiVisible);
            Assert.False(_diveModelSelectorViewModel.IsReadOnlyUiVisible);
        }
    }
}