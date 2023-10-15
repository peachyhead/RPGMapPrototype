// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Map.Services;
using Features.Map.Signals;
using Features.Mission.Services;
using Features.Mission.Signals;
using UniRx;
using Zenject;

namespace Features.Mission.Rules
{
    public class MissionRule : IInitializable, IDisposable
    {
        private readonly MissionService _missionService;
        private readonly SignalBus _signalBus;

        private readonly CompositeDisposable _compositeDisposable = new ();
        
        private MissionRule(MissionService missionService, 
            SignalBus signalBus)
        {
            _missionService = missionService;
            _signalBus = signalBus;
        }
        
        public void Initialize()
        {
            _signalBus
                .GetStream<MapSignals.SelectNode>()
                .Subscribe(signal => _missionService.ShowMission(signal.Node))
                .AddTo(_compositeDisposable);

            _signalBus
                .GetStream<MissionSignals.StartMission>()
                .Subscribe(signal => _missionService.StartMission(signal.Name))
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}