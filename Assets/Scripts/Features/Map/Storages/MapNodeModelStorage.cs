// PEACHYBAND CONFIDENTIAL
// __________________
// All Rights Reserved
// [2020]-[2023].

using System.Collections;
using System.Collections.Generic;
using Features.Map.Models;

namespace Features.Map.Storages
{
    public class MapNodeModelStorage : IEnumerable<MapNodeModel>
    {
        private readonly List<MapNodeModel> _items = new ();

        public void AddItem(MapNodeModel model)
        {
            _items.Add(model);
        }

        public IEnumerator<MapNodeModel> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}