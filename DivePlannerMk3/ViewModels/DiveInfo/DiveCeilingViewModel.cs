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

//TODO AH Need to hook this in!
        public void CalculateDiveCeiling(IEnumerable<double> toleratedAmbientPressures)
        {
            var diveCeilingController = new DiveCeilingController();

            DiveCeiling = diveCeilingController.CalculateDiveCeiling(toleratedAmbientPressures);
        }
    }
}
