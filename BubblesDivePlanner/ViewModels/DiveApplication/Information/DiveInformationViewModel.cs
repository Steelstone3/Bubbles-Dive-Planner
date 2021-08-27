using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Information
{
    public class DiveInformationViewModel : ViewModelBase, IDiveInformationViewModel
    {
        /*private InfoDecompressionProfileViewModel _decompressionProfile = new InfoDecompressionProfileViewModel();
        public InfoDecompressionProfileViewModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged(ref _decompressionProfile, value);
        }*/

        public ICnsToxicityViewModel CnsToxicity
        {
            get; set;
        } = new CnsToxicityViewModel();
    }
}
