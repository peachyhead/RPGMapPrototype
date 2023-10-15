// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using Features.Map.Data;
using UnityEngine;

namespace Features.Mission.Data.Config
{
    [Serializable]
    public class MissionRegistryItem
    {
        [SerializeField] private MapNodeID _id;
        [SerializeField] private MissionData _data;

        public MapNodeID ID => _id;
        public MissionData Data => _data;
    }
}