using DivePlannerMk3.Models;

namespace DivePlannerMk3.ViewModels.DiveInformation
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