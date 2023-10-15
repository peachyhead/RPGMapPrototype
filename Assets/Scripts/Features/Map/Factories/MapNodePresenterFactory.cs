// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Utils;
using Features.Map.Views;
using Zenject;

namespace Features.Map.Factories
{
    public class MapNodePresenterFactory : IFactory<string, MapNodeView>
    {
        private readonly DiContainer _container;

        private MapNodePresenterFactory(DiContainer container)
        {
            _container = container;
        }
        
        public MapNodeView Create(string nodeID)
        {
            return _container.InstantiatePrefabResourceForComponent
                <MapNodeView>(MapConsts.MapNodePrefab, new object[] { nodeID });
        }
    }
}