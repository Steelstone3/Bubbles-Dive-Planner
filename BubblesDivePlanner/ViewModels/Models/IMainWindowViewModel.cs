using BubblesDivePlanner.ViewModels.Models.DiveInformation;
using BubblesDivePlanner.ViewModels.Models.DivePlan;
using BubblesDivePlanner.ViewModels.Models.Header;

namespace BubblesDivePlanner.ViewModels.Models
{
    public interface IMainWindowViewModel
    {
        IHeaderModel Header { get; set; }
        IDivePlanModel Plan { get; set; }
        IDiveInformationModel Information { get; set; }
    }
}