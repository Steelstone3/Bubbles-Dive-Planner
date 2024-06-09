using Moq;
using Xunit;

public class DiveProfileStagesFactoryShould
{
    [Fact]
    public void Run()
    {
        // Given
        const byte COMPARTMENT_COUNT = 16;

        Mock<IDiveStep> diveStep = new();
        diveStep.Setup(ds => ds.Depth).Returns(50);
        diveStep.Setup(ds => ds.Time).Returns(10);
        Mock<IGasMixture> gasMixture = new();
        gasMixture.Setup(gm => gm.Oxygen).Returns(21);
        gasMixture.Setup(gm => gm.Helium).Returns(10);
        gasMixture.Setup(gm => gm.Nitrogen).Returns(69);
        Mock<ICylinder> cylinder = new();
        cylinder.Setup(c => c.GasMixture).Returns(gasMixture.Object);
        Mock<IDiveStageValidator> diveStageValidator = new();
        IDiveStage expectedDiveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann()
            {
                DiveModelProfile = new DiveModelProfile(COMPARTMENT_COUNT)
                {
                    OxygenAtPressure = 1.26f,
                    NitrogenAtPressure = 4.14f,
                    HeliumAtPressure = 0.6f,
                    NitrogenTissuePressures = new float[COMPARTMENT_COUNT]
                    {
                        3.5478f,
                        2.7315f,
                        2.2159f,
                        1.8368f,
                        1.5485f,
                        1.3446f,
                        1.1915f,
                        1.0784f,
                        0.9964f,
                        0.9453f,
                        0.9119f,
                        0.8858f,
                        0.8653f,
                        0.849f,
                        0.8363f,
                        0.8264f
                    },
                    HeliumTissuePressures = new float[COMPARTMENT_COUNT]
                    {
                        0.5939f,
                        0.5396f,
                        0.4618f,
                        0.3774f,
                        0.2957f,
                        0.2282f,
                        0.1719f,
                        0.1271f,
                        0.0929f,
                        0.0708f,
                        0.056f,
                        0.0443f,
                        0.035f,
                        0.0276f,
                        0.0217f,
                        0.0171f
                    },
                    TotalTissuePressures = new float[COMPARTMENT_COUNT]
                    {
                        4.1417f,
                        3.2711f,
                        2.6777f,
                        2.2142f,
                        1.8442f,
                        1.5728f,
                        1.3634f,
                        1.2055f,
                        1.0893f,
                        1.0161f,
                        0.9679f,
                        0.9301f,
                        0.9003f,
                        0.8766f,
                        0.858f,
                        0.8435f
                    },
                    AValues = new float[COMPARTMENT_COUNT]
                    {
                        1.3257f,
                        1.0632f,
                        0.9187f,
                        0.8056f,
                        0.7076f,
                        0.5978f,
                        0.5244f,
                        0.4711f,
                        0.4337f,
                        0.392f,
                        0.3603f,
                        0.3317f,
                        0.2941f,
                        0.2814f,
                        0.259f,
                        0.2384f,
                    },
                    BValues = new float[COMPARTMENT_COUNT]
                    {
                        0.4935f,
                        0.6387f,
                        0.7102f,
                        0.7722f,
                        0.8039f,
                        0.8365f,
                        0.8641f,
                        0.8872f,
                        0.9063f,
                        0.92f,
                        0.93f,
                        0.9387f,
                        0.9463f,
                        0.9532f,
                        0.9592f,
                        0.9645f,
                    },
                    ToleratedAmbientPressures = new float[COMPARTMENT_COUNT]
                    {
                        1.3897f,
                        1.4102f,
                        1.2492f,
                        1.0877f,
                        0.9137f,
                        0.8156f,
                        0.725f,
                        0.6516f,
                        0.5942f,
                        0.5742f,
                        0.5651f,
                        0.5617f,
                        0.5736f,
                        0.5673f,
                        0.5746f,
                        0.5836f,
                    },
                    MaxSurfacePressures = new float[COMPARTMENT_COUNT]
                    {
                        3.352f,
                        2.6289f,
                        2.3268f,
                        2.1006f,
                        1.9515f,
                        1.7933f,
                        1.6817f,
                        1.5982f,
                        1.5371f,
                        1.479f,
                        1.4356f,
                        1.397f,
                        1.3508f,
                        1.3305f,
                        1.3015f,
                        1.2752f,
                    },
                    CompartmentLoads = new float[COMPARTMENT_COUNT]
                    {
                        123.56f,
                        124.43f,
                        115.08f,
                        105.41f,
                        94.5f,
                        87.7f,
                        81.07f,
                        75.43f,
                        70.87f,
                        68.7f,
                        67.42f,
                        66.58f,
                        66.65f,
                        65.89f,
                        65.92f,
                        66.15f,
                    },
                }
            },
            DiveStep = diveStep.Object,
            Cylinder = cylinder.Object,
        };
        IDiveStage diveStage = new DiveStage(diveStageValidator.Object)
        {
            DiveModel = new Zhl16Buhlmann(),
            DiveStep = diveStep.Object,
            Cylinder = cylinder.Object,
        };

        IDiveProfileStagesFactory diveProfileStagesFactory = new DiveProfileStagesFactory();

        // When
        diveProfileStagesFactory.Run(diveStage);

        // Then
        Assert.Equal(expectedDiveStage.DiveStep.Depth, diveStage.DiveStep.Depth);
        Assert.Equal(expectedDiveStage.DiveStep.Time, diveStage.DiveStep.Time);

        Assert.Equal(expectedDiveStage.Cylinder.GasMixture.Oxygen, diveStage.Cylinder.GasMixture.Oxygen);
        Assert.Equal(expectedDiveStage.Cylinder.GasMixture.Helium, diveStage.Cylinder.GasMixture.Helium);
        Assert.Equal(expectedDiveStage.Cylinder.GasMixture.Nitrogen, diveStage.Cylinder.GasMixture.Nitrogen);

        Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.OxygenAtPressure, diveStage.DiveModel.DiveModelProfile.OxygenAtPressure, 4);
        Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.HeliumAtPressure, diveStage.DiveModel.DiveModelProfile.HeliumAtPressure, 4);
        Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.NitrogenAtPressure, diveStage.DiveModel.DiveModelProfile.NitrogenAtPressure, 4);

        for (int i = 0; i < COMPARTMENT_COUNT; i++)
        {
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[i], diveStage.DiveModel.DiveModelProfile.NitrogenTissuePressures[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[i], diveStage.DiveModel.DiveModelProfile.HeliumTissuePressures[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.TotalTissuePressures[i], diveStage.DiveModel.DiveModelProfile.TotalTissuePressures[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.AValues[i], diveStage.DiveModel.DiveModelProfile.AValues[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.BValues[i], diveStage.DiveModel.DiveModelProfile.BValues[i], 4);

            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.ToleratedAmbientPressures[i], diveStage.DiveModel.DiveModelProfile.ToleratedAmbientPressures[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.MaxSurfacePressures[i], diveStage.DiveModel.DiveModelProfile.MaxSurfacePressures[i], 4);
            Assert.Equal(expectedDiveStage.DiveModel.DiveModelProfile.CompartmentLoads[i], diveStage.DiveModel.DiveModelProfile.CompartmentLoads[i], 2);
        }
    }
}