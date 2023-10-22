// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Mission.Base;
using Features.Mission.Data;
using Features.Mission.Factories;
using UniRx;

namespace Features.Mission.Models
{
    public class MissionModel
    {
        public readonly string ID;
        public readonly MissionData Data;
        public MissionStateType CurrentState => _stateProperty.Value;

        private readonly MissionStateFactory _stateFactory;

        private readonly ReactiveProperty<bool> _shownProperty = new ();
        private readonly ReactiveProperty<MissionStateType> _stateProperty = new ();
        private readonly ReactiveProperty<bool> _availabilityProperty = new ();

        private BaseMissionState _currentState;
        
        public MissionModel(MissionStateFactory stateFactory, 
            MissionData data)
        {
            ID = Guid.NewGuid().ToString();
            _stateFactory = stateFactory;
            Data = data;
        }
        
        public void SetState(MissionStateType stateType)
        {
            _currentState?.Dispose();
            _currentState = _stateFactory.Create(stateType, this);
            _stateProperty.Value = stateType;
        }

        public void SetShown(bool status)
        {
            _shownProperty.Value = status;
        }

        public void SetAvailability(bool status)
        {
            _availabilityProperty.Value = status;
        }

        public IObservable<MissionStateType> OnStateChanged()
        {
            return _stateProperty.AsObservable();
        }

        public IObservable<bool> OnShowStatusChange()
        {
            return _shownProperty.AsObservable();
        }
        
        public IObservable<bool> OnAvailabilityStatusChange()
        {
            return _availabilityProperty.AsObservable();
        }
    }
}