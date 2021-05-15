using DivePlannerMk3.ViewModels.DivePlan;
using Xunit;

namespace DivePlannerTests
{
    public class GasManagementUsageUserInterfaceShould
    {
        private GasManagementViewModel _gasManagement = new GasManagementViewModel();
        
        [Fact(Skip = "Test needs Implementing")]
        public void HaveAPostDiveStepVisibiltyState()
        {
            //Arrange

            //Act

            //Assert
            Assert.True(_gasManagement.IsGasUsageVisible);
            Assert.False(_gasManagement.IsUiVisible);
            Assert.False(_gasManagement.IsUiEnabled);
        }
    }
}