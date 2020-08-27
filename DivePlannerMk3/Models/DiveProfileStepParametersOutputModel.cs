using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class DiveProfileStepParametersOutputModel : IDiveProfileStepParametersOutputModel
    {
        public int StepDepthParameter
        {
            get; set;
        }

        public int StepTimeParameter
        {
            get; set;
        }

        public string StepGasMixNameParameter
        {
            get; set;
        }

        public int GasUsedParameter
        {
            get; set;
        }

        public int GasRemainingParameter
        {
            get; set;
        }

        public string DiveModelUsed
        {
            get; set;
        }
    }
}
