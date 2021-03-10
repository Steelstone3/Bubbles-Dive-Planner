using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels.DivePlan;
using Newtonsoft.Json;

namespace DivePlannerMk3.DataAccessLayer.Serialisers
{
    public class DivePlanSerialiser : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            throw new System.NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new System.NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Newtonsoft.Json.JsonConvert.SerializeObject( value, Formatting.Indented);
        }

        /*private string SerialiseDiveDivePlan()
        {
            var jsonFile = string.Empty;

            jsonFile += SerialiseDiveModelSelector();
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.DiveStep, Formatting.Indented);
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.GasManagement, Formatting.Indented);
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.GasMixture.MaximumOperatingDepth, Formatting.Indented);
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.GasMixture.NewGasMixture, Formatting.Indented);
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.GasMixture.SelectedGasMixture, Formatting.Indented);
            
            //TODO AH may not work
            jsonFile += Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.GasMixture.GasMixtures, Formatting.Indented);

            return jsonFile;
        }

        private string SerialiseDiveModelSelector()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(_divePlan.DiveModelSelector.SelectedDiveModel, Formatting.Indented);
        }*/
    }
}