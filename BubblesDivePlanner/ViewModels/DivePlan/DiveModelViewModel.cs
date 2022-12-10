using System.Collections.ObjectModel;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.DivePlan
{
    public class DiveModelViewModel : ReactiveObject
    {
        public DiveModelViewModel()
        {
            DiveModels = new ObservableCollection<IDiveModel>();
            DiveModels.Add(new Zhl16Buhlmann(null));
            // DiveModels.Add(new Zhl12Buhlmann(null));
            DiveModels.Add(new UsnRevision6(null));
            DiveModels.Add(new DcapMf11f6(null));
        }

        public ObservableCollection<IDiveModel> DiveModels { get; }
    }
}