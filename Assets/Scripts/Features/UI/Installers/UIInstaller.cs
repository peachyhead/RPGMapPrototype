// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.UI.Services;
using Zenject;

namespace Features.UI.Installers
{
    public class UIInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<CanvasService>().AsSingle();
        }
    }
}