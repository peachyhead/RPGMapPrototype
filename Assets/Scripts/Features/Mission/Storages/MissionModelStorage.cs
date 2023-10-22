// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Features.Map.Data;
using Features.Mission.Models;
using UniRx;

namespace Features.Mission.Storages
{
    public class MissionModelStorage : IEnumerable<MissionModel>
    {
        private readonly Dictionary<MapNodeID, MissionModel> _items = new ();

        private IObservable<MissionModel> _missionStateChangeObservable = Observable.Empty<MissionModel>();

        public bool TryGetMissionsByNode(uint node, out List<MissionModel> models)
        {
            models = _items.Where(item => item.Key.Node == node)
                .Select(item => item.Value)
                .ToList();
            
            return models.Any();
        }

        public bool TryGetMissionByID(string id, out MissionModel model)
        {
            model = _items.Values.FirstOrDefault(item => item.ID == id);
            return model != default;
        }

        public bool TryGetMissionByNodeID(MapNodeID nodeID, out MissionModel model)
        {
            var key = _items.Keys.FirstOrDefault(item => item == nodeID);
            model = _items[key];
            return model != default;
        }
        
        public void AddItem(MapNodeID nodeID, MissionModel model)
        {
            _items.Add(nodeID, model);
            _missionStateChangeObservable = _missionStateChangeObservable
                .Merge(model.OnStateChanged().Select(_ => model));
        }
        
        public IObservable<MissionModel> OnMissionStateChange()
        {
            return _missionStateChangeObservable.AsObservable();
        }

        public IEnumerator<MissionModel> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}