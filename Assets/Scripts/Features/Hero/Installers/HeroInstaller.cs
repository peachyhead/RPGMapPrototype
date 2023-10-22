// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Hero.Data;
using Features.Hero.Factories;
using Features.Hero.Models;
using Features.Hero.Rules;
using Features.Hero.Services;
using Features.Hero.Signals;
using Features.Hero.Storages;
using Zenject;

namespace Features.Hero.Installers
{
    public class HeroInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallRules();
            InstallSignals();
            InstallServices();
            InstallStorages();
            InstallFactories();
        }

        private void InstallRules()
        {
            Container.BindInterfacesTo<HeroRule>().AsSingle();
        }
        
        private void InstallServices()
        {
            Container.Bind<HeroService>().AsSingle();
        }
        
        private void InstallSignals()
        {
            Container.DeclareSignal<HeroSignals.SelectHero>();
        }
        
        private void InstallStorages()
        {
            Container.Bind<HeroModelStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroCardViewContainer>().AsSingle();
        }

        private void InstallFactories()
        {
            Container.BindFactory<HeroData, HeroModel, HeroModelFactory>().AsSingle();
            Container.Bind<HeroCardViewFactory>().AsSingle();
        }
    }
}