// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Data;
using Features.Mission.Data.Config;
using Features.Mission.Views;
using Zenject;

namespace Features.Mission.Factories
{
    public class MissionResultWindowFactory : IFactory<MissionData, MissionResultWindow>
    {
        private readonly DiContainer _container;

        private readonly MissionRegistry _missionRegistry;

        private MissionResultWindowFactory(DiContainer container)
        {
            _container = container;
        }
        
        public MissionResultWindow Create(MissionData data)
        {
            return _container.InstantiatePrefabResourceForComponent<MissionResultWindow>(MissionConsts
                .MissionResultWindow, new object[] { data });
        }
    }
}