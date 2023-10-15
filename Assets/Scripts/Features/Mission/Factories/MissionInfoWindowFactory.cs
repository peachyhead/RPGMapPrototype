// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Data;
using Features.Mission.Views;
using Zenject;

namespace Features.Mission.Factories
{
    public class MissionInfoWindowFactory : IFactory<MissionData, BaseMissionWindow>
    {
        private readonly DiContainer _container;

        private MissionInfoWindowFactory(DiContainer container)
        {
            _container = container;
        }
        
        public BaseMissionWindow Create(MissionData data)
        {
            return _container.InstantiatePrefabResourceForComponent
                <MissionInfoWindow>(MissionConsts.MissionInfoWindow,
                    new object[] { data });
        }
    }
}