namespace DivePlannerMk3.Contracts
{
    public interface IDiveParametersResultModel : IDiveStepModel, IGasMixtureModel
    {
        string DiveProfileStepHeader
        {
            get; set;
        }

        string DiveModelUsed
        {
            get; set;
        }

        double DiveCeiling
        {
            get; set;
        }
    }
}