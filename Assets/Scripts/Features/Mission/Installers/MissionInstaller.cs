// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Factories;
using Features.Mission.Rules;
using Features.Mission.Services;
using Features.Mission.Signals;
using Zenject;

namespace Features.Mission.Installers
{
    public class MissionInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
            InstallServices();
            InstallSignals();
            InstallRules();
        }
        
        private void InstallFactories()
        {
            Container.Bind<MissionWindowFactory>().AsSingle();
            
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

        private void InstallRules()
        {
            Container.BindInterfacesTo<MissionRule>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<MissionSignals.StartMission>();
            Container.DeclareSignal<MissionSignals.CompleteMission>();
        }
    }
}