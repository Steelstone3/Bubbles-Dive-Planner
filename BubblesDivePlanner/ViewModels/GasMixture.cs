public class GasMixture : IGasMixture
{
    public float Oxygen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Nitrogen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public float Helium { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}

public interface IGasMixture
{
    float Oxygen { get; set; }
    float Nitrogen { get; set; }
    float Helium { get; set; }
}