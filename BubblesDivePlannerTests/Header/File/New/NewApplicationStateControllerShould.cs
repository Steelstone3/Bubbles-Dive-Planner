using BubblesDivePlanner.Header.File.New;
using BubblesDivePlanner.ApplicationEntry;
using Moq;
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
            var divePlannerDummy = new Mock<IMainWindowModel>();

            //Act
            var divePlanner = _newController.CreateNewApplicationInstance();

            //Assert
            Assert.NotSame(divePlannerDummy.Object, divePlanner);
        }
    }
}