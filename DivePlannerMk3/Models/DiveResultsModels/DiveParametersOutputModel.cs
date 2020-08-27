using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class DiveParametersOutputModel : IDiveParametersOutputModel
    {
        public string DiveProfileStepHeader
        {
            get; set;
        }

        public string DiveModelUsed
        {
            get; set;
        }

        public int Depth
        {
            get; set;
        }
        public int Time
        {
            get; set;
        }

        public string GasName
        {
            get; set;
        }

        public double Oxygen
        {
            get; set;
        }

        public double Helium
        {
            get; set;
        }

        public double Nitrogen
        {
            get; set;
        }

        public int GasUsedForStep
        {
            get; set;
        }
    }
}
