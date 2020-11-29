using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class DiveParametersOutputModel : IDiveParametersOutputModel
    {
        public string DiveProfileStepHeader 
        { 
            get;set;
        }

        public IDiveStepModel DiveStepModel 
        {
            get; set;
        } = new DiveStepModel();
        
        public IGasMixtureModel GasMixtureModel 
        {
            get; set;
        } = new GasMixtureModel();
        
        //TODO AH GasUsageModel and calculation
        /*public IGasUsageModel GasUsage
        {
            get; set;
        } = new GasUsageModel();*/

        public string DiveModelUsed
        {
            get; set;
        }
    }
}
