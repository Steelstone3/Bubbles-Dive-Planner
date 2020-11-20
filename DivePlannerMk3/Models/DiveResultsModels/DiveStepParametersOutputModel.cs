using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class DiveParametersOutputModel : IDiveParametersOutputModel
    {
        public string DiveProfileStepHeader 
        { 
            get;set;
        }

        public int DiveDepthUsed
        {
            get; set;
        }

        public int DiveTimeUsed
        {
            get; set;
        }

        public string GasMixNameUsed
        {
            get; set;
        }

        public int GasUsedOnDiveStep
        {
            get; set;
        }

        public int GasRemaining
        {
            get; set;
        }

        public string DiveModelUsed
        {
            get; set;
        }
    }
}
