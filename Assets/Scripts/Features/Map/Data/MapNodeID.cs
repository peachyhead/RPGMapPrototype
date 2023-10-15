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
        [Min(1)]
        [SerializeField] private int _node;
        [SerializeField] private int _subnode;

        public int Node => _node;
        public int Subnode => _subnode;
        
        public MapNodeID(int node, int subnode = default)
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
    }
}