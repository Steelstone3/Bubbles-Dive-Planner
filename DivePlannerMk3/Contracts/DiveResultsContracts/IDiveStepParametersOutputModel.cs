namespace DivePlannerMk3.Contracts
{
    public interface IDiveParametersOutputModel : IDiveStepModel, IGasMixtureModel
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