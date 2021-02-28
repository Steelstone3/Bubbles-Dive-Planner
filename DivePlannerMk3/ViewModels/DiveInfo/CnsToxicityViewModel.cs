using ReactiveUI;
using DivePlannerMk3.Models;
using System.Collections.Generic;

namespace DivePlannerMk3.ViewModels.DiveInfo
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
        }
    }
}