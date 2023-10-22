// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Hero.Installers;
using Features.Hero.Views;
using Features.Map.Installers;
using Features.Map.Views;
using Features.Mission.Installers;
using Features.UI.Installers;
using Features.UI.Services;
using Features.UI.Views;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private CanvasController _canvasController;
        [SerializeField] private HeroCardHolder _cardHolder;
        [SerializeField] private MapView _mapView;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Install<MapInstaller>();
            Container.Install<MissionInstaller>();
            Container.Install<HeroInstaller>();
            Container.Install<UIInstaller>();

            Container.BindInterfacesAndSelfTo<MapView>()
                .FromInstance(_mapView);

            Container.BindInterfacesAndSelfTo<HeroCardHolder>()
                .FromInstance(_cardHolder);

            Container.Bind<CanvasController>()
                .FromInstance(_canvasController)
                .WhenInjectedInto<CanvasService>();
        }
    }
}