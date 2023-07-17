using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Plan.DiveModels;

namespace BubblesDivePlanner.ViewModels.Plan.DiveModels
{
    public class DiveModelSelection : IDiveModelSelection
    {
        public ObservableCollection<IDiveModel> DiveModels => new() { new Zhl16bBuhlmann(), };
    }
}