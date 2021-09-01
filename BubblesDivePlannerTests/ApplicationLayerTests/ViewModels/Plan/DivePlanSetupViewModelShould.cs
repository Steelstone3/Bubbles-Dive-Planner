using System.Collections.Generic;
using BubblesDivePlanner.Contracts.Services;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Plan;
using BubblesDivePlanner.ViewModels.DiveApplication.Plan;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Plan
{
    public class DivePlanSetupViewModelShould
    {
        private DivePlanSetupViewModel _divePlanSetupViewModel;

        private Mock<IDiveProfileService> _diveProfileService = new();
        
        public DivePlanSetupViewModelShould()
        {
            _divePlanSetupViewModel = new(_diveProfileService.Object);
        }

        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _divePlanSetupViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _divePlanSetupViewModel.DiveModelSelector = new Mock<IDiveModelSelectorViewModel>().Object;
            _divePlanSetupViewModel.DiveStep = new Mock<IDiveStepViewModel>().Object;
            _divePlanSetupViewModel.GasMixture = new Mock<IGasMixtureSelectorViewModel>().Object;
            _divePlanSetupViewModel.GasManagement = new Mock<IGasManagementViewModel>().Object;
            
            //Assert
            Assert.Contains(nameof(_divePlanSetupViewModel.DiveModelSelector), viewModelEvents);
            Assert.Contains(nameof(_divePlanSetupViewModel.DiveStep), viewModelEvents);
            Assert.Contains(nameof(_divePlanSetupViewModel.GasMixture), viewModelEvents);
            Assert.Contains(nameof(_divePlanSetupViewModel.GasManagement), viewModelEvents);
        }
    }
}