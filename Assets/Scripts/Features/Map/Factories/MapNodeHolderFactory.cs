// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using Features.Map.Utils;
using Features.Map.Views;
using UnityEngine;
using Zenject;

namespace Features.Map.Factories
{
    public class MapNodeHolderFactory : IFactory<int, Vector2, MapNodeHolder>
    {
        private readonly DiContainer _container;

        private MapNodeHolderFactory(DiContainer container)
        {
            _container = container;
        }

        public MapNodeHolder Create(int node, Vector2 position)
        { 
            return _container.InstantiatePrefabResourceForComponent
                <MapNodeHolder>(MapConsts.NodeHolderPrefab, new object[] { node, position });
        }
    }
}