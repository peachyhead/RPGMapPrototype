// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using UnityEngine;

namespace Features.Map.Data
{
    [Serializable]
    public struct MapNodeID
    {
        public override int GetHashCode()
        {
            return HashCode.Combine(_node, _subnode);
        }

        [Min(1)]
        [SerializeField] private uint _node;
        [SerializeField] private uint _subnode;

        public uint Node => _node;
        public uint Subnode => _subnode;
        
        public MapNodeID(uint node, uint subnode = default)
        {
            _node = node;
            _subnode = subnode;
        }

        public override string ToString()
        {
            return _subnode == default 
                ? $"{_node}" 
                : $"{_node}{MapConsts.NodeSplitSymbol}{_subnode}";
        }
        
        public static bool operator ==(MapNodeID nodeA, MapNodeID nodeB)
        {
            return nodeA.ToString() == nodeB.ToString();
        }

        public static bool operator !=(MapNodeID nodeA, MapNodeID nodeB)
        {
            return !(nodeA == nodeB);
        }
        
        public bool Equals(MapNodeID other)
        {
            return _node == other._node && _subnode == other._subnode;
        }

        public override bool Equals(object obj)
        {
            return obj is MapNodeID other && Equals(other);
        }
    }
}