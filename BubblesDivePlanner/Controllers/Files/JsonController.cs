using Newtonsoft.Json;

public class JsonController : IJsonController
{
    // ICylinderSelector
    public string Serialise(IResult result)
    {
        // List<JsonConverter> converters = AddConverters();

        // JsonSerializerSettings settings = new()
        // {
        //     Converters = converters
        // };

        return JsonConvert.SerializeObject(result, Formatting.Indented);
    }

    // public List<IDivePlan> Deserialise(string json)
    // {
    //     List<JsonConverter> converters = AddConverters();

    //     JsonSerializerSettings settings = new()
    //     {
    //         Converters = converters
    //     };

    //     return JsonConvert.DeserializeObject<List<IDivePlan>>(json, settings);
    // }

    // private List<JsonConverter> AddConverters()
    // {
    //     List<JsonConverter> jsonConverters = new()
    //     {
    //             new AbstractConverter<DivePlan, IDivePlan>(),
    //             new AbstractConverter<Cylinder, ICylinder>(),
    //             new AbstractConverter<GasMixture, IGasMixture>(),
    //             new AbstractConverter<DiveStep, IDiveStep>(),
    //             new AbstractConverter<DiveModelProfile, IDiveModelProfile>(),
    //     };

    //     return jsonConverters;
    // }

    // private void AddDiveModelConverters(string json, List<JsonConverter> jsonConverters)
    // {
    //     if (json.Contains(DiveModelName.ZHL16_B.ToString()))
    //     {
    //         jsonConverters.Add(new AbstractConverter<Zhl16Buhlmann, IDiveModel>());
    //     }
    //     else if (json.Contains(DiveModelName.USN_REVISION_6.ToString()))
    //     {
    //         jsonConverters.Add(new AbstractConverter<UsnRevisionSix, IDiveModel>());
    //     }
    // }
}

public interface IJsonController
{
    string Serialise(IResult result);
}