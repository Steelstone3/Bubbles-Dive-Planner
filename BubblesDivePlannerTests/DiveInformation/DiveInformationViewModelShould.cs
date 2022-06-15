using System.Collections.Generic;
using BubblesDivePlanner.DecompressionProfile;
using BubblesDivePlanner.DiveInformation;
using Moq;
using Xunit;

namespace BubblesDivePlannerTests.DiveInformation
{
    public class DiveInformationViewModelShould
    {
        private DiveInformationViewModel _diveInformationViewModel = new();

        [Fact]
        public void AllowModelToBeSet()
        {
            //Arrange
            Mock<IDecompressionProfileModel> decompressionProfileModelDummy = new();

            //Act
            _diveInformationViewModel.DecompressionProfile = decompressionProfileModelDummy.Object;

            //Assert
            Assert.Equal(decompressionProfileModelDummy.Object, _diveInformationViewModel.DecompressionProfile);
            Assert.NotNull(_diveInformationViewModel.CentralNervousSystemToxicity);
        }

        [Fact]
        public void RaisePropertyChanged()
        {
            //Arrange
            Mock<IDecompressionProfileModel> decompressionProfileModelDummy = new();
            var viewModelEvents = new List<string>();
            _diveInformationViewModel.PropertyChanged += (sender, e) => viewModelEvents.Add(e.PropertyName);

            //Act
            _diveInformationViewModel.DecompressionProfile = decompressionProfileModelDummy.Object;

            //Assert
            Assert.NotEmpty(viewModelEvents);
            Assert.Contains(nameof(_diveInformationViewModel.DecompressionProfile), viewModelEvents);
        }
    }
}