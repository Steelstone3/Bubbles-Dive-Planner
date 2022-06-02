using BubblesDivePlanner.Header.File.New;
using BubblesDivePlanner.ApplicationEntry;
using Xunit;

namespace BubblesDivePlannerTests.Header.File.New
{
    public class NewApplicationStateControllerShould
    {
        private NewApplicationStateController _newController = new NewApplicationStateController();

        [Fact]
        public void CreateNewDivePlannerInstance() 
        {
            //Arrange
            var oldDivePlannerInstance = new MainWindowViewModel();

            //Act
            var newDivePlannerInstance = _newController.CreateNewApplicationInstance();

            //Assert
            Assert.NotSame(oldDivePlannerInstance, newDivePlannerInstance);
            Assert.NotSame(oldDivePlannerInstance.DiveModelSelector, newDivePlannerInstance.DiveModelSelector);
            Assert.NotSame(oldDivePlannerInstance.DiveStep, newDivePlannerInstance.DiveStep);
            Assert.NotSame(oldDivePlannerInstance.CylinderSelector, newDivePlannerInstance.CylinderSelector);
            Assert.NotSame(oldDivePlannerInstance.ResultsOverviewModel, newDivePlannerInstance.ResultsOverviewModel);
        }
    }
}