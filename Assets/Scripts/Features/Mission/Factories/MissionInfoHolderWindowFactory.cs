// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using Features.Mission.Data;
using Features.Mission.Views;
using Zenject;

namespace Features.Mission.Factories
{
    public class MissionInfoHolderWindowFactory : IFactory<List<BaseMissionWindow>, 
        BaseMissionWindow>
    {
        private readonly DiContainer _container;

        private MissionInfoHolderWindowFactory(DiContainer container)
        {
            _container = container;
        }
        
        public BaseMissionWindow Create(List<BaseMissionWindow> model)
        {
            return _container.InstantiatePrefabResourceForComponent
                <MissionHolderWindow>(MissionConsts.MissionInfoHolderWindow, new object[] { model });
        }
    }
}