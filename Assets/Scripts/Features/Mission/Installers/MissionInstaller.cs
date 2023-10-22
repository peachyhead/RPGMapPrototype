// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Base;
using Features.Mission.Data;
using Features.Mission.Factories;
using Features.Mission.Models;
using Features.Mission.Models.States;
using Features.Mission.Rules;
using Features.Mission.Services;
using Features.Mission.Signals;
using Features.Mission.Storages;
using Zenject;

namespace Features.Mission.Installers
{
    public class MissionInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
            InstallServices();
            InstallStorages();
            InstallSignals();
            InstallStates();
            InstallRules();
        }
        
        private void InstallFactories()
        {
            Container.Bind<MissionWindowFactory>().AsSingle();
            Container.Bind<MissionStateFactory>().AsSingle();
            Container.BindFactory<MissionData, MissionModel, MissionModelFactory>().AsSingle();
            
            Container.Bind<MissionInfoWindowFactory>()
                .AsSingle()
                .WhenInjectedInto<MissionWindowFactory>();
            
            Container.Bind<MissionInfoHolderWindowFactory>()
                .AsSingle()
                .WhenInjectedInto<MissionWindowFactory>();
            
            Container.Bind<MissionResultWindowFactory>().AsSingle();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<MissionService>().AsSingle();
        }

        private void InstallStates()
        {
            BindStateWithID<ActiveMissionState>(MissionStateType.Active);
            BindStateWithID<LockedMissionState>(MissionStateType.Locked);
            BindStateWithID<TemporaryLockedMissionState>(MissionStateType.TemporaryLocked);
            BindStateWithID<CompletedMissionState>(MissionStateType.Completed);
        }

        private void InstallRules()
        {
            Container.BindInterfacesTo<MissionRule>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<MissionSignals.StartMission>();
            Container.DeclareSignal<MissionSignals.CompleteMission>();
        }

        private void InstallStorages()
        {
            Container.Bind<MissionModelStorage>().AsSingle();
        }

        private void BindStateWithID<TState>(MissionStateType type)
                where TState : BaseMissionState
        {
            Container.Bind<BaseMissionState>()
                .WithId(type)
                .To<TState>()
                .AsTransient();
        }
    }
}