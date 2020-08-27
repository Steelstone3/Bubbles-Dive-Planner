using System.Collections.ObjectModel;
using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;

namespace DivePlannerMk3.ViewModels.DiveResult
{
    public class DiveProfileResultsListViewModel : ViewModelBase
    {
        public DiveProfileResultsListViewModel()
        {
            ParameterOutput = new DiveProfileStepParametersOutputModel();
        }

        public string DiveProfileStepHeader
        {
            get; set;
        }

        public ObservableCollection<IDiveProfileStepOutputModel> DiveProfileStepOutput
        {
            get;
        } = new ObservableCollection<IDiveProfileStepOutputModel>();

        public IDiveProfileStepParametersOutputModel ParameterOutput
        {
            get;set;
        }
    }
}
