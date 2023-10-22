// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Map.Data;
using UnityEngine;

namespace Features.Mission.Data
{
    [Serializable]
    public class MissionUnlockData
    {
        [SerializeField] private MapNodeID _missionNode;

        public MapNodeID MissionNode => _missionNode;
    }
}