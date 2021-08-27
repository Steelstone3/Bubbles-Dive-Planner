using BubblesDivePlanner.Contracts.Models.Information;
using BubblesDivePlanner.Contracts.ViewModels.DiveApplication.Information;
using BubblesDivePlanner.Models.Information;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Information
{
    public class CnsToxicityViewModel : ViewModelBase, ICnsToxicityViewModel
    {
        public CnsToxicityViewModel()
        {
            IsUiVisible = false;
        }

        public ICnsToxicityModel CnsToxicity
        {
            get; set;
        } = new CnsToxicityModel();
    }
}