using System.Collections.Generic;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication;
using BubblesDivePlanner.Contracts.ViewModels.Header;
using BubblesDivePlanner.ViewModels;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels
{
    public class MainWindowViewModelShould
    {
        private MainWindowViewModel _mainWindowViewModel = new();
        
        [Fact]
        public void ViewModelPropertiesCanBeSet()
        {
            Assert.NotNull(_mainWindowViewModel.DiveApplication);
            Assert.NotNull(_mainWindowViewModel.DiveHeader);
        }
        
        [Fact]
        public void RaisePropertyChangedWhenViewModelPropertiesAreSet()
        {
            //Arrange
            var viewModelEvents = new List<string>();
            _mainWindowViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _mainWindowViewModel.DiveApplication = new Mock<IDiveApplicationViewModel>().Object;
            _mainWindowViewModel.DiveHeader = new Mock<IDiveHeaderViewModel>().Object;
            
            //Assert
            Assert.Contains(nameof(_mainWindowViewModel.DiveApplication), viewModelEvents);
            Assert.Contains(nameof(_mainWindowViewModel.DiveHeader), viewModelEvents);
        }
    }
}