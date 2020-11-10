namespace DivePlannerMk3.Models
{
    public class GasMixtureModel
    {
        public GasMixtureModel()
        {
            GasName = "Air";
            Oxygen = 21;
            Helium = 0;
            Nitrogen = 100 - Oxygen - Helium;
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
    }
}
