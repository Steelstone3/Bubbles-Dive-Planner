using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Header;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class HeaderViewModelShould
    {
        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            Mock<IMainWindowModel> mainWindowModelDummy = new();
            HeaderViewModel headerViewModel = new(mainWindowModelDummy.Object);

            //Assert
            Assert.NotNull(headerViewModel.FileModel);
        }
    }
}