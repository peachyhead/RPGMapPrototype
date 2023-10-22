// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using System.Linq;
using Features.Map.Factories;
using Features.Map.Models;
using Features.Map.Views;
using UnityEngine;
using Zenject;

namespace Features.Map.Storages
{
    public class MapNodeViewContainer : IInitializable, IDisposable
    {
        private readonly Dictionary<MapNodeView, MapNodeHolder> _items = new ();

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
            foreach (var model in _modelStorage) 
                SetupView(model);
        }

        private void SetupView(MapNodeModel mapNodeModel)
        {
            var commonNode = _items.Keys.FirstOrDefault(item => 
                item.ID.Node == mapNodeModel.NodeID.Node);

            var nodeHolder = commonNode != null 
                ? _items[commonNode] 
                : CreateHolder(mapNodeModel.Position);
            
            var view = CreateView(mapNodeModel, nodeHolder);
            _items.Add(view, nodeHolder);
        }

        private MapNodeView CreateView(MapNodeModel nodeModel, MapNodeHolder holder)
        {
            var presenter = _presenterFactory.Create(nodeModel);
            presenter.SetToggleGroup(_mapView.ToggleGroup);
            holder.AddNode(presenter);
            return presenter;
        }

        private MapNodeHolder CreateHolder(Vector2 position)
        {
            var holder = _holderFactory.Create(position);
            holder.transform.SetParent(_mapView.transform);
            return holder;
        }

        public void Dispose()
        {
            _creationStream?.Dispose();
        }
    }
}