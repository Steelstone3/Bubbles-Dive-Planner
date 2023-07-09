using Avalonia.Controls;
using Avalonia.Controls.Templates;
using BubblesDivePlanner.ViewModels;
using BubblesDivePlanner.Views;
using Xunit;

namespace BubblesDivePlannerTests.Views
{
    public class ViewLocatorShould
    {
        private readonly IDataTemplate viewLocator = new ViewLocator();

        [Fact]
        public void ThrowExpections()
        {
            // Then
            Assert.Throws<ArgumentNullException>(() => viewLocator.Build(null));
            Assert.Throws<ArgumentNullException>(() => viewLocator.Match(null));
        }

        [Fact]
        public void Build()
        {
            // Given
            MainWindowViewModel mainWindowViewModel = new();

            // When
            Control control = viewLocator.Build(mainWindowViewModel);

            // Then
            Assert.NotNull(control);
        }

        [Fact]
        public void Match()
        {
            // Given
            ViewModelBase viewModelBase = new();

            // When
            bool isMatch = viewLocator.Match(viewModelBase);

            // Then
            Assert.True(isMatch);
        }
    }
}