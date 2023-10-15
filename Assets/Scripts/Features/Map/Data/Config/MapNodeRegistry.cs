// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections.Generic;
using UnityEngine;

namespace Features.Map.Data.Config
{
    [CreateAssetMenu(fileName = "NodeRegistry", menuName = "Registry/Node registry")]
    public class MapNodeRegistry : ScriptableObject
    {
        [SerializeField] private List<MapNodeData> _nodes;

        public List<MapNodeData> Nodes => _nodes;
    }
}