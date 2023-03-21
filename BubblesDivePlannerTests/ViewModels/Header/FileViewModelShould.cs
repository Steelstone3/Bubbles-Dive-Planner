using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.ViewModels.Header;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels.Header
{
    public class FileViewModelShould
    {
        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            Mock<IMainWindowModel> mainWindowModelDummy = new();
            FileViewModel fileViewModel = new(mainWindowModelDummy.Object);

            //Assert
            Assert.NotNull(fileViewModel.NewModel);
        }
    }
}