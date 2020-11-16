using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanGasManagementViewModel : ViewModelBase
    {
        private bool _uiEnabled = true;
        public bool UiEnabled
        {
            get => _uiEnabled;
            set
            {
                if (_uiEnabled != value)
                {
                    _uiEnabled = value;
                    this.RaisePropertyChanged(nameof(UiEnabled));
                }
            }
        }

        public GasManagementModel GasManagementModel
        {
            get; set;
        }
    }
}
