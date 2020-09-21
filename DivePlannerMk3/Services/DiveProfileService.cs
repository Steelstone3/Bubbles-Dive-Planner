using DivePlannerMk3.Contracts;
using DivePlannerMk3.Models;
using DivePlannerMk3.ViewModels.DivePlan;
using DivePlannerMk3.ViewModels.DiveResult;
using DivePlannerMK3.Contracts;

namespace DivePlannerMk3.Controllers
{
    public class DiveProfileService : IDiveProfileService
    {
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
            //TODO AH reconsider the whole output and input view model architechture to optimise the amount of classes...
            /*var outputResults = new DiveProfileResultsListViewModel()
            {
                DiveProfileStepHeader = _theDiveModel.DiveModelName + "\r\nDepth: " + diveStep.Depth.ToString() + " Time: " + diveStep.Time.ToString(),
            };

            var updatedOutputResults = DiveParameterOutput( diveStep, gasMixture, outputResults );

            return RunDiveStep( diveStep, gasMixture, updatedOutputResults );*/

            IDiveStage[] diveStages = new IDiveStage[]
            {
                new DiveStageAmbientPressure(_diveProfile ,gasMixture.SelectedGasMixture.Oxygen, gasMixture.SelectedGasMixture.Helium, diveStep.Depth),
                new DiveStageTissuePressure(TheDiveModel, _diveProfile, diveStep.Time),
                new DiveStageABValues(TheDiveModel, _diveProfile),
                new DiveStageToleratedAmbientPressure(TheDiveModel,_diveProfile),
                new DiveStageMaximumSurfacePressure(TheDiveModel, _diveProfile),
                new DiveStageCompartmentLoad(TheDiveModel, _diveProfile),
            };

            foreach(var stage in diveStages)
            {
                stage.RunStage();
            }

            return null;
        }

        //TODO AH finish this later
        /*public IEnumerable<DiveProfileResultsListViewModel> RunDecompressionDiveSteps(InfoDecompressionProfileViewModel decompressionDiveSteps, PlanGasMixtureViewModel gasMixture)
        {
            //TODO AH Insert Dive Step Calculation and environment dependencies

            //TODO AH consider how the dive model and dive profile are to be updated external to this class' state post calculations


            foreach( var diveStep in decompressionDiveSteps.DecoDiveSteps )
            {
            var outputResults = new DiveProfileResultsListViewModel()
            {
                DiveProfileStepHeader = _theDiveModel.DiveModelName + "\r\nDepth: " + diveStep.Depth.ToString() + " Time: " + diveStep.Time.ToString(),
            };
                var updatedOutputResults = DiveParameterOutput( diveStep, gasMixture, outputResults );

                yield return RunDiveStep( diveStep, gasMixture, updatedOutputResults );
            }
        }*/


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

        private DiveProfileResultsListViewModel RunDiveStep(PlanDiveStepViewModel diveStep, PlanGasMixtureViewModel gasMixture, DiveProfileResultsListViewModel outputResults)
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
        }

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
