using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Header.File;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
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