using BubblesDivePlanner.ViewModels.DiveApplication.Information;
using Xunit;

namespace BubblesDivePlannerTests.ApplicationLayerTests.ViewModels.Information
{
    public class DiveInformationViewModelShould
    {
        private DiveInformationViewModel _diveInformationViewModel = new();
        
        [Fact]
        public void ViewModelPropertiesAreSet()
        {
            Assert.NotNull(_diveInformationViewModel.CnsToxicity);
        }
    }
}