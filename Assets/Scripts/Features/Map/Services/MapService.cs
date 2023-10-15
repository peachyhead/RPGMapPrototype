// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Data.Config;
using Features.Map.Storages;
using Zenject;

namespace Features.Map.Services
{
    public class MapService : IInitializable
    { 
        private readonly MapNodeModelStorage _modelStorage;
        private readonly MapNodeRegistry _nodeRegistry;

        private MapService(MapNodeModelStorage modelStorage,
            MapNodeRegistry nodeRegistry)
        {
            _modelStorage = modelStorage;
            _nodeRegistry = nodeRegistry;
        }

        public void Initialize()
        {
            foreach (var nodeData in _nodeRegistry.Nodes)
            {
                AddNode(nodeData);
            }
        }

        private void AddNode(MapNodeData data)
        {
            _modelStorage.GetNewModel(data);
        }
    }
}