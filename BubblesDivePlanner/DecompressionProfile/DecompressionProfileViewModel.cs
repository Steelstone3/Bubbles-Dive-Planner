using System;
using System.Collections.Generic;
using BubblesDivePlanner.Cylinders.CylinderSetup;
using BubblesDivePlanner.DiveModels;
using BubblesDivePlanner.DiveStep;
using ReactiveUI;

namespace BubblesDivePlanner.DecompressionProfile
{
    public class DecompressionProfileViewModel : ReactiveObject, IDecompressionProfileModel
    {
        private IDiveModel _diveModel;
        private ICylinderSetupModel _selectedCylinder;

        public DecompressionProfileViewModel(IDiveModel diveModel, ICylinderSetupModel selectedCylinder)
        {
            var diveProfile = diveModel.DiveProfile.DeepClone();
            diveModel.DiveProfile = diveProfile;
            _diveModel = diveModel;
            _selectedCylinder = new CylinderPrototype().DeepClone(selectedCylinder);
        }

        public Queue<IDiveStepModel> DecompressionDiveSteps => new DecompressionProfileController().CollateDecompressionDiveSteps(_diveModel, _selectedCylinder);
        
        // private bool _isVisible = false;
        // public bool IsVisible
        // {
        //     get => _isVisible;
        //     set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        // }
    }
}