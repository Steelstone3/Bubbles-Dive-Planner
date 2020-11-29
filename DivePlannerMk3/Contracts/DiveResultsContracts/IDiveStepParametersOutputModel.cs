namespace DivePlannerMk3.Contracts
{
    public interface IDiveParametersOutputModel
    {
        string DiveProfileStepHeader
        {
            get; set;
        }

        IDiveStepModel DiveStepModel
        {
            get; set;
        }

        IGasMixtureModel GasMixtureModel
        {
            get; set;
        }

        string DiveModelUsed
        {
            get; set;
        }
    }
}