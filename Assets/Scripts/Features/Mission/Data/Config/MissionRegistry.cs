// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Features.Mission.Data.Config
{
    [CreateAssetMenu(fileName = "MissionRegistry", menuName = "Registry/Mission registry")]
    public class MissionRegistry : ScriptableObject
    {
        [SerializeField] private List<MissionRegistryItem> _items;

        public List<MissionData> GetDataByNode(int node)
        {
            var suitableItems = _items
                .Where(item => item.ID.Node == node)
                .Select(item => item.Data)
                .ToList();

            return suitableItems;
        }

        public MissionData FindByMissionName(string missionName)
        {
            var item = _items
                .FirstOrDefault(item => item.Data.Name == missionName);

            return item?.Data;
        }
    }
}