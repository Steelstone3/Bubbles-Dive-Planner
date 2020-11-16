namespace DivePlannerMk3.Contracts
{
    public interface IDiveParametersOutputModel
    {
        string DiveProfileStepHeader
        {
            get; set;
        }

        int DiveDepthUsed
        {
            get; set;
        }

        int DiveTimeUsed
        {
            get; set;
        }

        string GasMixNameUsed
        {
            get; set;
        }

        int GasUsedOnDiveStep
        {
            get; set;
        }

        int GasRemaining
        {
            get; set;
        }
        
        string DiveModelUsed
        {
            get; set;
        }
    }
}