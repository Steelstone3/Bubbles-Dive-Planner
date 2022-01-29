using BubblesDivePlanner.DiveModels;

namespace BubblesDivePlanner.DiveStages
{
    public class AbValuesCommand : IDiveStageCommand
    {
        private IDiveModel _diveModel;

        public AbValuesCommand(IDiveModel diveModel) {
            _diveModel = diveModel;
        }

        public void RunDiveStage()
        {
            for (int compartment = 0; compartment < _diveModel.CompartmentCount; compartment++)
            {
                CalculateAValues(compartment);
                CalculateBValues(compartment);
            }
        }

        private void CalculateAValues(int compartment)
        {
            _diveModel.DiveProfile.AValues[compartment] = ((_diveModel.AValuesNitrogen[compartment] * _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] + _diveModel.AValuesHelium[compartment] * _diveModel.DiveProfile.TissuePressuresHelium[compartment]) / _diveModel.DiveProfile.TissuePressuresTotal[compartment]);
        }

        private void CalculateBValues(int compartment)
        {
            _diveModel.DiveProfile.BValues[compartment] = ((_diveModel.BValuesNitrogen[compartment] * _diveModel.DiveProfile.TissuePressuresNitrogen[compartment] + _diveModel.BValuesHelium[compartment] * _diveModel.DiveProfile.TissuePressuresHelium[compartment]) / _diveModel.DiveProfile.TissuePressuresTotal[compartment]);
        }
    }
}