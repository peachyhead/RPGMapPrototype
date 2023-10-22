// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using Features.Map.Data;

namespace Features.Map.Signals
{
    public sealed class MapSignals
    {
        public class SelectNode
        {
            public uint Node { get; }

            public SelectNode(uint node)
            {
                Node = node;
            }
        }
    }
}