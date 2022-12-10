using System.Collections.Generic;
using System.Linq;
using BubblesDivePlanner.Models;
using BubblesDivePlanner.Models.Cylinders;
using BubblesDivePlanner.Models.DiveModels;
using BubblesDivePlanner.Models.DiveModels.Types;
using Newtonsoft.Json;

namespace BubblesDivePlanner.Controllers.Json
{
    public class JsonController : IJsonController
    {
        public string Serialise(List<IDivePlan> divePlans)
        {
            return JsonConvert.SerializeObject(divePlans, Formatting.Indented);
        }

        public IDivePlan Deserialise(string expectedDivePlanJson)
        {
            var converters = AddConverters(expectedDivePlanJson);

            var settings = new JsonSerializerSettings
            {
                Converters = converters
            };

            var divePlans = JsonConvert.DeserializeObject<List<IDivePlan>>(expectedDivePlanJson, settings);
            var divePlan = divePlans.Last();

            return divePlan ?? null;
        }

        private List<JsonConverter> AddConverters(string expectedDivePlanJson)
        {
            List<JsonConverter> jsonConverters = new()
            {
                new AbstractConverter<DivePlan, IDivePlan>(),
                new AbstractConverter<Cylinder, ICylinder>(),
                new AbstractConverter<GasMixture, IGasMixture>(),
                new AbstractConverter<DiveStep, IDiveStep>(),
                new AbstractConverter<DiveProfile, IDiveProfile>(),
            };

            DetermineDiveModel(expectedDivePlanJson, jsonConverters);

            return jsonConverters;
        }

        private void DetermineDiveModel(string expectedDivePlanJson, List<JsonConverter> jsonConverters)
        {
            if (expectedDivePlanJson.Contains(DiveModelNames.ZHL16_B.ToString()))
            {
                jsonConverters.Add(new AbstractConverter<Zhl16Buhlmann, IDiveModel>());
            }
            else if (expectedDivePlanJson.Contains(DiveModelNames.ZHL12.ToString()))
            {
                jsonConverters.Add(new AbstractConverter<Zhl12Buhlmann, IDiveModel>());
            }
            else if (expectedDivePlanJson.Contains(DiveModelNames.USN_REVISION_6.ToString()))
            {
                jsonConverters.Add(new AbstractConverter<UsnRevision6, IDiveModel>());
            }
            else if (expectedDivePlanJson.Contains(DiveModelNames.DCAP_MF11F6.ToString()))
            {
                jsonConverters.Add(new AbstractConverter<DcapMf11f6, IDiveModel>());
            }
        }
    }
}