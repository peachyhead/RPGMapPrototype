// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Map.Data;
using Features.Mission.Models;
using UnityEngine;

namespace Features.Map.Models
{
    public class MapNodeModel
    {
        public Vector2 Position { get; }

        public MapNodeID NodeID { get; }

        private readonly MissionModel _missionModel;

        private MapNodeModel(MapNodeID nodeID, Vector2 position, MissionModel missionModel)
        {
            NodeID = nodeID;
            Position = position;
            _missionModel = missionModel;
        }

        public IObservable<bool> OnShowStatusChange() => _missionModel?
            .OnShowStatusChange();
        
        public IObservable<bool> OnLockStatusChange() => _missionModel?
            .OnAvailabilityStatusChange();
    }
}