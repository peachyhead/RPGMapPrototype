// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Linq;
using Features.Map.Data;

namespace Features.Map.Utils
{
    public static class MapUtils
    {
        public static MapNodeID GetNodeFromID(this string nodeID)
        {
            var nodeData = nodeID
                .Split(MapConsts.NodeSplitSymbol)
                .Select(int.Parse)
                .ToList();

            return new MapNodeID(nodeData[0], nodeData[1]);
        }
    }
}