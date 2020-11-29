using DivePlannerMk3.Models;
using ReactiveUI;

namespace DivePlannerMk3.ViewModels.DiveInfo
{
    public class InfoGasManagementReadOnlyViewModel : ViewModelBase
    {
        public InfoGasManagementReadOnlyViewModel()
        {
            UiEnabled = false;
        }

        public GasManagementInfoModel GasManagementInfoModel
        {
            get; set;
        } = new GasManagementInfoModel();
    }
}