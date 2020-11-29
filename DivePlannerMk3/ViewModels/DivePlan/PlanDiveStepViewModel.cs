using DivePlannerMk3.Models;

namespace DivePlannerMk3.ViewModels.DivePlan
{
    public class PlanDiveStepViewModel : ViewModelBase
    {
        public DiveStepModel DiveStepModel
        {
            get; set;
        } = new DiveStepModel();
    }
}