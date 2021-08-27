using BubblesDivePlanner.Contracts.Models.Information;

namespace BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information
{
    public interface ICnsToxicityViewModel : IVisibility
    {
        ICnsToxicityModel CnsToxicity { get; set; }
    }
}