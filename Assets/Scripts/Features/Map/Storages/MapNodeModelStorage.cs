// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Map.Data;
using Features.Map.Factories;
using Features.Map.Models;
using UniRx;

namespace Features.Map.Storages
{
    public class MapNodeModelStorage
    {
        private readonly ReactiveCollection<MapNodeModel> _items = new ();

        private readonly MapNodeModelFactory _modelFactory;

        private MapNodeModelStorage(MapNodeModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public MapNodeModel GetNewModel(MapNodeData data)
        {
            var model = _modelFactory.Create(data);
            _items.Add(model);
            return model;
        }

        public IObservable<MapNodeModel> OnItemAdded()
        {
            return _items.ObserveAdd()
                .Select(action => action.Value)
                .AsObservable();
        }

        public List<MapNodeModel> GetItems => _items.ToList();
    }
}