using BubblesDivePlanner.GasManagement.Cylinder;
using BubblesDivePlanner.GasManagement;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.GasManagement
{
    public class GasManagementViewModelShould
    {
        private GasManagementViewModel _gasManagementViewModel;
        private Mock<ICylinderModel> cylinderModelDummy = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Act
            _gasManagementViewModel.Cylinders.Add(cylinderModelDummy.Object);

            //Assert
            Assert.NotEmpty(_gasManagementViewModel.Cylinders);
        }
    }
}