// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using Features.Mission.Data;
using Features.Mission.Views;
using Zenject;

namespace Features.Mission.Factories
{
    public class MissionWindowFactory : IFactory<List<MissionData>, BaseMissionWindow>
    {
        private readonly MissionInfoWindowFactory _missionInfoFactory;
        private readonly MissionInfoHolderWindowFactory _missionInfoHolderFactory;

        private MissionWindowFactory(MissionInfoWindowFactory missionInfoFactory,
            MissionInfoHolderWindowFactory missionInfoHolderFactory)
        {
            _missionInfoFactory = missionInfoFactory;
            _missionInfoHolderFactory = missionInfoHolderFactory;
        }
        
        public BaseMissionWindow Create(List<MissionData> data)
        {
            if (data.Count <= 1) return 
                _missionInfoFactory.Create(data.First());
            
            var windows = data
                .Select(item => _missionInfoFactory.Create(item))
                .ToList();

            return _missionInfoHolderFactory.Create(windows);
        }
    }
}