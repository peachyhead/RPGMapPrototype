// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Linq;
using Features.Map.Data;
using Features.Mission.Data;
using Features.Mission.Data.Config;
using Features.Mission.Factories;
using Features.Mission.Models;
using Features.Mission.Storages;
using Features.Mission.Views;
using Features.UI.Data;
using Features.UI.Services;
using UniRx;

namespace Features.Mission.Services
{
    public class MissionService : IDisposable
    {
        private BaseMissionWindow _currentWindow;
        
        private readonly MissionRegistry _missionRegistry;
        private readonly MissionModelFactory _missionModelFactory;
        private readonly MissionModelStorage _missionModelStorage;
        private readonly MissionWindowFactory _infoWindowFactory;
        private readonly MissionResultWindowFactory _resultWindowFactory;
        private readonly CanvasService _canvasService;

        private readonly CompositeDisposable _compositeDisposable = new ();
        
        public MissionService(MissionModelStorage missionModelStorage,
            MissionWindowFactory infoWindowFactory,
            MissionResultWindowFactory resultWindowFactory,
            MissionRegistry missionRegistry, 
            MissionModelFactory missionModelFactory, 
            CanvasService canvasService)
        {
            _missionModelStorage = missionModelStorage;
            _infoWindowFactory = infoWindowFactory;
            _missionRegistry = missionRegistry;
            _missionModelFactory = missionModelFactory;
            _resultWindowFactory = resultWindowFactory;
            _canvasService = canvasService;
        }

        public void ShowMission(uint node)
        {
            if (!_missionModelStorage.TryGetMissionsByNode(node, out var models)) 
                return;
            var mission = _infoWindowFactory.Create(models);
            SetupMissionView(mission);
        }

        public void StartMission(string id)
        {
            if (!_missionModelStorage.TryGetMissionByID(id, out var model)) 
                return;
            var mission = _resultWindowFactory.Create(model);
            SetupMissionView(mission);
            model.SetState(MissionStateType.TemporaryLocked);
        }

        public void CompleteMission(string id)
        {
            if (!_missionModelStorage.TryGetMissionByID(id, out var model)) 
                return;
            
            model.SetState(MissionStateType.Completed);
            if (_currentWindow != null) 
                _currentWindow.Close();

            if (!_missionRegistry.TryGetNodeByName(model.Data.Name, out var node)) 
                return;
            if (!_missionModelStorage.TryGetMissionsByNode(node.Node, out var missions)) 
                return;
            
            foreach (var abandonedMission in missions) 
                abandonedMission.SetState(MissionStateType.Abandoned);
        }
        
        public bool TrySetupMission(MapNodeID nodeID, out MissionModel model)
        {
            model = default;
            if (!_missionRegistry.TryGetDataByNode(nodeID, out var missionData)) 
                return false;
            var mission = _missionModelFactory.Create(missionData);
            _missionModelStorage.AddItem(nodeID, mission);
            mission.SetState(MissionStateType.Locked);
            TryUnlockMission(mission);

            model = mission;
            return true;
        }

        public void TryUnlockMission(MissionModel missionModel)
        {
            if (missionModel.CurrentState 
                is MissionStateType.Completed 
                or MissionStateType.Abandoned)
                return;
            
            if (!missionModel.Data.RequiredMissions.Any())
            {
                missionModel.SetState(MissionStateType.Active);
                return;
            }
            
            foreach (var unlockData in missionModel.Data.RequiredMissions)
            {
                var succeed = _missionModelStorage
                    .TryGetMissionByNodeID(unlockData.MissionNode, out var mission);
                if (!succeed) continue;
                
                if (mission.CurrentState == MissionStateType.Completed)
                    missionModel.SetState(MissionStateType.Active);
            }
        }

        private void SetupMissionView(BaseMissionWindow mission)
        {
            if (_currentWindow != null) 
                _currentWindow.Close();

            var holder = _canvasService.GetHolderByType(HolderType.Window).transform;
            mission.transform.SetParent(holder);
            mission.Show();
            _currentWindow = mission;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}