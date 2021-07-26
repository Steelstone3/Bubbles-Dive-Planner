using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInformation
{
    public class DiveInformationViewModel : ViewModelBase
    {
        /*private InfoDecompressionProfileViewModel _decompressionProfile = new InfoDecompressionProfileViewModel();
        public InfoDecompressionProfileViewModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged(ref _decompressionProfile, value);
        }*/

        public CnsToxicityViewModel CnsToxicity
        {
            get; set;
        } = new CnsToxicityViewModel();
    }
}
