// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Hero.Data;
using Features.Hero.Services;
using Features.Hero.Signals;
using Features.Mission.Data;
using Features.Mission.Storages;
using UniRx;
using Zenject;

namespace Features.Hero.Rules
{
    public class HeroRule : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly HeroService _heroService;
        private readonly MissionModelStorage _missionModelStorage;

        private readonly CompositeDisposable _compositeDisposable = new ();

        private HeroRule(MissionModelStorage missionModelStorage,
            HeroService heroService, SignalBus signalBus)
        {
            _missionModelStorage = missionModelStorage;
            _heroService = heroService;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus
                .GetStream<HeroSignals.SelectHero>()
                .Subscribe(signal => _heroService.Select(signal.Type))
                .AddTo(_compositeDisposable);
            
            _missionModelStorage
                .OnMissionStateChange()
                .Where(mission => mission.CurrentState == MissionStateType.Completed)
                .Subscribe(mission =>
                {
                    _heroService.Attend(mission.ID);
                    _heroService.Reward(mission.Data.RewardData);
                    
                    foreach (var heroType in mission.Data.UnlocksHeroes) 
                        _heroService.Unlock(heroType);
                })
                .AddTo(_compositeDisposable);
            
            _heroService.Unlock(HeroType.Orel);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}