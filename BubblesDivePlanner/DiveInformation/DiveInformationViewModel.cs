using BubblesDivePlanner.CentralNervousSystemToxicity;
using BubblesDivePlanner.DecompressionProfile;
using ReactiveUI;

namespace BubblesDivePlanner.DiveInformation
{
    public class DiveInformationViewModel : ReactiveObject, IDiveInformationModel
    {
        public ICentralNervousSystemToxicityModel CentralNervousSystemToxicity
        {
            get;
        } = new CentralNervousSystemToxicityViewModel();

        private IDecompressionProfileModel _decompressionProfile = new DecompressionProfileViewModel();
        public IDecompressionProfileModel DecompressionProfile
        {
            get => _decompressionProfile;
            set => this.RaiseAndSetIfChanged(ref _decompressionProfile, value);
        }
    }
}