// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Factories;
using Features.Map.Models;
using Features.Map.Services;
using Features.Map.Signals;
using Features.Map.Storages;
using Zenject;

namespace Features.Map.Installers
{
    public class MapInstaller : Installer
    {
        public override void InstallBindings()
        {
            InstallFactories();
            InstallServices();
            InstallStorages();
            InstallSignals();
        }

        private void InstallFactories()
        {
            Container.Bind<MapNodeHolderFactory>()
                .AsSingle()
                .WhenInjectedInto<MapNodeViewContainer>();
            Container.Bind<MapNodePresenterFactory>()
                .AsSingle()
                .WhenInjectedInto<MapNodeViewContainer>();;
            Container.BindFactory<MapNodeData, MapNodeModel, MapNodeModelFactory>()
                .WhenInjectedInto<MapNodeModelStorage>();
        }

        private void InstallServices()
        {
            Container.BindInterfacesAndSelfTo<MapService>().AsSingle();
        }

        private void InstallStorages()
        {
            Container.BindInterfacesAndSelfTo<MapNodeViewContainer>().AsSingle();
            Container.Bind<MapNodeModelStorage>().AsSingle();
        }

        private void InstallSignals()
        {
            Container.DeclareSignal<MapSignals.SelectNode>();
        }
    }
}