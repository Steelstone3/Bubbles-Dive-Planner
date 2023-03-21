using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.ViewModels.Header;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Header
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