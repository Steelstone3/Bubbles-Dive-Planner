using System.Collections.ObjectModel;
using BubblesDivePlanner.ViewModels.Model.Plan;
using BubblesDivePlanner.ViewModels.Model.Planner.Plan.Result;
using ReactiveUI;

namespace BubblesDivePlanner.ViewModels.Planner.Plan
{
    public class Results : ViewModelBase, IResults
    {
        public ObservableCollection<IDiveStage> HistoricResults { get; } = new ObservableCollection<IDiveStage>();

        private IDiveStage result;
        public IDiveStage Result
        {
            get => result;
            set
            {
                result = value;
                this.RaisePropertyChanged(nameof(Result));
            }
        }
    }
}