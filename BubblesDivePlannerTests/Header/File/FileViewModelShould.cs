using BubblesDivePlanner.ApplicationEntry;
using BubblesDivePlanner.Header.File;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.Header.File
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