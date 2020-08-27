namespace DivePlannerMk3.Contracts
{
    public interface IDiveProfileStepParametersOutputModel
    {
        int StepDepthParameter
        {
            get; set;
        }

        int StepTimeParameter
        {
            get; set;
        }

        string StepGasMixNameParameter
        {
            get; set;
        }

        int GasUsedParameter
        {
            get; set;
        }

        int GasRemainingParameter
        {
            get; set;
        }
        
        string DiveModelUsed
        {
            get; set;
        }
    }
}