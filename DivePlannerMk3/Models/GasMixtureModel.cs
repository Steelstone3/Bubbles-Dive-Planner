namespace DivePlannerMk3.Models
{
    public class GasMixtureModel
    {
        public GasMixtureModel()
        {
            GasName = "Air";
            Oxygen = 21;
            Helium = 0;
        }

        public string GasName
        {
            get; set;
        }

        private double _oxygen;
        public double Oxygen
        {
            get => _oxygen;
            set
            {
                _oxygen = value;
                _nitrogen = CalculateNitrogen();
            }
        }

        private double _helium;
        public double Helium
        {
            get => _helium;
            set
            {
                _helium = value;
                _nitrogen = CalculateNitrogen();
            }
        }

        private double _nitrogen;
        public double Nitrogen
        {
            get => _nitrogen;
            set
            {
                _nitrogen = value;
            }
        }

        private double CalculateNitrogen() => 100 - Oxygen - Helium;
    }
}
