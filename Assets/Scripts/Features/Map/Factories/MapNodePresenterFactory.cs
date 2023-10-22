// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Models;
using Features.Map.Views;
using Features.Mission.Models;
using Zenject;

namespace Features.Map.Factories
{
    public class MapNodePresenterFactory : IFactory<MapNodeModel, MapNodeView>
    {
        private readonly DiContainer _container;

        private MapNodePresenterFactory(DiContainer container)
        {
            _container = container;
        }
        
        public MapNodeView Create(MapNodeModel nodeModel)
        {
            return _container.InstantiatePrefabResourceForComponent
                <MapNodeView>(MapConsts.MapNodePrefab, new object[] { nodeModel });
        }
    }
}