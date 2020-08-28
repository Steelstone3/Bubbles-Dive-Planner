using Moq;
using Xunit;

public class GasMixtureCloneShould
{
    [Fact]
    public void Clone()
    {
        // Given
        Mock<IGasMixture> gasMixture = new();
        GasMixtureClone gasMixtureClone = new();

        // When
        IGasMixture newGasMixture = gasMixtureClone.Clone(gasMixture.Object);

        // Then
        Assert.NotNull(newGasMixture);
        Assert.NotSame(gasMixture.Object, newGasMixture);
    }
}