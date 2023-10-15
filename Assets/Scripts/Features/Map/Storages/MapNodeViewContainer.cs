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
using Features.Map.Views;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Map.Storages
{
    public class MapNodeViewContainer : IInitializable, IDisposable
    {
        private readonly Dictionary<MapNodeHolder, List<MapNodeView>> _items = new ();

        private readonly MapNodeModelStorage _modelStorage;
        private readonly MapNodePresenterFactory _presenterFactory;
        private readonly MapNodeHolderFactory _holderFactory;

        private readonly MapView _mapView;
        
        private IDisposable _creationStream;
        
        private MapNodeViewContainer(MapNodeModelStorage modelStorage,
            MapNodeHolderFactory holderFactory,
            MapNodePresenterFactory presenterFactory,
            MapView mapView)
        {
            _modelStorage = modelStorage;
            _holderFactory = holderFactory;
            _presenterFactory = presenterFactory;
            _mapView = mapView;
        }
        
        public void Initialize()
        {
            foreach (var model in _modelStorage.GetItems) 
                SetupView(model);

            _creationStream = _modelStorage
                .OnItemAdded()
                .Subscribe(SetupView);
        }

        private void SetupView(MapNodeModel model)
        {
            var holder = _holderFactory.Create(model.Data.ID, model.Position);
            holder.transform.SetParent(_mapView.transform);
            holder.SetToggleGroup(_mapView.GetToggleGroup);
            
            var presenterCollection = model.Data.Subnodes.Any()
                ? model.Data.Subnodes
                    .Select(subnode => new MapNodeID(model.Data.ID, subnode))
                    .Select(nodeID => CreateView(nodeID, holder.LayoutRoot))
                    .ToList()
                : new List<MapNodeView>
                {
                    CreateView(new MapNodeID(model.Data.ID), holder.LayoutRoot)
                };

            _items.Add(holder, presenterCollection);
        }

        private MapNodeView CreateView(MapNodeID nodeID, Transform parent)
        {
            var presenter = _presenterFactory.Create(nodeID.ToString());
            presenter.transform.SetParent(parent);
            return presenter;
        }

        public void Dispose()
        {
            _creationStream?.Dispose();
        }
    }
}