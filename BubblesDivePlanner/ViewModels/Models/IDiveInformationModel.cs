using BubblesDivePlanner.ViewModels.Models;

namespace BubblesDivePlanner.DiveInformation
{
    public interface IDiveInformationModel
    {
        ICentralNervousSystemToxicityModel CentralNervousSystemToxicity { get; }
        IDecompressionProfileModel DecompressionProfile { get; set; }
    }
}