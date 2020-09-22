using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveProfileService : IDiveProfileService
    {
        private IDiveProfileStepOutputModel stepProfileOutputModel = new DiveProfileStepOutputModel();
        private IDiveProfile _diveProfile = new DiveProfile();

        private IDiveModel _theDiveModel;
        public IDiveModel TheDiveModel
        {
            get => _theDiveModel;
            set
            {
                _theDiveModel = value;
                InitaliseDiveProfile();
            }
        }

        //TODO AH Strategy pattern

        public DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture)
        {
            int compartment = 1;
            string diveHeader = _theDiveModel.DiveModelName + "\r\nDepth: " + diveStep.Depth.ToString() + " Time: " + diveStep.Time.ToString();

            //TODO AH reconsider the whole output and input view model architechture to optimise the amount of classes...
            var outputResults = new DiveProfileResultsListViewModel()
            {
                DiveProfileStepHeader = diveHeader,
            };

            outputResults = DiveParameterOutput( diveStep, gasMixture, outputResults );

            IDiveStage[] diveStages = new IDiveStage[]
            {
                new DiveStageAmbientPressure(stepProfileOutputModel, _diveProfile ,gasMixture.SelectedGasMixture.Oxygen, gasMixture.SelectedGasMixture.Helium, diveStep.Depth),
                new DiveStageTissuePressure(stepProfileOutputModel, TheDiveModel, _diveProfile, diveStep.Time),
                new DiveStageABValues(stepProfileOutputModel, TheDiveModel, _diveProfile),
                new DiveStageToleratedAmbientPressure(stepProfileOutputModel, TheDiveModel,_diveProfile),
                new DiveStageMaximumSurfacePressure(stepProfileOutputModel, TheDiveModel, _diveProfile),
                new DiveStageCompartmentLoad(stepProfileOutputModel, TheDiveModel, _diveProfile),
            };

            foreach(var stage in diveStages)
            {
                stepProfileOutputModel.Compartment = compartment;
                stage.RunStage();
            }
            
            outputResults.DiveProfileStepOutput.Add(stepProfileOutputModel);

            return outputResults;
        }

        private DiveProfileResultsListViewModel DiveParameterOutput(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture, DiveProfileResultsListViewModel outputResults)
        {
            outputResults.ParameterOutput.DiveModelUsed = _theDiveModel.DiveModelName;
            outputResults.ParameterOutput.StepDepthParameter = diveStep.Depth;
            outputResults.ParameterOutput.StepTimeParameter = diveStep.Time;
            outputResults.ParameterOutput.StepGasMixNameParameter = gasMixture.SelectedGasMixture.GasName;

            //TODO AH add when gas management is integrated
            //outputResults.ParameterOutput.GasUsedParameter
            //outputResults.ParameterOutput.GasRemainingParameter

            return outputResults;
        }

        /*private DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture, DiveProfileResultsListViewModel outputResults)
        {
            for (int compartment = 0; compartment < TheDiveModel.CompartmentIndexMax; compartment++)
            {
                IDiveProfileStepOutputModel output = new DiveProfileStepOutputModel();
                //SetAmbientPressures( gasMixture.SelectedGasMixture.Oxygen, gasMixture.SelectedGasMixture.Helium, diveStep.Depth );

                //output.Compartment = compartment + 1;
                //output.TissuePressureResult = Math.Round( CalculateTissuePressures( compartment, diveStep.Time ), 2 );
                //CalculateABValues( compartment );
                //output.ToleratedAmbientPressureResult = Math.Round( CalculateToleratedAmbientPressure( compartment ), 2 );
                //output.MaximumSurfacePressureResult = Math.Round( CalculateMaximumSurfacePressure( compartment ), 2 );
                //output.CompartmentLoadResult = Math.Round( CalculateCompartmentLoad( compartment ), 2 );

                outputResults.DiveProfileStepOutput.Add(output);
            }

            return outputResults;
        }*/

        private void InitaliseDiveProfile()
        {
            ResetDiveProfile();

            for (int i = 0; i < TheDiveModel.CompartmentIndexMax; i++)
            {
                _diveProfile.MaxSurfacePressures.Add(0);
                _diveProfile.ToleratedAmbientPressures.Add(0);
                _diveProfile.CompartmentLoad.Add(0);

                _diveProfile.TissuePressuresNitrogen.Add(0.79);
                _diveProfile.TissuePressuresHelium.Add(0);
                _diveProfile.TissuePressuresTotal.Add(0);
            }

        }

        private void ResetDiveProfile()
        {
            _diveProfile.MaxSurfacePressures.Clear();
            _diveProfile.ToleratedAmbientPressures.Clear();
            _diveProfile.CompartmentLoad.Clear();

            _diveProfile.TissuePressuresNitrogen.Clear();
            _diveProfile.TissuePressuresHelium.Clear();
            _diveProfile.TissuePressuresTotal.Clear();
        }
    }
}
