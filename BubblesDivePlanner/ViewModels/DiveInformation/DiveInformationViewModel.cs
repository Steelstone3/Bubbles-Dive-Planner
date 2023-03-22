using BubblesDivePlanner.DiveInformation;
using BubblesDivePlanner.ViewModels.DiveStages;
using BubblesDivePlanner.ViewModels.Models;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DiveInformation
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