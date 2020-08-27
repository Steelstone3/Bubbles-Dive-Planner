using DivePlannerMk3.Contracts;

namespace DivePlannerMk3.Models
{
    public class GasMixtureModel : IGasMixtureModel
    {
        public GasMixtureModel()
        {
            GasName = "Air";
            Oxygen = 21;
            Helium = 0;
            Nitrogen = 79;
        }

        public string GasName
        {
            get; set;
        }

        public double Oxygen
        {
           get;set;
        }

        public double Helium
        {
          get;set;
        }

        public double Nitrogen
        {
           get; set;
        }
    }
}
