namespace DivePlannerMk3.Contracts
{
    public interface IDiveParametersResult : IDiveStepModel, IGasMixtureModel
    {
        string DiveProfileStepHeader
        {
            get; set;
        }

        string DiveModelUsed
        {
            get; set;
        }
    }
}