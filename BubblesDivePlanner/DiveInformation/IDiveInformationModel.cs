using BubblesDivePlanner.CentralNervousSystemToxicity;
using BubblesDivePlanner.DecompressionProfile;

namespace BubblesDivePlanner.DiveInformation
{
    public interface IDiveInformationModel
    {
        ICentralNervousSystemToxicityModel CentralNervousSystemToxicity { get; }
        IDecompressionProfileModel DecompressionProfile { get; set; }
    }
}