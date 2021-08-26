using BubblesDivePlanner.Models.Information;

namespace BubblesDivePlanner.ViewModels.DiveApplication.Information
{
    public class CnsToxicityViewModel : ViewModelBase
    {
        public CnsToxicityViewModel()
        {
            IsUiVisible = false;
        }

        public CnsToxicityModel CnsToxicity
        {
            get; set;
        } = new CnsToxicityModel();
    }
}