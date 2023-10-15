// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Mission.Data.Config;
using Features.Mission.Factories;
using Features.Mission.Views;
using UnityEngine;
using Zenject;

namespace Features.Mission.Services
{
    public class MissionService
    {
        private BaseMissionWindow _currentWindow;
        
        private readonly MissionRegistry _missionRegistry;
        private readonly MissionWindowFactory _infoWindowFactory;
        private readonly MissionResultWindowFactory _resultWindowFactory;
        private readonly RectTransform _canvasTransform;

        public MissionService(MissionRegistry missionRegistry, 
            MissionWindowFactory infoWindowFactory,
            MissionResultWindowFactory resultWindowFactory,
            [Inject(Id = "CanvasTransform")] RectTransform canvasTransform)
        {
            _missionRegistry = missionRegistry;
            _infoWindowFactory = infoWindowFactory;
            _resultWindowFactory = resultWindowFactory;
            _canvasTransform = canvasTransform;
        }

        public void ShowMission(int node)
        {
            var data = _missionRegistry.GetDataByNode(node);
            var mission = _infoWindowFactory.Create(data);
            SetupMission(mission);
        }

        public void StartMission(string name)
        {
            var data = _missionRegistry.FindByMissionName(name);
            var mission = _resultWindowFactory.Create(data);
            SetupMission(mission);
        }

        private void SetupMission(BaseMissionWindow mission)
        {
            if (_currentWindow != null) 
                _currentWindow.Close();
            
            mission.Show();
            mission.transform.SetParent(_canvasTransform);
            _currentWindow = mission;
        }
    }
}