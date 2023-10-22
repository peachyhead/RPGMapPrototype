// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Map.Signals;
using Features.Mission.Data;
using Features.Mission.Services;
using Features.Mission.Signals;
using Features.Mission.Storages;
using UniRx;
using Zenject;

namespace Features.Mission.Rules
{
    public class MissionRule : IInitializable, IDisposable
    {
        private readonly MissionService _missionService;
        private readonly MissionModelStorage _missionModelStorage;
        private readonly SignalBus _signalBus;

        private readonly CompositeDisposable _compositeDisposable = new ();
        
        private MissionRule(MissionService missionService,
            MissionModelStorage missionModelStorage,
            SignalBus signalBus)
        {
            _missionService = missionService;
            _missionModelStorage = missionModelStorage;
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

            _signalBus
                .GetStream<MissionSignals.CompleteMission>()
                .Subscribe(signal => _missionService.CompleteMission(signal.Name))
                .AddTo(_compositeDisposable);

            _missionModelStorage
                .OnMissionStateChange()
                .Where(mission => mission.CurrentState == MissionStateType.Completed)
                .Subscribe(_ =>
                {
                    foreach (var missionModel in _missionModelStorage)
                        _missionService.TryUnlockMission(missionModel);
                })
                .AddTo(_compositeDisposable);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}