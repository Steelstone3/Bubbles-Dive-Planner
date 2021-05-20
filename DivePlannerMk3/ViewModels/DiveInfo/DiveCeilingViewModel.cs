using ReactiveUI;
using DivePlannerMk3.Controllers;
using System.Collections.Generic;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class DiveCeilingViewModel : ViewModelBase
    {
        public DiveCeilingViewModel()
        {
            IsUiVisible = false;
        }

        private double _diveCeiling;
        public double DiveCeiling
        {
            get => _diveCeiling;
            set => this.RaiseAndSetIfChanged(ref _diveCeiling, value);
        }

        //TODO AH Need to hook this in better!!!
        //TODO AH One option is moving calculate up to the main viewmodel and putting in the dive ceiling as a parameter that is updated as a dive step
        //TODO AH The problem is stemming from the linq statement passed in from the results which has probably all the tolerated ambient pressures in it!
        public void CalculateDiveCeiling(IEnumerable<double> toleratedAmbientPressures)
        {
            var diveCeilingController = new DiveCeilingController();

            DiveCeiling = diveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures);
        }
    }
}
