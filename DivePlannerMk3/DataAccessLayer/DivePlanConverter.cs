using DivePlannerMk3.Contracts;
using DivePlannerMk3.ViewModels.DivePlan;
using Newtonsoft.Json;

namespace DivePlannerMk3.DataAccessLayer
{
    public class DivePlanConverter : IDataConverter
    {
        private DivePlanViewModel _divePlan;

        public DivePlanConverter(DivePlanViewModel divePlan)
        {
            _divePlan = divePlan;
        }

        public string ConvertModelToEntity()
        {
            return SerialiseDiveDivePlan();
        }

        private string SerialiseDiveDivePlan()
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
        }
    }
}