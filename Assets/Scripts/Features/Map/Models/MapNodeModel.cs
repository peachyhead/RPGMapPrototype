// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;
using UnityEngine;

namespace Features.Map.Models
{
    public class MapNodeModel
    {
        public Vector2 Position { get; }

        public MapNodeData Data { get; }

        private MapNodeModel(MapNodeData data)
        {
            Data = data;
            Position = data.Position;
        }
    }
}