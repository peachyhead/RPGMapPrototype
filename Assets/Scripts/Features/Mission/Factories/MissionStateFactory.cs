// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Base;
using Features.Mission.Data;
using Features.Mission.Models;
using Zenject;

namespace Features.Mission.Factories
{
    public class MissionStateFactory : IFactory<MissionStateType, MissionModel, BaseMissionState>
    {
        private readonly DiContainer _container;

        private MissionStateFactory(DiContainer container)
        {
            _container = container;
        }
        
        public BaseMissionState Create(MissionStateType type, MissionModel model)
        {
            var state = _container.ResolveId<BaseMissionState>(type);
            state.SetContext(model);
            state.Initialize();
            return state;
        }
    }
}