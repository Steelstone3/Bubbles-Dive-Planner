using BubblesDivePlanner.Contracts.Models.Results;

namespace BubblesDivePlanner.Models.Results
{
    public class DiveParametersResultModel : IDiveParametersResultModel
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

        public double DiveCeiling
        {
            get; set;
        }
    }
}
