using BubblesDivePlanner.ViewModels;
using ReactiveUI;
using Xunit;

namespace BubblesDivePlannerTests.ViewModels
{
    public class ViewModelBaseShould
    {
        private readonly ViewModelBase viewModelBase = new();

        [Fact]
        public void BeAReactiveObject()
        {
            // Then
            Assert.IsAssignableFrom<ReactiveObject>(viewModelBase);
        }

        [Fact]
        public void IsVisible()
        {
            // Then
            Assert.True(viewModelBase.IsVisible);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            const bool isVisible = false;
            List<string> viewModelEvents = new();
            viewModelBase.PropertyChanged += (_, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            viewModelBase.IsVisible = isVisible;

            //Assert
            Assert.Contains(nameof(viewModelBase.IsVisible), viewModelEvents);
        }
    }
}