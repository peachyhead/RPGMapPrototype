// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Installers;
using Features.Map.Views;
using Features.Mission.Installers;
using Features.Utils;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private RectTransform _canvas;
        [SerializeField] private MapView _mapView;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.Install<MapInstaller>();
            Container.Install<MissionInstaller>();

            Container.BindInterfacesAndSelfTo<MapView>()
                .FromInstance(_mapView);

            Container.Bind<RectTransform>()
                .WithId(GlobalConsts.CanvasTransform)
                .FromInstance(_canvas);
        }
    }
}