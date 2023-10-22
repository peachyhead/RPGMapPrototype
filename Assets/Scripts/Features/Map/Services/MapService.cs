// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Linq;
using Features.Map.Data;
using Features.Map.Data.Config;
using Features.Map.Factories;
using Features.Map.Models;
using Features.Map.Storages;
using Features.Mission.Models;
using Features.Mission.Services;
using UnityEngine;
using Zenject;

namespace Features.Map.Services
{
    public class MapService : IInitializable
    {
        private readonly MapNodeModelStorage _modelStorage;
        private readonly MapNodeRegistry _nodeRegistry;
        private readonly MissionService _missionService;
        private readonly MapNodeModelFactory _modelFactory;

        private MapService(MapNodeModelStorage modelStorage,
            MapNodeModelFactory modelFactory,
            MissionService missionService,
            MapNodeRegistry nodeRegistry)
        {
            _modelStorage = modelStorage;
            _modelFactory = modelFactory;
            _missionService = missionService;
            _nodeRegistry = nodeRegistry;
        }

        public void Initialize()
        {
            foreach (var nodeData in _nodeRegistry.Nodes)
            {
                if (!nodeData.Subnodes.Any())
                {
                    SetupNode(nodeData.Position, nodeData.ID);
                    continue;
                }
                
                foreach (var subnode in nodeData.Subnodes)
                {
                    SetupNode(nodeData.Position, nodeData.ID, subnode);
                }
            }
        }

        private void SetupNode(Vector2 position, uint node, uint subnode = default)
        {
            var nodeID = new MapNodeID(node, subnode);
            if (_missionService.TrySetupMission(nodeID,
                    out var missionModel))
                AddNode(nodeID, position, missionModel);
        }

        private void AddNode(MapNodeID nodeID, Vector2 position, MissionModel missionModel)
        {
            var model = _modelFactory.Create(nodeID, position, missionModel);
            _modelStorage.AddItem(model);
        }
    }
}