using ReactiveUI;

// TODO AH Test
public class GasMixture : ReactiveObject, IGasMixture
{
    private float oxygen;
        public float Oxygen
        {
            get => oxygen;
            set
            {
                this.RaiseAndSetIfChanged(ref oxygen, value);
                Nitrogen = CalculateNitrogen();
            }
        }

        private float helium;
        public float Helium
        {
            get => helium;
            set
            {
                this.RaiseAndSetIfChanged(ref helium, value);
                Nitrogen = CalculateNitrogen();
            }
        }

        private float nitrogen = 100;
        public float Nitrogen
        {
            get => nitrogen;
            private set => this.RaiseAndSetIfChanged(ref nitrogen, value);
        }

        // TODO AH Move to a controller
        public float CalculateNitrogen() => 100.0F - Oxygen - Helium;
}

public interface IGasMixture
{
    float Oxygen { get; set; }
    float Helium { get; set; }
    float Nitrogen { get; }
}