using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Planner.DiveModels;

namespace BubblesDivePlanner.ViewModels.Planner.DiveModels
{
    public class DiveModelSelection : ViewModelBase, IDiveModelSelection
    {
        public ObservableCollection<IDiveModel> DiveModels => new() { new Zhl16bBuhlmann(), };
    }
}