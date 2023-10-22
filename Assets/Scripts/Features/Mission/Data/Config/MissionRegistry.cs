// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using Features.Map.Data;
using UnityEngine;

namespace Features.Mission.Data.Config
{
    [CreateAssetMenu(fileName = "MissionRegistry", menuName = "Registry/Mission registry")]
    public class MissionRegistry : ScriptableObject
    {
        [SerializeField] private List<MissionRegistryItem> _items;

        public bool TryGetDataByNode(MapNodeID nodeID, out MissionData data)
        {
            data = _items
                .FirstOrDefault(item => item.ID.ToString() == nodeID.ToString())
                ?.Data;
            return data != default;
        }
        
        public bool TryGetNodeByName(string name, out MapNodeID nodeID)
        {
            nodeID = default;
            var data = _items
                .FirstOrDefault(item => item.Data.Name == name);

            if (data == default)
                return false;

            nodeID = data.ID;
            return true;
        }
    }
}