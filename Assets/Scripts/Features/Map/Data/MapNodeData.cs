// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.Map.Data
{
    [Serializable]
    public class MapNodeData
    {
        [Min(1)]
        [SerializeField] private int _id;
        [SerializeField] private Vector2 _position;
        [Min(1)]
        [SerializeField] private List<int> _subnodes;

        public int ID => _id;
        public Vector2 Position => _position;
        public List<int> Subnodes => _subnodes;
    }
}